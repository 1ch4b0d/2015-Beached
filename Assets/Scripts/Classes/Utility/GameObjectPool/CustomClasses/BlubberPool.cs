using UnityEngine;
using System.Collections;

public class BlubberPool : GameObjectPool {
    //BEGINNING OF SINGLETON CODE CONFIGURATION
    private static volatile GameObjectPool _instance;
    private static object _lock = new object();
    
    //Stops the lock being created ahead of time if it's not necessary
    static BlubberPool() {
    }
    
    public static GameObjectPool Instance {
        get {
            if(_instance == null) {
                lock(_lock) {
                    _instance = GameObject.FindObjectOfType<BlubberPool>();
                    if(_instance == null) {
                        GameObject BlubberPoolGameObject = new GameObject("BlubberPool");
                        _instance = (BlubberPoolGameObject.AddComponent<BlubberPool>()).GetComponent<BlubberPool>();
                    }
                }
            }
            return _instance;
        }
    }
    
    private BlubberPool() {
    }
    
    // protected override void Awake() {
    //     base.Awake();
    // }
    // END OF SINGLETON CODE CONFIGURATION
    
    // // Use this for initialization
    // protected override void Start() {
    //     base.Start();
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    //     base.Update();
    // }
    
    protected override void Initialize() {
        base.Initialize();
    }
}
