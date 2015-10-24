using UnityEngine;
using System.Collections;

public class AudioSourceManagerValuesEvent : CustomEventObject {
    public AudioSourceManager audioSourceManager = null;
    public bool loopAudio = false;
    public float playScheduled = float.PositiveInfinity;
    public float scheduledStartTime = float.PositiveInfinity;
    public float scheduledEndTime = float.PositiveInfinity;
    public bool play = false;
    public bool pause = false;
    public bool stop = false;
    //--------------------------------------------------------------------------
    public float delay = 0;
    public float endVolume = 1f;
    public float volumeTweenDuration = float.Epsilon;
    public GoEaseType volumeEasingType = GoEaseType.Linear;
    
    // Use this for initialization
    protected override void Awake() {
        base.Awake();
    }
    
    // Use this for initialization
    protected override void Start() {
        base.Start();
    }
    
    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }
    
    protected override void Initialize() {
        base.Initialize();
        
        if(audioSourceManager == null) {
            this.gameObject.LogComponentError("audioSourceManager", this.GetType());
        }
    }
    
    public override void Execute() {
        FireStartEvents();
        ExecuteLogic();
    }
    
    public override void ExecuteLogic() {
        SetAudioPoolValuesEvent();
    }
    
    public void SetAudioPoolValuesEvent() {
        audioSourceManager.Loop(loopAudio);
        if(playScheduled != float.PositiveInfinity) {
            audioSourceManager.PlayScheduled(playScheduled);
        }
        if(scheduledEndTime != float.PositiveInfinity) {
            audioSourceManager.SetScheduledEndTime(scheduledEndTime);
        }
        if(scheduledStartTime != float.PositiveInfinity) {
            audioSourceManager.SetScheduledStartTime(scheduledStartTime);
        }
        
        if(play) {
            audioSourceManager.PlayDelayed(delay);
        }
        if(stop) {
            audioSourceManager.Stop();
        }
        
        //------------------------------------------------
        // Volume Tweening
        //------------------------------------------------
        Mathf.Clamp(endVolume, 0, 1f);
        GoTweenConfig tweenConfig = new GoTweenConfig();
        
        tweenConfig.setDelay(delay)
        .setEaseType(volumeEasingType)
        .floatProp("volume", endVolume)
        .onComplete(complete => {
            FireFinishEvents();
        });
        
        audioSourceManager.gameObject.AddGoTween(Go.to(audioSourceManager.GetAudioSource(),
                                                       volumeTweenDuration,
                                                       tweenConfig));
    }
}