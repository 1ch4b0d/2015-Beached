using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubbleInteractionTrigger : InteractionTrigger {
    public List<string> textSet = null;
    public SpeechBubble speechBubble = null;
    
    void Awake() {
        if(textSet == null) {
            textSet = new List<string>();
        }
    }
    
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
    
    public override void Execute(GameObject gameObjectToExecute) {
        // Perform only if it's the first iteration, or it should loop
        if(currentIteration < 1
            || loop) {
            ExecuteLogic(gameObjectToExecute);
            // This isn't used in the execute logic, this is iterated in the ExecuteLogic function
            // currentIteration++;
            
            if(loop == false) {
                this.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        // Debug.Log("SpeechBubble Triggered Interaction");
        Player player = gameObjectExecuting.GetComponent<Player>();
        if(player != null
            && speechBubble != null) {
            if(!speechBubble.IsInUse()) {
                // speechBubble.SetTextSet("lol", "for real though", "ELL.", "OH.", "ELL.");
                // speechBubble.OnFinish(() => { Debug.Log("FINISHED SPEECH BUBBLE."); });
                speechBubble.SetTextSet(textSet.ToArray());
                
                player.StartInteraction();
                speechBubble.StartInteraction();
            }
            else {
                if(speechBubble.HasFinished()) {
                    speechBubble.FinishInteraction();
                    player.StartGameplayState();
                    currentIteration++;
                }
                else {
                    speechBubble.MoveToNextText();
                }
            }
        }
    }
}