using UnityEngine;
using System.Collections;

///<summary>
/// Used to trigger a camera shake from an event
///<summary>
public class ShakeObjectEvent : CustomEventObject {
    [Tooltip("This is the camera to shake")]
    public GoTween shakeTween = null;
    public GameObject gameObjectToShake = null;
    public float duration = 1f;
    public float shakeFrequency = 0.1f;
    public Vector3 magnitude = new Vector3(1f, 1f, 0f);
    public GoEaseType goEaseType = GoEaseType.Linear;
    public GoShakeType goShakeType = GoShakeType.Position;
    int frameMod = 1;
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
            Debug.LogError("The 'gameObjectToShake' reference needs to be set in the 'ShakeObjectEvent' Script on " + this.gameObject.name);
        }
    }
    
    public override void ExecuteLogic() {
        PerformObjectShake();
    }
    
    public void PerformObjectShake() {
        Debug.Log("Watch yo'self, back dat ass up");
        if(shakeTween != null) {
            shakeTween.destroy();
            shakeTween = null;
        }
        
        shakeTween = Go.to(gameObjectToShake.transform,
                           duration,
                           new GoTweenConfig().setEaseType(GoEaseType.Linear).shake(magnitude, goShakeType, frameMod, useLocalProperties)
        .onComplete(complete => {
            FireFinishEvents();
        }));
    }
}