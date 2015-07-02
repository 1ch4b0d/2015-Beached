using UnityEngine;
using System.Collections;

// TODO: Extend this out to a base class where it's called GameObjectWorldLock
//       then extend it to this class, and do it in terms of camera
// TODO: Do this based on local position as well
// TODO: Generalize and abstract out the ClampBasedOnCameraCenter and ClampBasedOnCameraBorders
//       so that they're using some sort of reset that sets to an anchor offset or something
//       maybe this isn't a good idea, re-evaluate this later
public class CameraWorldBoundLock : MonoBehaviour {
    [Tooltip("This is the camera that is being moved.")]
    public Camera cameraToLock = null;
    
    [Tooltip("The maximum x and y coordinates the camera can have.")]
    public Vector2 maximumPosition = new Vector2(5000, 5000);
    [Tooltip("The minimum x and y coordinates the camera can have.")]
    public Vector2 minimumPosition = new Vector2(-5000, -5000);
    [Tooltip("If true the min and max position check is based on the borders of the camera viewport instead of the camera's anchor.")]
    public bool useCameraBordersForBoundsCheck = true;
    
    public void Awake() {
        Initialize();
    }
    
    void Start() {
    }
    
    void FixedUpdate() {
        Vector3 fixedPosition = ClampCameraWithinBounds(cameraToLock, minimumPosition, maximumPosition);
        cameraToLock.transform.position = fixedPosition;
    }
    
    public void Initialize() {
        if(cameraToLock == null) {
            cameraToLock = this.gameObject.GetComponent<Camera>();
            if(cameraToLock == null) {
                Debug.LogError("'cameraToLock' must not be null in order for the script on '" + this.gameObject.name + "' to work.");
            }
        }
    }
    
    // Has the point passed the bound coming from the larger side of the threshold variable (the right side?)
    public bool HasExceededMinimumBound(float position, float positionThreshold) {
        if(position < positionThreshold) {
            return true;
        }
        else {
            return false;
        }
    }
    
    // Has the point passed the bound coming from the smaller side of the threshold variable (the left side?)
    public bool HasExceededMaximumBound(float position, float positionThreshold) {
        if(position > positionThreshold) {
            return true;
        }
        else {
            return false;
        }
    }
    
    public Vector3 ClampCameraWithinBounds(Camera cameraBeingClamped, Vector3 minimumBounds, Vector3 maximumBounds) {
        if(useCameraBordersForBoundsCheck) {
            return ClampBasedOnCameraBorders(cameraBeingClamped, minimumBounds, maximumBounds);
        }
        else {
            return ClampBasedOnCameraCenter(cameraBeingClamped, minimumBounds, maximumBounds);
        }
    }
    
    public Vector3 ClampBasedOnCameraCenter(Camera cameraBeingClamped, Vector3 minimumBounds, Vector3 maximumBounds) {
        Vector3 newPosition = cameraBeingClamped.transform.position;
        
        bool hasExceededLeftBound = HasExceededMinimumBound(cameraBeingClamped.transform.position.x, minimumBounds.x);
        bool hasExceededRightBound = HasExceededMaximumBound(cameraBeingClamped.transform.position.x, maximumBounds.x);
        bool hasExceededTopBound = HasExceededMaximumBound(cameraBeingClamped.transform.position.y, maximumBounds.y);
        bool hasExceededBottomBound = HasExceededMinimumBound(cameraBeingClamped.transform.position.y, minimumBounds.y);
        
        // basically one long if statement of the combinatorics
        if(hasExceededLeftBound
            && hasExceededRightBound) {
            // set to mid point
            // Debug.Log("Is between both the left and right bound of the x vector.");
            newPosition.x = (minimumBounds.x + maximumBounds.x) / 2;
        }
        else if(hasExceededLeftBound) {
            // Debug.Log("Is over the min bound of the x vector.");
            newPosition.x = minimumBounds.x;
        }
        else if(hasExceededRightBound) {
            // Debug.Log("Is over the max bound of the x vector.");
            newPosition.x = maximumBounds.x;
        }
        if(hasExceededTopBound
            && hasExceededBottomBound) {
            // set to mid point
            // Debug.Log("Is over both the min and max bound of the y vector.");
            newPosition.y = (minimumBounds.y + maximumBounds.y) / 2;
        }
        else if(hasExceededTopBound) {
            // Debug.Log("Is over the max bound of the y vector.");
            newPosition.y = maximumBounds.y;
        }
        else if(hasExceededBottomBound) {
            // Debug.Log("Is over the min bound of the y vector.");
            newPosition.y = minimumBounds.y;
        }
        
        return newPosition;
    }
    
    public Vector3 ClampBasedOnCameraBorders(Camera cameraBeingClamped, Vector3 minimumBounds, Vector3 maximumBounds) {
        Vector3 newPosition = cameraBeingClamped.transform.position;
        
        bool hasExceededLeftBound = HasExceededMinimumBound(cameraBeingClamped.GetLeftBoundWorldPosition(), minimumBounds.x);
        bool hasExceededRightBound = HasExceededMaximumBound(cameraBeingClamped.GetRightBoundWorldPosition(), maximumBounds.x);
        bool hasExceededTopBound = HasExceededMaximumBound(cameraBeingClamped.GetTopBoundWorldPosition(), maximumBounds.y);
        bool hasExceededBottomBound = HasExceededMinimumBound(cameraBeingClamped.GetBottomBoundWorldPosition(), minimumBounds.y);
        
        if(hasExceededLeftBound
            && hasExceededRightBound) {
            // set to mid point
            // Debug.Log("Is between both the left and right bound of the x vector.");
            newPosition.x = (minimumBounds.x + maximumBounds.x) / 2;
        }
        else if(hasExceededLeftBound) {
            // Debug.Log("Is over the min bound of the x vector.");
            newPosition.x = minimumBounds.x + cameraBeingClamped.OrthographicWidth() / 2;
        }
        else if(hasExceededRightBound) {
            // Debug.Log("Is over the max bound of the x vector.");
            newPosition.x = maximumBounds.x - cameraBeingClamped.OrthographicWidth() / 2;
        }
        if(hasExceededTopBound
            && hasExceededBottomBound) {
            // Debug.Log("Is over both the min and max bound of the y vector.");
            newPosition.y = (minimumBounds.y + maximumBounds.y) / 2;
        }
        else if(hasExceededTopBound) {
            // Debug.Log("Is over the max bound of the y vector.");
            newPosition.y = maximumBounds.y - cameraBeingClamped.OrthographicHeight() / 2;
        }
        else if(hasExceededBottomBound) {
            // Debug.Log("Is over the min bound of the y vector.");
            newPosition.y = minimumBounds.y + cameraBeingClamped.OrthographicHeight() / 2;
        }
        
        return newPosition;
    }
}