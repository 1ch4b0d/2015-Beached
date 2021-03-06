﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIWidgetValuesEvent : CustomEventObject {
    public UIWidget uiWidgetToModify = null;
    public float duration = 1f;
    public Color finalColor;
    
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
        // .floatProp("alpha", 0f)
        .colorProp("color", finalColor)
        .onComplete(complete => {
            FireFinishEvents();
        });
        
        uiWidgetToModify.gameObject.AddGoTween(Go.to(uiWidgetToModify,
                                                     duration,
                                                     uiWidgetTweenConfig));
    }
}
