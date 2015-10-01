using UnityEngine;
using System.Collections;

public class RuntimePlatformConditionalEvent : ConditionalEventObject {
    public RuntimePlatform runtimePlatform = RuntimePlatform.OSXPlayer;
    
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
    
    protected override void Initialize() {
        base.Initialize();
    }
    
    public override void ExecuteLogic() {
    }
}