using UnityEngine;
using System.Collections;

public class Collider2DSnapshot : BaseSnapshot<Collider2D> {
    bool isEnabled = false;
    
    public override void Capture(Collider2D collider2D) {
        isEnabled = collider2D.enabled;
    }
    
    public override void Restore(Collider2D collider2D) {
        collider2D.enabled = isEnabled;
    }
}
