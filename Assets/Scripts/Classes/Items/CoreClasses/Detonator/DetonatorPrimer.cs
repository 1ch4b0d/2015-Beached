using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DetonatorPrimer : Item {
    public List<GameObject> objectsPrimingDetonator = null;
    public int numberOfObjectsNeededToPrime = 1;
    public List<CustomEventsManager> onPrimedEvents = null;
    public List<CustomEventsManager> onUnprimedEvents = null;
    
    // Use this for initialization
    protected override void Start() {
        Initialize();
    }
    
    // Update is called once per frame
    protected override void Update() {
        UpdateAnimator(animator);
    }
    
    protected override void Initialize() {
        base.Initialize();
        objectsPrimingDetonator = new List<GameObject>();
        if(onPrimedEvents == null) {
            onPrimedEvents = new List<CustomEventsManager>();
        };
        if(onUnprimedEvents == null) {
            onUnprimedEvents = new List<CustomEventsManager>();
        };
    }
    
    public void AddPrimerComponent(GameObject primerComponent) {
        if(!objectsPrimingDetonator.Contains(primerComponent)) {
            objectsPrimingDetonator.Add(primerComponent);
        }
        if(IsPrimed()) {
            FireOnPrimedEvents();
        }
    }
    
    public void RemovePrimerComponent(GameObject primerComponent) {
        objectsPrimingDetonator.Remove(primerComponent);
        if(!IsPrimed()) {
            FireOnUnprimedEvents();
        }
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
    
    protected void FireOnPrimedEvents() {
        foreach(CustomEventsManager customEventsManager in onPrimedEvents) {
            customEventsManager.Execute();
        }
    }
    
    protected void FireOnUnprimedEvents() {
        foreach(CustomEventsManager customEventsManager in onUnprimedEvents) {
            customEventsManager.Execute();
        }
    }
}
