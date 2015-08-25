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
        Player playerReference = gameObjectEntering.GetComponent<Player>();
        if(playerReference != null) {
            ShowSpeechBubble(0.25f);
        }
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        // Debug.Log("SpeechBubble Trigger Exited");
        Player playerReference = gameObjectExiting.GetComponent<Player>();
        if(playerReference != null) {
            HideSpeechBubble(0.25f);
        }
    }
}
