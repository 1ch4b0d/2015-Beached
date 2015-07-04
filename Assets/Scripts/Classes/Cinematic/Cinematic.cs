using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cinematic : MonoBehaviour {
    public LinkedList<GameObject> cinematicWaveQueue = new LinkedList<GameObject>();
    public GameObject currentWaveGameObject = null;
    public float delayTimer = 0f;
    
    // used to delay initialization later than the Start method
    public bool hasInitialized = false;
    public bool hasFinished = false;
    
    public virtual void Awake() {
        Initialize();
    }
    
    public virtual void Start() {
    }
    
    public virtual void Update() {
        PerformDelayTimerLogic();
    }
    
    public virtual void ResetWaveQueue() {
        cinematicWaveQueue.Clear();
    }
    
    public virtual void Finish() {
        hasFinished = true;
    }
    
    public virtual void Initialize() {
        hasInitialized = true;
    }
    
    public virtual void PerformDelayTimerLogic() {
        if(delayTimer > 0) {
            delayTimer -= Time.deltaTime;
        }
    }
    
    public virtual bool HasDelayFinished() {
        if(delayTimer <= 0) {
            return true;
        }
        else {
            return false;
        }
    }
    
    public virtual void Delay(float newTimer) {
        delayTimer = newTimer;
    }
    
    public virtual void Completed() {
        if(currentWaveGameObject != null) {
            CinematicWave currentCinematicWave = currentWaveGameObject.GetComponent<CinematicWave>();
            currentCinematicWave.Completed();
        }
        // Debug.Log("WaveLogic Completed");
    }
    
    public virtual bool HasFinished() {
        return hasFinished;
    }
    
    // Think of this as the main update loop
    public virtual void PerformLogic() {
        // Debug.Log("Current Timer: " + delayTimer);
        if(HasDelayFinished()) {
            PerformLevelFailCheck();
            PerformLevelFinishCheck();
            
            //this assumes that the paused state is already managed by the wave manager
            if(currentWaveGameObject != null) {
                // Debug.Log("performing wave state logic");
                CinematicWave currentCinematicWave = currentWaveGameObject.GetComponent<CinematicWave>();
                currentCinematicWave.PerformLogic();
                
                if(currentCinematicWave.HasFinished()) {
                    if(cinematicWaveQueue.Count > 0) {
                        currentWaveGameObject = (GameObject)DequeueWave();
                    }
                    else {
                        currentWaveGameObject = null;
                    }
                }
            }
        }
    }
    
    public GameObject CreateWave(string gameObjectName, CinematicWave.WaveLogic waveStateLogic) {
        GameObject newWaveStateGameObject = ((new GameObject(gameObjectName)).AddComponent<CinematicWave>()).gameObject;
        newWaveStateGameObject.GetComponent<CinematicWave>().ConfigureLogic(waveStateLogic);
        newWaveStateGameObject.transform.parent = this.gameObject.transform;
        
        return newWaveStateGameObject;
    }
    
    public GameObject CreateDelayWave(float delayTime) {
        return CreateWave("Delay", () => {
            Delay(delayTime);
            Completed();
        });
    }
    
    public void StartWaves() {
        if(cinematicWaveQueue.Count != 0
            && currentWaveGameObject == null) {
            currentWaveGameObject = (GameObject)DequeueWave();
        }
        
        // Adds the final wave that will finish the cinematic
        EnqueueWave(CreateWave("Final Cinematic Wave", () => {
            Completed();
            Finish();
        }));
    }
    
    public void PerformLevelFailCheck() {
    }
    
    public void PerformLevelFinishCheck() {
    }
    
    public void PerformWaveStatesThenReturn(params GameObject[] waves) {
        System.Array.Reverse(waves);
        foreach(GameObject wave in waves) {
            cinematicWaveQueue.AddFirst(wave);
        }
    }
    
    public void PerformDelay(float duration) {
        cinematicWaveQueue.AddLast(CreateDelayWave(duration));
    }
    
    public void PerformWave(GameObject wave) {
        cinematicWaveQueue.AddLast(wave);
    }
    
    public void PerformWaves(params GameObject[] waves) {
        foreach(GameObject wave in waves) {
            cinematicWaveQueue.AddLast(wave);
        }
    }
    
    public void PerformWaveAndWait(GameObject wave) {
        cinematicWaveQueue.AddLast(wave);
        cinematicWaveQueue.AddLast(CreateWave("Wait", delegate() { /* wait */ }));
        
    }
    public void PerformWavesAndWait(params GameObject[] waves) {
        foreach(GameObject wave in waves) {
            cinematicWaveQueue.AddLast(wave);
        }
        cinematicWaveQueue.AddLast(CreateWave("Wait", delegate() { /* wait */ }));
    }
    
    public void EnqueueWave(GameObject wave) {
        cinematicWaveQueue.AddLast(wave);
    }
    
    public void EnqueueWaveAtFront(GameObject wave) {
        cinematicWaveQueue.AddFirst(wave);
    }
    
    public GameObject DequeueWave() {
        GameObject waveStateDequeued = cinematicWaveQueue.First.Value;
        cinematicWaveQueue.RemoveFirst();
        return waveStateDequeued;
    }
    
    public void AddWaveStateToFrontOfQueue(GameObject wave) {
        cinematicWaveQueue.AddFirst(wave);
    }
    
    public void AddWaveStateToEndOfQueue(GameObject wave) {
        cinematicWaveQueue.AddLast(wave);
    }
    
    // This is used for triggering the creation of waves
    public virtual void TriggerStart() {
    }
}