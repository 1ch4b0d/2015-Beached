using UnityEngine;
using System.Collections;

public class TranslateObjectEvent : CustomEventObject {
    public GoTween translationTween = null;
    public GameObject objectToTranslate = null;
    public Transform targetPosition = null;
    public float delay = 0f;
    public float duration = 1f;
    public bool useLocal = false;
    public GoEaseType easingType = GoEaseType.Linear;
    
    // Use this for initialization
    // protected override void Start() {
    // base.Start();
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    // base.Update();
    // }
    
    // public override void Execute() {
    // base.Execute();
    // }
    
    protected override void Initialize() {
        base.Initialize();
        if(objectToTranslate == null) {
            Debug.LogError("The 'objectToTranslate' reference needs to be set in the 'TranslateObjectEvent' Script on " + this.gameObject.name);
        }
    }
    
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
        tweenConfig.setDelay(delay)
        .setEaseType(easingType)
        .onComplete(complete => {
            FireFinishEvents();
        });
        
        translationTween = Go.to(objectToTranslate.transform,
                                 duration,
                                 tweenConfig);
    }
}
