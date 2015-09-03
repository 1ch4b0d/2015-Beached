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
        
        audioDictionary[AudioType.Ocean] = "Audio/Placeholder/Effects/Ocean/131276__soundmanfilms__rockyseashorestereo.mp3";
    }
    
    public string GetPath(AudioType audioTypeToGet) {
        string audioPath = "";
        audioDictionary.TryGetValue(audioTypeToGet, out audioPath);
        return (audioPath.Equals("")) ? null : audioPath;
    }
}
