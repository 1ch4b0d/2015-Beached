using UnityEngine;
using System.Collections;

public class DetonatorPrimer : Item {
    public bool isPrimed = false;
    
    // Use this for initialization
    void Start() {
        Initialize();
    }
    
    // Update is called once per frame
    void Update() {
        UpdateAnimator(animator);
    }
    
    public override void Initialize() {
        base.Initialize();
    }
    
    public bool IsPrimed() {
        return isPrimed;
    }
    
    public override void UpdateAnimator(Animator animatorToUpdate) {
        animatorToUpdate.SetBool("isPrimed", isPrimed);
    }
}
