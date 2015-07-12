﻿using UnityEngine;
using System.Collections;

public class DetonatorInteractionTrigger : InteractionTrigger {
    public Detonator detonator = null;
    
    // Use this for initialization
    protected override void Start() {
        Initialize();
    }
    
    // Update is called once per frame
    protected override void Update() {
    }
    
    protected override void Initialize() {
        base.Initialize();
        detonator = Utility.GetFirstParentOfType<Detonator>(this.gameObject);
    }
    
    public override void ShowSpeechBubble(float duration) {
        if(speechBubble != null
            && detonator.IsPrimed()
            && !detonator.HasBeenDetonated()) {
            speechBubble.Show(duration);
        }
    }
    
    public override void HideSpeechBubble(float duration) {
        if(speechBubble != null) {
            speechBubble.Hide(duration);
        }
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        // base.Entered(gameObjectEntering);
        ShowSpeechBubble(0.25f);
    }
    
    // public override void Exited(GameObject gameObjectExiting) {
    //     base.Exited(gameObjectExiting);
    // }
    
    // public override void Execute(GameObject gameObjectToExecute) {
    //     base.Execute(gameObjectToExecute);
    // }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        if(detonator.IsPrimed()
            && !detonator.HasBeenDetonated()) {
            detonator.Detonate();
        }
    }
}
