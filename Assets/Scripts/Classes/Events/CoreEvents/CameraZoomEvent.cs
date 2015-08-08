using UnityEngine;
using System.Collections;

public class CameraZoomEvent : CustomEventObject {
    public Camera cameraToZoom = null;
    public float endZoom = 5f;
    public float delay = 0f;
    public float duration = 1f;
    public GoEaseType easingType = GoEaseType.Linear;
    private GoTween zoomTween = null;
    
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
        FireStartEvents();
        ExecuteLogic();
    }
    
    public override void ExecuteLogic() {
        Zoom();
        FireExecuteEvents();
    }
    
    public void Zoom() {
        // Debug.Log("Executing zoom");
        if(zoomTween != null) {
            zoomTween.destroy();
            zoomTween = null; // because I'm cautious like that
        }
        
        GoTweenConfig zoomTweenConfig = new GoTweenConfig()
        .floatProp("orthographicSize", endZoom)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
            FireFinishEvents();
        });
        zoomTween = Go.to(cameraToZoom,
                          duration,
                          zoomTweenConfig);
    }
}