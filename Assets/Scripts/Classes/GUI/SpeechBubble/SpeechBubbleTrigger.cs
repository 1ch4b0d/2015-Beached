using UnityEngine;
using System.Collections;

public class SpeechBubbleTrigger : InteractionTrigger {
    public SpeechBubble speechBubble = null;
    
    // Use this for initialization
    void Start() {
        Initialize();
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public void Initialize() {
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        // Debug.Log("SpeechBubble Trigger Entered");
        if(speechBubble != null) {
            speechBubble.Show(0.25f);
        }
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        // Debug.Log("SpeechBubble Trigger Exited");
        if(speechBubble != null) {
            speechBubble.Hide(0.25f);
        }
    }
    
    public override void Interact(GameObject gameObjectInteracting) {
        // Debug.Log("SpeechBubble Triggered Interaction");
        Player player = gameObjectInteracting.GetComponent<Player>();
        if(player != null
            && speechBubble != null) {
            if(!speechBubble.IsInUse()) {
                // speechBubble.SetTextSet("lol", "for real though", "ELL.", "OH.", "ELL.");
                speechBubble.SetTextSet("lol", "for real though");
                speechBubble.OnFinish(() => { Debug.Log("FINISHED SPEECH BUBBLE."); });
                // speechBubble.OnFinish(() => { player.StartGameplayState(); });
                
                player.StartInteraction();
                speechBubble.StartInteraction();
            }
            else {
                if(!speechBubble.HasFinished()) {
                    speechBubble.MoveToNextText();
                }
                else {
                    speechBubble.FinishInteraction();
                    player.StartGameplayState();
                    Debug.Log("Finished finished");
                }
            }
        }
    }
}