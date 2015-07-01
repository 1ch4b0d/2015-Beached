using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DetonatorPrimer : Item {
    public List<GameObject> objectsPrimingDetonator = null;
    public int numberOfObjectsNeededToPrime = 1;
    
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
        objectsPrimingDetonator = new List<GameObject>();
    }
    
    public void AddPrimerComponent(GameObject primerComponent) {
        if(!objectsPrimingDetonator.Contains(primerComponent)) {
            objectsPrimingDetonator.Add(primerComponent);
        }
    }
    
    public void RemovePrimerComponent(GameObject primerComponent) {
        objectsPrimingDetonator.Remove(primerComponent);
    }
    
    public bool IsPrimed() {
        if(objectsPrimingDetonator.Count >= numberOfObjectsNeededToPrime) {
            return true;
        }
        else {
            return false;
        }
    }
    
    public override void UpdateAnimator(Animator animatorToUpdate) {
        animatorToUpdate.SetBool("IsPrimed", IsPrimed());
    }
}
