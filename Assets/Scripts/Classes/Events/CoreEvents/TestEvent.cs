using UnityEngine;
using System.Collections;

public class TestEvent : CustomEventObject {
    // protected override void Awake() {
    //     base.Awake();
    // }
    
    // // Use this for initialization
    // protected override void Start() {
    //     base.Start();
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    //     base.Update();
    // }
    
    protected override void Initialize() {
        base.Initialize();
    }
    
    public override void Execute() {
        Debug.Log("Testing!");
    }
}