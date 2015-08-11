using UnityEngine;
using System.Collections;

public class CameraFollowEvent : CustomEventObject {
    public CameraFollow cameraFollow = null;
    //-------------------------------------------------------
    public bool cameraFollowEnabled = true;
    
    public Camera cameraToMove = null;
    public Transform objectToFollow = null;
    
    public bool useDefaultOffsetFromObjectToFollow = true;
    public Vector2 offsetFromObjectToFollow = Vector2.zero;
    
    public bool useDefaultFollowSpeed = true;
    public Vector2 followSpeed = new Vector2(10f, 10f);
    
    // // Use this for initialization
    // protected override void Awake() {
    // }
    
    // // Use this for initialization
    // protected override void Start() {
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    // }
    
    protected override void Initialize() {
        base.Initialize();
        //---------------------
        if(cameraFollow == null) {
            Debug.LogError("The 'cameraFollow' property is null for " + this.gameObject.name + ". Please assign it then retry again.");
        }
    }
    
    // public override void Execute() {
    // }
    
    public override void ExecuteLogic() {
        SetCameraFollowValues();
    }
    
    public void SetCameraFollowValues() {
        if(cameraToMove != null) {
            cameraFollow.cameraToMove = cameraToMove;
        }
        
        if(cameraFollow != null) {
            cameraFollow.objectToFollow = objectToFollow;
        }
        
        if(!useDefaultOffsetFromObjectToFollow) {
            cameraFollow.offsetFromObjectToFollow = offsetFromObjectToFollow;
        }
        
        if(!useDefaultFollowSpeed) {
            cameraFollow.followSpeed = followSpeed;
        }
        
        cameraFollow.enabled = cameraFollowEnabled;
    }
}
