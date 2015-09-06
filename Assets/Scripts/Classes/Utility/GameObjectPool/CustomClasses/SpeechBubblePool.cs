using UnityEngine;
using System.Collections;

public class SpeechBubblePool : GameObjectPool {
    //BEGINNING OF SINGLETON CODE CONFIGURATION
    private static volatile GameObjectPool _instance;
    private static object _lock = new object();
    
    //Stops the lock being created ahead of time if it's not necessary
    static SpeechBubblePool() {
    }
    
    public static GameObjectPool Instance {
        get {
            if(_instance == null) {
                lock(_lock) {
                    _instance = GameObject.FindObjectOfType<SpeechBubblePool>();
                    if(_instance == null) {
                        GameObject speechBubblePoolGameObject = new GameObject("SpeechBubblePool");
                        _instance = (speechBubblePoolGameObject.AddComponent<SpeechBubblePool>()).GetComponent<SpeechBubblePool>();
                    }
                }
            }
            return _instance;
        }
    }
    
    private SpeechBubblePool() {
    }
    
    protected override void Awake() {
        base.Awake();
        if(_instance == null) {
            _instance = this;
        }
    }
    //END OF SINGLETON CODE CONFIGURATION
    
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
    
    protected override GameObject Create() {
        GameObject newGameObject = (GameObject)GameObject.Instantiate(prefabToGenerate);
        newGameObject.transform.parent = parentGameObject.transform;
        newGameObject.transform.localScale = new Vector3(1, 1, 1);
        return newGameObject;
    }
}
