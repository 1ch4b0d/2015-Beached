using UnityEngine;
using System.Collections;
using InControl;

public class TouchButtonControlValuesEvent : CustomEventObject {
    public TouchButtonControl touchButtonControl = null;
    public float delay = float.Epsilon;
    public float duration = float.Epsilon;
    public GoEaseType easingType = GoEaseType.Linear;
    public Vector2 offset = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
    public Vector2 size = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
    public Color endBusyColor = Color.white;
    public Color endIdleColor = Color.white;
    
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
    
    protected override void LateUpdate() {
        base.Update();
    }
    
    protected override void Initialize() {
        base.Initialize();
        if(touchButtonControl == null) {
            this.gameObject.LogComponentError("touchButtonControl", this.GetType());
        }
    }
    
    public override void ExecuteLogic() {
        SetTouchButtonControlValues();
    }
    
    public void SetTouchButtonControlValues() {
        GoTweenConfig tweenConfig = new GoTweenConfig();
        
        tweenConfig.setDelay(delay)
        .setEaseType(easingType)
        // .floatProp("orthographicSize", endZoom)
        // .vector2Prop( string propertyName, Vector2 endValue, bool isRelative = false )
        .colorProp("IdleColor", endIdleColor)
        .colorProp("BusyColor", endBusyColor)
        .onComplete(complete => {
            FireFinishEvents();
        });
        
        // TODO: configure tween options to point towards the TouchButton
        //       specifically to address the position and options sections
        
        // Sprites Section
        // Point to the button reference
        touchButtonControl.gameObject.AddGoTween(Go.to(touchButtonControl.button,
                                                       duration,
                                                       tweenConfig));
    }
}
