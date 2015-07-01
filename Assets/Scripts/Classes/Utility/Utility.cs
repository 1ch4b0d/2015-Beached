using UnityEngine;
using System.Collections;

public static class Utility {
    public static T GetFirstChildOfType<T>(GameObject gameObjectToSearch) {
        int currentChildTransform = 0;
        while(currentChildTransform < gameObjectToSearch.transform.childCount) {
            GameObject currentChildGameObject = gameObjectToSearch.transform.GetChild(currentChildTransform).gameObject;
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
    
    public static T GetFirstParentOfType<T>(GameObject gameObjectToSearch) {
        if(gameObjectToSearch != null) {
            T currentTypeCheck = gameObjectToSearch.GetComponent<T>();
            if(currentTypeCheck != null) {
                return currentTypeCheck;
            }
            else {
                Transform parentTransform = gameObjectToSearch.transform.parent;
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
}