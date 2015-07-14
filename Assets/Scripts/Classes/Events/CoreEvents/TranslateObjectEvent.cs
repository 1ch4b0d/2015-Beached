using UnityEngine;
using System.Collections;

public class TranslateObjectEvent : CustomEventsManager {
    public GoTween translationTween = null;
    public GameObject objectToTranslate = null;
    public float duration = 1f;
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public override void Execute() {
        // if(translationTween != null) {
        //     translationTween.destroy();
        //     translationTween = null; // because I'm cautious like that
        // }
        
        // translationTween
        // currentTween = Go.to(cameraToZoom,
        //                      duration,
        //                      new GoTweenConfig().floatProp("orthographicSize", endZoomSize).setEaseType(startZoomEaseType)
        // .onComplete(complete => {
        //     FireOnZoomStartFinish();
        // }));
    }
}
