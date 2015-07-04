using UnityEngine;
using System.Collections;

public class DefaultCredits : Cinematic {
    // Use this for initialization
    public override void Start() {
    }
    
    // Update is called once per frame
    // public override void Update() {
    //     base.Update();
    // }
    
    public override void TriggerStart() {
        // Debug.Log("Bottom bound: " + NGUIManager.Instance.UIRoot().activeHeight);
        Vector2 topLeftOfCamera = new Vector2(0, 0 + NGUIManager.Instance.UIRoot().activeHeight / 2);
        Vector2 centerOfCamera = Vector2.zero;
        Vector2 bottomRightOfCamera = new Vector2(0, 0 - NGUIManager.Instance.UIRoot().activeHeight / 2);
        
        float offsetFromCamera = 50f;
        
        float scrollSpeed = 2f;
        
        Debug.Log("Starting DefaultCredits");
        
        PerformWaveAndWait(CreateWave("Lead Programmer Credits", () => {
            GameObject creditGameObject = Factory.CreditPanel();
            CreditPanel creditPanel = creditGameObject.GetComponent<CreditPanel>();
            Vector3 startScrollPosition = new Vector3(creditGameObject.transform.localPosition.x, bottomRightOfCamera.y - offsetFromCamera, creditGameObject.transform.localPosition.z);
            Vector3 endScrollPosition = new Vector3(creditGameObject.transform.localPosition.x, topLeftOfCamera.y + offsetFromCamera, creditGameObject.transform.localPosition.z);
            Vector3 centerScrollPosition = new Vector3(creditGameObject.transform.localPosition.x, centerOfCamera.y, creditGameObject.transform.localPosition.z);
            creditGameObject.transform.localPosition = startScrollPosition;
            
            creditPanel.GetHeader().text = "[u][b]Lead Programmer[/b][/u]";
            creditPanel.GetCredit().text = "Logan Knecht";
            
            Go.to(creditGameObject.transform,
                  scrollSpeed,
                  new GoTweenConfig().localPosition(centerScrollPosition).setEaseType(GoEaseType.Linear)
            .onComplete(complete => {
                Go.to(creditGameObject.transform,
                      scrollSpeed,
                      new GoTweenConfig().localPosition(endScrollPosition).setEaseType(GoEaseType.Linear).setDelay(2f)
                .onComplete(c => {
                    Destroy(creditGameObject);
                    Completed();
                }));
            }));
            Completed();
        }));
        PerformWaveAndWait(CreateWave("Programmer Credits", () => {
            GameObject creditGameObject = Factory.CreditPanel();
            CreditPanel creditPanel = creditGameObject.GetComponent<CreditPanel>();
            Vector3 startScrollPosition = new Vector3(creditGameObject.transform.localPosition.x, bottomRightOfCamera.y - offsetFromCamera, creditGameObject.transform.localPosition.z);
            Vector3 endScrollPosition = new Vector3(creditGameObject.transform.localPosition.x, topLeftOfCamera.y + offsetFromCamera, creditGameObject.transform.localPosition.z);
            Vector3 centerScrollPosition = new Vector3(creditGameObject.transform.localPosition.x, centerOfCamera.y, creditGameObject.transform.localPosition.z);
            creditGameObject.transform.localPosition = startScrollPosition;
            
            creditPanel.GetHeader().text = "[u][b]Programmers[/b][/u]";
            creditPanel.GetCredit().text = "Logan Knecht\nTyler Knecht";
            
            Go.to(creditGameObject.transform,
                  scrollSpeed,
                  new GoTweenConfig().localPosition(centerScrollPosition).setEaseType(GoEaseType.Linear)
            .onComplete(complete => {
                Go.to(creditGameObject.transform,
                      scrollSpeed,
                      new GoTweenConfig().localPosition(endScrollPosition).setEaseType(GoEaseType.Linear).setDelay(2f)
                .onComplete(c => {
                    Destroy(creditGameObject);
                    Completed();
                }));
            }));
            Completed();
        }));
        PerformWavesAndWait(CreateWave("Lead Game Designer Credits", () => {
            GameObject creditGameObject = Factory.CreditPanel();
            CreditPanel creditPanel = creditGameObject.GetComponent<CreditPanel>();
            Vector3 startScrollPosition = new Vector3(creditGameObject.transform.localPosition.x, bottomRightOfCamera.y - offsetFromCamera, creditGameObject.transform.localPosition.z);
            Vector3 endScrollPosition = new Vector3(creditGameObject.transform.localPosition.x, topLeftOfCamera.y + offsetFromCamera, creditGameObject.transform.localPosition.z);
            Vector3 centerScrollPosition = new Vector3(creditGameObject.transform.localPosition.x, centerOfCamera.y, creditGameObject.transform.localPosition.z);
            creditGameObject.transform.localPosition = startScrollPosition;
            
            creditPanel.GetHeader().text = "[u][b]Lead Game Designer[/b][/u]";
            creditPanel.GetCredit().text = "Logan Knecht";
            
            Go.to(creditGameObject.transform,
                  scrollSpeed,
                  new GoTweenConfig().localPosition(centerScrollPosition).setEaseType(GoEaseType.Linear)
            .onComplete(complete => {
                Go.to(creditGameObject.transform,
                      scrollSpeed,
                      new GoTweenConfig().localPosition(endScrollPosition).setEaseType(GoEaseType.Linear).setDelay(2f)
                .onComplete(c => {
                    Destroy(creditGameObject);
                    Completed();
                }));
            }));
            Completed();
        }));
        
        PerformWavesAndWait(CreateWave("Game Designer Credits", () => {
            GameObject creditGameObject = Factory.CreditPanel();
            CreditPanel creditPanel = creditGameObject.GetComponent<CreditPanel>();
            Vector3 startScrollPosition = new Vector3(creditGameObject.transform.localPosition.x, bottomRightOfCamera.y - offsetFromCamera, creditGameObject.transform.localPosition.z);
            Vector3 endScrollPosition = new Vector3(creditGameObject.transform.localPosition.x, topLeftOfCamera.y + offsetFromCamera, creditGameObject.transform.localPosition.z);
            Vector3 centerScrollPosition = new Vector3(creditGameObject.transform.localPosition.x, centerOfCamera.y, creditGameObject.transform.localPosition.z);
            creditGameObject.transform.localPosition = startScrollPosition;
            
            creditPanel.GetHeader().text = "[u][b]Game Designers[/b][/u]";
            creditPanel.GetCredit().text = "Logan Knecht\nTyler Knecht";
            
            Go.to(creditGameObject.transform,
                  scrollSpeed,
                  new GoTweenConfig().localPosition(centerScrollPosition).setEaseType(GoEaseType.Linear)
            .onComplete(complete => {
                Go.to(creditGameObject.transform,
                      scrollSpeed,
                      new GoTweenConfig().localPosition(endScrollPosition).setEaseType(GoEaseType.Linear).setDelay(2f)
                .onComplete(c => {
                    Destroy(creditGameObject);
                    Completed();
                }));
            }));
            Completed();
        }));
        
        StartWaves();
    }
}