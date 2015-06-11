using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubble : MonoBehaviour {
    public Animator animatorReference = null;
    public SpeechBubbleImage speechBubbleImage = SpeechBubbleImage.None;
    public List<UI2DSprite> sprites = null;
    public List<GoTween> tweens = null;
    
    void Awake() {
        Initialize();
    }
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
        UpdateAnimator();
        DebugInfo();
    }
    
    void DebugInfo() {
        if(Input.GetKeyDown(KeyCode.F)) {
            Debug.Log("Hiding");
            Hide();
        }
        else if(Input.GetKeyDown(KeyCode.G)) {
            Debug.Log("Showing");
            Show();
        }
    }
    
    protected void Initialize() {
        if(animatorReference == null) {
            Debug.LogError("animatorReference is null, please set it to a reference");
        }
        
        if(sprites == null) {
            sprites = new List<UI2DSprite>();
        }
        
        if(tweens == null) {
            tweens = new List<GoTween>();
        }
    }
    
    protected void UpdateAnimator() {
        if(animatorReference != null) {
            foreach(SpeechBubbleImage iterationSpeechBubble in SpeechBubbleImage.GetValues(typeof(SpeechBubbleImage))) {
                animatorReference.SetBool(iterationSpeechBubble.ToString(), false);
            }
            animatorReference.SetBool(speechBubbleImage.ToString(), true);
        }
    }
    
    public void ClearTweens() {
        foreach(GoTween tween in tweens) {
            tween.pause();
            tween.destroy();
        }
        tweens.Clear();
    }
    
    public SpeechBubble Show() {
        ClearTweens();
        
        float duration = 1f;
        foreach(UI2DSprite sprite in sprites) {
            tweens.Add(sprite.alphaTo(duration, 1f));
        }
        
        return this;
    }
    
    public SpeechBubble Hide() {
        ClearTweens();
        
        float duration = 1f;
        foreach(UI2DSprite sprite in sprites) {
            tweens.Add(sprite.alphaTo(duration, 0f));
        }
        return this;
    }
}
