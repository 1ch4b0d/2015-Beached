using UnityEngine;
using System.Collections;

public class SpeechBubble : MonoBehaviour {
    public Animator animatorReference = null;
    public SpeechBubbleImage speechBubbleImage = SpeechBubbleImage.None;
    
    void Awake() {
        Initialize();
    }
    // Use this for initialization
    void Start() {
    }
    
    // Update is called once per frame
    void Update() {
        UpdateAnimator();
    }
    
    protected void Initialize() {
        if(animatorReference == null) {
            Debug.LogError("animatorReference is null, please set it to a reference");
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
}
