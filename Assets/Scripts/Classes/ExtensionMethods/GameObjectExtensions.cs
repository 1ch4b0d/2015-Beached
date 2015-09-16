using UnityEngine;
using System.Collections;
using System.Linq;

// Grabbed from here:
// http://answers.unity3d.com/questions/8500/how-can-i-get-the-full-path-to-a-gameobject.html
public static class GameObjectExtensions {
    public static T GetFirstChildOfType<T>(this GameObject go) {
        int currentChildTransform = 0;
        while(currentChildTransform < go.transform.childCount) {
            GameObject currentChildGameObject = go.transform.GetChild(currentChildTransform).gameObject;
            T currentTypeCheck = currentChildGameObject.gameObject.GetComponent<T>();
            if(currentTypeCheck != null) {
                return currentTypeCheck;
            }
            else {
                T childOfType = GetFirstChildOfType<T>(currentChildGameObject);
                if(childOfType != null) {
                    return childOfType;
                }
            }
            currentChildTransform++;
        }
        return default(T);
    }
    
    public static T GetFirstParentOfType<T>(this GameObject go) {
        if(go != null) {
            T currentTypeCheck = go.GetComponent<T>();
            if(currentTypeCheck != null) {
                return currentTypeCheck;
            }
            else {
                Transform parentTransform = go.transform.parent;
                if(parentTransform != null) {
                    return GetFirstParentOfType<T>(parentTransform.gameObject);
                }
                else {
                    return GetFirstParentOfType<T>(null);
                }
            }
        }
        else {
            return default(T);
        }
    }
    
    public static void LogComponentError<T>(this GameObject currentGameObject, string componentName, T scriptType) {
        // currentGameObject.transform.GetFullPath();
        Debug.LogError(currentGameObject.transform.GetFullPath() + " needs its " + componentName + " reference to be set in the " + scriptType.ToString() + " Script.");
    }
    
    public static GameObject FindInChildren(this GameObject go, string name) {
        if(go.transform.childCount <= 0) {
            return null;
        }
        else {
            GameObject gameObjectToReturn = null;
            foreach(Transform childTransform in go.transform) {
                // Debug.Log("Name sought: " + name + "\n" + "Current Name: " + childTransform.name);
                if(childTransform.name.Equals(name)) {
                    gameObjectToReturn = childTransform.gameObject;
                }
                else {
                    gameObjectToReturn = childTransform.gameObject.FindInChildren(name);
                }
                if(gameObjectToReturn != null) {
                    // stops the recursion
                    break;
                }
            }
            return gameObjectToReturn;
        }
    }
}


