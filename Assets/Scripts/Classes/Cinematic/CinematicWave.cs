using UnityEngine;
using System.Collections;

public class CinematicWave : MonoBehaviour {
    public bool hasTriggeredLogic = false;
    public bool hasCompletedLogic = false;
    
    public delegate void WaveLogic();
    public WaveLogic waveLogic = null;
    
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public void ConfigureLogic(WaveLogic newWaveLogic) {
        waveLogic = new WaveLogic(newWaveLogic);
    }
    
    public bool HasStarted() {
        return hasTriggeredLogic;
    }
    
    public bool HasFinished() {
        if(hasTriggeredLogic
            && hasCompletedLogic) {
            return true;
        }
        else {
            return false;
        }
    }
    
    public void PerformLogic() {
        if(!CinematicManager.Instance.IsPaused()) {
            if(waveLogic != null) {
                if(!HasStarted()) {
                    // Debug.Log("Starting wave.");
                    StartWave();
                }
                // Debug.Log("Performing Logic.");
                waveLogic();
            }
            else {
                // Debug.Log("Wave logic does not exist!!!");
            }
        }
    }
    
    public void StartWave() {
        hasTriggeredLogic = true;
    }
    
    public void Completed() {
        // Debug.Log("WaveState Completed");
        hasCompletedLogic = true;
    }
}
