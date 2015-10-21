using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCInteractionTrigger : SpeechBubbleInteractionTrigger {
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
    
    public override void Entered(GameObject gameObjectEntering) {
        // Debug.Log("SpeechBubble Trigger Entered");
        Player playerRef = gameObjectEntering.GetComponent<Player>();
        if(playerRef != null
            && !playerRef.GetInteractionController().IsInteracting()) {
            ShowSpeechBubble(horizontalScaleOutDuration, verticalScaleOutDuration);
        }
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        Player playerRef = gameObjectExiting.GetComponent<Player>();
        if(playerRef != null//) {
            // && !playerRef.GetInteractionController().IsInteracting()) {
            && this != playerRef.GetInteractionController().GetTriggerBeingInteractedWith()) {
            HideSpeechBubble(horizontalScaleInDuration, verticalScaleInDuration);
        }
    }
}
