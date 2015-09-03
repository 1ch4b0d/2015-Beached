using UnityEngine;
using System.Collections;

// TODO: I don't know if the rotation and scale components work. Fix this so that
//       all three transformations work
public class TransformValuesEvent : CustomEventObject {
    public Transform transformToModify = null;
    public Transform finalPosition = null;
    public Vector3 positionOffset = Vector3.zero;
    public bool useLocalPosition = false;
    public bool excludeXPositionComponent = false;
    public bool excludeYPositionComponent = false;
    public bool excludeZPositionComponent = false;
    public Transform finalRotation = null;
    public Vector3 rotationOffset = Vector3.zero;
    public bool useLocalRotation = false;
    public bool excludeXRotationComponent = false;
    public bool excludeYRotationComponent = false;
    public bool excludeZRotationComponent = false;
    public Transform finalScale = null;
    bool useLocalScale = true; // kept private because localScale is always going to be used
    public Vector3 scaleOffset = Vector3.zero;
    public bool excludeXScaleComponent = false;
    public bool excludeYScaleComponent = false;
    public bool excludeZScaleComponent = false;
    
    // // Use this for initialization
    // protected override void Awake() {
    //     base.Awake();
    // }
    
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
        if(transformToModify == null) {
            this.gameObject.LogComponentError("transformToModify", this.GetType());
        }
    }
    
    public override void ExecuteLogic() {
        SetTransformValues();
    }
    
    public void SetTransformValues() {
        if(finalPosition != null) {
            Vector3 newPosition = Vector3.zero;
            Vector3 originalPosition = (useLocalPosition) ? transformToModify.localPosition : transformToModify.position;
            Vector3 tempPosition = (useLocalPosition) ? finalPosition.localPosition : finalPosition.position;
            newPosition = new Vector3((excludeXPositionComponent) ? originalPosition.x : tempPosition.x,
                                      (excludeYPositionComponent) ? originalPosition.y : tempPosition.y,
                                      (excludeZPositionComponent) ? originalPosition.z : tempPosition.z);
            newPosition += positionOffset;
            transformToModify.position = newPosition;
        }
        //----------------------------------------------------------------------
        // I'm like 90% sure this is wrong because of quaternions or something?
        //----------------------------------------------------------------------
        
        //----------------------------------------------------------------------
        // LocalScale is always used. There is no global scale. I mean there's
        // Lossy scale, but that doesn't work 100%
        if(finalScale != null) {
            if(useLocalScale) {
                Vector3 newLocalScale = finalScale.localScale;
                if(excludeZScaleComponent) {
                    newLocalScale.z = transformToModify.localScale.z;
                }
                transformToModify.localScale = newLocalScale;
            }
        }
    }
}
