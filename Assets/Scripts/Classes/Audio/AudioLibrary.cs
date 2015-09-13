using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: Make this more automated or something. It's ok for one offs, but at this
//       point it requires a lot of manual work in order to prevent mess ups. I
//       don't know maybe make a way to traverse the file directory or something
//       just think about optimizing this somehow
public class AudioLibrary : MonoBehaviour {
    public Dictionary<AudioType, string> audioDictionary = new Dictionary<AudioType, string>();
    
    //BEGINNING OF SINGLETON CODE CONFIGURATION5
    private static volatile AudioLibrary _instance;
    private static object _lock = new object();
    
    //Stops the lock being created ahead of time if it's not necessary
    static AudioLibrary() {
    }
    
    public static AudioLibrary Instance {
        get {
            if(_instance == null) {
                lock(_lock) {
                    _instance = GameObject.FindObjectOfType<AudioLibrary>();
                    if(_instance == null) {
                        GameObject audioLibraryGameObject = new GameObject("AudioLibrary");
                        _instance = (audioLibraryGameObject.AddComponent<AudioLibrary>()).GetComponent<AudioLibrary>();
                    }
                }
            }
            return _instance;
        }
    }
    
    private AudioLibrary() {
    }
    
    // Use this for initialization
    void Awake() {
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
    protected void Start() {
    }
    
    // Update is called once per frame
    protected void Update() {
    }
    
    protected void Initialize() {
        ConfigureSoundDictionary();
    }
    
    protected void ConfigureSoundDictionary() {
        audioDictionary[AudioType.None] = "path/to/file/from/root level";
        // Other sound is configured here
        ConfigureMusic();
        ConfigureEnvironmentSoundEffects();
        ConfigureItemSoundEffects();
        ConfigurePlayerSoundEffects();
        ConfigureNPCSoundEffects();
        ConfigureGUISoundEffects();
    }
    
    protected void ConfigureMusic() {
        audioDictionary[AudioType.ExplorationSong] = "Audio/Placeholder/Music/Fluffy/fluffy_-_feed";
        audioDictionary[AudioType.SufjanStevensExplodingWhale] = "Audio/Placeholder/Music/Sufjan Stevens - Exploding Whale";
    }
    
    protected void ConfigureEnvironmentSoundEffects() {
        audioDictionary[AudioType.Explosion] = "Audio/Placeholder/Effects/Rumble/rumble1";
        audioDictionary[AudioType.Ocean] = "Audio/Placeholder/Effects/Ocean/131276__soundmanfilms__rockyseashorestereo";
        audioDictionary[AudioType.Rumble01] = "Audio/Placeholder/Effects/Rumble/rumble1";
        audioDictionary[AudioType.Rumble02] = "Audio/Placeholder/Effects/Rumble/rumble2";
        audioDictionary[AudioType.Rumble03] = "Audio/Placeholder/Effects/Rumble/rumble3";
        audioDictionary[AudioType.TypeWriter] = "Audio/Placeholder/Effects/TypeWriter/typewriter-1";
    }
    protected void ConfigureItemSoundEffects() {
        // Item Sound Effects
        audioDictionary[AudioType.BlubberSquish01] = "Audio/Placeholder/Effects/Squishes/196725__paulmorek__sz-squish-12";
        audioDictionary[AudioType.BlubberSquish02] = "Audio/Placeholder/Effects/Squishes/271666__honorhunter__tomato-squish-wet";
        audioDictionary[AudioType.GiantBlubberExplosion] = "Audio/Placeholder/Effects/Rumble/rumble02";
        audioDictionary[AudioType.GiantBlubberAirResistance] = "Audio/Placeholder/Effects/rumble1";
        audioDictionary[AudioType.DetonatorPrimerPrimed] = "Audio/Placeholder/Effects/Zap/zapTwoToneUp";
        audioDictionary[AudioType.DetonatorPrimerUnprimed] = "Audio/Placeholder/Effects/Zap/zapTwoToneDown";
        audioDictionary[AudioType.DetonatorActivated] = "Audio/Placeholder/Effects/RPG/dropLeather";
    }
    protected void ConfigurePlayerSoundEffects() {
        audioDictionary[AudioType.PlayerJump] = "Audio/Placeholder/Effects/Jump/jump1";
        audioDictionary[AudioType.PlayerLandOnGround] = "Audio/Placeholder/Effects/Footsteps/footstep02";
        
        audioDictionary[AudioType.PlayerPickedUpItem] = "Audio/Placeholder/Effects/Laser/laser2";
        audioDictionary[AudioType.PlayerDropItem] = "Audio/Placeholder/Effects/Laser/laser3";
        audioDictionary[AudioType.PlayerThrewItem] = "Audio/Placeholder/Effects/Laser/laser1";
    }
    protected void ConfigureNPCSoundEffects() {
    }
    protected void ConfigureGUISoundEffects() {
        audioDictionary[AudioType.SpeechBubbleInteractionStarted] = "Audio/Placeholder/Effects/Digital_Sounds/powerUp2";
        audioDictionary[AudioType.SpeechBubbleIterate] = "Audio/Placeholder/Effects/Digital_Sounds/powerUp2";
        audioDictionary[AudioType.SpeechBubbleInteractionFinished] = "Audio/Placeholder/Effects/Digital_Sounds/powerUp2";
    }
    
    public string GetClipPath(AudioType audioTypeToGet) {
        string audioPath = "";
        audioDictionary.TryGetValue(audioTypeToGet, out audioPath);
        return (audioPath.Equals("")) ? null : audioPath;
    }
}
