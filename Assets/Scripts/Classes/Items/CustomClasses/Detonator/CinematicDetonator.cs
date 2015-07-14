using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CinematicDetonator : Detonator {
    public bool finishedCinematic = false;
    
    // // Use this for initialization
    // protected override void Start() {
    //     Initialize();
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    //     UpdateAnimator(animator);
    // }
    
    // protected override void Initialize() {
    //     base.Initialize();
    // }
    
    public override void Detonate() {
        if(finishedCinematic) {
            if(IsPrimed()
                && !HasBeenDetonated()) {
                hasBeenDetonated = true;
                FireOnDetonateEvents();
            }
        }
    }
}
