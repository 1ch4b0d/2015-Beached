using UnityEngine;
using System.Collections;

public static class Factory {
    public static GameObject MakeChildOfNGUIRoot(GameObject gameObject) {
        Vector3 initialScale = gameObject.transform.localScale;
        gameObject.transform.parent = NGUIManager.Instance.UIRoot().transform;
        gameObject.transform.localScale = initialScale;
        return gameObject;
    }
    
    public static GameObject CreditPanel() {
        GameObject newCreditText = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/GUI/Credits/CreditPanel") as GameObject);
        MakeChildOfNGUIRoot(newCreditText);
        return newCreditText;
    }
    
    public static GameObject CreditText() {
        GameObject newCreditText = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/GUI/Credits/CreditText") as GameObject);
        MakeChildOfNGUIRoot(newCreditText);
        return newCreditText;
    }
    
    public static GameObject SpeechBubble() {
        GameObject newSpeechBubbleGameObject = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/GUI/SpeechBubble/SpeechBubble") as GameObject);
        return newSpeechBubbleGameObject;
    }
}
