using UnityEngine        ;
using System             ;
using System.Collections ;
using MTON               ;
using MTON.Interface     ;

namespace MTON.Controller {

  [RequireComponent (typeof (CharacterController))]
  public class CharUpdateController : MonoBehaviour, IRbody, IForce {

    #region Public Events
    // Move
    public Vector3 doMove(_enum.Dirn IN_DPAD){
      this.dState = IN_DPAD     ; //for visual debugging
      this.vMove = Vector3.zero ;
      if(IN_DPAD == _enum.Dirn.RT){
        this.vMove = Vector3.right * this.moveForce  ; //horizontal transform (move) 
      }
      else if(IN_DPAD == _enum.Dirn.LT){
        this.vMove = -Vector3.right * this.moveForce ; //horizontal transform (move) 
      }
      return this.vMove;
    }

    // Jump
    public void doJump(){
      this.bJump = true;
    }
    #endregion

    private LayerMask _layerGround ;
    private CharacterController cc ;

    public bool  bJump     = false ;
    public float moveForce = 3.0f  ;
    public float jumpForce = 5.75f ;
    public float flapForce = 4.25f ;
    public float dashForce = 3.0f  ;

    public Vector3 ccVelocity = Vector3.zero;

    [SerializeField]
    private _enum.Dirn dState = _enum.Dirn.Neutral;
    [SerializeField]
    private _enum.VState vstate = _enum.VState.Ground;
    public  _enum.VState vState{
      get{
        return this.vstate;
      }
      set{
//        if(value != this.vstate){
          Pools.pool.CreateEntity().AddRbodyEvent(this.cc, value);
          this.vstate = value;
//        }
      }
    }
    private _enum.Rbody  rState;

    [Flags] // Powers of two
    public enum rayState {
      // Decimal                    // Binary
      NULL     = 0 ,                // 000000 // Why must I start at 0? This being Neutral and FALL >> 1 didn't work
      MIDL     = 1 ,                // 000001
      LEFT     = 2 ,                // 000010
      RGHT     = 4 ,                // 000100
      DUCK     = 16,                // 001000 // Must be power of two for bitwise operator

      MLFT     = MIDL|LEFT,         // 000011 // at ledge right
      MLRT     = MIDL|RGHT,         // 000101 // at ledge left
      MNUL     = LEFT|RGHT,         // 000110 // falling down tube?
      FULL     = MIDL|LEFT|RGHT,    // 000111 // fully planted
    }
    
    [SerializeField]
    private rayState _gcheck = rayState.NULL;
    private rayState _gCheck{
      get{ return _gcheck ; }
      set{
        if(value != _gcheck){
          _gcheck = value;
        }
      }
    }
    
    [SerializeField]
    private Vector3 pGrav = Vector3.zero ;
    [SerializeField]
    private float   accel = 0.0f         ;
    [SerializeField]
    private float   vy    = 0.0f         ;
    [SerializeField]
    private float   tVelc = 54.0f        ;

    #region IRbody implementation
    public Vector3    center { get; set; }
    public float      height { get; set; }
    public float      radius { get; set; }
    public Quaternion initRo { get; set; } // initRotation
    #endregion

    #region IForce implementation
    public Vector3 vMove { get; set;}
    [SerializeField]
    private Vector3 vgrav = Vector3.up;
    public Vector3 vGrav { 
      get{ return this.vgrav ; }
      set{ this.vgrav = value; }
    }
    public float   fMass { get; set;}
    #endregion

    public Vector3 vGravm = Vector3.zero; // can't use vGrav, because set/get don't allow bGrav.y += vMove.y

    public void Awake(){ //earlier than Start(); need to get xform and rbody
      this._layerGround = LayerMask.GetMask (MTON._CONSTANTComponent._FLOOR);
      this.cc = this.GetComponent<CharacterController>();

      this.center  = this.cc.center;
      this.height  = (this.cc.height * this.transform.localScale.y * 0.5f) + this.cc.skinWidth ; 
      this.radius  = this.cc.radius * this.transform.localScale.x ;
      this.initRo  = this.transform.rotation;  

      this.pGrav = _GravityComponent.dir * _GravityComponent.magnitude ;
      this.accel = _GravityComponent.accleration                       ;
      this.tVelc = _GravityComponent.terminalVelocity * 0.25f          ; // 54.0 * 0.25f = 13.35f
      this.fMass = 1.0f;
    }

    private void FixedUpdate(){
      this.ccVelocity = this.cc.velocity;
      // PHYSICS UPDATE : gravity
      // Determine state
      if(!this.OnGround()){ // apply gravity when not on ground
        this.vGravm   += (pGrav * this.fMass) * Time.deltaTime ; // Dang. Forgot to initialize fMass and spent 2 days not having fall work
        this.vGravm.y += -this.vy                              ; // multiplying vy as opposed to adding gave shitty jump behaviour
        //check for rising or falling
        if(this.cc.velocity.y < -0.1f){
          this.vState = _enum.VState.OnFall                                      ;
          this.vy     = Mathf.Clamp(this.vy+this.accel, -this.tVelc, this.tVelc * 2.0f);
        }
        else if(this.cc.velocity.y > 0.1f){
          this.vState = _enum.VState.OnRise;
          if(this.OnCeilng()){                //hit ceiling
            Debug.LogFormat("CEILING HIT : {0}", this.transform);
            this.vy     = -this.accel  ;
            this.vGravm = Vector3.zero ;
          }
        }
        else{
          this.vState = _enum.VState.OnApex;
        }
      }
      else{            // onGround zero out gravity
        this.vState = _enum.VState.Ground;
        this.vy     = 0.0f         ;
        this.vGravm = Vector3.zero ;
      }
      // EVENT CHANGES : jump, move ...etc
      // HACK : doJump() must follow Fall() => order matters! Else vertical twitch and not jump curve
      // bJump updated here as a side effect of doJump() injection
      if(this.bJump){ 
        if(this.vState == _enum.VState.Ground){
          this.vGravm.y = this.jumpForce ;
        }
        else{
          this.vGravm.y = this.flapForce ;
        }
        this.vy        = 0.0f           ;
        this.bJump     = false          ;
      }
      // vMove updated here as a side effect of doMove() injection
      this.vGravm.x  = this.vMove.x                  ; //combine with move from Move()=>oMoveH() for final position
      this.vGravm.y += this.vMove.y                  ;
      this.vGravm.y  = Mathf.Clamp(vGravm.y, -this.tVelc, this.tVelc * 2.0f); // prevents crazy fall
      this.vGravm.z  = 0.0f                          ; //forces character to stay in 2D plane
      this.vGrav     = this.vGravm                   ;
      this.cc.Move(this.vGrav *  Time.deltaTime)     ; // * -this.vy) ; //do gravity

      // Smooth edge drop via collider radius tweaking
      if(this.vState == _enum.VState.Ground && this._gCheck != rayState.FULL){  //Not all rays hitting ground; reduce radius of collider
        this.cc.radius = this.radius * 0.05f ; //reduce radius collider
      }
      else{
        this.cc.radius = this.radius         ; //else leave at default
      }
    }


#region OnGround(){}

    //Utilities -- Not extending xForm so reimplementing ground logic
    public bool OnGround(){
      Vector3 vPos = this.transform.position + this.center                                         ;
      return this.OnGround(vPos, -Vector3.up, new Vector3(this.radius * 0.85f, this.height, 0.0f)) ;
    }

    public bool OnGround(Vector3 vPos, Vector3 vDir, Vector3 vCol){                 // vCol: x = cRadius, y = cHeight   
      float bCentCheck = this.dirRayCheck(vPos                            , vDir, vCol.y) ; // check center
      float bRghtCheck = this.dirRayCheck(vPos + ( Vector3.right * vCol.x), vDir, vCol.y) ; // check right edge
      float bLeftCheck = this.dirRayCheck(vPos + (-Vector3.right * vCol.x), vDir, vCol.y) ; // check left edge
      this._gCheck = rayState.NULL;
      if(bCentCheck > 0.0f){
        this._gCheck|=rayState.MIDL;
      }
      if(bLeftCheck > 0.0f){
        this._gCheck|=rayState.LEFT;
      }
      if(bRghtCheck > 0.0f){
        this._gCheck|=rayState.RGHT;
      }

      if (this._gCheck > rayState.NULL){    //either edge connects, then character is onGround
        return true ;
      }
      else{
        return false;
      }
    }

#endregion

#region OnCeilng(){}

    public bool OnCeilng(){
      Vector3 vPos = this.transform.position + this.center                                 ;
      return this.OnCeilng(vPos, Vector3.up, new Vector3(0.0f, this.height * 1.05f, 0.0f)) ;
    }

    public bool OnCeilng(Vector3 vPos, Vector3 vDir, Vector3 vCol){
      float ceilingCheck = dirRayCheck(vPos, vDir, vCol.y) ; //check directly overhead
      if(ceilingCheck > 0.0f){
        return true  ;
      }
      else{
        return false ;
      }
    }

#endregion

    public float dirRayCheck(Vector3 vPos, Vector3 vDir, float IN_magnitude){
      return this.transform.dirRayCheck(vPos, vDir, IN_magnitude, this._layerGround);
    }

  }

}
