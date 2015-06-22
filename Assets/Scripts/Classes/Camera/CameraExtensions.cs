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
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(camera.transform.position,
                                   new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return (bounds.max.x - bounds.min.x);
    }
    
    public static float OrthographicHeight(this Camera camera) {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(camera.transform.position,
                                   new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return (bounds.max.y - bounds.min.y);
    }
}