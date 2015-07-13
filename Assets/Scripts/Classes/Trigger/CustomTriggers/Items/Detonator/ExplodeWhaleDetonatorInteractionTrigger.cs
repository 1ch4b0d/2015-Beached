using UnityEngine;
using System.Collections;

public class ExplodeWhaleDetonatorInteractionTrigger : DetonatorInteractionTrigger {
    // Use this for initialization
    // void Start() {
    //     Initialize();
    // }
    
    // Update is called once per frame
    // void Update() {
    // }
    
    // public override void Initialize() {
    //     base.Initialize();
    // }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        if(detonator.IsPrimed()
            && !detonator.HasBeenDetonated()) {
            detonator.Detonate();
            LevelManager.Instance.TriggerExplosionCinematic();
        }
    }
}
