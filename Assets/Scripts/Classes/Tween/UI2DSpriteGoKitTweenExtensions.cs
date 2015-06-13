using UnityEngine;
using System.Collections;

public static class UI2DSpriteGoKitTweenExtensions {
    // to tweens
    public static GoTween alphaTo(this UI2DSprite self, float duration, float endValue) {
        return Go.to(self,
                     duration,
                     new GoTweenConfig().floatProp("alpha", endValue));
    }
}

public static class UIRectGoKitTweenExtensions {
    // to tweens
    public static GoTween alphaTo(this UIRect self, float duration, float endValue) {
        return Go.to(self,
                     duration,
                     new GoTweenConfig().floatProp("alpha", endValue));
    }
}
