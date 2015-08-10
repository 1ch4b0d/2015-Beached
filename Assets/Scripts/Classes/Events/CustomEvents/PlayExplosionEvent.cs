using UnityEngine;
using System.Collections;

public class PlayExplosionEvent : CustomEventObject {
    public string explosionAnimationState = "ExplosionOne";
    public Transform explosionTransform = null;
    
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
        // GameObject explosionGameObject = Factory.Explosion();
        GameObject explosionGameObject = ExplosionPool.Instance.Issue();
        Animator explosionAnimator = explosionGameObject.GetComponent<Animator>();
        AnimatorHelper explosionAnimatorHelper = explosionGameObject.GetComponent<AnimatorHelper>();
        
        explosionGameObject.transform.position = explosionTransform.position;
        explosionAnimator.Play(explosionAnimationState);
        explosionAnimatorHelper.AddOnAnimationFinish(explosionAnimationState, () => {
            ExplosionPool.Instance.Decomission(explosionGameObject);
        });
        // Don't destroy the explosion object on animation end because it needs
        // to be recycled in the game object pool
    }
}