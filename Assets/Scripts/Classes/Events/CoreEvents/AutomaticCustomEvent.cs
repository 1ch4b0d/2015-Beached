using UnityEngine;
using System.Collections;

/// <summary>
/// This is used to execute events immediately.
/// For example on level load you want certain events to trigger. This will
/// handle that for you.
/// </summary>
public class AutomaticCustomEvent : CustomEventObject {
    // // Use this for initialization
    // protected override void Awake() {
    //     base.Awake();
    // }
    
    // Use this for initialization
    protected override void Start() {
        FireStartEvents();
        FireExecuteEvents();
        FireFinishEvents();
    }
    
    // // Update is called once per frame
    // protected override void Update() {
    //     base.Update();
    // }
    
    // // Use this for initialization
    // protected override void Initialize() {
    //     base.Initialize();
    // }
}
