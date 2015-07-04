using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Detonator : Item {
    public List<DetonatorPrimer> detonatorPrimers = null;
    public bool hasBeenDetonated = false;
    
    // Use this for initialization
    void Start() {
        Initialize();
    }
    
    // Update is called once per frame
    void Update() {
        UpdateAnimator(animator);
    }
    
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
    
    public override void Initialize() {
        base.Initialize();
    }
    
    public override void UpdateAnimator(Animator animatorToUpdate) {
        animatorToUpdate.SetBool("IsPrimed", IsPrimed());
    }
    
    public void Detonate() {
        hasBeenDetonated = true;
        Debug.Log("Explodie!!!");
        LevelManager.Instance.TriggerExplosionCinematic();
    }
}
