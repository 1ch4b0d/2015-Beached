using UnityEngine;
using System.Collections;

public static class GoTweenExtensions {
    public static void AddGoTween(this GameObject currentGameObject, GoTween newGoTween) {
        GoTweenManager goTweenManager = currentGameObject.GetComponent<GoTweenManager>();
        if(goTweenManager == null) {
            goTweenManager = (currentGameObject.AddComponent<GoTweenManager>()).GetComponent<GoTweenManager>();
            goTweenManager.Add(newGoTween);
        }
    }
}
