using UnityEngine;
using System.Collections;

// Grabbed from here:
// http://answers.unity3d.com/questions/8500/how-can-i-get-the-full-path-to-a-gameobject.html
public static class TransformExtensions {
    public static string GetFullPath(this Transform current) {
        if(current.parent == null) {
            return current.name + "/";
        }
        else  {
            return current.parent.GetFullPath() + current.name + "/";
        }
    }
}

