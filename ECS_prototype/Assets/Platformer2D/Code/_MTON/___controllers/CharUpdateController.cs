using UnityEngine;
using System;
using System.Collections;
using MTON;
using MTON.Interface;

namespace MTON.Controller {

  [RequireComponent (typeof (CharacterController))]
  public class CharUpdateController : MonoBehaviour, IRbody, IForce {

    private LayerMask _layerGround ;
    private CharacterController cc ;

    public bool  bJump     = false ;
    public float moveForce = 3.0f  ;
    public float jumpForce = 5.25f ;
    public float flapForce = 4.25f ;
    public float dashForce = 3.0f  ;

    public Vector3 ccVelocity = Vector3.zero;

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

    public Vector3 vGravm = Vector3.zero;

    public virtual void Awake(){ //earlier than Start(); need to get xform and rbody
      this._layerGround = LayerMask.GetMask (MTON._CONSTANTComponent._FLOOR);
      this.cc = this.GetComponent<CharacterController>();

      this.center  = this.cc.center;
      this.height  = (this.cc.height * this.transform.localScale.y * 0.5f) + this.cc.skinWidth ; 
      this.radius  = this.cc.radius * this.transform.localScale.x ;
      this.initRo  = this.transform.rotation;  

      this.pGrav = _GravityComponent.dir * _GravityComponent.magnitude ;
      this.accel = _GravityComponent.accleration                       ;
      this.tVelc = _GravityComponent.terminalVelocity            * 0.25f;
      this.fMass = 1.0f;
    }

    private void FixedUpdate(){
      this.ccVelocity = this.cc.velocity;
      // Determine state
      if(!OnGround()){ // apply gravity when not on ground
        this.vGravm   += (pGrav * this.fMass) * Time.deltaTime ; // Dang. Forgot to initialize fMass and spent 2 days not having fall work
        this.vGravm.y +=  -this.vy                             ; // multiplying vy as opposed to adding gave shitty jump behaviour
        //check for rising or falling
        if(this.cc.velocity.y < 0.1f){
          this.vState = _enum.VState.OnFall                                      ;
//          this.vy     = Mathf.Clamp(this.vy+this.accel, -this.tVelc, this.tVelc) ;
          this.vy     = Mathf.Clamp(this.vy+this.accel, -this.tVelc, this.tVelc * 2.0f);
        }
        else if(this.cc.velocity.y > 0.1f){
          this.vState = _enum.VState.OnRise;
//          if(OnCeiling){}
        }
        else{
          this.vState = _enum.VState.OnApex;
        }
      }
      else{            // onGround zero out gravity
        this.vState = _enum.VState.Ground;
        this.vy    = 0.0f         ;
        this.vGravm = Vector3.zero ;
      }
      //check for event changes
      if(this.bJump){ // Jumping?
        if(this.vState == _enum.VState.Ground){
          this.vGravm.y   = this.jumpForce ;
        }
        else{
          this.vGravm.y   = this.flapForce ;
        }
        this.vy        = 0.0f           ;
        this.bJump     = false          ;
      }

//      this.bGround = this.OnGround()            ; //calculate ground state
//      this.cHeight = this.ccHeight(this.contrl) ; //update ccontrol height ??? Why check on every update ???
//        if(bFall){
//          Fall()                                   ; //calculate vertical state
//        }
//        doHit(this.vDirOnHit);
//        //HACK : doJump() must follow Fall(), order matters! Else vertical twitch and not jump curve
//        doJump()                                   ; //calculate jump state : NOTE : Can't replace with longform bJump prop handler???
//
//        this.vGravm.x  = vMove.x                       ; //combine with move from Move()=>oMoveH() for final position
//        this.vGravm.y += vMove.y                       ;
//        this.vGravm.z  = 0.0f                          ; //forces character to stay in 2D plane
      this.cc.Move(this.vGravm *  Time.deltaTime); // * -this.vy) ; //do gravity
//      this.cc.Move(Vector3.down * Time.deltaTime) ; //do gravity
    }

    public void doJump(){
      this.bJump = true;
    }


//    public virtual Vector3 Fall(){ //vertical transform (this.vGravm)
//      var vgrav = Vector3.zero;
//      if(this._gCheck == rayState.NULL){ //in air
//        vgrav      += pGrav * Time.deltaTime * this.fMass ;
//        vgrav.y    += -this.vy                                 ; //adding velocity
////        bCeilng    = this.OnCeilng()                          ;
////        if((this.cc.velocity.y) < 0.1f){        //apply velocity after apex...changed from 0.0=>0.1 to blend apex transition
//          this.vy += this.accel;
////        }
////        else if(bCeilng){
////          this.vGravm.y = -accelY;
////        }
//      }
//      else{ //on FIRST foot down
////        Debug.Log("FIRST LANDING :!!!");
////        if(Mathf.Abs(this.cc.velocity.y) < 0.1f){ //and not on rise ; else get caught on ledges
////          //reset velocity when on ground
////          this.vGravm = Vector3.zero ;
////          this.vy     = 0.0f         ;                 
////        }
////        if(dash){
////          vMove *= this.dashForce ;
////        }
//      }
//      vgrav.y = Mathf.Clamp(this.vgrav.y, -this.tVelc, this.tVelc) ; //clamp to terminal velocity
//      Debug.LogFormat("FALLING:!!! {0}", vgrav);
//      return vgrav                                          ;
//    }

#region OnGround(){}

    //Utilities -- Not extending xForm so reimplementing ground logic
    public virtual bool OnGround(){                                          
      Vector3 vPos = this.transform.position + this.center                                         ;
      return this.OnGround(vPos, -Vector3.up, new Vector3(this.radius * 0.85f, this.height, 0.0f)) ;
    }

    public virtual bool OnGround(Vector3 vPos, Vector3 vDir, Vector3 vCol){                 // vCol: x = cRadius, y = cHeight   
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

      if (this._gCheck > rayState.NULL){  //either edge connects, then character is onGround
        if(this._gCheck != rayState.FULL){ //Not all rays hitting ground; reduce radius of collider
          this.cc.radius = vCol.x * 0.05f ; //reduce radius collider
        }
        else{
          this.cc.radius = vCol.x         ; //else leave at default
        }
        return true;
      }
      else{
        return false;
      }
    }

#endregion

#region OnCeilng(){}

    public virtual bool OnCeilng(CharacterController cc){
      Vector3 vPos = cc.transform.position + cc.center                                 ;
      return this.OnCeilng(vPos, Vector3.up, new Vector3(0.0f, cc.height * 1.25f, 0.0f)) ;
    }

    public virtual bool OnCeilng(Vector3 vPos, Vector3 vDir, Vector3 vCol){
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
