using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubbleValuesEvent : CustomEventObject {
    public SpeechBubble speechBubble = null;
    public SpeechBubbleImage speechBubbleImage = SpeechBubbleImage.None;
    public List<string> textSet = null;
    public float showVerticalScaleDuration = 0.25f;
    public float showHorizontalScaleDuration = 0.25f;
    public float hideVerticalScaleDuration = 0.25f;
    public float hideHorizontalScaleDuration = 0.25f;
    public bool isEnabled = true;
    public bool show = false;
    public bool hide = false;
    public bool startInteraction = false;
    public bool finishInteraction = false;
    public bool clearOnStartInteraction = false;
    public List<CustomEventsManager> onStartInteraction = null;
    public bool clearOnTextIteration = false;
    public List<CustomEventsManager> onTextIteration = null;
    public bool clearOnFinishInteraction = false;
    public List<CustomEventsManager> onFinishInteraction = null;
    
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
    
    // // public override void Execute() {
    // base.Execute();
    // }
    
    protected override void Initialize() {
        base.Initialize();
        if(speechBubble == null) {
            this.gameObject.LogComponentError("speechBubble", this.GetType());
        }
        if(textSet == null) {
            textSet = new List<string>();
        }
    }
    
    public override void ExecuteLogic() {
        SetSpeechBubbleValues();
    }
    
    public void SetSpeechBubbleValues() {
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
        if(textSet.Count > 0) {
            speechBubble.SetTextSet(textSet.ToArray());
        }
        
        //------------------------------
        // Show/Hide
        //------------------------------
        if(show
            && hide) {
            Debug.LogError("Show and hide cannot BOTH be TRUE. Please fix this on " + this.gameObject.name);
        }
        else if(show) {
            speechBubble.Show(showVerticalScaleDuration, showHorizontalScaleDuration);
        }
        else if(hide) {
            speechBubble.Hide(hideVerticalScaleDuration, hideHorizontalScaleDuration);
        }
        
        if(startInteraction
            && !speechBubble.IsInUse()) {
            speechBubble.StartInteraction();
        }
        if(finishInteraction
            && speechBubble.IsInUse()) {
            speechBubble.FinishInteraction();
        }
        
        //----------------------------------------------------------------------
        // Events
        //----------------------------------------------------------------------
        
        //------------------------------
        // Speech Bubble - On Start Interation
        if(clearOnStartInteraction) {
            speechBubble.onStartInteraction.Clear();
        }
        // ToArray() is used to get a copy so that this doesn't trip over itself recursively
        // when it modifies the finish
        foreach(CustomEventsManager customEventsManager in onStartInteraction.ToArray()) {
            speechBubble.onStartInteraction.Add(customEventsManager);
        }
        //------------------------------
        
        //------------------------------
        // Speech Bubble - On Iteratie
        if(clearOnTextIteration) {
            speechBubble.onTextIteration.Clear();
        }
        // ToArray() is used to get a copy so that this doesn't trip over itself recursively
        // when it modifies the finish
        foreach(CustomEventsManager customEventsManager in onTextIteration.ToArray()) {
            speechBubble.onTextIteration.Add(customEventsManager);
        }
        //------------------------------
        
        //------------------------------
        // Speech Bubble - On Finish Interaction
        if(clearOnFinishInteraction) {
            speechBubble.onFinishInteraction.Clear();
        }
        // ToArray() is used to get a copy so that this doesn't trip over itself recursively
        // when it modifies the finish
        foreach(CustomEventsManager customEventsManager in onFinishInteraction.ToArray()) {
            speechBubble.onFinishInteraction.Add(customEventsManager);
        }
        //------------------------------
    }
}