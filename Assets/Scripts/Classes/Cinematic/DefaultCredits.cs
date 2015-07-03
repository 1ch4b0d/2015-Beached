using UnityEngine;
using System.Collections;

public class DefaultCredits : Cinematic {
    // Use this for initialization
    public override void Start() {
    }
    
    // Update is called once per frame
    public override void Update() {
    }
    
    public override void TriggerStart() {
        Debug.Log("Starting DefaultCredits");
        InitializeWaves(CreateWave("First Credits", () => {
            Debug.Log("First Credits");
            Completed();
        }));
    }
}
