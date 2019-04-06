using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsLoader : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var test = new Settings {cameraViewDistance = 10};
        //test.SaveSettings(Application.streamingAssetsPath + "/JsonFiles/settings.json");
    }

}
