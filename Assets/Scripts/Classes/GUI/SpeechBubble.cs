using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubble : MonoBehaviour {
    public Animator animatorReference = null;
    public SpeechBubbleImage speechBubbleImage = SpeechBubbleImage.None;
    public List<UI2DSprite> sprites = null;
    
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
        
		if (sprites == null) {
			sprites = new List<UI2DSprite>();
		}
    }
    
    protected void UpdateAnimator() {
        if(animatorReference != null) {
            // SpeechBubbleImage[] speechBubbleTypes = (SpeechBubbleImage[])SpeechBubbleImage.GetValues(typeof(SpeechBubbleImage));
            foreach(SpeechBubbleImage iterationSpeechBubble in SpeechBubbleImage.GetValues(typeof(SpeechBubbleImage))) {
                animatorReference.SetBool(iterationSpeechBubble.ToString(), false);
            }
            animatorReference.SetBool(speechBubbleImage.ToString(), true);
        }
    }
    
    public SpeechBubble Show() {
        float duration = 1f;
        foreach(UI2DSprite sprite in sprites) {
            // UIPanel panelToModify = this.gameObject.GetComponent<UIPanel>();
            // UIPanel panelToModify = speechBubblePanel.GetComponent<UIPanel>();
            // Go.to(sprite,
            //       duration,
            //       new GoTweenConfig()
            //       .floatProp("alpha", 1f));
            // sprite.alphaTo(sprite, duration, 1f);
            sprite.alphaTo(duration, 1f);
        }
        return this;
    }
    
    public SpeechBubble Hide() {
        // sprite2D
        float duration = 1f;
        foreach(UI2DSprite sprite in sprites) {
            // UIPanel panelToModify = this.gameObject.GetComponent<UIPanel>();
            // UIPanel panelToModify = speechBubblePanel.GetComponent<UIPanel>();
            // Go.to(sprite,
            //       duration,
            //       new GoTweenConfig()
            //       .floatProp("alpha", 0f));
            // UI2DSpriteGoKitTweenExtensions.alphaTo(sprite, duration, 1f);
			sprite.alphaTo(duration, 0f);
        }
        return this;
    }
}
