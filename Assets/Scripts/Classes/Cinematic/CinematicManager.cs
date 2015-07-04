using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CinematicManager : MonoBehaviour {
    public Cinematic currentCinematic = null;
    public bool isPaused = false;
    
    //BEGINNING OF SINGLETON CODE CONFIGURATION
    private static volatile CinematicManager _instance;
    private static object _lock = new object();
    
    //Stops the lock being created ahead of time if it's not necessary
    static CinematicManager() {
    }
    
    public static CinematicManager Instance {
        get {
            if(_instance == null) {
                lock(_lock) {
                    _instance = GameObject.FindObjectOfType<CinematicManager>();
                    if(_instance == null) {
                        GameObject CameraManagerGameObject = new GameObject("CinematicManager");
                        _instance = (CameraManagerGameObject.AddComponent<CinematicManager>()).GetComponent<CinematicManager>();
                    }
                }
            }
            return _instance;
        }
    }
    
    private CinematicManager() {
    }
    
    public void Awake() {
        _instance = this;
        Initialize();
    }
    //END OF SINGLETON CODE CONFIGURATION
    
    public void Start() {
    }
    
    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.T)) {
            StartCinematic<DefaultCredits>();
        }
        //------------------
        PerformCinematicLogic();
    }
    
    public void Initialize() {
    }
    
    public bool IsPaused() {
        return isPaused;
    }
    
    public void Pause() {
        isPaused = true;
    }
    public void Unpause() {
        isPaused = false;
    }
    
    public void PerformCinematicLogic() {
        if(currentCinematic != null) {
            currentCinematic.PerformLogic();
        }
    }
    
    public void StartCinematic<T>() where T : MonoBehaviour {
        if(currentCinematic != null) {
            Destroy(currentCinematic.gameObject);
        }
        GameObject newCinematic = new GameObject("Cinematic");
        newCinematic.transform.parent = CinematicManager.Instance.gameObject.transform;
        newCinematic.AddComponent<T>();
        Cinematic cinematic = newCinematic.GetComponent<Cinematic>();
        currentCinematic = cinematic;
        
        cinematic.TriggerStart();
    }
}