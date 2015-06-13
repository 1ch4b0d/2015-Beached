using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubble : MonoBehaviour {
    UIPanel rootPanel = null;
    
    public Animator animatorReference = null;
    public SpeechBubbleImage speechBubbleImage = SpeechBubbleImage.None;
    public List<GoTween> tweens = null;
    public UILabel label = null;
    
    public Queue<string> textSet = new Queue<string>();
    
    public bool hasFinishedTextSet = false;
    public CustomEvents<System.Action> onFinshedTextSet = null;
    
    void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    void Start() {
        // by default the speech bubble is hidden on start
        Hide(float.Epsilon);
    }
    
    // Update is called once per frame
    void Update() {
        UpdateAnimator();
        DebugInfo();
    }
    
    public bool HasFinished() {
        return false;
    }
    
    protected void Initialize() {
        if(rootPanel == null) {
            rootPanel = this.gameObject.GetComponent<UIPanel>();
        }
        if(animatorReference == null) {
            Debug.LogError("animatorReference is null, please set it to a reference");
        }
        
        if(tweens == null) {
            tweens = new List<GoTween>();
        }
        
        onFinshedTextSet = new CustomEvents<System.Action>();
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
        // Text
        if(Input.GetKeyDown(KeyCode.V)) {
            Debug.Log("Setting Text");
            SetTextSet("testing testing testing");
        }
    }
    
    public void PerformFinishCheck() {
        if(!hasFinishedTextSet) {
            if(textSet.Count == 0) {
            }
        }
    }
    
    public string PopText() {
        if(textSet.Count > 0) {
            return textSet.Dequeue();
        }
        else {
            return label.text;
        }
    }
    
    // I don't know if this is necessarily needed, but fuggit you know
    public void PopTextAndUpdateSpeechBubbleText() {
        TypewriterEffect typewriterEffect = label.GetComponent<TypewriterEffect>();
        
        if(textSet.Count > 0) {
            if(typewriterEffect != null
                && !typewriterEffect.isActive) {
                SetSpeechBubbleText(PopText());
            }
        }
        else {
            if(!hasFinishedTextSet) {
                hasFinishedTextSet = true;
                onFinshedTextSet.Execute();
            }
        }
    }
    
    /// <summary>
    /// This sets the UILabel's text
    /// </summary>
    public void SetSpeechBubbleText(string newText, bool resetTypeWriter = true) {
        TypewriterEffect typewriterEffect = label.GetComponent<TypewriterEffect>();
        
        label.text = newText;
        
        if(typewriterEffect != null
            && resetTypeWriter) {
            typewriterEffect.ResetToBeginning();
        }
    }
    
    /// <summary>
    /// This sets the text set to be iterated through
    /// </summary>
    public void SetTextSet(params string[] newText) {
        hasFinishedTextSet = false;
        textSet.Clear();
        foreach(string text in newText) {
            textSet.Enqueue(text);
        }
        PopTextAndUpdateSpeechBubbleText();
    }
    
    public bool IsHidden() {
        return (rootPanel.alpha > 0f) ? true : false;
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
    
    public SpeechBubble Show(float duration = 1f) {
        ClearTweens();
        tweens.Add(rootPanel.alphaTo(duration, 1f));
        return this;
    }
    
    public SpeechBubble Hide(float duration = 1f) {
        ClearTweens();
        tweens.Add(rootPanel.alphaTo(duration, 0f));
        return this;
    }
}
