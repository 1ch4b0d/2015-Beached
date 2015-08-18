using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabGenerator : MonoBehaviour {
    public GameObject prefabToGenerate = null;
    public Transform generationTransform = null;
    public bool timerIsStochastic = false;
    public float timer = 0f;
    public float timerMax = 1f;
    public Vector2 timerBounds = new Vector2(0, 1);
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
            generationTransform = this.gameObject.transform;
            // Debug.LogError(this.gameObject.name + " needs its 'generationTransform' reference to be set in the 'PrefabGenerator' Script");
        }
        
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
            if(timerIsStochastic) {
                timerMax = Random.Range(timerBounds.x, timerBounds.y);
            }
            timer = 0f;
            
            if(iterations < totalIterations) {
                PerformGeneration();
                iterations++;
            }
        }
    }
    
    public virtual GameObject PerformGeneration() {
        GameObject objectToGenerate = GenerateGameObject();
        objectToGenerate.transform.parent = this.gameObject.transform;
        objectToGenerate.transform.position = generationTransform.position;
        
        foreach(PrefabGeneratorRule prefabGeneratorRule in prefabGeneratorRules) {
            prefabGeneratorRule.PerformGenerationRule(objectToGenerate);
        }
        
        return objectToGenerate;
    }
    
    public virtual GameObject GenerateGameObject() {
        return (GameObject)GameObject.Instantiate(prefabToGenerate);
    }
}