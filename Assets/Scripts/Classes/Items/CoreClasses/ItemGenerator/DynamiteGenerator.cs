using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamiteGenerator : MonoBehaviour {
    public PrefabGenerator prefabGenerator = null;
    public List<DetonatorPrimer> detonatorPrimers = null;
    public List<Dynamite> dynamiteOut = null;
    
    // Use this for initialization
    protected virtual void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected virtual void Start() {
    }
    
    // Update is called once per frame
    protected virtual void Update() {
        PruneInvalidDynamite();
        PerformGenerationCheck();
    }
    
    protected virtual void Initialize() {
        if(prefabGenerator == null) {
            this.gameObject.LogComponentError("prefabGenerator", this.GetType());
        }
        if(detonatorPrimers == null) {
            // this.gameObject.LogComponentError("targetPosition", this.GetType());
            detonatorPrimers = new List<DetonatorPrimer>();
        }
        if(detonatorPrimers.Count == 0) {
            Debug.LogWarning(this.gameObject.transform.GetFullPath() + " has " + detonatorPrimers.Count + " " + this.GetType() + ". Was this intentional?");
        }
        if(dynamiteOut == null) {
            // this.gameObject.LogComponentError("targetPosition", this.GetType());
            dynamiteOut = new List<Dynamite>();
        }
    }
    
    // Removes the dynamite that has become null or missing. The can occur
    // in part because of the player's interaction. They destroy it in stupid
    // ways. ¯\_(ツ)_/¯
    public void PruneInvalidDynamite() {
        List<Dynamite> dynamiteToRemove = new List<Dynamite>();
        foreach(Dynamite dynamite in dynamiteOut) {
            if(dynamite == null) {
                dynamiteToRemove.Add(dynamite);
            }
        }
        
        foreach(Dynamite dynamite in dynamiteToRemove) {
            dynamiteOut.Remove(dynamite);
        }
    }
    public void PerformGenerationCheck() {
        int detonatorPrimersPrimed = 0;
        foreach(DetonatorPrimer detonatorPrimer in detonatorPrimers) {
            if(detonatorPrimer.IsPrimed()) {
                detonatorPrimersPrimed++;
            }
        }
        
        // Only if there is no dynamite or the dynamite out is all used up for priming
        if((dynamiteOut.Count == detonatorPrimersPrimed && dynamiteOut.Count < detonatorPrimers.Count)
            || (dynamiteOut.Count == 0 && detonatorPrimersPrimed == 0)) {
            dynamiteOut.Add(prefabGenerator.PerformGeneration().GetComponent<Dynamite>());
        }
    }
}
