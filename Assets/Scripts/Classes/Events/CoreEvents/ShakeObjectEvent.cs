using UnityEngine;
using System.Collections;

///<summary>
/// Used to trigger a camera shake from an event
///<summary>
public class ShakeObjectEvent : CustomEventObject {
    [Tooltip("This is the shake tween that controls the shake")]
    public GoTween shakeTween = null;
    [Tooltip("This is the camera to shake")]
    public GameObject gameObjectToShake = null;
    [Tooltip("The duration of the shake")]
    public float duration = 1f;
    [Tooltip("The magnitude of the shake in each vector component")]
    public Vector3 magnitude = new Vector3(1f, 1f, 0f);
    [Tooltip("The easinig type of the shake")]
    public GoEaseType goEaseType = GoEaseType.Linear;
    [Tooltip("The shake type of the go tween")]
    public GoShakeType goShakeType = GoShakeType.Position;
    [Tooltip("I have no idea what this is")]
    int frameMod = 1;
    [Tooltip("Use the local frame of reference")]
    bool useLocalProperties = false;
    
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
        if(gameObjectToShake == null) {
            this.gameObject.LogComponentError("gameObjectToShake", this.GetType());
        }
    }
    
    public override void ExecuteLogic() {
        PerformObjectShake();
    }
    
    public void PerformObjectShake() {
        if(shakeTween != null) {
            shakeTween.destroy();
            shakeTween = null;
        }
        
        shakeTween = Go.to(gameObjectToShake.transform,
                           duration,
                           new GoTweenConfig().setEaseType(goEaseType).shake(magnitude, goShakeType, frameMod, useLocalProperties)
        .onComplete(complete => {
            FireFinishEvents();
        }));
    }
}