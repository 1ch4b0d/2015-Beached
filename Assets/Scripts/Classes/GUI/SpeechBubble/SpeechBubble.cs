using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubble : MonoBehaviour {
    public UIPanel rootPanel = null;
    
    public GameObject speechBubbleGameObject = null;
    public GameObject speechBubbleAnchorGameObject = null;
    
    public Animator animatorReference = null;
    public SpeechBubbleImage speechBubbleImage = SpeechBubbleImage.None;
    public List<GoTween> tweens = null;
    public UILabel label = null;
    public TypewriterEffect labelTypeWriterEffect = null;
    
    public Queue<string> textSet = new Queue<string>();
    public bool isInUse = false;
    public bool hasFinishedTextSet = false;
    public CustomEvents<System.Action> onFinshedTextSet = null;
    
    public SpeechBubbleTrigger speechBubbleTrigger = null;
    
    public static GameObject Create() {
        GameObject newSpeechBubbleGameObject = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/GUI/SpeechBubble/SpeechBubble") as GameObject);
        newSpeechBubbleGameObject.transform.parent = NGUIManager.Instance.UIRoot().gameObject.transform;
        newSpeechBubbleGameObject.transform.localScale = new Vector3(1, 1, 1);
        return newSpeechBubbleGameObject;
    }
    
    void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    void Start() {
        // by default the speech bubble is hidden on start
        Hide(float.Epsilon);
        SetSpeechBubbleImageToDevice();
    }
    
    // Update is called once per frame
    void Update() {
        PerformLogic();
        UpdateAnimator();
        DebugInfo();
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
        if(speechBubbleTrigger == null) {
            speechBubbleTrigger = Utility.GetFirstChildOfType<SpeechBubbleTrigger>(this.gameObject);
            Debug.Log(speechBubbleTrigger);
            speechBubbleTrigger.rootGameObject = rootPanel.gameObject;
            speechBubbleTrigger.speechBubble = this;
        }
        
        labelTypeWriterEffect = label.GetComponent<TypewriterEffect>();
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
        switch(newSpeechBubbleImage) {
        case(SpeechBubbleImage.None):
            break;
        case(SpeechBubbleImage.DesktopInteractButton):
            label.text = "";
            break;
        case(SpeechBubbleImage.PS3InteractButton):
            label.text = "";
            break;
        case(SpeechBubbleImage.TouchInteractButton):
            label.text = "";
            break;
        case(SpeechBubbleImage.VitaInteractButton):
            label.text = "";
            break;
        case(SpeechBubbleImage.WiiInteractButton):
            label.text = "";
            break;
        case(SpeechBubbleImage.XBoxInteractButton):
            label.text = "";
            break;
        default:
            break;
        }
        
        speechBubbleImage = newSpeechBubbleImage;
    }
    
    protected void PerformLogic() {
        PerformHasFinishedCheck();
    }
    
    protected void PerformHasFinishedCheck() {
        if(isInUse
            && !hasFinishedTextSet) {
            if(labelTypeWriterEffect != null
                && !labelTypeWriterEffect.isActive
                && textSet.Count == 0) {
                hasFinishedTextSet = true;
            }
            else {
                if(textSet.Count == 0) {
                    hasFinishedTextSet = true;
                }
            }
        }
    }
    
    public void StartInteraction() {
        isInUse = true;
        SetSpeechBubbleImage(SpeechBubbleImage.None);
    }
    public void FinishInteraction() {
        isInUse = false;
        onFinshedTextSet.Execute();
        SetSpeechBubbleImageToDevice();
    }
    
    public bool HasFinished() {
        return (hasFinishedTextSet && textSet.Count == 0) ? true : false;
    }
    public bool IsInUse() {
        return isInUse;
    }
    
    public SpeechBubble OnFinish(System.Action newOnFinish, bool loop = false) {
        onFinshedTextSet.Add(newOnFinish, loop);
        return this;
    }
    
    public string PopText() {
        if(textSet.Count > 0) {
            return textSet.Dequeue();
        }
        else {
            return label.text;
        }
    }
    
    // Dumb convenience method
    public void MoveToNextText() {
        PopTextAndUpdateSpeechBubbleText();
    }
    
    // I don't know if this is necessarily needed, but fuggit you know
    public void PopTextAndUpdateSpeechBubbleText() {
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
    public void SetSpeechBubbleText(string newText, bool resetTypeWriter = true) {
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
        hasFinishedTextSet = false;
        textSet.Clear();
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