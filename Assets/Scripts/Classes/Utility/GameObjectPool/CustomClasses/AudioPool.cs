using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioPool : GameObjectPool {
    //BEGINNING OF SINGLETON CODE CONFIGURATION5
    private static volatile AudioPool _instance;
    private static object _lock = new object();
    
    //Stops the lock being created ahead of time if it's not necessary
    static AudioPool() {
    }
    
    public static AudioPool Instance {
        get {
            if(_instance == null) {
                lock(_lock) {
                    _instance = GameObject.FindObjectOfType<AudioPool>();
                    if(_instance == null) {
                        GameObject audioPoolGameObject = new GameObject("AudioPool");
                        _instance = (audioPoolGameObject.AddComponent<AudioPool>()).GetComponent<AudioPool>();
                    }
                }
            }
            return _instance;
        }
    }
    
    private AudioPool() {
    }
    
    // Use this for initialization
    protected override void Awake() {
        base.Awake();
        _instance = this;
    }
    //END OF SINGLETON CODE CONFIGURATION
    
    // Use this for initialization
    // protected override void Start() {
    //     base.Start();
    // }
    
    // Update is called once per frame
    // protected override void Update() {
    //     base.Update();
    // }
    
    protected override GameObject Create() {
        return base.Create();
    }
    
    public override GameObject Issue() {
        return base.Issue();
    }
    
    public override void Decomission(GameObject gameObjectToDecomission) {
        base.Decomission(gameObjectToDecomission);
    }
    
    protected override void Destroy(GameObject gameObjectToDestroy) {
        base.Destroy(gameObjectToDestroy);
    }
    
    protected override void AddToAvailable(GameObject gameObjectToAdd) {
        base.AddToAvailable(gameObjectToAdd);
    }
    
    protected override void AddToInUse(GameObject gameObjectToAdd) {
        base.AddToInUse(gameObjectToAdd);
    }
    
    public override void PerformRecycleCheck() {
        List<GameObject> audioObjectsThatFinishedPlaying = new List<GameObject>();
        foreach(GameObject gameObj in inUseGameObjects) {
            if(gameObj.GetComponent<AudioSourceManager>().hasFinished) {
                audioObjectsThatFinishedPlaying.Add(gameObj);
            }
        }
        foreach(GameObject gameObj in audioObjectsThatFinishedPlaying) {
            inUseGameObjects.Remove(gameObj);
            AddToAvailable(gameObj);
        }
    }
}
