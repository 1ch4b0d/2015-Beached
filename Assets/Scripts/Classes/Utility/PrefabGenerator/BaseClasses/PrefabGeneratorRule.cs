using UnityEngine;
using System.Collections;

public class PrefabGeneratorRule : MonoBehaviour {
    // Use this for initialization
    protected virtual void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected virtual void Start() {
    }
    
    // Update is called once per frame
    protected virtual void Update() {
    }
    
    protected virtual void Initialize() {
    }
    
    public virtual void PerformGenerationRule(GameObject gameObjectToPerformRuleOn) {
    }
}
