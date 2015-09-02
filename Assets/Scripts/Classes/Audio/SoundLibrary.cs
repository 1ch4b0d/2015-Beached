using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: Make this more automated or something. It's ok for one offs, but at this
//       point it requires a lot of manual work in order to prevent mess ups. I
//       don't know maybe make a way to traverse the file directory or something
//       just think about optimizing this somehow
public class SoundLibrary : MonoBehaviour {
    public Dictionary<AudioType, string> soundDictionary = new Dictionary<AudioType, string>();
    
    //BEGINNING OF SINGLETON CODE CONFIGURATION5
    private static volatile SoundLibrary _instance;
    private static object _lock = new object();
    
    //Stops the lock being created ahead of time if it's not necessary
    static SoundLibrary() {
    }
    
    public static SoundLibrary Instance {
        get {
            if(_instance == null) {
                lock(_lock) {
                    _instance = GameObject.FindObjectOfType<SoundLibrary>();
                    if(_instance == null) {
                        GameObject soundLibraryGameObject = new GameObject("SoundLibrary");
                        _instance = (soundLibraryGameObject.AddComponent<SoundLibrary>()).GetComponent<SoundLibrary>();
                    }
                }
            }
            return _instance;
        }
    }
    
    private SoundLibrary() {
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
        soundDictionary[AudioType.None] = "path/to/file/from/root level";
        
        soundDictionary[AudioType.CosmicSpaceHeadSurfing] = "Sound/Music/Random/Cosmic Spacehead OST - Surfing";
        
        soundDictionary[AudioType.EntranceQueueDoorOpenClubMusic] = "Sound/Effects/Bathroom/EntranceQueue/DoorOpenClubMusic";
        
        soundDictionary[AudioType.Fart1] = "Sound/Effects/Bathroom/Farts/135716__robmoth__fart";
        soundDictionary[AudioType.Fart2] = "Sound/Effects/Bathroom/Farts/241000__dsisstudios__short-fart-01";
        soundDictionary[AudioType.Fart3] = "Sound/Effects/Bathroom/Farts/241001__dsisstudios__medium-fart-02";
        soundDictionary[AudioType.Fart4] = "Sound/Effects/Bathroom/Farts/241002__dsisstudios__medium-fart-01";
        soundDictionary[AudioType.Fart5] = "Sound/Effects/Bathroom/Farts/242750__dsisstudios__medium-fart-03";
        
        soundDictionary[AudioType.Flush1] = "Sound/Effects/Bathroom/ToiletFlushes/185046__justeluis__flush";
        soundDictionary[AudioType.Flush2] = "Sound/Effects/Bathroom/ToiletFlushes/185046__justeluis__flush";
        
        soundDictionary[AudioType.GreenDogPedalCopter] = "Sound/Music/Random/Greendog (MD) - Pedal-copter";
        soundDictionary[AudioType.GreendogIntroAztecTemples] = "Sound/Music/Random/Greendog (MD) - Intro_Aztec_Temples";
        
        soundDictionary[AudioType.Peeing1] = "Sound/Effects/Bathroom/Peeing/232539__arnaump__male-peeing";
        
        soundDictionary[AudioType.Pooping1] = "Sound/Effects/Bathroom/Pooping/134708__anovosel__max-s-poop";
        
        soundDictionary[AudioType.TextboxNextButtonPressBeep] = "Sound/Effects/Beep/124900__greencouch__beeps-18";
    }
    
    public string GetPath(AudioType audioTypeToGet) {
        string audioPath = "";
        soundDictionary.TryGetValue(audioTypeToGet, out audioPath);
        return (audioPath.Equals("")) ? null : audioPath;
    }
}
