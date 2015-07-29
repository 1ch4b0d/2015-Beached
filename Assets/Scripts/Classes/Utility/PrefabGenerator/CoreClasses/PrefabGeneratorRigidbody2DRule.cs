using UnityEngine;
using System.Collections;

public class PrefabGeneratorRigidbody2DRule : PrefabGeneratorRule {
    public Vector3 minVelocity = Vector3.zero;
    public Vector3 maxVelocity = Vector3.zero;
    
    public Vector3 minForce = Vector3.zero;
    public Vector3 maxForce = Vector3.zero;
    
    // // Use this for initialization
    // protected override void Start() {
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    // }
    
    public override void PerformGenerationRule(GameObject gameObjectToPerformRuleOn) {
        Rigidbody2D rigidbody2D = gameObjectToPerformRuleOn.GetComponent<Rigidbody2D>();
        if(rigidbody2D != null) {
            //-------------------------
            // Velocity
            //-------------------------
            Vector3 newVelocity = Vector3.zero;
            newVelocity.x = Random.Range(minVelocity.x, maxVelocity.x);
            newVelocity.y = Random.Range(minVelocity.y, maxVelocity.y);
            rigidbody2D.velocity = newVelocity;
            //-------------------------
            // Force
            //-------------------------
            Vector3 forceToAdd = Vector3.zero;
            forceToAdd.x = Random.Range(minForce.x, maxForce.x);
            forceToAdd.y = Random.Range(minForce.y, maxForce.y);
            rigidbody2D.AddForce(forceToAdd);
        }
    }
}
