using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
    public GameObject leftWorldBorder = null;
    public GameObject rightWorldBorder = null;
    
    public GameObject georgeThorntonGameObject = null;
    public List<Transform> whaleExplosionMarkers = null;
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
        
        //----------------------------------------------------------------------
        // Markers
        //----------------------------------------------------------------------
        if(whaleExplosionMarkers == null) {
            whaleExplosionMarkers  = new List<Transform>();
            // Debug.LogError("The 'whaleExplosionMarkers' list is null, please declare it for '" + this.gameObject.name + "' and set it in order for the level logic to be consistent.");
        }
        //----------------------------------------------------------------------
        // NPCs
        //----------------------------------------------------------------------
        // if(georgeThorntonGameObject == null) {
        //     Debug.LogError("The 'georgeThorntonGameObject' is null, please declare it for '" + this.gameObject.name + "' and set it in order for the level logic to be consistent.");
        // }
        //----------------------------------------------------------------------
        // World Boundaries
        //----------------------------------------------------------------------
        if(leftWorldBorder == null) {
            Debug.LogError("The 'leftWorldBorder' is null, please declare it for '" + this.gameObject.name + "' and set it in order for the level logic to be consistent.");
        }
        if(rightWorldBorder == null) {
            Debug.LogError("The 'rightWorldBorder' is null, please declare it for '" + this.gameObject.name + "' and set it in order for the level logic to be consistent.");
        }
    }
    
    public void TriggerExplosionCinematic() {
        // CinematicManager.Instance.StartCinematic<WhaleExplosion>();
        PerformExplosionCinematic();
    }
    
    public void PerformExplosionCinematic() {
        PlayerManager.Instance.GetPlayer().ToggleAcrocatic(PlayerManager.Instance.GetPlayerGameObject(), false);
        
        // Just do one explosion for now
        // PlayExplosion("ExplosionOne", whaleExplosionMarkers[0].transform.position);
    }
    
    // public void SetOnAnimationFinish(string stateName, StateEvent stateEventFunction, bool destroyOnFinish = false, bool loopAnimationEvent = false) {
    public void PlayExplosion(string explosionAnimationName, Vector3 position, bool destroyOnFinish = true) {
        GameObject explosion = Factory.Explosion();//(GameObject)GameObject.Instantiate(Resources.Load("Prefabs/GUI/Credits/CreditText") as GameObject);
        Animator explosionAnimator = explosion.GetComponent<Animator>();
        AnimatorHelper explosionAnimatorHelper = explosion.GetComponent<AnimatorHelper>();
        
        // explosionAnimatorHelper.SetDestroyOnFinish(destroyOnFinish);
        explosionAnimatorHelper.SetDestroyOnFinish(destroyOnFinish);
        explosionAnimator.Play(explosionAnimationName);
        
        explosion.transform.position = new Vector3(position.x, position.y, explosion.transform.position.z);
    }
}