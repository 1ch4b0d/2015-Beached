using UnityEngine;
using System.Collections;

// Grabbed from here:
// http://answers.unity3d.com/questions/8500/how-can-i-get-the-full-path-to-a-gameobject.html
public static class GameObjectExtensions {
    public static void LogComponentError<T>(this GameObject currentGameObject, string componentName, T scriptType) {
        // currentGameObject.transform.GetFullPath();
        Debug.LogError(currentGameObject.transform.GetFullPath() + " needs its " + componentName + " reference to be set in the " + scriptType.ToString() + " Script.");
    }
}

