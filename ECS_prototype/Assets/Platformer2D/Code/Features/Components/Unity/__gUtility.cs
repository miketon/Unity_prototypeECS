using UnityEngine                ;
using System                     ; // NOTE : ??? must import to use anonymous function ; And the IComparable Interface for Dictionary
using System.Collections         ;
using System.Collections.Generic ; // Dictionary, List
using System.Reflection          ; // NOTE : Required for deep copy

namespace MTON {

  public static class __gUtility{ // ??? NOTE : MUST add static to make class non generic ???

    //Prevents duplicate components, checks to see that one doesn't already exist before adding
    public static T AddComponent_mton<T>(GameObject IN_GO) where T:Component{ 
      T cExist = IN_GO.GetComponent<T>();
      if(cExist == null){
//      Debug.Log(" Adding " + cExist); // (Component)T.ToString());
      return IN_GO.AddComponent<T>();
      }
      else{
//      Debug.Log(" Found " + cExist); //(Component)T.ToString());
        return cExist;
      }
    }

    public static IEnumerator WaitUntilDistantLess<T>(Transform IN_xform_SRC, Transform IN_xform_TGT, float In_threshold, Func<T> funcToRun){
      float distToOther = 0.0f  ;
      while(distToOther  < In_threshold){
//      distToOther = Vector3.Distance(IN_xform_SRC.position, IN_xform_TGT.position) ;
        float distX = Mathf.Abs(IN_xform_SRC.position.x - IN_xform_TGT.position.x)        ; // check x
        float distY = Mathf.Abs(IN_xform_SRC.position.y - IN_xform_TGT.position.y) * 0.5f ; // vertical height too much delta change, weight down by 1/5th
    
        if(distY > distX){
          distToOther = distY ;
        }
        else{
          distToOther = distX ;
        }
        yield return null     ;
      }
      funcToRun()             ; // NOTE : anonymous method of type `System.Func<T>' must return a value ; else error
    }

    public static void CheckAndInitLayer(GameObject IN_GO, string IN_LAYERNAME){
      string curLayer = LayerMask.LayerToName(IN_GO.layer);
      if(curLayer != IN_LAYERNAME){
        IN_GO.layer = LayerMask.NameToLayer (IN_LAYERNAME); 
        Debug.LogWarning(IN_GO+" Layer/Level Hint Not Setup Correctly : Converting '" + curLayer + "' to : '" + IN_LAYERNAME + "' ");
      }
    }

//  public static string[] LoopThroughEnum<T>(T Tenum){
//    string[] retString = Tenum.GetNames(typeof(Tenum));
//    foreach(string s in retString){
//      Debug.Log("Looping Through Enum : " + s);
//    }
//  }

    public static void Debugmton(int IN_mton){
      Debug.Log("IN_mton");
    }
  
//      answer by vexe : http://answers.unity3d.com/questions/530178/how-to-get-a-component-from-an-object-and-add-it-t.html
//      Extension methods must be defined in a non-generic static class
//      The following points need to be considered when creating an extension method:
//      
//      The class which defines an extension method must be non-generic, static and non-nested
//      Every extension method must be a static method
//      The first parameter of the extension method should use the this keyword.
//

    public static T GetCopyOf<T>(this Component comp, T other) where T : Component{
      Type type = comp.GetType();
      if (type != other.GetType()) return null; // type mis-match
      BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
      PropertyInfo[] pinfos = type.GetProperties(flags);
      foreach (var pinfo in pinfos) {
        if (pinfo.CanWrite) {
          try {
            pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
          }
            catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
        }
      }
      FieldInfo[] finfos = type.GetFields(flags);
      foreach (var finfo in finfos) {
        finfo.SetValue(comp, finfo.GetValue(other));
      }
      return comp as T;
    }

    public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component{
      return go.AddComponent<T>().GetCopyOf(toAdd) as T;
    }
  
//  var copy = myComp.GetCopyOf(someOtherComponent); //USAGE : example

  }
  
}
