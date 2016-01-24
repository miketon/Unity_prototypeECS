using UnityEngine        ;
using System             ; //NOTE : ??? must import to use anonymous function ; And the IComparable Interface for Dictionary
using System.Collections ;
//using DG.Tweening        ; //import DemiGiant DoTween

//It is common to create a class to contain all of your
//extension methods. This class must be static.
public static class __gEXTENSIONS {

  // Collider : Adding GetComponent functionality
  public static T GetComponentEX<T>(this Collider self){// , Func<T, true> OnValid){
    return self.gameObject.GetComponent<T>();
  }

//  public static Transform Emit<T>(this Transform self, Transform IN_XFORM, Vector3 IN_POS, Quaternion IN_ROT, Func<T> funcToRun){
//    Transform retXform = IN_XFORM.lpSpawn(IN_POS, IN_ROT);
//    funcToRun();
//    return retXform;
//  }

  // Ray Check Ground
  private static float groundThreshold = 0.05f; //margin for ground check and object swap out.
  public  static float dirRayCheck(this Transform self, Vector3 vPos, Vector3 vDir, float IN_magnitude, int IN_layerMask){    //direction, magnitude and x offset
    RaycastHit hit                                                               ;
    Debug.DrawLine(vPos, vPos + (vDir * IN_magnitude), Color.red, 0.5f, false)   ;
    if (Physics.Raycast(vPos, vDir, out hit, Mathf.Abs(IN_magnitude), IN_layerMask)){       //return hit distance to the ground
      return hit.distance     ; //found ground, returning distance > 0.0f
    }
    else{
      return -groundThreshold ; //not found ground returning < 0.0f
    }
  }

  // Ray Hit Object
  public static GameObject doRayHit(this Transform self, Vector3 IN_POS, Vector3 IN_DIR, float IN_DIST = 2.0f){
    RaycastHit hit;
    Ray ray = new Ray(IN_POS, IN_DIR);
    Physics.Raycast(ray, out hit, IN_DIST);
    Debug.DrawRay(IN_POS, IN_DIR, Color.yellow, 0.75f);
    if(hit.collider != null){
      return hit.collider.gameObject;
    }
    else{
      return null;
    }
  }

    // Check Range Between Source and Target transform
  public static void doRangeCheck<T>(this Transform self, Transform IN_TGT, float IN_DIST, Func<bool, float, T> funcToRun){
    float dist = Vector3.Distance(self.position, IN_TGT.position);
    bool  bRng = false;
    if(dist < IN_DIST){
      bRng = true ;
    }
    funcToRun(bRng, dist);
  }

  public static void doAimTowardsY(this Transform self, Vector3 IN_POS_TGT, float IN_FACING = 1.0f, float IN_ROTSPEED = 8.0f){ //IN_FACING = 1.0f forward, -1.0f reverse
    var rDir = Vector3.Normalize((self.position - IN_POS_TGT) * IN_FACING) ;
    var newRotation = Quaternion.LookRotation(rDir, Vector3.forward)       ;
    newRotation.x = 0.0f                                                   ;
    newRotation.y = 0.0f                                                   ;
    self.rotation = Quaternion.Slerp(self.rotation, newRotation, Time.deltaTime * IN_ROTSPEED);
  }

  public static Quaternion doRotateTowards(this Quaternion self, Vector3 IN_DIR){
    float angle = Mathf.Atan2(IN_DIR.y, IN_DIR.x) * Mathf.Rad2Deg;
    Quaternion rLook = Quaternion.AngleAxis(angle, Vector3.forward) * Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f)); //offset to forward z
    return rLook;
  }

  public static Vector3 doRadiusPos(this Vector3 self, Vector3 IN_POS, float IN_RAD = 1.0f){
    return IN_POS + (UnityEngine.Random.insideUnitSphere * IN_RAD);
  }

  public static Vector3 MoveOrtho(this Transform self, Vector3 IN_VEC3){
    int max = -1;
    int len =  2;
    for(var i=0; i<=len; i++){
      if(Mathf.Abs(IN_VEC3[i]) > Mathf.Abs(IN_VEC3[(i+1)%(len+1)])){
        max = i;
      }
//        Debug.Log("MoveOrtho : " + IN_VEC3[i] + " i " + i + " : " + (i+1)%(len+1));
    }
//    Debug.Log("MoveOrtho : MAX : " + max);
    if(max==0){
      if(IN_VEC3[max] < 0){ //move left
        return Vector3.left ;
      }
      else{ //move right
        return Vector3.right ;
      }
    }
    else if(max==1){
      if(IN_VEC3[max] < 0){ //move down
        return Vector3.down;
      }
      else{ //move up
        return Vector3.up;
      }
    }
    else{
      if(IN_VEC3[max] < 0){ //move backwards
        return Vector3.back;
      }
      else{ //move forward
        return Vector3.forward;
      }
    }
  }

  public static void doTweenToValue<T>(this float self, float fTarget, float fDur, Func<T> funcToRun){
//    DOTween.To(()=> self, x=> self = x, fTarget, fDur);
    funcToRun();
  }

//  public static void doCoroutineToValue<T>(this float self, float fTarget, float fDur, Func<T> funcToRun){
//    this.
//    funcToRun();
//  }

}