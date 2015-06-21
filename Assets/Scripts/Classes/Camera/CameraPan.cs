using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour {
    public GameObject cameraToPan = null;
    public Vector3 targetPosition = Vector3.zero;
    public Vector3 moveSpeed = new Vector3(1f, 1f, 1f);
    public bool useLocalTransform = false;
    
    // Use this for initialization
    void Start() {
        cameraToPan = this.gameObject;
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public Transform GetCameraTransform() {
        if(useLocalTransform) {
            return cameraToPan.transform;
        }
        else {
            return cameraToPan.transform;
        }
    }
    
    public Vector3 GetCameraPosition() {
        return GetCameraTransform().position;
    }
    
    public void PerformPanLogic() {
        Vector3 movementOffset = CalculateMovementOffset();
        movementOffset *= Time.deltaTime;
        Vector3 nextPosition = PerformMovementLogic(GetCameraPosition(), movementOffset);
        Translate(nextPosition, useLocalTransform);
    }
    
    public Vector3 CalculateMovementOffset() {
        return moveSpeed;
    }
    
    public Vector3 PerformMovementLogic(Vector3 startPosition, Vector3 movementOffset) {
        return startPosition + movementOffset;
    }
    
    public void Translate(Vector3 positionToTranslateTo, bool useLocalTransform) {
        if(useLocalTransform) {
            this.gameObject.transform.localPosition = positionToTranslateTo;
        }
        else {
            this.gameObject.transform.position = positionToTranslateTo;
        }
    }
    
    public void PanTo(Vector3 positionToPanTo) {
        targetPosition = positionToPanTo;
    }
}
