using UnityEngine;
using System.Collections;

public class DetonatorPanFromTriggerToMarker : PanFromTriggerToMarker {
    public Detonator detonator = null;
    
    // // Use this for initialization
    // protected override void Awake() {
    //     base.Awake();
    // }
    
    // // Use this for initialization
    // protected override void Start() {
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    // }
    
    protected override void Initialize() {
        base.Initialize();
        if(detonator == null) {
            Debug.LogError("The 'detonator' variable for the 'DetonatorPanFromTriggerToMarker' component of the '" + this.gameObject.name + "' is null. Please fix this and try again.");
        }
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        // base.Entered(gameObjectEntering);
        FireEnterEvents();
        
        if(detonator.IsPrimed()) {
            PanIn();
        }
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        // base.Exited(gameObjectExiting);
        FireExitEvents();
        
        PanOut();
    }
}