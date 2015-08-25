using UnityEngine;
using System.Collections;

public class PlayExplosionEvent : CustomEventObject {
    public string explosionAnimationState = "ExplosionOne";
    public Transform explosionTransform = null;
    public Vector3 positionOffset = Vector3.zero;
    public Vector3 scaleOffset = Vector3.zero;
    
    // // Use this for initialization
    // public override void Awake() {
    //     base.Awake();
    // }
    
    // // Use this for initialization
    // public override void Start() {
    //     base.Start();
    // }
    
    // // Update is called once per frame
    // public override void Update() {
    //     base.Update();
    // }
    
    public override void ExecuteLogic() {
        CreateExplosion();
    }
    
    public void CreateExplosion() {
        GameObject explosionGameObject = ExplosionPool.Instance.Issue();
        Animator explosionAnimator = explosionGameObject.GetComponent<Animator>();
        // AnimatorHelper explosionAnimatorHelper = explosionGameObject.GetComponent<AnimatorHelper>();
        
        explosionGameObject.transform.position = (explosionTransform.position + positionOffset);
        explosionGameObject.transform.localScale = (explosionTransform.localScale + scaleOffset);
        
        explosionAnimator.Play(explosionAnimationState);
        
        // IMPORTANT!!!
        // The explosion needs to have an external event created that will
        // decomission the event from the explosion pool
    }
}