using UnityEngine;
using System.Collections;

public class UILabelValuesEvent : CustomEventObject {
    public bool scriptIsEnabled = true;
    public UILabel uilabelToModify = null;
    public string text = null;
    
    // Use this for initialization
    protected override void Awake() {
        base.Awake();
    }
    
    // Use this for initialization
    protected override void Start() {
        base.Start();
    }
    
    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }
    
    // Update is called once per frame
    protected override void Initialize() {
        base.Initialize();
        if(uilabelToModify == null) {
            this.gameObject.LogComponentError("uilabelToModify", this.GetType());
        }
    }
    
    public override void ExecuteLogic() {
        SetUILabelValues();
    }
    
    public void SetUILabelValues() {
        uilabelToModify.enabled = true;
        uilabelToModify.text = text;
    }
}
