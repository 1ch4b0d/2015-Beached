using UnityEngine;
using System.Collections;

public class BlubberPool : MonoBehaviour {
    //BEGINNING OF SINGLETON CODE CONFIGURATION
    private static volatile GameObjectPool<Blubber> _instance;
    private static object _lock = new object();
    
    //Stops the lock being created ahead of time if it's not necessary
    static BlubberPool() {
    }
    
    public static GameObjectPool<Blubber> Instance {
        get {
            if(_instance == null) {
                lock(_lock) {
                    _instance = GameObject.FindObjectOfType<GameObjectPool<Blubber>>();
                    if(_instance == null) {
                        GameObject BlubberPoolGameObject = new GameObject("BlubberPool");
                        _instance = (BlubberPoolGameObject.AddComponent<GameObjectPool<Blubber>>()).GetComponent<GameObjectPool<Blubber>>();
                    }
                }
            }
            return _instance;
        }
    }
    
    private BlubberPool() {
    }
    
    protected void Awake() {
        Initialize();
    }
    //END OF SINGLETON CODE CONFIGURATION
    
    // // Use this for initialization
    // protected void Start() {
    // }
    
    // // Update is called once per frame
    // protected void Update() {
    // }
    
    protected void Initialize() {
    }
}
