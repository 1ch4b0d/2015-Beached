using UnityEngine;
using System.Collections;

public class DetonatorCameraZoomTrigger : CameraZoomTrigger {
    public Detonator detonator = null;
    
    // Use this for initialization
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
            detonator = Utility.GetFirstParentOfType<Detonator>(this.gameObject);
            if(detonator == null) {
                Debug.LogError("The 'detonator' variable for the 'DetonatorPanFromTriggerToMarker' component of the '" + this.gameObject.name + "' is null. Please fix this and try again.");
            }
        }
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        FireEnterEvents();
        if(detonator.IsPrimed()) {
            StartZoom();
        }
    }
    
    // public override void Stay(GameObject gameObjectStaying) {
    //     FireStayEvents();
    // }
    
    public override void Exited(GameObject gameObjectExiting) {
        FireExitEvents();
        EndZoom();
    }
}