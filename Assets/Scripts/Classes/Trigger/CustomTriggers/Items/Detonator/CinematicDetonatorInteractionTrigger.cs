using UnityEngine;
using System.Collections;

public class CinematicDetonatorInteractionTrigger : DetonatorInteractionTrigger {
    CinematicDetonator cinematicDetonator = null;
    
    // // Use this for initialization
    // protected override void Awake() {
    //     Initialize();
    // }
    
    // // Use this for initialization
    // protected override void Start() {
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    // }
    
    protected override void Initialize() {
        base.Initialize();
        if(cinematicDetonator == null) {
            cinematicDetonator = this.gameObject.GetFirstParent<CinematicDetonator>();
            if(cinematicDetonator == null) {
                Debug.LogError("Please assign the 'CinematicDetonator' property, or make " + this.gameObject.name + " a child of an object with the 'CinematicDetonator' script. Because, it is currently null.");
            }
        }
    }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        cinematicDetonator.Detonate();
    }
}
