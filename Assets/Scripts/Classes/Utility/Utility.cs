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
        GameObject currentParentGameObject = gameObjectToSearch.transform.parent.gameObject;
        T currentTypeCheck = currentParentGameObject.gameObject.GetComponent<T>();
        
        // Debug.Log("-------------------------------------");
        // Debug.Log("Checking: " + gameObjectToSearch.name);
        // Debug.Log("Current Parent: " + currentParentGameObject.name);
        
        if(currentTypeCheck != null) {
            return currentTypeCheck;
        }
        else {
            T parentOfType = GetFirstParentOfType<T>(currentParentGameObject);
            if(parentOfType != null) {
                return parentOfType;
            }
        }
        
        return default(T);
    }
}