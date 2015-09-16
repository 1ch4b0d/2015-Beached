using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This class is so tightly coupled it hurts....
// Makes the assumption that the speech bubble image has the pivot set to the bottom
public class SpeechBubble : MonoBehaviour {
    public GameObject speechBubbleGameObject = null;
    public GameObject speechBubbleAnchorGameObject = null;
    public UIPanel container = null;
    public UIPanel controllerButtonPanel = null;
    public UI2DSprite controllerButtonSprite = null;
    public UIPanel speechBubblePanel = null;
    public UI2DSprite speechBubbleSprite = null;
    
    Animator animatorReference = null;
    SpeechBubbleImage speechBubbleImage = SpeechBubbleImage.None;
    List<GoTween> tweens = null;
    UILabel label = null;
    TypewriterEffect labelTypeWriterEffect = null;
    
    public Queue<string> textSet = new Queue<string>();
    public bool isInUse = false;
    
    public SpeechBubbleInteractionTrigger interactionTrigger = null;
    
    // private string rootContainerName = "RootContainer";
    private string textLabelName = "Text";
    private string controllerButtonSpriteContainerName = "ButtonSpriteContainer";
    private string controllerButtonSpriteName = "ButtonSprite";
    private string speechBubbleSpriteContainerName = "SpeechBubbleSpriteContainer";
    private string speechBubbleSpriteName = "SpeechBubbleSprite";
    public Vector2 controllerButtonBubbleSize = new Vector2(100, 100);
    public Vector2 controllerButtonSize = new Vector2(100, 100);
    public Vector2 textBubleWidthMinAndMax = new Vector2(100, 200);
    public Vector2 textBubbleHeightMinAndMax = new Vector2(100, 200);
    public GoEaseType horizontalScaleOutEaseType = GoEaseType.Linear;
    public GoEaseType verticalScaleOutEaseType = GoEaseType.Linear;
    public GoEaseType horizontalScaleInEaseType = GoEaseType.Linear;
    public GoEaseType verticalScaleInEaseType = GoEaseType.Linear;
    
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
        // Hide(float.Epsilon, float.Epsilon);
        SetSpeechBubbleImageToDevice();
    }
    
    // Update is called once per frame
    protected void Update() {
        UpdateAnimator();
        DebugInfo();
    }
    
    protected void Initialize() {
        //----------------------------------------------------------------------
        // Creates the Speech Bubble
        //----------------------------------------------------------------------
        if(speechBubbleGameObject == null) {
            speechBubbleGameObject = Create();
        }
        //----------------------------------------------------------------------
        // Root Panel
        //----------------------------------------------------------------------
        if(container == null) {
            // GameObject rootPanelGameObject = speechBubbleGameObject.FindInChildren(rootContainerName);
            // if(rootPanelGameObject != null) {
            container = speechBubbleGameObject.GetComponent<UIPanel>();
            if(container == null) {
                this.gameObject.LogComponentError("container", this.GetType());
            }
            // }
            // else {
            //     this.gameObject.LogComponentError("containerPanelGameObject", this.GetType());
            // }
        }
        // Sets the size just in case, I don't know.
        container.SetRect(0f, 0f, controllerButtonBubbleSize.x, controllerButtonBubbleSize.y);
        
        //----------------------------------------------------------------------
        // Controller Button Sprite Container Panel
        //----------------------------------------------------------------------
        if(controllerButtonPanel == null) {
            GameObject controllerButtonPanelGameObject = speechBubbleGameObject.FindInChildren(controllerButtonSpriteContainerName);
            if(controllerButtonPanelGameObject != null) {
                controllerButtonPanel = controllerButtonPanelGameObject.GetComponent<UIPanel>();
                if(controllerButtonPanel == null) {
                    this.gameObject.LogComponentError("controllerButtonPanel", this.GetType());
                }
            }
            else {
                this.gameObject.LogComponentError("containerPanelGameObject", this.GetType());
            }
        }
        
        //----------------------------------------------------------------------
        // Controller Button Sprite
        //----------------------------------------------------------------------
        if(controllerButtonSprite == null) {
            GameObject controllerButtonSpriteGameObject = speechBubbleGameObject.FindInChildren(controllerButtonSpriteName);
            if(controllerButtonSpriteGameObject != null) {
                controllerButtonSprite = controllerButtonSpriteGameObject.GetComponent<UI2DSprite>();
                if(controllerButtonSprite == null) {
                    this.gameObject.LogComponentError("controllerButtonPanel", this.GetType());
                }
            }
            else {
                this.gameObject.LogComponentError("containerPanelGameObject", this.GetType());
            }
        }
        
        //----------------------------------------------------------------------
        // Speech Bubble Container Panel
        //----------------------------------------------------------------------
        if(speechBubblePanel == null) {
            GameObject speechBubblePanelGameObject = speechBubbleGameObject.FindInChildren(speechBubbleSpriteContainerName);
            if(speechBubblePanelGameObject != null) {
                speechBubblePanel = speechBubblePanelGameObject.GetComponent<UIPanel>();
                if(speechBubblePanel == null) {
                    this.gameObject.LogComponentError("speechBubblePanel", this.GetType());
                }
            }
            else {
                this.gameObject.LogComponentError("containerPanelGameObject", this.GetType());
            }
        }
        
        //----------------------------------------------------------------------
        // Speech Bubble Sprite
        //----------------------------------------------------------------------
        if(speechBubbleSprite == null) {
            GameObject speechBubbleSpriteGameObject = speechBubbleGameObject.FindInChildren(speechBubbleSpriteName);
            if(speechBubbleSpriteGameObject != null) {
                speechBubbleSprite = speechBubbleSpriteGameObject.GetComponent<UI2DSprite>();
                if(speechBubbleSprite == null) {
                    this.gameObject.LogComponentError("speechBubbleSprite", this.GetType());
                }
            }
            else {
                this.gameObject.LogComponentError("containerPanelGameObject", this.GetType());
            }
        }
        
        //----------------------------------------------------------------------
        // Label
        //----------------------------------------------------------------------
        if(label == null) {
            GameObject labelGameObject = speechBubbleGameObject.FindInChildren(textLabelName);
            if(labelGameObject != null) {
                label = labelGameObject.GetComponent<UILabel>();
                if(label == null) {
                    this.gameObject.LogComponentError("label", this.GetType());
                }
            }
            else {
                this.gameObject.LogComponentError("labelGameObject", this.GetType());
            }
        }
        //----------------------------------------------------------------------
        // Animator
        //----------------------------------------------------------------------
        if(animatorReference == null) {
            animatorReference = speechBubbleGameObject.GetComponent<Animator>();
            if(animatorReference == null) {
                this.gameObject.LogComponentError("animator", this.GetType());
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
            this.gameObject.LogComponentError("uiFollowTarget", this.GetType());
        }
        //----------------------------------------------------------------------
        // Configure Speech Bubble's Trigger If Needed
        //----------------------------------------------------------------------
        // This is optional and does not need to be configured
        if(interactionTrigger == null) {
            interactionTrigger = this.gameObject.GetFirstChildOfType<SpeechBubbleInteractionTrigger>();
            if(interactionTrigger != null) {
                interactionTrigger.speechBubble = this;
            }
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
            Hide(0.25f, 0.25f);
        }
        else if(Input.GetKeyDown(KeyCode.G)) {
            Debug.Log("Showing");
            Show(0.25f, 0.25f);
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
        return (container.alpha > 0f) ? true : false;
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
    
    public SpeechBubble Show(float horizontalScaleDuration, float verticalScaleDuration) {
        // clears the tweens
        this.gameObject.DestroyGoTweens();
        
        ScaleVerticalThenHorizontal(speechBubbleSprite, (int)controllerButtonBubbleSize.x, (int)controllerButtonBubbleSize.y, horizontalScaleOutEaseType, verticalScaleOutEaseType, horizontalScaleDuration, verticalScaleDuration);
        ScaleVerticalThenHorizontal(controllerButtonSprite, (int)controllerButtonSize.x, (int)controllerButtonSize.y, horizontalScaleOutEaseType, verticalScaleOutEaseType, horizontalScaleDuration, verticalScaleDuration);
        
        return this;
    }
    
    public SpeechBubble Hide(float horizontalScaleDuration, float verticalScaleDuration) {
        // clears the tweens
        this.gameObject.DestroyGoTweens();
        
        ScaleHorizontalThenVertical(speechBubbleSprite, 0, 0, horizontalScaleInEaseType, verticalScaleInEaseType, horizontalScaleDuration, verticalScaleDuration);
        ScaleHorizontalThenVertical(controllerButtonSprite, 0, 0, horizontalScaleInEaseType, verticalScaleInEaseType, horizontalScaleDuration, verticalScaleDuration);
        
        return this;
    }
    
    public void ScaleHorizontalThenVertical(UIWidget widget, int width, int height, GoEaseType horizontalScaleEaseType, GoEaseType verticalScaleEaseType, float horizontalScaleDuration, float verticalScaleDuration) {
        // second
        GoTweenConfig scaleVerticalTweenConfig = new GoTweenConfig()
        .intProp("height", height)
        .setEaseType(verticalScaleEaseType)
        .onComplete(complete => {
            // do nothing
        });
        // first
        GoTweenConfig scaleHorizontalTweenConfig = new GoTweenConfig()
        .intProp("width", width)
        .setEaseType(horizontalScaleEaseType)
        .onComplete(complete => {
            // scale vertical
            this.gameObject.AddGoTween(Go.to(widget,
                                             verticalScaleDuration,
                                             scaleVerticalTweenConfig));
        });
        
        // scale horizontal
        this.gameObject.AddGoTween(Go.to(widget,
                                         horizontalScaleDuration,
                                         scaleHorizontalTweenConfig));
    }
    public void ScaleVerticalThenHorizontal(UIWidget widget, int width, int height, GoEaseType horizontalScaleEaseType, GoEaseType verticalScaleEaseType, float horizontalScaleDuration, float verticalScaleDuration) {
        // second
        GoTweenConfig scaleHorizontalTweenConfig = new GoTweenConfig()
        .intProp("width", width)
        .setEaseType(horizontalScaleEaseType)
        .onComplete(complete => {
            // do nothing
        });
        // first
        GoTweenConfig scaleVerticalTweenConfig = new GoTweenConfig()
        .intProp("height", height)
        .setEaseType(verticalScaleEaseType)
        .onComplete(complete => {
            // scale horizontal
            this.gameObject.AddGoTween(Go.to(widget,
                                             horizontalScaleDuration,
                                             scaleHorizontalTweenConfig));
        });
        
        // scale vertical
        this.gameObject.AddGoTween(Go.to(widget,
                                         verticalScaleDuration,
                                         scaleVerticalTweenConfig));
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
