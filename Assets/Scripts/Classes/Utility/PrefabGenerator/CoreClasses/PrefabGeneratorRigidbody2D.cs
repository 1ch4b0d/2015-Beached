using UnityEngine;
using System.Collections;

public class PrefabGeneratorRigidbody2D : PrefabGeneratorRule {
    public Vector3 minVelocity = new Vector3(1, 1, 0);
    public Vector3 maxVelocity = new Vector3(1, 1, 0);
    
    // // Use this for initialization
    // protected override void Start() {
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    // }
    
    public override void PerformGenerationRule(GameObject gameObjectToPerformRuleOn) {
        Rigidbody2D rigidbody2D = gameObjectToPerformRuleOn.GetComponent<Rigidbody2D>();
        if(rigidbody2D != null) {
            Vector3 newVelocity = Vector3.zero;
            newVelocity.x = Random.Range(minVelocity.x, maxVelocity.x);
            newVelocity.y = Random.Range(minVelocity.y, maxVelocity.y);
            rigidbody2D.velocity = newVelocity;
        }
    }
}
