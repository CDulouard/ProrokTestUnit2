using UnityEngine;

public class SettingsLoader : MonoBehaviour
{
    public GameObject mainCamera;
    public static Settings settings;
    private void Start()
    {
       ApplySettings(Application.streamingAssetsPath + "/JsonFiles/settings.json");
    }

    private void ApplySettings(string path)
    {
        settings = Settings.FromJson(path);
        //test.SaveSettings(Application.streamingAssetsPath + "/JsonFiles/settings.json");
        
        
        /*    Apply camera settings    */
        if (UIScripts.SceneLoader.GetMode().ToLower() != "training")
        {
            var cam = mainCamera.GetComponent<Camera>();
            cam.farClipPlane = settings.cameraViewDistance;            
        }

        CameraController.distance = settings.cameraDefaultDistance;
        CameraController.sensivityX = settings.xSensivity;
        CameraController.sensivityY = settings.ySensivity;
        CameraController.scrollSensivity = settings.scrollSensivity;
        
        /*    Apply Lidar settings    */

        Lidar.rayRange = settings.rayRange;
        Lidar.horizontalRange = settings.horizontalRange;
        Lidar.verticalRange = settings.verticalRange;
        Lidar.horizontalStep = settings.horizontalStep;
        Lidar.verticalStep = settings.verticalStep;
        Lidar.horizontalOffset = settings.horizontalOffset;
        Lidar.verticalOffset = settings.verticalOffset;


    }

}
