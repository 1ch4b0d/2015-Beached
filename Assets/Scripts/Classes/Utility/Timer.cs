using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
    public bool loop = true;
    public bool isPaused = false;
    public float timer = 0f;
    public float timerMax = 1f;
    public int iterations = 0;
    public CustomEventsManager onTimerFinish = null;
    
    // Use this for initialization
    protected virtual void Initialize() {
        Initialize();
    }
    
    // Use this for initialization
    protected virtual void Start() {
    }
    
    // Update is called once per frame
    protected virtual void Update() {
        if(!isPaused) {
            PerformLogic();
        }
    }
    
    public virtual void Begin() {
        timer = 0f;
        isPaused = false;
    }
    
    public virtual void Stop() {
        Pause();
    }
    
    public virtual void Pause() {
        isPaused = true;
    }
    
    public virtual void Unpause() {
        isPaused = false;
    }
    
    protected virtual void PerformLogic() {
        if(timer <= timerMax
            && (loop || iterations < 1)) {
            timer += Time.deltaTime;
        }
        if(timer > timerMax) {
            if(iterations < 1
                || loop) {
                timer = 0;
                iterations++;
                PerformTimerFinishedLogic();
            }
        }
    }
    
    protected virtual void PerformTimerFinishedLogic() {
        if(onTimerFinish != null) {
            onTimerFinish.Execute();
        }
    }
}
