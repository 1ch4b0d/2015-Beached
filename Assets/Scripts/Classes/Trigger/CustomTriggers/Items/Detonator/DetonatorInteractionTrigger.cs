using UnityEngine;
using System.Collections;

public class DetonatorInteractionTrigger : ItemInteractionTrigger {
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
        if(detonator == null) {
            detonator = Utility.GetFirstParentOfType<Detonator>(this.gameObject);
            if(detonator == null) {
                Debug.LogError("Please assign the 'Detonator' property, or make " + this.gameObject.name + " a child of an object with the 'Detonator' script. Because, it is currently null.");
            }
        }
    }
    
    public override void ShowSpeechBubble(float duration) {
        if(speechBubble != null
            && detonator.IsPrimed()
            && !detonator.HasBeenDetonated()) {
            speechBubble.Show(duration);
        }
    }
    
    public override void HideSpeechBubble(float duration) {
        // if(speechBubble != null) {
        //     speechBubble.Hide(duration);
        // }
        base.HideSpeechBubble(duration);
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        base.Entered(gameObjectEntering);
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        base.Exited(gameObjectExiting);
    }
    
    public override void Execute(GameObject gameObjectToExecute) {
        base.Execute(gameObjectToExecute);
    }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        detonator.Detonate();
    }
}
