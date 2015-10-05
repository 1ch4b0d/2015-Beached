using UnityEngine;
using System.Collections;
using InControl;

public class TouchStickControlValuesEvent : CustomEventObject {
    public TouchStickControl touchStickControl = null;
    public float delay = float.Epsilon;
    public float duration = float.Epsilon;
    public GoEaseType easingType = GoEaseType.Linear;
    public Vector2 offset = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
    public Vector2 size = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
    public Color endRingBusyColor = Color.white;
    public Color endRingIdleColor = Color.white;
    public Color endKnobBusyColor = Color.white;
    public Color endKnobIdleColor = Color.white;
    
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
        if(touchStickControl == null) {
            this.gameObject.LogComponentError("touchStickControl", this.GetType());
        }
    }
    
    public override void ExecuteLogic() {
        SetTouchStickControlValues();
    }
    
    public void SetTouchStickControlValues() {
        SetRingValues();
        SetKnobValues();
    }
    
    public void SetRingValues() {
        GoTweenConfig tweenConfig = new GoTweenConfig();
        
        tweenConfig.setDelay(delay)
        .setEaseType(easingType)
        .colorProp("IdleColor", endRingIdleColor)
        .colorProp("BusyColor", endRingBusyColor)
        .onComplete(complete => {
            FireFinishEvents();
        });
        // TODO: configure tween options to point towards the TouchButton
        //       specifically to address the position and options sections
        
        // Sprites Section
        touchStickControl.gameObject.AddGoTween(Go.to(touchStickControl.ring,
                                                      duration,
                                                      tweenConfig));
    }
    
    public void SetKnobValues() {
        GoTweenConfig tweenConfig = new GoTweenConfig();
        
        tweenConfig.setDelay(delay)
        .setEaseType(easingType)
        .colorProp("IdleColor", endKnobIdleColor)
        .colorProp("BusyColor", endKnobBusyColor)
        .onComplete(complete => {
            FireFinishEvents();
        });
        touchStickControl.gameObject.AddGoTween(Go.to(touchStickControl.knob,
                                                      duration,
                                                      tweenConfig));
    }
}
