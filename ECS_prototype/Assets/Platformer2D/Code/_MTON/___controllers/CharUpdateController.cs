using UnityEngine;
using System;
using System.Collections;
using MTON.Interface;

namespace MTON.Controller {

  [RequireComponent (typeof (CharacterController))]
  public class CharUpdateController : MonoBehaviour, IRbody, IForce {

    private LayerMask _layerGround ;
    private CharacterController cc ;

    [Flags] // Powers of two
    public enum groundState {
      // Decimal                   // Binary
      Neutral  = 0,                // 000000
      MIDL     = 1,                // 000001
      LEFT     = 2,                // 000010
      RGHT     = 4,                // 000100 // Must be power of two for bitwise operator

      MLFT     = MIDL|LEFT,        // 000011 // at ledge right
      MLRT     = MIDL|RGHT,        // 000101 // at ledge left
      MNUL     = LEFT|RGHT,        // 000110 // falling down tube?
      FULL     = MIDL|LEFT|RGHT,   // 000111 // fully planted
    }
    
    [SerializeField]
    private groundState _gState = groundState.Neutral;

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
      this.OnGround();
//      this.bGround = this.OnGround()            ; //calculate ground state
//      this.cHeight = this.ccHeight(this.contrl) ; //update ccontrol height ??? Why check on every update ???
//        if(bFall){
//          Fall()                                   ; //calculate vertical state
//        }
//        doHit(this.vDirOnHit);
//        //HACK : doJump() must follow Fall(), order matters! Else vertical twitch and not jump curve
//        doJump()                                   ; //calculate jump state : NOTE : Can't replace with longform bJump prop handler???
//
//        gravity.x  = vMove.x                       ; //combine with move from Move()=>oMoveH() for final position
//        gravity.y += vMove.y                       ;
//        gravity.z  = 0.0f                          ; //forces character to stay in 2D plane
//        this.contrl.Move(gravity * Time.deltaTime) ; //do gravity
    }

    //Utilities -- Not extending xForm so reimplementing ground logic
    public virtual bool OnGround(){                                          
      Vector3 vPos = this.transform.position + this.center                                         ;
      return this.OnGround(vPos, -Vector3.up, new Vector3(this.radius * 0.85f, this.height, 0.0f)) ;
    }

    public int countCheck = 0;
    public virtual bool OnGround(Vector3 vPos, Vector3 vDir, Vector3 vCol){                 // vCol: x = cRadius, y = cHeight   
      float bCentCheck = this.dirRayCheck(vPos                            , vDir, vCol.y) ; // check center
      float bRghtCheck = this.dirRayCheck(vPos + ( Vector3.right * vCol.x), vDir, vCol.y) ; // check right edge
      float bLeftCheck = this.dirRayCheck(vPos + (-Vector3.right * vCol.x), vDir, vCol.y) ; // check left edge
      this._gState = groundState.Neutral;
      this.countCheck = 0 ;
      if(bCentCheck > 0.0f){
        countCheck=countCheck+2; //center counts more
        this._gState|=groundState.MIDL;
      }
      if(bLeftCheck > 0.0f){
        countCheck++;
        this._gState|=groundState.LEFT;
      }
      if(bRghtCheck > 0.0f){
        countCheck++;
        this._gState|=groundState.RGHT;
      }

      if (this._gState > 0){                                //either edge connects, then character is onGround
        if(countCheck<2){ //Not all rays hitting ground; reduce radius of collider
//        contrl.radius = vCol.x * 0.05f ; //reduce radius collider
        }
        else{
//        contrl.radius = vCol.x         ; //else leave at default
        }
        return true;
      }
      else{
        return false;
      }
    }

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

    public float dirRayCheck(Vector3 vPos, Vector3 vDir, float IN_magnitude){
      return this.transform.dirRayCheck(vPos, vDir, IN_magnitude, this._layerGround);
    }

  }

}
