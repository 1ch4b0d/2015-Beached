using UnityEngine;
using System.Collections;

public class WhaleExplosion : Cinematic {

    // // Use this for initialization
    // protected override void Awake() {
    // }
    
    // // Use this for initialization
    // protected override void Start() {
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    // }
    
    // protected override void Initialize() {
    // }
    
    public override void TriggerStart() {
        PerformWaveAndWait(CreateWave("Explosions", () => {
        }));
        PerformWaveAndWait(CreateWave("Programmer Credits", () => {
        }));
        PerformWavesAndWait(CreateWave("Lead Game Designer Credits", () => {
        }));
        PerformWavesAndWait(CreateWave("Game Designer Credits", () => {
        }));
        
        StartWaves();
    }
}