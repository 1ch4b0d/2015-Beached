using UnityEngine;
using System.Collections;

public class CameraZoomEvent : CustomEventsManager {
    public Camera cameraToZoom = null;
    
    // protected override void Awake() {
    //     base.Awake();
    // }
    
    // // Use this for initialization
    // protected override void Start() {
    //     base.Start();
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    //     base.Update();
    // }
    protected override void Initialize() {
        base.Initialize();
        //---------------------
        if(cameraToZoom == null) {
            Debug.LogError("The 'cameraToZoom' property is null for " + this.gameObject.name + ". Please assign it then retry again.");
        }
    }
    
    public override void Execute() {
        Zoom();
    }
    
    public void Zoom() {
        Debug.Log("lol zooming");
    }
}