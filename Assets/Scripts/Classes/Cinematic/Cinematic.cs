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
    
    public void InitializeWaves(params GameObject[] waves) {
        cinematicWaveQueue = new LinkedList<GameObject>();
        foreach(GameObject waveState in waves) {
            EnqueueWave(waveState);
        }
        if(cinematicWaveQueue.Count != 0) {
            currentWaveGameObject = (GameObject)DequeueWave();
        }
        // Adds the final wave that will finish the cinematic
        EnqueueWave(CreateWave("Final Cinematic Wave", () => {
            Finish();
        }));
    }
    
    public void PerformLevelFailCheck() {
    }
    
    public void PerformLevelFinishCheck() {
    }
    
    public void PerformWaveStatesThenReturn(params GameObject[] waveStatesToJumpTo) {
        System.Array.Reverse(waveStatesToJumpTo);
        foreach(GameObject waveStateToJumpTo in waveStatesToJumpTo) {
            cinematicWaveQueue.AddFirst(waveStateToJumpTo);
        }
    }
    
    public void PerformWaves(params GameObject[] waveStatesToJumpTo) {
        foreach(GameObject waveStateToJumpTo in waveStatesToJumpTo) {
            cinematicWaveQueue.AddLast(waveStateToJumpTo);
        }
    }
    
    public void PerformWavesAndWait(params GameObject[] waveStatesToJumpTo) {
        foreach(GameObject waveStateToJumpTo in waveStatesToJumpTo) {
            cinematicWaveQueue.AddLast(waveStateToJumpTo);
        }
        cinematicWaveQueue.AddLast(CreateWave("Wait", delegate() { /* wait */ }));
        
    }
    
    public void EnqueueWave(GameObject waveStateGameObjectToEnqueue) {
        cinematicWaveQueue.AddLast(waveStateGameObjectToEnqueue);
    }
    
    public void EnqueueWaveAtFront(GameObject waveStateGameObjectToEnqueue) {
        cinematicWaveQueue.AddFirst(waveStateGameObjectToEnqueue);
    }
    
    public GameObject DequeueWave() {
        GameObject waveStateDequeued = cinematicWaveQueue.First.Value;
        cinematicWaveQueue.RemoveFirst();
        return waveStateDequeued;
    }
    
    public void AddWaveStateToFrontOfQueue(GameObject waveStateGameObjectToAdd) {
        cinematicWaveQueue.AddFirst(waveStateGameObjectToAdd);
    }
    
    public void AddWaveStateToEndOfQueue(GameObject waveStateGameObjectToAdd) {
        cinematicWaveQueue.AddLast(waveStateGameObjectToAdd);
    }
    
    // This is used for triggering the creation of waves
    public virtual void TriggerStart() {
    }
}