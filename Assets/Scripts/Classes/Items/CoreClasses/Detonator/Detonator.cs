using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Detonator : Item {
    public List<DetonatorPrimer> detonatorPrimers = null;
    public bool hasBeenDetonated = false;
    public List<CustomEventsManager> onDetonate = null;
    
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
    
    public bool IsPrimed() {
        bool allDetonatorsActive = true;
        if(detonatorPrimers.Count == 0) {
            // Base Case - Do nothing, all detonators are active
        }
        else {
            foreach(DetonatorPrimer detonatorPrimer in detonatorPrimers) {
                if(!detonatorPrimer.IsPrimed()) {
                    allDetonatorsActive = false;
                }
            }
        }
        
        return allDetonatorsActive;
    }
    
    public void SetHasBeenDetonated(bool newHasBeenDetonated) {
        hasBeenDetonated = newHasBeenDetonated;
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
            SetHasBeenDetonated(true);
            FireOnDetonateEvents();
        }
    }
    
    public virtual void FireOnDetonateEvents() {
        if(onDetonate != null) {
            foreach(CustomEventsManager customEventsManager in onDetonate) {
                customEventsManager.Execute();
            }
        }
    }
}