using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
    public GameObject cameraGameObject = null;
    
    public Vector3 cameraAnchor = Vector3.zero;
    
    [Tooltip("True when the camera is shaking")]
    public bool isShaking = false;
    [Tooltip("Tracks the elapsed time for a camera shake's duration")]
    public float shakeTimer = 0f;
    public float shakeFrequencyTimer = 0f;
    public float shakeFrequency = 0.1f;
    public float shakeDuration = 1f;
    public float xMagnitude = 0.5f;
    public float yMagnitude = 0.5f;
    
    public delegate void OnCameraShakeFinishDelegate();
    public OnCameraShakeFinishDelegate OnCameraShakeFinish;
    
    // Use this for initialization
    protected void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected void Start() {
    }
    
    // Update is called once per frame
    protected void Update() {
        PerformShakingLogic();
    }
    
    protected void Initialize() {
        if(cameraGameObject == null) {
            cameraGameObject = this.gameObject;
            // Debug.LogError("The 'cameraGameObject' reference needs to be set in the 'CameraShake' Script on " + this.gameObject.name);
        }
    }
    
    public void StartCameraShake() {
        if(!isShaking) {
            cameraAnchor = cameraGameObject.transform.localPosition;
            isShaking = true;
            shakeTimer = 0f;
            shakeFrequencyTimer = 0f;
        }
    }
    
    public void StartCameraShake(float newShakeFrequency, float newShakeDuration, float newXMagnitude, float newYMagnitude) {
        if(!isShaking) {
            shakeFrequency = newShakeFrequency;
            shakeDuration = newShakeDuration;
            xMagnitude = newXMagnitude;
            yMagnitude = newYMagnitude;
            
            cameraAnchor = cameraGameObject.transform.localPosition;
            isShaking = true;
            shakeTimer = 0f;
            shakeFrequencyTimer = 0f;
        }
    }
    public void StopCameraShake() {
        isShaking = false;
    }
    
    void PerformShakingLogic() {
        if(isShaking) {
            shakeTimer += Time.deltaTime;
            shakeFrequencyTimer += Time.deltaTime;
            if(shakeFrequencyTimer > shakeFrequency) {
                Vector3 newCameraShakePositionOffset = Vector3.zero;
                newCameraShakePositionOffset = cameraAnchor + new Vector3(Random.Range(-xMagnitude, xMagnitude),
                                                                          Random.Range(-yMagnitude, yMagnitude),
                                                                          0);
                cameraGameObject.transform.localPosition = newCameraShakePositionOffset;
                shakeFrequencyTimer = 0f;
            }
            
            if(shakeTimer > shakeDuration) {
                isShaking = false;
                cameraGameObject.transform.localPosition = cameraAnchor;
                PerformOnCameraShakeFinishLogic();
            }
        }
    }
    
    public void SetOnCameraShakeFinish(OnCameraShakeFinishDelegate newOnCameraShakeFinish) {
        OnCameraShakeFinish = new OnCameraShakeFinishDelegate(newOnCameraShakeFinish);
    }
    
    public void PerformOnCameraShakeFinishLogic() {
        if(OnCameraShakeFinish != null) {
            OnCameraShakeFinish();
        }
    }
}
