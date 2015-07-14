using UnityEngine;
using System.Collections;

public class TranslateObjectEvent : CustomEventObject {
    public GoTween translationTween = null;
    public GameObject objectToTranslate = null;
    public Transform targetPosition = null;
    public float duration = 1f;
    public bool useLocal = false;
    public GoEaseType easingType = GoEaseType.Linear;
    
    // Use this for initialization
    protected override void Start() {
    }
    
    // Update is called once per frame
    protected override void Update() {
    }
    
    // public override void Execute() {
    // }
    
    public override void ExecuteLogic() {
        if(translationTween != null) {
            translationTween.destroy();
            translationTween = null; // because I'm cautious like that
        }
        
        //-----------------------------------------
        // Translate Tween
        //-----------------------------------------
        GoTweenConfig tweenConfig = null;
        if(useLocal) {
            tweenConfig = new GoTweenConfig().localPosition(targetPosition.position);
        }
        else {
            tweenConfig = new GoTweenConfig().position(targetPosition.position);
        }
        tweenConfig.setEaseType(easingType)
        .onComplete(complete => {
            FireFinishEvents();
        });
        
        translationTween = Go.to(objectToTranslate.transform,
                                 duration,
                                 tweenConfig);
    }
}
