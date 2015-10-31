using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabGeneratorValuesEvent : CustomEventObject {
    public bool? isGenerating = null;
    public List<PrefabGenerator> prefabGeneratorsToModify = null;
    
    // Use this for initialization
    // protected override void Start() {
    // base.Start();
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    // base.Update();
    // }
    
    // public override void Execute() {
    // base.Execute();
    // }
    
    protected override void Initialize() {
        base.Initialize();
        if(prefabGeneratorsToModify == null
            || prefabGeneratorsToModify.Count == 0) {
            this.gameObject.LogComponentError("objectToTranslate", this.GetType());
        }
    }
    
    public override void ExecuteLogic() {
        foreach(PrefabGenerator prefabGenerator in prefabGeneratorsToModify) {
            if(isGenerating != null) {
                prefabGenerator.isGenerating = (bool)isGenerating;
            }
        }
    }
}
