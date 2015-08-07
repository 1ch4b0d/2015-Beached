using UnityEngine;
using System.Collections;

public class BasePlayer : Character {
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
    
    protected override void PerformLogic() {
        base.PerformLogic();
    }
    
    public override void Pause() {
        base.Pause();
    }
    
    public override void Unpause() {
        base.Unpause();
    }
    
    public override bool IsPaused() {
        return base.IsPaused();
    }
}
