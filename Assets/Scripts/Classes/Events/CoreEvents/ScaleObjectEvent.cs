using UnityEngine;
using System.Collections;

public class ScaleObjectEvent : CustomEventObject {
    // public GoTween translationTween = null;
    public GameObject objectToScale = null;
    public Vector3 scale = new Vector3(1, 1, 1);
    public float delay = 0f;
    public float duration = 1f;
    public bool addValuesAsOffset = false; // if true it scales to the objectToScale.transform.localScale + scale, if false it just scales to the scale value
    public bool excludeXComponent = false;
    public bool excludeYComponent = false;
    public bool excludeZComponent = false;
    public GoEaseType easingType = GoEaseType.Linear;
    
    // Use this for initialization
    // protected override void Start() {
    //     base.Start();
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    //     base.Update();
    // }
    
    // public override void Execute() {
    //     base.Execute();
    // }
    
    protected override void Initialize() {
        base.Initialize();
        if(objectToScale == null) {
            this.gameObject.LogComponentError("objectToScale", this.GetType());
        }
    }
    
    public override void Execute() {
        FireStartEvents();
        ExecuteLogic();
    }
    
    public override void ExecuteLogic() {
        GoTweenConfig tweenConfig = new GoTweenConfig();
        Vector3 finalScale = Vector3.zero;
        
        //-----------------------------------------
        // Translate Tween
        //-----------------------------------------
        if(addValuesAsOffset) {
            finalScale = new Vector3((excludeXComponent) ? objectToScale.transform.localScale.x : objectToScale.transform.localScale.x + scale.x,
                                     (excludeYComponent) ? objectToScale.transform.localScale.y : objectToScale.transform.localScale.y + scale.y,
                                     (excludeZComponent) ? objectToScale.transform.localScale.z : objectToScale.transform.localScale.z + scale.z);
        }
        else {
            finalScale = new Vector3((excludeXComponent) ? objectToScale.transform.localScale.x : scale.x,
                                     (excludeYComponent) ? objectToScale.transform.localScale.y : scale.y,
                                     (excludeZComponent) ? objectToScale.transform.localScale.z : scale.z);
        }
        tweenConfig.scale(finalScale);
        
        
        tweenConfig.setDelay(delay)
        .setEaseType(easingType)
        .onComplete(complete => {
            FireFinishEvents();
        });
        
        objectToScale.AddGoTween(Go.to(objectToScale.transform,
                                       duration,
                                       tweenConfig));
        // translationTween = Go.to(objectToScale.transform,
        //                          duration,
        //                          tweenConfig);
    }
}
