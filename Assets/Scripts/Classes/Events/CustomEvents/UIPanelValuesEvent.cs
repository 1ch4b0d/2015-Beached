using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIPanelValuesEvent : CustomEventObject {
    public UIPanel uiPanelToModify = null;
    public float duration = 1f;
    public float endAlpha = 1f;
    
    // Use this for initialization
    protected override void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected override void Start() {
    }
    
    // Update is called once per frame
    protected override void Update() {
    }
    
    protected override void Initialize() {
    }
    
    public override void Execute() {
        FireStartEvents();
        ExecuteLogic();
    }
    
    public override void ExecuteLogic() {
        SetUIWidgetValues();
        FireExecuteEvents();
    }
    
    private void SetUIWidgetValues() {
        GoTweenConfig uiWidgetTweenConfig = new GoTweenConfig()
        .setEaseType(GoEaseType.Linear)
        .floatProp("alpha", endAlpha)
        .onComplete(complete => {
            FireFinishEvents();
        });
        
        uiPanelToModify.gameObject.AddGoTween(Go.to(uiPanelToModify,
                                                    duration,
                                                    uiWidgetTweenConfig));
    }
}
