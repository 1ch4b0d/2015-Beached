using UnityEngine;
using System.Collections;

// Class that makes the camera follow the player. This is the same as the free 2D demo asset from Unity.
// Source: https://www.assetstore.unity3d.com/#/content/11228
public class CameraFollow : MonoBehaviour {
    [Tooltip("This is the camera that is being moved.")]
    public Camera cameraToMove = null;
    
    [Tooltip("Object The Camera is Supposed to Follow.")]
    public Transform objectToFollow = null;
    [Tooltip("The offset for the camera based on the object being followed.")]
    public Vector2 offsetFromObjectToFollow = Vector2.zero;
    
    [Tooltip("How smoothly the camera catches up with it's target movement.")]
    public Vector2 followSpeed = new Vector2(8f, 8f);
    
    public void Awake() {
        Initialize();
    }
    
    void Start() {
    }
    
    void FixedUpdate() {
        Vector3 cameraMovementOffset = CalculateMovementOffset();
        cameraMovementOffset *= Time.deltaTime;
        cameraMovementOffset = LockOffsetToTarget(cameraToMove.transform.position, GetTargetPositionWithOffset(), cameraMovementOffset);
        cameraToMove.gameObject.transform.position += cameraMovementOffset;
    }
    
    public void Initialize() {
        if(cameraToMove == null) {
            cameraToMove = this.gameObject.GetComponent<Camera>();
            if(cameraToMove == null) {
                Debug.LogError("'cameraToMove' must not be null in order for the script on '" + this.gameObject.name + "' to work.");
            }
        }
        
        if(objectToFollow == null) {
            objectToFollow = GameObject.FindGameObjectWithTag("Player").transform;
            if(objectToFollow == null) {
                Debug.LogError("'objectToFollow' must not be null in order for the script on '" + this.gameObject.name + "' to work.");
            }
        }
    }
    
    public Vector2 GetFollowOffsetVector2() {
        return offsetFromObjectToFollow;
    }
    
    public Vector3 GetFollowOffsetVector3() {
        return new Vector3(offsetFromObjectToFollow.x, offsetFromObjectToFollow.y, 0);
    }
    
    public Vector2 GetTargetPositionWithOffset() {
        return new Vector2(objectToFollow.transform.position.x + offsetFromObjectToFollow.x,
                           objectToFollow.transform.position.y + offsetFromObjectToFollow.y);
    }
    
    public Vector3 CalculateMovementOffset() {
        Vector3 movementOffset = Vector3.zero;
        
        if(cameraToMove.gameObject.transform.position.x < GetTargetPositionWithOffset().x) {
            movementOffset.x += followSpeed.x;
        }
        else if(cameraToMove.gameObject.transform.position.x > GetTargetPositionWithOffset().x) {
            movementOffset.x -= followSpeed.x;
        }
        
        if(cameraToMove.gameObject.transform.position.y < GetTargetPositionWithOffset().y) {
            movementOffset.y += followSpeed.y;
        }
        else if(cameraToMove.gameObject.transform.position.y > GetTargetPositionWithOffset().y) {
            movementOffset.y -= followSpeed.y;
        }
        
        return new Vector3(movementOffset.x, movementOffset.y, 0);
    }
    
    public Vector3 ClampOffsetWithinBounds(Vector3 movementOffset) {
        return movementOffset;
    }
    
    /// <summary>
    /// This will clamp the movement position when translating towards the target
    /// object so that it won't overshoot a target when moving towards it
    /// </summary>
    public Vector3 LockOffsetToTarget(Vector3 startPosition, Vector3 targetPosition, Vector3 movementOffset) {
        // X
        if(movementOffset.x > 0
            && movementOffset.x > (targetPosition.x - startPosition.x)) {
            movementOffset.x = (targetPosition.x - startPosition.x);
        }
        if(movementOffset.x < 0
            && movementOffset.x < (targetPosition.x - startPosition.x)) {
            movementOffset.x = (targetPosition.x - startPosition.x);
        }
        // Y
        if(movementOffset.y > 0
            && movementOffset.y > (targetPosition.y - startPosition.y)) {
            movementOffset.y = (targetPosition.y - startPosition.y);
        }
        if(movementOffset.y < 0
            && movementOffset.y < (targetPosition.y - startPosition.y)) {
            movementOffset.y = (targetPosition.y - startPosition.y);
        }
        
        return movementOffset;
    }
}