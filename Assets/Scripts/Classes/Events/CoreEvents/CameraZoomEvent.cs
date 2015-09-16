using UnityEngine;
using System.Collections;

public class CameraZoomEvent : CustomEventObject {
    public Camera cameraToZoom = null;
    public float endZoom = 5f;
    public float delay = 0f;
    public float duration = 1f;
    public GoEaseType easingType = GoEaseType.Linear;
    
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
            this.gameObject.LogComponentError("cameraToZoom", this.GetType());
        }
    }
    
    public override void Execute() {
        FireStartEvents();
        ExecuteLogic();
    }
    
    public override void ExecuteLogic() {
        Zoom();
        FireExecuteEvents();
    }
    
    public void Zoom() {
        // Debug.Log("Executing zoom");
        cameraToZoom.gameObject.DestroyGoTweens();
        
        GoTweenConfig zoomTweenConfig = new GoTweenConfig()
        .floatProp("orthographicSize", endZoom)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
            FireFinishEvents();
        });
        cameraToZoom.gameObject.AddGoTween(Go.to(cameraToZoom,
                                                 duration,
                                                 zoomTweenConfig));
    }
}