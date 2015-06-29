using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionTrigger : CustomTrigger {
    public SpeechBubble speechBubble = null;
    
    void Awake() {
    }
    
    // Use this for initialization
    void Start() {
        Initialize();
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public override void Initialize() {
        base.Initialize();
    }
    
    public virtual void Interact(GameObject gameObjectInteracting) {
        Execute(gameObjectInteracting);
    }
    
    public void ShowSpeechBubble(float duration) {
        if(speechBubble != null) {
            speechBubble.Show(duration);
        }
    }
    
    public void HideSpeechBubble(float duration) {
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