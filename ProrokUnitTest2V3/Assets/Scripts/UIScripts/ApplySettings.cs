using UnityEngine;

namespace UIScripts
{
    public class ApplySettings : MonoBehaviour
    {

        public void Apply()
        {
            SettingsManager.settings.SaveSettings(Application.streamingAssetsPath + "/JsonFiles/settings.json");
        }
    }
}
