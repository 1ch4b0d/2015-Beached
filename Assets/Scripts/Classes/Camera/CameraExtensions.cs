using UnityEngine;
using System.Collections;

public static class CameraExtensions {
    public static Bounds OrthographicBounds(this Camera camera) {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(camera.transform.position,
                                   new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }
    
    public static float OrthographicWidth(this Camera camera) {
        return OrthographicBounds(camera).size.x;
    }
    
    public static float OrthographicHeight(this Camera camera) {
        return OrthographicBounds(camera).size.y;
    }
    
    public static float GetLeftBoundWorldPosition(this Camera camera) {
        return camera.OrthographicBounds().min.x;
    }
    
    public static float GetBottomBoundWorldPosition(this Camera camera) {
        return camera.OrthographicBounds().min.y;
    }
    
    public static float GetRightBoundWorldPosition(this Camera camera) {
        return camera.OrthographicBounds().max.x;
    }
    
    public static float GetTopBoundWorldPosition(this Camera camera) {
        return camera.OrthographicBounds().max.y;
    }
}