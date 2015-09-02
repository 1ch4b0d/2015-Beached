using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TypewriterEffectValuesEvent : CustomEventObject {
    public bool scriptIsEnabled = true;
    public TypewriterEffect typewriterEffect = null;
    public int charactersPerSecond = 20;
    public bool resetToBeginning = false;
    public List<CustomEventsManager> onTypewriterFinish = null;
    
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
        if(typewriterEffect == null) {
            this.gameObject.LogComponentError("typewriterEffect", this.GetType());
        }
        if(onTypewriterFinish == null) {
            onTypewriterFinish = new List<CustomEventsManager>();
        }
        foreach(CustomEventsManager customEventsManager in onTypewriterFinish) {
            if(customEventsManager == null) {
                Debug.LogError(this.transform.GetFullPath() + " has a NULL CustomEventsManager declared for the onTypewriterFinish.");
            }
        }
    }
    
    public override void ExecuteLogic() {
        SetUILabelValues();
    }
    
    public void SetUILabelValues() {
        typewriterEffect.enabled = scriptIsEnabled;
        typewriterEffect.charsPerSecond = charactersPerSecond;
        if(resetToBeginning) {
            typewriterEffect.ResetToBeginning();
        }
        if(onTypewriterFinish != null) {
            foreach(CustomEventsManager customEventsManager in onTypewriterFinish) {
                typewriterEffect.OnTypewriterFinish(customEventsManager);
            }
        }
    }
}
