using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubbleInteractionTrigger : InteractionTrigger {
    public float horizontalScaleOutDuration = 0.25f;
    public float verticalScaleOutDuration = 0.25f;
    public float horizontalScaleInDuration = 0.25f;
    public float verticalScaleInDuration = 0.25f;
    public SpeechBubble speechBubble = null;
    public List<SpeechBubbleTextSet> textSets = null;
    private SpeechBubbleTextSet placeholderTextSet = null;
    
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
        if(textSets == null
            || textSets.Count == 0) {
            Debug.LogError(this.gameObject.transform.GetFullPath() + " needs its 'textSets' reference to be set and have more than 0 elements in it for the 'SpeechBubbleInteractionTrigger' Script");
        }
        
        foreach(SpeechBubbleTextSet speechBubbleTextSet in textSets) {
            if(speechBubbleTextSet == null) {
                Debug.Log(this.gameObject.transform.GetFullPath() + " has a NULL text set. Please fix this.");
            }
        }
        if(speechBubble == null) {
            this.gameObject.LogComponentError("speechBubble", this.GetType());
        }
    }
    
    public virtual void SetTextSets(List<SpeechBubbleTextSet> newTextSets) {
        textSets = newTextSets;
    }
    
    public virtual SpeechBubbleTextSet PopNextTextSet() {
        SpeechBubbleTextSet textSetToReturn = null;
        if(textSets.Count > 0) {
            textSetToReturn = textSets[0];
            textSets.Remove(textSetToReturn);
            textSets.Add(textSetToReturn);
        }
        else {
            if(placeholderTextSet == null) {
                GameObject newPlaceholderTextSet = new GameObject("PlaceholderTextSet");
                placeholderTextSet = (newPlaceholderTextSet.AddComponent<SpeechBubbleTextSet>()).GetComponent<SpeechBubbleTextSet>();
                newPlaceholderTextSet.transform.parent = this.gameObject.transform;
            }
            textSetToReturn = placeholderTextSet;
        }
        return textSetToReturn;
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        // Debug.Log("SpeechBubble Trigger Entered");
        ShowSpeechBubble(horizontalScaleOutDuration, verticalScaleOutDuration);
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        // Debug.Log("SpeechBubble Trigger Exited");
        HideSpeechBubble(horizontalScaleInDuration, verticalScaleInDuration);
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
        // Debug.Log("----------------------------------");
        // Debug.Log("SpeechBubble Triggered Interaction");
        // Debug.Log("SpeechBubble In Use: " + speechBubble.IsInUse());
        Player playerRef = gameObjectExecuting.GetComponent<Player>();
        if(playerRef != null
            && speechBubble != null) {
            // Starts the speech bubble
            if(!speechBubble.IsInUse()
                && speechBubble.hasFinishedShowing) {
                // Debug.Log("Starting");
                playerRef.StartInteraction();
                
                // speechBubble.SetTextSet(PopNextTextSet().GetTextSet().ToArray());
                speechBubble.StartInteraction(PopNextTextSet().GetTextSet().ToArray());
            }
            else {
                // Ends the speech bubble
                if(speechBubble.HasFinishedInteraction()) {
                    if(speechBubble.IsDisplayingText()) {
                        // Debug.Log("Finished");
                        
                        speechBubble.FinishInteraction();
                        playerRef.StartGameplayState();
                        
                        currentIteration++;
                    }
                }
                // Proceeds in the speech bubble
                else {
                    if(speechBubble.HasFinishedDisplayingText()) {
                        // Debug.Log("Moving To Next");
                        speechBubble.MoveToNextText();
                    }
                }
            }
        }
    }
    
    public virtual void ShowSpeechBubble(float horizontalScaleOutDuration, float verticalScaleOutDuration) {
        if(speechBubble != null
            && speechBubble.enabled
            && !speechBubble.IsDisplayed()) {
            // Debug.Log("Showing");
            speechBubble.Show(horizontalScaleOutDuration, verticalScaleOutDuration);
        }
    }
    
    public virtual void HideSpeechBubble(float horizontalScaleInDuration, float verticalScaleInDuration) {
        if(speechBubble != null
            && speechBubble.enabled
            && speechBubble.IsDisplayed()) {
            Debug.Log("Hiding");
            speechBubble.Hide(horizontalScaleInDuration, verticalScaleInDuration);
        }
    }
}
