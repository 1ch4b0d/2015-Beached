using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Detonator : Item {
    public List<DetonatorPrimer> detonatorPrimers = null;
    
    // Use this for initialization
    void Start() {
        Initialize();
    }
    
    // Update is called once per frame
    void Update() {
        UpdateAnimator(animator);
    }
    
    public bool CanBeActivated() {
        if(detonatorPrimers.Count == 0) {
            return true;
        }
        else {
            bool allDetonatorsActive = true;
            foreach(DetonatorPrimer detonatorPrimer in detonatorPrimers) {
                if(!detonatorPrimer.IsPrimed()) {
                    allDetonatorsActive = false;
                    // terminate because why waste the cylce
                    break;
                }
            }
            
            return allDetonatorsActive;
        }
    }
    
    public override void Initialize() {
        base.Initialize();
    }
    
    public override void UpdateAnimator(Animator animatorToUpdate) {
        animatorToUpdate.SetBool("CanBeActivated", CanBeActivated());
    }
}
