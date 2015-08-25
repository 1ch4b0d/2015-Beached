using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionTrigger : CustomTrigger {
    // This needs to be moved to the SpeechBubbleInteractionTrigger.
    // "You're out of your element Donny!~"
    public SpeechBubble speechBubble = null;
    
    protected override void Awake() {
        base.Awake();
    }
    
    // Use this for initialization
    protected override void Start() {
        base.Start();
    }
    
    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }
    
    protected override void Initialize() {
        base.Initialize();
    }
    
    public virtual void Interact(GameObject gameObjectInteracting) {
        Execute(gameObjectInteracting);
    }
    
    public virtual void ShowSpeechBubble(float duration) {
        if(speechBubble != null
            && speechBubble.enabled) {
            speechBubble.Show(duration);
        }
    }
    
    public virtual void HideSpeechBubble(float duration) {
        if(speechBubble != null
            && speechBubble.enabled) {
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