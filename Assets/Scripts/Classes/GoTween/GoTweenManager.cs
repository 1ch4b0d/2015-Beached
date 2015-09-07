using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoTweenManager : MonoBehaviour {
    public List<GoTween> goTweens = null;
    
    // Use this for initialization
    protected virtual void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected virtual void Start() {
    }
    
    // Update is called once per frame
    protected virtual void Update() {
        PruneInvalidTweens();
    }
    
    // Update is called once per frame
    protected virtual void Initialize() {
        goTweens = new List<GoTween>();
    }
    
    // There has got to be a shorter more concise way to do this
    protected virtual void PruneInvalidTweens() {
        List<GoTween> tweensToPrune = new List<GoTween>();
        foreach(GoTween goTween in goTweens) {
            if(goTween == null) {
                tweensToPrune.Add(goTween);
            }
        }
        
        foreach(GoTween goTween in tweensToPrune) {
            goTweens.Remove(goTween);
        }
    }
    
    public void Add(GoTween newGoTween) {
        goTweens.Add(newGoTween);
    }
    
    public void DestroyTweens() {
        foreach(GoTween goTween in goTweens) {
            if(goTween != null) {
                goTween.destroy();
            }
        }
        // PruneInvalidTweens();
    }
    
    public void CompleteTweens() {
        foreach(GoTween goTween in goTweens) {
            if(goTween != null) {
                goTween.complete();
            }
        }
        // PruneInvalidTweens();
    }
}