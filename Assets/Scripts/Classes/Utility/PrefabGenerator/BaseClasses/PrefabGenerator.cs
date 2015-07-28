using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabGenerator : MonoBehaviour {
    public GameObject prefabToGenerate = null;
    public GameObject prefabParent = null;
    public Transform generationTransform = null;
    public float timer = 0f;
    public float timerMax = 1f;
    public int iterations = 0;
    public int totalIterations = int.MaxValue;
    public GameObject prefabGeneratorRulesGameObject = null;
    public List<PrefabGeneratorRule> prefabGeneratorRules = null;
    
    // Use this for initialization
    protected virtual void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected virtual void Start() {
    }
    
    // Update is called once per frame
    protected virtual void Update() {
        PerformLogic();
    }
    
    protected virtual void Initialize() {
        if(prefabToGenerate == null) {
            Debug.LogError(this.gameObject.name + " needs its 'prefabToGenerate' reference to be set in the 'PrefabGenerator' Script");
        }
        
        if(generationTransform == null) {
            Debug.LogError(this.gameObject.name + " needs its 'positionToGenerateAt' reference to be set in the 'PrefabGenerator' Script");
        }
        
        // Will not always be initialized, but has to be initialized on creation
        // if(prefabGeneratorRules == null) {
        //     prefabGeneratorRules = new List<PrefabGeneratorRule>();
        // }
        if(prefabGeneratorRulesGameObject == null) {
            prefabGeneratorRulesGameObject = this.gameObject;
        }
        foreach(PrefabGeneratorRule prefabGeneratorRule in prefabGeneratorRulesGameObject.GetComponentsInChildren<PrefabGeneratorRule>()) {
            prefabGeneratorRules.Add(prefabGeneratorRule);
        }
    }
    
    protected virtual void PerformLogic() {
        timer += Time.deltaTime;
        if(timer > timerMax) {
            timer = 0f;
            
            GameObject objectToGenerate = (GameObject)GameObject.Instantiate(prefabToGenerate);
            objectToGenerate.transform.parent = this.gameObject.transform;
            objectToGenerate.transform.position = generationTransform.position;
            
            foreach(PrefabGeneratorRule prefabGeneratorRule in prefabGeneratorRules) {
                prefabGeneratorRule.PerformGenerationRule(objectToGenerate);
            }
        }
    }
}