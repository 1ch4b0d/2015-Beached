using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: Refactor this so that it's using the CustomEventsManager for onFinishedTextSet
public class SpeechBubble : MonoBehaviour {
    UIPanel rootPanel = null;
    
    public GameObject speechBubbleGameObject = null;
    public GameObject speechBubbleAnchorGameObject = null;
    
    Animator animatorReference = null;
    SpeechBubbleImage speechBubbleImage = SpeechBubbleImage.None;
    List<GoTween> tweens = null;
    UILabel label = null;
    TypewriterEffect labelTypeWriterEffect = null;
    
    public Queue<string> textSet = new Queue<string>();
    public bool isInUse = false;
    
    public SpeechBubbleInteractionTrigger interactionTrigger = null;
    
    public List<CustomEventsManager> onStartInteraction = null;
    public List<CustomEventsManager> onTextIteration = null;
    public List<CustomEventsManager> onFinishInteraction = null;
    
    public static GameObject Create() {
        return SpeechBubblePool.Instance.Issue();
    }
    
    protected void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected void Start() {
        // by default the speech bubble is hidden on start
        Hide(float.Epsilon);
        SetSpeechBubbleImageToDevice();
    }
    
    // Update is called once per frame
    protected void Update() {
        UpdateAnimator();
        // DebugInfo();
    }
    
    protected void Initialize() {
        //----------------------------------------------------------------------
        // Creates the Speech Bubble
        //----------------------------------------------------------------------
        if(speechBubbleGameObject == null) {
            speechBubbleGameObject = Create();
            Transform labelTransform = speechBubbleGameObject.transform.Find("Sprites/TextPanel/Text");
            if(labelTransform != null) {
                label = labelTransform.gameObject.GetComponent<UILabel>();
                if(label == null) {
                    Debug.LogError("COULD NOT FIND THE SPEECH BUBBLE'S LABEL");
                }
            }
            else {
                Debug.LogError("COULD NOT FIND THE SPEECH BUBBLE'S LABEL GAME OBJECT");
            }
        }
        //----------------------------------------------------------------------
        // Root Panel
        //----------------------------------------------------------------------
        if(rootPanel == null) {
            rootPanel = speechBubbleGameObject.GetComponent<UIPanel>();
            if(rootPanel == null) {
                Debug.LogError("Could not find the speech bubble's: UIPanel");
            }
        }
        //----------------------------------------------------------------------
        // Animator
        //----------------------------------------------------------------------
        if(animatorReference == null) {
            animatorReference = speechBubbleGameObject.GetComponent<Animator>();
            if(animatorReference == null) {
                Debug.LogError("Could not find the speech bubble's: Animator");
            }
        }
        //----------------------------------------------------------------------
        // Tweens
        //----------------------------------------------------------------------
        if(tweens == null) {
            tweens = new List<GoTween>();
        }
        //----------------------------------------------------------------------
        // Configure Speech Bubble's UIFollowTarget
        //----------------------------------------------------------------------
        UIFollowTarget speechBubbleFollowTarget = speechBubbleGameObject.GetComponent<UIFollowTarget>();
        if(speechBubbleFollowTarget != null) {
            if(speechBubbleAnchorGameObject == null) {
                speechBubbleAnchorGameObject = this.gameObject;
            }
            speechBubbleFollowTarget.target = speechBubbleAnchorGameObject.transform;
            speechBubbleFollowTarget.gameCamera = Camera.main;
            speechBubbleFollowTarget.uiCamera = NGUIManager.Instance.Camera();
        }
        else {
            Debug.LogError("Could not find the speech bubble's: UIFollowTarget");
        }
        //----------------------------------------------------------------------
        // Configure Speech Bubble's Trigger If Needed
        //----------------------------------------------------------------------
        // This is optional and does not need to be configured
        if(interactionTrigger == null) {
            interactionTrigger = Utility.GetFirstChildOfType<SpeechBubbleInteractionTrigger>(this.gameObject);
            interactionTrigger.speechBubble = this;
        }
        
        labelTypeWriterEffect = label.GetComponent<TypewriterEffect>();
        
        //----------------------------------------------------------------------
        // Configure Custom Events
        //----------------------------------------------------------------------
        if(onStartInteraction == null) {
            onStartInteraction = new List<CustomEventsManager>();
        }
        if(onTextIteration == null) {
            onTextIteration = new List<CustomEventsManager>();
        }
        if(onFinishInteraction == null) {
            onFinishInteraction = new List<CustomEventsManager>();
        }
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
        
        if(Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log(this.gameObject.transform.GetFullPath() + " - Total Text - #: " + textSet.Count);
            foreach(string text in textSet) {
                Debug.Log("Text: " + text);
            }
        }
    }
    
    // From here:
    // http://docs.unity3d.com/ScriptReference/RuntimePlatform.html
    public void SetSpeechBubbleImageToDevice() {
        switch(Application.platform) {
        case(RuntimePlatform.OSXEditor):
            SetSpeechBubbleImage(SpeechBubbleImage.DesktopInteractButton);
            break;
        case(RuntimePlatform.OSXPlayer):
            SetSpeechBubbleImage(SpeechBubbleImage.DesktopInteractButton);
            break;
        case(RuntimePlatform.WindowsPlayer):
            SetSpeechBubbleImage(SpeechBubbleImage.DesktopInteractButton);
            break;
        case(RuntimePlatform.OSXWebPlayer):
            SetSpeechBubbleImage(SpeechBubbleImage.DesktopInteractButton);
            break;
        case(RuntimePlatform.OSXDashboardPlayer):
            SetSpeechBubbleImage(SpeechBubbleImage.DesktopInteractButton);
            break;
        case(RuntimePlatform.WindowsWebPlayer):
            SetSpeechBubbleImage(SpeechBubbleImage.DesktopInteractButton);
            break;
        case(RuntimePlatform.WindowsEditor):
            SetSpeechBubbleImage(SpeechBubbleImage.DesktopInteractButton);
            break;
        case(RuntimePlatform.IPhonePlayer):
            SetSpeechBubbleImage(SpeechBubbleImage.TouchInteractButton);
            break;
        case(RuntimePlatform.XBOX360):
            SetSpeechBubbleImage(SpeechBubbleImage.XBoxInteractButton);
            break;
        case(RuntimePlatform.PS3):
            SetSpeechBubbleImage(SpeechBubbleImage.PS3InteractButton);
            break;
        case(RuntimePlatform.Android):
            SetSpeechBubbleImage(SpeechBubbleImage.TouchInteractButton);
            break;
        case(RuntimePlatform.LinuxPlayer):
            SetSpeechBubbleImage(SpeechBubbleImage.DesktopInteractButton);
            break;
        case(RuntimePlatform.WebGLPlayer):
            SetSpeechBubbleImage(SpeechBubbleImage.DesktopInteractButton);
            break;
        case(RuntimePlatform.WSAPlayerX86):
            SetSpeechBubbleImage(SpeechBubbleImage.DesktopInteractButton);
            break;
        case(RuntimePlatform.WSAPlayerX64):
            SetSpeechBubbleImage(SpeechBubbleImage.DesktopInteractButton);
            break;
        case(RuntimePlatform.WSAPlayerARM):
            SetSpeechBubbleImage(SpeechBubbleImage.DesktopInteractButton);
            break;
        case(RuntimePlatform.WP8Player):
            SetSpeechBubbleImage(SpeechBubbleImage.DesktopInteractButton);
            break;
        case(RuntimePlatform.TizenPlayer):
            SetSpeechBubbleImage(SpeechBubbleImage.DesktopInteractButton);
            break;
        case(RuntimePlatform.PSP2):
            SetSpeechBubbleImage(SpeechBubbleImage.PS3InteractButton);
            break;
        case(RuntimePlatform.PS4):
            SetSpeechBubbleImage(SpeechBubbleImage.PS3InteractButton);
            break;
        case(RuntimePlatform.XboxOne):
            SetSpeechBubbleImage(SpeechBubbleImage.XBoxInteractButton);
            break;
        case(RuntimePlatform.SamsungTVPlayer):
            SetSpeechBubbleImage(SpeechBubbleImage.DesktopInteractButton);
            break;
        default:
            SetSpeechBubbleImage(SpeechBubbleImage.DesktopInteractButton);
            break;
        }
    }
    
    public void SetSpeechBubbleImage(SpeechBubbleImage newSpeechBubbleImage) {
        if(newSpeechBubbleImage != SpeechBubbleImage.None) {
            label.text = "";
        }
        
        speechBubbleImage = newSpeechBubbleImage;
    }
    
    public void StartInteraction() {
        SetInUse(true);
        SetSpeechBubbleImage(SpeechBubbleImage.None);
        FireStartInteractionEvents();
    }
    public void FinishInteraction() {
        // Debug.Log("Finished Interaction");
        SetInUse(false);
        SetSpeechBubbleImageToDevice();
        // You need to finish the typewriter effect first before setting it to
        // an empty string
        labelTypeWriterEffect.Finish();
        label.text = "";
        
        // Debug.Log(this.gameObject.name + " executed OnSpeechBubbleFinish events.");
        FireFinishInteractionEvents();
    }
    
    public bool HasFinishedInteraction() {
        return (HasFinishedDisplayingText() && textSet.Count == 0) ? true : false;
    }
    
    public void SetInUse(bool newInUse) {
        isInUse = newInUse;
    }
    public bool IsInUse() {
        return isInUse;
    }
    
    public bool HasFinishedDisplayingText() {
        bool hasFinishedDisplayingText = false;
        if(IsInUse()) {
            if(labelTypeWriterEffect != null) {
                if(!labelTypeWriterEffect.isActive) {
                    hasFinishedDisplayingText = true;
                }
            }
        }
        return hasFinishedDisplayingText;
    }
    
    private string PopText() {
        string returnString = label.text;
        if(textSet.Count > 0) {
            returnString = textSet.Dequeue();
        }
        // Debug.Log("Pop! " + returnString);
        return returnString;
    }
    
    // Dumb convenience method
    public void MoveToNextText() {
        PopTextAndUpdateSpeechBubbleText();
        FireTextIterationEvents();
    }
    
    // I don't know if this is necessarily needed, but fuggit you know
    // lol cyclomatic complexity
    private void PopTextAndUpdateSpeechBubbleText() {
        // Pops the next text node
        if(textSet.Count > 0) {
            if(labelTypeWriterEffect != null) {
                if(!labelTypeWriterEffect.isActive) {
                    SetSpeechBubbleText(PopText());
                }
            }
            else {
                SetSpeechBubbleText(PopText());
            }
        }
    }
    
    /// <summary>
    /// This sets the UILabel's text
    /// </summary>
    private void SetSpeechBubbleText(string newText, bool resetTypeWriter = true) {
        label.text = newText;
        
        if(labelTypeWriterEffect != null
            && resetTypeWriter) {
            labelTypeWriterEffect.ResetToBeginning();
        }
    }
    
    /// <summary>
    /// This sets the text set to be iterated through
    /// </summary>
    public SpeechBubble SetTextSet(params string[] newText) {
        ClearText();
        
        foreach(string text in newText) {
            textSet.Enqueue(text);
        }
        
        PopTextAndUpdateSpeechBubbleText();
        return this;
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
    
    public void ClearText() {
        textSet.Clear();
        label.text = "";
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
    
    public virtual void FireStartInteractionEvents() {
        if(onStartInteraction != null) {
            // .ToArray() used in order to get copy of elements
            foreach(CustomEventsManager customEventsManager in onStartInteraction.ToArray()) {
                customEventsManager.Execute();
            }
        }
    }
    
    public virtual void FireTextIterationEvents() {
        if(onTextIteration != null) {
            // .ToArray() used in order to get copy of elements
            foreach(CustomEventsManager customEventsManager in onTextIteration.ToArray()) {
                customEventsManager.Execute();
            }
        }
    }
    
    public virtual void FireFinishInteractionEvents() {
        if(onFinishInteraction != null) {
            // .ToArray() used in order to get copy of elements
            foreach(CustomEventsManager customEventsManager in onFinishInteraction.ToArray()) {
                customEventsManager.Execute();
            }
        }
    }
}
