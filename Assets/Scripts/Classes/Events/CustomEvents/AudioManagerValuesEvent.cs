using UnityEngine;
using System.Collections;

public class AudioManagerValuesEvent : CustomEventObject {
    private AudioPool audioPool = null;
    public AudioType audioToPlay = AudioType.None;
    public bool loopAudio = false;
    
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
            AudioManager.Instance
            .Play(audioToPlay)
            .Loop(loopAudio);
        }
    }
}
