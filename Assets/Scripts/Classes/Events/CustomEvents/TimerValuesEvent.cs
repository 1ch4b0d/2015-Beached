using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimerValuesEvent : CustomEventObject {
    public Timer timerToModify = null;
    public bool begin = false;
    public bool stop = false;
    public bool pause = false;
    
    // Use this for initialization
    protected override void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected override void Start() {
    }
    
    // Update is called once per frame
    protected override void Update() {
    }
    
    protected override void Initialize() {
        if(timerToModify == null) {
            Debug.LogError(this.gameObject.name + " needs its 'timerToModify' reference to be set in the 'TimerValuesEvent' Script");
        }
    }
    
    public override void Execute() {
        FireStartEvents();
        ExecuteLogic();
        FireFinishEvents();
    }
    
    public override void ExecuteLogic() {
        FireExecuteEvents();
        SetTimerValues();
    }
    
    private void SetTimerValues() {
        if(begin) {
            timerToModify.Begin();
        }
        
        if(stop) {
            timerToModify.Stop();
        }
        
        if(pause) {
            timerToModify.Pause();
        }
        else {
            timerToModify.Unpause();
        }
    }
}
