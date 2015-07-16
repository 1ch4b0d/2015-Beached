using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubbleValuesEvent : CustomEventObject {
    public SpeechBubble speechBubble = null;
    public SpeechBubbleImage speechBubbleImage = SpeechBubbleImage.None;
    public List<string> textSet = null;
    public bool isEnabled = true;
    public bool show = false;
    public bool hide = false;
    
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
    
    protected override void Initialize() {
        if(speechBubble == null) {
            Debug.LogError("The 'speechBubble' reference needs to be set in the 'SetSpeechBubbleValuesEvent' Script on " + this.gameObject.name);
        }
    }
    
    public override void ExecuteLogic() {
        SetSpeechBubbleValues();
    }
    
    public void SetSpeechBubbleValues() {
        // Debug.Log("setting speech bubble values!");
        
        //------------------------------
        // Enable/Disable
        //------------------------------
        // this needs to happen first because the speech bubble has logic based
        // on if it's enabled or not
        speechBubble.enabled = isEnabled;
        
        //------------------------------
        // SpeechBubbleImage
        //------------------------------
        speechBubble.SetSpeechBubbleImage(speechBubbleImage);
        
        //------------------------------
        // Text Set
        //------------------------------
        if(textSet != null
            && textSet.Count > 0) {
            speechBubble.SetTextSet(textSet.ToArray());
        }
        Debug.Log("speech text:" + speechBubble.textSet.Count);
        
        //------------------------------
        // Show/Hide
        //------------------------------
        if(show
            && hide) {
            Debug.LogError("Show and hide cannot BOTH be TRUE. Please fix this on " + this.gameObject.name);
        }
        else if(show) {
            speechBubble.Show();
        }
        else if(hide) {
            speechBubble.Hide();
        }
        
        speechBubble.StartInteraction();
    }
}