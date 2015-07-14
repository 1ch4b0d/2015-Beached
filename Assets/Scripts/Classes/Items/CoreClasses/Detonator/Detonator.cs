using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Detonator : Item {
    public List<DetonatorPrimer> detonatorPrimers = null;
    public bool hasBeenDetonated = false;
    public CustomEventsManager onDetonate = null;
    
    // protected virtual void Awake() {
    //     Initialize();
    // }
    
    // // Use this for initialization
    // protected override void Start() {
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    // }
    
    // protected override void Initialize() {
    //     base.Initialize();
    // }
    
    public bool IsPrimed() {
        if(detonatorPrimers.Count == 0) {
            return true;
        }
        else {
            bool allDetonatorsActive = true;
            foreach(DetonatorPrimer detonatorPrimer in detonatorPrimers) {
                if(!detonatorPrimer.IsPrimed()) {
                    allDetonatorsActive = false;
                    // exit the loop - because why waste the cycles
                    break;
                }
            }
            
            return allDetonatorsActive;
        }
    }
    
    public bool HasBeenDetonated() {
        return hasBeenDetonated;
    }
    
    public override void UpdateAnimator(Animator animatorToUpdate) {
        animatorToUpdate.SetBool("IsPrimed", IsPrimed());
    }
    
    public virtual void Detonate() {
        if(IsPrimed()
            && !HasBeenDetonated()) {
            hasBeenDetonated = true;
            FireOnDetonateEvents();
        }
    }
    
    public virtual void FireOnDetonateEvents() {
        if(onDetonate != null) {
            onDetonate.Execute();
        }
    }
}
