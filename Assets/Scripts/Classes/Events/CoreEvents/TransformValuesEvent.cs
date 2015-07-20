using UnityEngine;
using System.Collections;

// TODO: I don't know if the rotation and scale components work. Fix this so that
//       all three transformations work
public class TransformValuesEvent : CustomEventObject {
    public Transform transformToModify = null;
    public bool useLocalPosition = false;
    public Transform position = null;
    public bool useLocalRotation = false;
    public Transform rotation = null;
    bool useLocalScale = true; // kept private because localScale is always going to be used
    public Transform scale = null;
    public bool ignoreZComponent = true;
    
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
            Debug.LogError(this.gameObject.name + " needs its 'transformToModify' reference to be set in the 'TransformValuesEvent' Script");
        }
    }
    
    public override void ExecuteLogic() {
        SetTransformValues();
    }
    
    public void SetTransformValues() {
        if(position != null) {
            if(useLocalPosition) {
                Vector3 newLocalPosition = position.position;
                if(ignoreZComponent) {
                    newLocalPosition.z = transformToModify.localPosition.z;
                }
                transformToModify.localPosition = newLocalPosition;
            }
            else {
                Vector3 newPosition = position.position;
                if(ignoreZComponent) {
                    newPosition.z = transformToModify.position.z;
                }
                transformToModify.position = newPosition;
            }
        }
        //----------------------------------------------------------------------
        // I'm like 90% sure this is wrong because of quaternions or something?
        //----------------------------------------------------------------------
        if(rotation != null) {
            if(useLocalRotation) {
                Vector3 newLocalRotation = rotation.eulerAngles;
                if(ignoreZComponent) {
                    newLocalRotation.z = transformToModify.localRotation.z;
                }
                transformToModify.localRotation = Quaternion.Euler(new Vector3(newLocalRotation.x,
                                                                               newLocalRotation.y,
                                                                               newLocalRotation.z));
            }
            else {
                Vector3 newRotation = rotation.eulerAngles;
                if(ignoreZComponent) {
                    newRotation.z = transformToModify.rotation.z;
                }
                transformToModify.rotation = Quaternion.Euler(new Vector3(newRotation.x,
                                                                          newRotation.y,
                                                                          newRotation.z));
            }
        }
        //----------------------------------------------------------------------
        // LocalScale is always used. There is no global scale. I mean there's
        // Lossy scale, but that doesn't work 100%
        if(scale != null) {
            if(useLocalScale) {
                Vector3 newLocalScale = scale.localScale;
                if(ignoreZComponent) {
                    newLocalScale.z = transformToModify.localScale.z;
                }
                transformToModify.localScale = newLocalScale;
            }
        }
    }
}
