using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
    public GameObject leftWorldBorder = null;
    public GameObject rightWorldBorder = null;
    
    public GameObject georgeThorntonGameObject = null;
    public GameObject whaleGameObject = null;
    // public Transform explosionsMarker = null;
    
    //BEGINNING OF SINGLETON CODE CONFIGURATION
    private static volatile LevelManager _instance;
    private static object _lock = new object();
    
    //Stops the lock being created ahead of time if it's not necessary
    static LevelManager() {
    }
    
    public static LevelManager Instance {
        get {
            if(_instance == null) {
                lock(_lock) {
                    _instance = GameObject.FindObjectOfType<LevelManager>();
                    if(_instance == null) {
                        GameObject CameraManagerGameObject = new GameObject("LevelManager");
                        _instance = (CameraManagerGameObject.AddComponent<LevelManager>()).GetComponent<LevelManager>();
                    }
                }
            }
            return _instance;
        }
    }
    
    private LevelManager() {
    }
    
    protected void Awake() {
        Initialize();
    }
    //END OF SINGLETON CODE CONFIGURATION
    
    protected void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    protected void Initialize() {
        _instance = this;
        
        if(leftWorldBorder == null) {
            Debug.LogError("The LeftBorder is null, please declare it and set it in order for the level logic to be consistent.");
        }
        if(rightWorldBorder == null) {
            Debug.LogError("The RightWorldBorder is null, please declare it and set it in order for the level logic to be consistent.");
        }
    }
    
    public void TriggerExplosionCinematic() {
        CinematicManager.Instance.StartCinematic<WhaleExplosion>();
        PlayerManager.Instance.GetPlayer().ToggleAcrocatic(PlayerManager.Instance.GetPlayerGameObject(), false);
        GameObject explosionOne = Factory.Explosion();//(GameObject)GameObject.Instantiate(Resources.Load("Prefabs/GUI/Credits/CreditText") as GameObject);
        Animator explosionOneAnimator = explosionOne.GetComponent<Animator>();
        AnimatorHelper explosionOneAnimatorHelper = explosionOne.GetComponent<AnimatorHelper>();
        explosionOneAnimatorHelper.SetDestroyOnFinish(true);
        explosionOneAnimator.Play("ExplosionOne");
    }
}