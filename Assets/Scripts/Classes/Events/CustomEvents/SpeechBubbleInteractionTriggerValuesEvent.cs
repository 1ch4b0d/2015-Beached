using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubbleInteractionTriggerValuesEvent : CustomEventObject {
    public SpeechBubbleInteractionTrigger speechBubbleInteractionTriggerToModify = null;
    public List<string> textSet = null;
    public bool isEnabled = true;
    
    // // Use this for initialization
    // protected override void Awake(){
    // base.Awake();
    // }
    
    // // Use this for initialization
    // protected override void Start() {
    // base.Start();
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    // base.Update();
    // }
    
    // public override void Execute() {
    // }
    
    protected override void Initialize() {
        base.Initialize();
        if(speechBubbleInteractionTriggerToModify == null) {
            Debug.LogError(this.gameObject.name + " needs its 'speechBubbleInteractionTriggerToModify' reference to be set in the 'SpeechBubbleValuesEvent' Script");
        }
    }
    
    public override void ExecuteLogic() {
        SetSpeechBubbleInteractionTriggerValues();
    }
    
    public void SetSpeechBubbleInteractionTriggerValues() {
        speechBubbleInteractionTriggerToModify.enabled = isEnabled;
        
        if(textSet != null
            && textSet.Count > 0) {
            speechBubbleInteractionTriggerToModify.SetTextSet(textSet.ToArray());
        }
    }
}