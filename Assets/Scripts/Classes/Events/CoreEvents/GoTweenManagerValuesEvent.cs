using UnityEngine;
using System.Collections;

public class GoTweenManagerValuesEvent : CustomEventObject {
    public GameObject objectToModify = null;
    // private GoTweenManager goTweenManager = null;
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
    }
    
    public override void ExecuteLogic() {
        SetGoTweenManagerValues();
        FireExecuteEvents();
    }
    
    public virtual void SetGoTweenManagerValues() {
        if(completeTweens) {
            objectToModify.CompleteGoTweens();
        }
        if(destroyTweens) {
            objectToModify.DestroyGoTweens();
        }
    }
}
