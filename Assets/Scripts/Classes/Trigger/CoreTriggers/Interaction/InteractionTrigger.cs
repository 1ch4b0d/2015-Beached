using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionTrigger : CustomTrigger {
    public SpeechBubble speechBubble = null;
    
    protected override void Awake() {
    }
    
    // Use this for initialization
    protected override void Start() {
        Initialize();
    }
    
    // Update is called once per frame
    protected override void Update() {
    }
    
    protected override void Initialize() {
        base.Initialize();
    }
    
    public virtual void Interact(GameObject gameObjectInteracting) {
        Execute(gameObjectInteracting);
    }
    
    public virtual void ShowSpeechBubble(float duration) {
        if(speechBubble != null) {
            speechBubble.Show(duration);
        }
    }
    
    public virtual void HideSpeechBubble(float duration) {
        if(speechBubble != null) {
            speechBubble.Hide(duration);
        }
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        base.Entered(gameObjectEntering);
        ShowSpeechBubble(0.25f);
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        base.Exited(gameObjectExiting);
        HideSpeechBubble(0.25f);
    }
    
    public override void Execute(GameObject gameObjectToExecute) {
        base.Execute(gameObjectToExecute);
    }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        base.ExecuteLogic(gameObjectExecuting);
    }
}