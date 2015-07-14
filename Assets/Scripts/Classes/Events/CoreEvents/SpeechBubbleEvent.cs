using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubbleEvent : CustomEventObject {
    public SpeechBubble speechBubble = null;
    public List<string> textSet = null;
    
    // // Use this for initialization
    // protected override void Awake(){
    // }
    
    // // Use this for initialization
    // protected override void Start() {
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    // }
    
    // public override void Execute() {
    // }
    
    public override void ExecuteLogic() {
        SetSpeechBubbleValues();
    }
    
    public void SetSpeechBubbleValues() {
        Debug.Log("starting!");
    }
}
