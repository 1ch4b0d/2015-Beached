using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This class is so tightly coupled it hurts....
// This is garbage. Rewrite this better so that it reflects the correct transitions
// between how it's displaying and how information should be displayed
// Makes the assumption that the speech bubble image has the pivot set to the bottom
public class SpeechBubble : MonoBehaviour {
    public GameObject speechBubbleGameObject = null;
    public GameObject speechBubbleAnchorGameObject = null;
    public UIPanel rootContainer = null;
    public UI2DSprite controllerButtonSprite = null;
    public UI2DSprite speechBubbleSprite = null;
    public UI2DSprite speechBubbleTailSprite = null;
    public UIScrollView scrollView = null;
    public UIPanel textPanel = null;
    public UILabel uiLabelText = null;
    private TypewriterEffect labelTypeWriterEffect = null;
    
    Animator animatorReference = null;
    public SpeechBubbleImage speechBubbleImage = SpeechBubbleImage.None;
    
    public Queue<string> textSet = new Queue<string>();
    public bool hasFinishedShowing = false; // used to prevent starting interaction before show finishes
    public bool isDisplayed = false; // used to determine when the speech bubble is started so that the show and hide function are not called more than once
    public bool isDisplayingText = false;
    public bool isInUse = false;
    
    public SpeechBubbleInteractionTrigger interactionTrigger = null;
    
    // private string rootContainerName = "RootContainer";
    private string textPanelName = "TextPanel";
    private string textLabelName = "Text";
    private string controllerButtonSpriteName = "ButtonSprite";
    private string speechBubbleSpriteName = "SpeechBubbleSprite";
    private string speechBubbleTailSpriteName = "SpeechBubbleTailSprite";
    public Vector2 controllerButtonBubbleSize = new Vector2(100, 100);
    public Vector2 controllerButtonSize = new Vector2(100, 100);
    public float controllerButtonFadeDuration = 0.25f;
    public Vector2 tailSize = new Vector2(30, 10);
    public Vector2 textBubbleWidthMinAndMax = new Vector2(100, 200);
    public Vector2 textBubbleHeightMinAndMax = new Vector2(100, 200);
    public GoEaseType horizontalScaleOutEaseType = GoEaseType.Linear;
    public GoEaseType verticalScaleOutEaseType = GoEaseType.Linear;
    public GoEaseType horizontalScaleInEaseType = GoEaseType.Linear;
    public GoEaseType verticalScaleInEaseType = GoEaseType.Linear;
    public Color defaultTextColor = Color.black;
    
    public List<CustomEventsManager> onHideFinish = null;
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
        Hide(float.Epsilon, float.Epsilon);
        SetSpeechBubbleImageToDevice();
    }
    
    // Update is called once per frame
    protected void Update() {
        // Resize Logic goes here
        if(IsInUse()
            && !HasFinishedInteraction()) {
            // Debug.Log("---------------------");
            // Debug.Log("Speech Bubble Sprite:");
            // Debug.Log("Width: " + speechBubbleSprite.width + "\nHeight: " + speechBubbleSprite.height);
            // Debug.Log("Text Label:");
            // Debug.Log("Width: " + uiLabelText.width + "\nHeight: " + uiLabelText.height);
            
            // accounts for anchors
            int labelHeight = uiLabelText.height - textPanel.topAnchor.absolute + textPanel.bottomAnchor.absolute;
            // Debug.Log("CalculatedHeight: " + labelHeight);
            // automatically handles scaling speech bubble with text
            if(labelHeight >= speechBubbleSprite.height) {
                if(speechBubbleSprite.height < textBubbleHeightMinAndMax.y) {
                    float binarySearch = ((labelHeight - speechBubbleSprite.height) / 2f);
                    if(binarySearch > 1) {
                        speechBubbleSprite.height += (int)binarySearch;
                    }
                    else {
                        speechBubbleSprite.height = labelHeight;
                    }
                    speechBubbleSprite.height = (int)Mathf.Clamp(speechBubbleSprite.height, textBubbleHeightMinAndMax.x, textBubbleHeightMinAndMax.y);
                }
            }
            else {
                // shrink back down if greater than min, and greater than text
                if(speechBubbleSprite.height >= textBubbleHeightMinAndMax.x
                    && speechBubbleSprite.height > labelHeight) {
                    float binarySearch = ((speechBubbleSprite.height - labelHeight) / 2f);
                    if(binarySearch > 1) {
                        speechBubbleSprite.height -= (int)binarySearch;
                    }
                    else {
                        speechBubbleSprite.height = labelHeight;
                    }
                    speechBubbleSprite.height = (int)Mathf.Clamp(speechBubbleSprite.height, textBubbleHeightMinAndMax.x, textBubbleHeightMinAndMax.y);
                }
            }
        }
        
        UpdateAnimator();
        // DebugInfo();
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
        if(rootContainer == null) {
            rootContainer = speechBubbleGameObject.GetComponent<UIPanel>();
            if(rootContainer == null) {
                this.gameObject.LogComponentError("rootContainer", this.GetType());
            }
        }
        // Sets the size just in case, I don't know.
        rootContainer.SetRect(0f, 0f, controllerButtonBubbleSize.x, controllerButtonBubbleSize.y);
        
        //----------------------------------------------------------------------
        // Controller Button Sprite
        //----------------------------------------------------------------------
        if(controllerButtonSprite == null) {
            GameObject controllerButtonSpriteGameObject = speechBubbleGameObject.FindInChildren(controllerButtonSpriteName);
            if(controllerButtonSpriteGameObject != null) {
                controllerButtonSprite = controllerButtonSpriteGameObject.GetComponent<UI2DSprite>();
                if(controllerButtonSprite == null) {
                    this.gameObject.LogComponentError("controllerButtonSprite", this.GetType());
                }
            }
            else {
                this.gameObject.LogComponentError("controllerButtonSpriteGameObject", this.GetType());
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
                this.gameObject.LogComponentError("speechBubbleSpriteGameObject", this.GetType());
            }
        }
        //----------------------------------------------------------------------
        // Speech Bubble Tail Sprite
        //----------------------------------------------------------------------
        if(speechBubbleTailSprite == null) {
            GameObject speechBubbleTailSpriteGameObject = speechBubbleGameObject.FindInChildren(speechBubbleTailSpriteName);
            if(speechBubbleTailSpriteGameObject != null) {
                speechBubbleTailSprite = speechBubbleTailSpriteGameObject.GetComponent<UI2DSprite>();
                if(speechBubbleTailSprite == null) {
                    this.gameObject.LogComponentError("speechBubbleTailSprite", this.GetType());
                }
            }
            else {
                this.gameObject.LogComponentError("speechBubbleTailSpriteGameObject", this.GetType());
            }
        }
        
        //----------------------------------------------------------------------
        // Text Panel
        //----------------------------------------------------------------------
        if(textPanel == null) {
            GameObject textPanelGameObject = speechBubbleGameObject.FindInChildren(textPanelName);
            if(textPanelGameObject != null) {
                textPanel = textPanelGameObject.GetComponent<UIPanel>();
                if(textPanel == null) {
                    this.gameObject.LogComponentError("textPanel", this.GetType());
                }
                scrollView = textPanelGameObject.GetComponent<UIScrollView>();
                if(scrollView == null) {
                    this.gameObject.LogComponentError("scrollView", this.GetType());
                }
            }
            else {
                this.gameObject.LogComponentError("textPanelGameObject", this.GetType());
            }
        }
        //----------------------------------------------------------------------
        // UILabel Text
        //----------------------------------------------------------------------
        if(uiLabelText == null) {
            GameObject labelGameObject = speechBubbleGameObject.FindInChildren(textLabelName);
            if(labelGameObject != null) {
                uiLabelText = labelGameObject.GetComponent<UILabel>();
                if(uiLabelText == null) {
                    this.gameObject.LogComponentError("uiLabelText", this.GetType());
                }
            }
            else {
                this.gameObject.LogComponentError("labelGameObject", this.GetType());
            }
        }
        uiLabelText.color = defaultTextColor;
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
            interactionTrigger = this.gameObject.GetFirstChild<SpeechBubbleInteractionTrigger>();
            if(interactionTrigger != null) {
                interactionTrigger.speechBubble = this;
            }
        }
        
        labelTypeWriterEffect = uiLabelText.GetComponent<TypewriterEffect>();
        
        //----------------------------------------------------------------------
        // Configure Custom Events
        //----------------------------------------------------------------------
        if(onHideFinish == null) {
            onHideFinish = new List<CustomEventsManager>();
        }
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
            uiLabelText.text = "";
        }
        
        speechBubbleImage = newSpeechBubbleImage;
    }
    
    public void StartInteraction(params string[] newTextSet) {
        // Debug.Log("Starting Interaction.");
        this.gameObject.DestroyGoTweens();
        
        // this is done to enable the controller button to fade on start interaction
        if(newTextSet.Length >= 0) {
            textSet.Clear();
        }
        
        // root container - this is done to guarantee that cinematics work so that this won't be hidden when start interaction is called
        GoTweenConfig rootAlphaTweenConfig = new GoTweenConfig()
        .floatProp("alpha", 1)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
        });
        // scale horizontal
        GoTweenConfig horizontalScaleTweenConfig = new GoTweenConfig()
        .intProp("width", (int)textBubbleWidthMinAndMax.x)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
        });
        // scale vertical
        GoTweenConfig verticalScaleTweenConfig = new GoTweenConfig()
        .intProp("height", (int)textBubbleHeightMinAndMax.x)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
        });
        //-----------------------------------
        // Button Fade
        //-----------------------------------
        GoTweenConfig fadeButtonTweenConfig = new GoTweenConfig()
        .floatProp("alpha", 0)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
            // Pretty sure setting the button image isn't needed now
            // SetSpeechBubbleImage(SpeechBubbleImage.None);
            if(newTextSet.Length >= 0) {
                SetTextSet(newTextSet);
            }
            SetIsDisplayingText(true);
            FireStartInteractionEvents();
        });
        this.gameObject.AddGoTween(Go.to(speechBubbleSprite,
                                         controllerButtonFadeDuration,
                                         horizontalScaleTweenConfig));
        this.gameObject.AddGoTween(Go.to(speechBubbleSprite,
                                         controllerButtonFadeDuration,
                                         verticalScaleTweenConfig));
        this.gameObject.AddGoTween(Go.to(controllerButtonSprite,
                                         controllerButtonFadeDuration,
                                         fadeButtonTweenConfig));
        // scale horizontal
        this.gameObject.AddGoTween(Go.to(rootContainer,
                                         controllerButtonFadeDuration,
                                         rootAlphaTweenConfig));
        // this is done to enable the controller button to fade on start interaction
        SetIsInUse(true);
    }
    
    public void FinishInteraction() {
        // Debug.Log("Finishing Interaction.");
        this.gameObject.DestroyGoTweens();
        
        SetSpeechBubbleImageToDevice();
        
        // You need to finish the typewriter effect first before setting it to
        // an empty string
        labelTypeWriterEffect.Finish();
        // uiLabelText.text = "";
        
        // scale horizontal
        GoTweenConfig horizontalScaleTweenConfig = new GoTweenConfig()
        .intProp("width", (int)controllerButtonBubbleSize.x)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
        });
        // scale vertical
        GoTweenConfig verticalScaleTweenConfig = new GoTweenConfig()
        .intProp("height", (int)controllerButtonBubbleSize.y)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
        });
        this.gameObject.AddGoTween(Go.to(speechBubbleSprite,
                                         controllerButtonFadeDuration,
                                         horizontalScaleTweenConfig));
        this.gameObject.AddGoTween(Go.to(speechBubbleSprite,
                                         controllerButtonFadeDuration,
                                         verticalScaleTweenConfig));
                                         
        //-----------------------------------
        // Button Fade
        //-----------------------------------
        GoTweenConfig fadeButtonTweenConfig = new GoTweenConfig()
        .floatProp("alpha", 1f)
        .setEaseType(GoEaseType.Linear)
        .setDelay(0.15f)
        .onComplete(complete => {
            // Debug.Log(this.gameObject.name + " executed OnSpeechBubbleFinish events.");
            SetIsInUse(false);
        });
        this.gameObject.AddGoTween(Go.to(controllerButtonSprite,
                                         controllerButtonFadeDuration,
                                         fadeButtonTweenConfig));
        FireFinishInteractionEvents();
        SetIsDisplayingText(false);
    }
    
    public bool HasFinishedInteraction() {
        // Debug.Log("HasFinishedDisplayingText: " + HasFinishedDisplayingText() + "\n" + "Text Set Size: " + textSet.Count);
        return (HasFinishedDisplayingText() && textSet.Count == 0) ? true : false;
    }
    
    public void SetIsInUse(bool newInUse) {
        isInUse = newInUse;
    }
    public bool IsInUse() {
        return isInUse;
    }
    public void SetFacing(bool isFacingRight) {
        // Debug.Log("Setting Facing");
        if(isFacingRight) {
            if(speechBubbleTailSprite.gameObject.transform.localScale.x < 0) {
                speechBubbleTailSprite.gameObject.transform.localScale = new Vector3((speechBubbleTailSprite.gameObject.transform.localScale.x * -1),
                                                                                     speechBubbleTailSprite.gameObject.transform.localScale.y,
                                                                                     speechBubbleTailSprite.gameObject.transform.localScale.z);
            }
        }
        // facing left
        else {
            if(speechBubbleTailSprite.gameObject.transform.localScale.x > 0) {
                speechBubbleTailSprite.gameObject.transform.localScale = new Vector3((speechBubbleTailSprite.gameObject.transform.localScale.x * -1),
                                                                                     speechBubbleTailSprite.gameObject.transform.localScale.y,
                                                                                     speechBubbleTailSprite.gameObject.transform.localScale.z);
            }
        }
    }
    public void SetIsDisplayed(bool newIsDisplayed) {
        isDisplayed = newIsDisplayed;
    }
    /// <summary>
    /// This is for identifying when the speech bubble is showing in the world
    /// </summary>
    public bool IsDisplayed() {
        // TODO: make this a value that is returned based on the current
        //          alpha of the panel and sprite that are tweened respectively
        return isDisplayed;
    }
    /// <summary>
    /// This is for identifying when the speech bubble is showing in the world
    /// AND displaying text
    /// </summary>
    public bool IsDisplayingText() {
        return isDisplayingText;
    }
    public void SetIsDisplayingText(bool newSetIsDisplayingText) {
        isDisplayingText = newSetIsDisplayingText;
    }
    
    public bool HasFinishedDisplayingText() {
        bool hasFinishedDisplayingText = true;
        if(IsInUse()) {
            if(labelTypeWriterEffect != null) {
                if(labelTypeWriterEffect.isActive) {
                    hasFinishedDisplayingText = false;
                }
            }
        }
        return hasFinishedDisplayingText;
    }
    
    private string PopText() {
        string returnString = uiLabelText.text;
        if(textSet.Count > 0) {
            returnString = textSet.Dequeue();
        }
        // Debug.Log("Pop!\n" + returnString);
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
        uiLabelText.text = newText;
        
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
        return (rootContainer.alpha > 0f) ? true : false;
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
        uiLabelText.text = "";
    }
    
    public SpeechBubble Show(float horizontalScaleDuration, float verticalScaleDuration) {
        // Debug.Log("Show");
        
        uiLabelText.text = "";
        
        // clears the tweens
        this.gameObject.DestroyGoTweens();
        
        ScaleVerticalThenHorizontal(speechBubbleSprite, (int)controllerButtonBubbleSize.x, (int)controllerButtonBubbleSize.y, horizontalScaleOutEaseType, verticalScaleOutEaseType, horizontalScaleDuration, verticalScaleDuration);
        ScaleVerticalThenHorizontal(controllerButtonSprite, (int)controllerButtonSize.x, (int)controllerButtonSize.y, horizontalScaleOutEaseType, verticalScaleOutEaseType, horizontalScaleDuration, verticalScaleDuration, () => { hasFinishedShowing = true; });
        ScaleVerticalThenHorizontal(speechBubbleTailSprite, (int)tailSize.x, (int)tailSize.y, horizontalScaleOutEaseType, verticalScaleOutEaseType, horizontalScaleDuration, verticalScaleDuration);
        //----------------------------------------
        GoTweenConfig rootAlphaTweenConfig = new GoTweenConfig()
        .floatProp("alpha", 1)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
        });
        // scale horizontal
        this.gameObject.AddGoTween(Go.to(rootContainer,
                                         Mathf.Max(horizontalScaleDuration, verticalScaleDuration),
                                         rootAlphaTweenConfig));
                                         
        isDisplayed = true;
        hasFinishedShowing = false;
        
        return this;
    }
    
    public SpeechBubble Hide(float horizontalScaleDuration, float verticalScaleDuration) {
        Debug.Log("Hide");
        
        // clears the tweens
        this.gameObject.DestroyGoTweens();
        
        ScaleHorizontalThenVertical(speechBubbleSprite, 0, 0, horizontalScaleInEaseType, verticalScaleInEaseType, horizontalScaleDuration, verticalScaleDuration);
        ScaleHorizontalThenVertical(controllerButtonSprite, 0, 0, horizontalScaleInEaseType, verticalScaleInEaseType, horizontalScaleDuration, verticalScaleDuration);
        ScaleHorizontalThenVertical(speechBubbleTailSprite, 0, 0, horizontalScaleInEaseType, verticalScaleInEaseType, horizontalScaleDuration, verticalScaleDuration);
        //----------------------------------------
        GoTweenConfig rootAlphaTweenConfig = new GoTweenConfig()
        .floatProp("alpha", 0)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
        });
        // scale horizontal
        this.gameObject.AddGoTween(Go.to(rootContainer,
                                         Mathf.Max(horizontalScaleDuration, verticalScaleDuration),
                                         rootAlphaTweenConfig));
                                         
        GoTweenConfig fadeButtonTweenConfig = new GoTweenConfig()
        .floatProp("alpha", 1f)
        .setEaseType(GoEaseType.Linear)
        .onComplete(complete => {
        });
        this.gameObject.AddGoTween(Go.to(controllerButtonSprite,
                                         Mathf.Max(horizontalScaleDuration, verticalScaleDuration),
                                         fadeButtonTweenConfig));
                                         
        isDisplayed = false;
        hasFinishedShowing = false;
        SetIsInUse(false);
        
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
    public void ScaleVerticalThenHorizontal(UIWidget widget, int width, int height, GoEaseType horizontalScaleEaseType, GoEaseType verticalScaleEaseType, float horizontalScaleDuration, float verticalScaleDuration, System.Action onFinish = null) {
        // second
        GoTweenConfig scaleHorizontalTweenConfig = new GoTweenConfig()
        .intProp("width", width)
        .setEaseType(horizontalScaleEaseType)
        .onComplete(complete => {
            // do nothing
            if(onFinish != null) {
                onFinish();
            }
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
