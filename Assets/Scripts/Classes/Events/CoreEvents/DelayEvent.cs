using UnityEngine;
using System.Collections;

public class DelayEvent : CustomEventObject {
    public float duration = 1f;
    
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
    }
    
    public override void Execute() {
        FireStartEvents();
        ExecuteLogic();
    }
    
    public override void ExecuteLogic() {
        GoTweenConfig tweenConfig = new GoTweenConfig();
        
        tweenConfig.onComplete(complete => {
            FireFinishEvents();
        });
        
        Go.to(this.gameObject, duration, tweenConfig);
    }
}
