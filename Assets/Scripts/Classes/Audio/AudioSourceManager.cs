using UnityEngine;
using System.Collections;

/// <summary>
/// DO NOT CONFUSE THIS WITH THE AUDIOMANAGER. THE AUDIOMANAGER IS THE HIGH LEVEL
/// INTERFACE TO EXECUTE CALLS TO. THE AUDIOSOURCEMANAGER IS A WRAPPER FOR UNITY
/// You're going to forget why you created this in the first place.
/// The unity audio source class is just terrible in providing information that
/// lets you know when it finished. Because of this, you created a wrapper that
/// allows you to track the logic of the audio source as it plays through. Typically
/// the audio source's isPlaying variable could be relied on to know if it's
/// playing, but on focus changes and when paused the audio source isPlaying
/// variable is set to false. This means it's unreliable for determining state.
/// On top of this to determine if it finished you could try waiting for
/// isPlaying to return false, but again its state is unreliable. You could also
/// wait to determine if the time has finished, but it resets back to 0 when it
/// has finished. This would be fine if you /// could determine if the audio
/// source had started, but you can't. SO in order to make tracking the logic
/// easier you created this wrapper for tracking when the audio object is
/// started, when it finishes, and so forth, so that outside logic can infer when to
/// use the audio object itself.
/// </summary>
public class AudioSourceManager : MonoBehaviour {
    public AudioSource audioSourceReference = null;
    public bool hasStartedPlaying = false;
    public bool hasFinished = false;
    
    protected void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected void Start() {
    }
    
    // Update is called once per frame
    protected void Update() {
        // Debug.Log("-----------------------");
        // Debug.Log("Sample: " + audioSourceReference.clip.samples);
        // Debug.Log("Frequency: " + audioSourceReference.clip.frequency);
        // Debug.Log("Seconds: " + audioSourceReference.clip.samples/audioSourceReference.clip.frequency);
        // Debug.Log("Current Time: " + audioSourceReference.time);
        // if(Input.GetKeyDown(KeyCode.P)) {
        //     Pause();
        // }
        
        PerformStateLogic();
    }
    
    protected void Initialize() {
        if(audioSourceReference == null) {
            audioSourceReference = this.gameObject.GetComponent<AudioSource>();
            if(audioSourceReference == null) {
                this.gameObject.LogComponentError("audioSourceReference", this.GetType());
            }
        }
    }
    
    public void PerformStateLogic() {
        if(hasStartedPlaying == false
            && audioSourceReference.isPlaying) {
            hasStartedPlaying = true;
        }
        
        if(hasStartedPlaying
            && audioSourceReference.time == 0
            && audioSourceReference.isPlaying == false) {
            hasFinished = true;
        }
    }
    
    public AudioClip GetClip() {
        return audioSourceReference.clip;
    }
    public float GetClipLength() {
        Debug.Log("" + audioSourceReference.clip.length);
        return audioSourceReference.clip.length;
    }
    public AudioSourceManager Clip(string audioClipPath) {
        Debug.Log("Loading: " + audioClipPath);
        audioSourceReference.clip = (AudioClip)Resources.Load(audioClipPath, typeof(AudioClip));
        return this;
    }
    public AudioSourceManager Clip(AudioClip newAudioClip) {
        audioSourceReference.clip = newAudioClip;
        return this;
    }
    
    public AudioSourceManager Loop(bool loop) {
        audioSourceReference.loop = loop;
        return this;
    }
    
    public AudioSourceManager Play(ulong delay = 0) {
        Debug.Log("Playing");
        audioSourceReference.Play(delay);
        return this;
    }
    
    public AudioSourceManager PlayDelayed(float delay) {
        audioSourceReference.PlayDelayed(delay);
        return this;
    }
    public AudioSourceManager PlayScheduled(double time) {
        audioSourceReference.PlayScheduled(time);
        return this;
    }
    
    public AudioSourceManager SetScheduledEndTime(double time) {
        audioSourceReference.SetScheduledEndTime(time);
        return this;
    }
    
    public AudioSourceManager SetScheduledStartTime(double time) {
        audioSourceReference.SetScheduledStartTime(time);
        return this;
    }
    
    public AudioSourceManager Pause() {
        audioSourceReference.Pause();
        return this;
    }
    
    public AudioSourceManager Stop() {
        audioSourceReference.Stop();
        return this;
    }
}