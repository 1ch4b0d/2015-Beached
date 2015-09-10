using UnityEngine;
using System.Collections;

public class AudioManagerValuesEvent : CustomEventObject {
    private AudioPool audioPool = null;
    public AudioType audioToPlay = AudioType.None;
    public bool loopAudio = false;
    public float playScheduled = float.PositiveInfinity;
    // public float scheduledStartTime = float.PositiveInfinity;
    // public float scheduledEndTime = float.PositiveInfinity;
    
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
        
        if(audioPool == null) {
            audioPool = AudioPool.Instance;
            if(audioPool == null) {
                this.gameObject.LogComponentError("audioPool", this.GetType());
            }
        }
    }
    
    public override void ExecuteLogic() {
        SetAudioPoolValuesEvent();
    }
    
    public void SetAudioPoolValuesEvent() {
        if(audioToPlay != AudioType.None) {
            // AudioManager.Instance.Play returns an AudioSourceManager
            AudioSourceManager audioSourceManager = null;
            //-----------------------------------------------
            if(playScheduled != float.PositiveInfinity) {
                // I'm skeptical of this.
                audioSourceManager = AudioManager.Instance.PlayScheduled(audioToPlay, playScheduled);
            }
            else {
                audioSourceManager = AudioManager.Instance.Play(audioToPlay);
            }
            //-----------------------------------------------
            // http://docs.unity3d.com/ScriptReference/AudioSource.SetScheduledStartTime.html
            // This is really really really dumb. I don't have the time to look
            // into this. Just know there's some stupidness that needs to be
            // resolved on your end to get this working with Unity's API
            // if(scheduledStartTime == float.PositiveInfinity) {
            //     audioSourceManager.SetScheduledStartTime(0);
            // }
            // else {
            //     audioSourceManager.SetScheduledStartTime(scheduledStartTime);
            // }
            //-----------------------------------------------
            // http://docs.unity3d.com/ScriptReference/AudioSource.SetScheduledEndTime.html
            // This is really really really dumb. I don't have the time to look
            // into this. Just know there's some stupidness that needs to be
            // resolved on your end to get this working with Unity's API
            // if(scheduledEndTime == float.PositiveInfinity) {
            //     audioSourceManager.SetScheduledEndTime(audioSourceManager.GetClipLength());
            // }
            // else {
            //     audioSourceManager.SetScheduledEndTime(scheduledEndTime);
            // }
            //-----------------------------------------------
            audioSourceManager.Loop(loopAudio);
            //-----------------------------------------------
        }
    }
}