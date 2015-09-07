using UnityEngine;
using System.Collections;

/// <summary>
/// This is a class that should be the main interface for firing any calls to
/// trigger playing audio within the game. This has been composed with a singleton
/// class in order to provide the easiest way to access this functionality without
/// requiring a reference.
/// </summary>
public class AudioManager : MonoBehaviour {
    AudioPool audioPool = null;
    
    //BEGINNING OF SINGLETON CODE CONFIGURATION5
    private static volatile AudioManager _instance;
    private static object _lock = new object();
    
    //Stops the lock being created ahead of time if it's not necessary
    static AudioManager() {
    }
    
    public static AudioManager Instance {
        get {
            if(_instance == null) {
                lock(_lock) {
                    _instance = GameObject.FindObjectOfType<AudioManager>();
                    if(_instance == null) {
                        GameObject SoundManagerGameObject = new GameObject("AudioManager");
                        _instance = (SoundManagerGameObject.AddComponent<AudioManager>()).GetComponent<AudioManager>();
                    }
                }
            }
            return _instance;
        }
    }
    
    private AudioManager() {
    }
    
    // Use this for initialization
    protected virtual void Awake() {
        //There's a lot of magic happening right here. Basically, the THIS keyword is a reference to
        //the script, which is assumedly attached to some GameObject. This in turn allows the instance
        //to be assigned when a game object is given this script in the scene view.
        //This also allows the pre-configured lazy instantiation to occur when the script is referenced from
        //another call to it, so that you don't need to worry if it exists or not.
        _instance = this;
        Initialize();
    }
    //END OF SINGLETON CODE CONFIGURATION
    // Use this for initialization
    protected virtual void Start() {
    }
    
    // Update is called once per frame
    protected virtual void Update() {
    }
    
    protected virtual void Initialize() {
        if(audioPool == null) {
            audioPool = AudioPool.Instance;
            if(audioPool == null) {
                this.gameObject.LogComponentError("audioPool", this.GetType());
            }
        }
    }
    
    // Returns audio source manager to enable build method calls on it
    public AudioSourceManager Play(AudioType audioToPlay, ulong delay = 0) {
        Debug.Log("Playing: " + audioToPlay.ToString());
        GameObject audioGameObject = null;
        AudioSourceManager audioSourceManager = null;
        
        audioGameObject = audioPool.Issue();
        audioSourceManager = audioGameObject.GetComponent<AudioSourceManager>();
        audioSourceManager.Clip(AudioLibrary.Instance.GetClipPath(audioToPlay));
        audioSourceManager.Play(delay);
        
        return audioSourceManager;
    }
    
    public AudioSourceManager PlayScheduled(AudioType audioToPlay, double time) {
        Debug.Log("PlayingScheduled: " + audioToPlay.ToString());
        GameObject audioGameObject = null;
        AudioSourceManager audioSourceManager = null;
        
        audioGameObject = audioPool.Issue();
        audioSourceManager = audioGameObject.GetComponent<AudioSourceManager>();
        audioSourceManager.Clip(AudioLibrary.Instance.GetClipPath(audioToPlay));
        audioSourceManager.PlayScheduled(time);
        
        return audioSourceManager;
    }
}
