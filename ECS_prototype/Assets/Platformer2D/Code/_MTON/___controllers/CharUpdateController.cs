using UnityEngine;
using System;
using System.Collections;
using MTON.Interface;

namespace MTON.Controller {

  [RequireComponent (typeof (CharacterController))]
  public class CharUpdateController : MonoBehaviour, IRbody, IForce {

    private LayerMask _layerGround ;
    private CharacterController cc ;

    private _enum.VState vstate;
    private _enum.VState vState;

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

    #region IRbody implementation
    public Vector3    center { get; set; }
    public float      height { get; set; }
    public float      radius { get; set; }
    public Quaternion initRo { get; set; } // initRotation
    #endregion

    #region IForce implementation
    public Vector3 vMove { get; set;}
    public Vector3 vGrav { get; set;}
    #endregion

    public virtual void Awake(){ //earlier than Start(); need to get xform and rbody
      this._layerGround = LayerMask.GetMask (MTON._CONSTANTComponent._FLOOR);
      this.cc = this.GetComponent<CharacterController>();

      this.center  = this.cc.center;
      this.height  = (this.cc.height * this.transform.localScale.y * 0.5f) + this.cc.skinWidth ; 
      this.radius  = this.cc.radius * this.transform.localScale.x ;
      this.initRo  = this.transform.rotation;  
    }


    private void FixedUpdate(){
      OnGround();
      if(this._gCheck == rayState.NULL){ 
        Fall();
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
//        this.vGrav.x  = vMove.x                       ; //combine with move from Move()=>oMoveH() for final position
//        this.vGrav.y += vMove.y                       ;
//        this.vGrav.z  = 0.0f                          ; //forces character to stay in 2D plane
//        this.contrl.Move(this.vGrav * Time.deltaTime) ; //do gravity
    }


    public virtual void Fall(){ //vertical transform (this.vGrav)
//      if(this._gState == groundState.FALL){ //in air
//        this.vGrav   += pGrav * Time.deltaTime * this.massForce  ;
//        this.vGrav.y += -vy                                      ; //adding velocity
//        bCeilng    = this.OnCeilng()                          ;
//        if((this.contrl.velocity.y) < 0.1f){        //apply velocity after apex...changed from 0.0=>0.1 to blend apex transition
//          vy += accelY;
//        }
//        else if(bCeilng){
//          this.vGrav.y = -accelY;
//        }
//      }
//      else{ //on ground
//        if(Mathf.Abs(this.contrl.velocity.y) < 0.1f){ //and not on rise ; else get caught on ledges
//          ResetVelocity()         ;                   //reset velocity when on ground
//        }
//        if(dash){
//          vMove *= this.dashForce ;
//        }
//      }
//      this.vGrav.y = Mathf.Clamp(this.vGrav.y, -termVeloc, termVeloc) ; //clamp to terminal velocity
    }

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
