using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionTrigger : CustomTrigger {
    public SpeechBubble speechBubble = null;
    
    void Awake() {
    }
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public override void Initialize() {
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
}