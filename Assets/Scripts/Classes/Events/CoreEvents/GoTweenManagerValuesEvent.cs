using UnityEngine;
using System.Collections;

public class GoTweenManagerValuesEvent : CustomEventObject {
    public GameObject objectToModify = null;
    private GoTweenManager goTweenManager = null;
    public bool completeTweens = false;
    public bool destroyTweens = false;
    
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
        if(objectToModify == null) {
            this.gameObject.LogComponentError("objectToModify", this.GetType());
        }
        goTweenManager = objectToModify.GetGoTweenManager();
    }
    
    public override void Execute() {
        FireStartEvents();
        ExecuteLogic();
    }
    
    public override void ExecuteLogic() {
        SetGoTweenManagerValues();
    }
    
    public virtual void SetGoTweenManagerValues() {
        if(completeTweens) {
            goTweenManager.Complete();
        }
        if(destroyTweens) {
            goTweenManager.Destroy();
        }
    }
}
