using UnityEngine;
using System.Collections;

public class ExplosionPool : GameObjectPool {
    //BEGINNING OF SINGLETON CODE CONFIGURATION
    private static volatile GameObjectPool _instance;
    private static object _lock = new object();
    
    //Stops the lock being created ahead of time if it's not necessary
    static ExplosionPool() {
    }
    
    public static GameObjectPool Instance {
        get {
            if(_instance == null) {
                lock(_lock) {
                    _instance = GameObject.FindObjectOfType<ExplosionPool>();
                    if(_instance == null) {
                        GameObject ExplosionPoolGameObject = new GameObject("ExplosionPool");
                        _instance = (ExplosionPoolGameObject.AddComponent<ExplosionPool>()).GetComponent<ExplosionPool>();
                    }
                }
            }
            return _instance;
        }
    }
    
    private ExplosionPool() {
    }
    
    protected override void Awake() {
        Initialize();
    }
    //END OF SINGLETON CODE CONFIGURATION
    
    // // Use this for initialization
    // protected override void Start() {
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    // }
    
    protected override void Initialize() {
    }
    
    protected override GameObject Create() {
        GameObject newExplosionGameObject = (GameObject)GameObject.Instantiate(prefabToGenerate);
        newExplosionGameObject.transform.parent = this.gameObject.transform;
        return newExplosionGameObject;
    }
}
