using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIWidgetValuesEvent : CustomEventObject {
    public UIWidget uiWidgetToModify = null;
    public float duration = 1f;
    public Color finalColor;
    private GoTween uiWidgetTween = null;
    
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
        if(uiWidgetTween != null) {
            uiWidgetTween.destroy();
            uiWidgetTween = null; // because I'm cautious like that
        }
        
        GoTweenConfig uiWidgetTweenConfig = new GoTweenConfig()
        .setEaseType(GoEaseType.Linear)
        // .floatProp("alpha", 0f)
        .colorProp("color", finalColor)
        .onComplete(complete => {
            FireFinishEvents();
        });
        uiWidgetTween = Go.to(uiWidgetToModify,
                              duration,
                              uiWidgetTweenConfig);
    }
}
