using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class SettingsManager : MonoBehaviour
    {
        public GameObject content;
        
        public Text camViewDistanceText;
        public Text xSensivityText;
        public Text ySensivityText;
        public Text scrollSensivityText;
        public Text cameraDistanceText;

        public Scrollbar camViewDistanceScroll;
        public Scrollbar xSensivityScroll;
        public Scrollbar ySensivityScroll;
        public Scrollbar scrollSensivityScroll;
        public Scrollbar cameraDistanceScroll;

        public static Settings settings;

        private const float CamViewDistanceMin = 1000f;
        private const float CamViewDistanceMax = 50000f;

        private const float XSensivityMin = 1f;
        private const float XSensivityMax = 20f;

        private const float YSensivityMin = 1f;
        private const float YSensivityMax = 20f;

        private const float ScrollSensivityMin = 1f;
        private const float ScrollSensivityMax = 20f;

        private const float CamDistanceMin = 100f;
        private const float CamDistanceMax = 1000f;

        private void Start()
        {
            settings = Settings.FromJson(Application.streamingAssetsPath + "/JsonFiles/settings.json");
            BoxManager.HideContentGeneric(content);
            
            camViewDistanceText.text = ((int) settings.cameraViewDistance).ToString(CultureInfo.InvariantCulture);
            xSensivityText.text = ((int) settings.xSensivity).ToString(CultureInfo.InvariantCulture);
            ySensivityText.text = ((int) settings.ySensivity).ToString(CultureInfo.InvariantCulture);
            scrollSensivityText.text = ((int) settings.scrollSensivity).ToString(CultureInfo.InvariantCulture);
            cameraDistanceText.text = ((int) settings.cameraDefaultDistance).ToString(CultureInfo.InvariantCulture);

            // x = (v * (max - min)) + min
            //    (x - min) / (max - min) = v
            camViewDistanceScroll.value = (settings.cameraViewDistance - CamViewDistanceMin) /
                                          (CamViewDistanceMax - CamViewDistanceMin);

            xSensivityScroll.value = (settings.xSensivity - XSensivityMin) /
                                     (XSensivityMax - XSensivityMin);

            ySensivityScroll.value = (settings.ySensivity - YSensivityMin) /
                                     (YSensivityMax - YSensivityMin);

            scrollSensivityScroll.value = (settings.scrollSensivity - ScrollSensivityMin) /
                                          (ScrollSensivityMax - ScrollSensivityMin);

            cameraDistanceScroll.value = (settings.cameraDefaultDistance - CamDistanceMin) /
                                         (CamDistanceMax - CamDistanceMin);
        }


        private void Update()
        {
            settings.cameraViewDistance = camViewDistanceScroll.value * (CamViewDistanceMax - CamViewDistanceMin) +
                                           CamViewDistanceMin;

            settings.xSensivity = xSensivityScroll.value * (XSensivityMax - XSensivityMin) + XSensivityMin;

            settings.ySensivity = ySensivityScroll.value * (YSensivityMax - YSensivityMin) + YSensivityMin;

            settings.scrollSensivity = scrollSensivityScroll.value * (ScrollSensivityMax - ScrollSensivityMin) +
                                        ScrollSensivityMin;
            
            settings.cameraDefaultDistance = cameraDistanceScroll.value * (CamDistanceMax - CamDistanceMin) +
                                        CamDistanceMin;
        }

        private void LateUpdate()
        {
            camViewDistanceText.text = ((int) settings.cameraViewDistance).ToString(CultureInfo.InvariantCulture);
            xSensivityText.text = ((int) settings.xSensivity).ToString(CultureInfo.InvariantCulture);
            ySensivityText.text = ((int) settings.ySensivity).ToString(CultureInfo.InvariantCulture);
            scrollSensivityText.text = ((int) settings.scrollSensivity).ToString(CultureInfo.InvariantCulture);
            cameraDistanceText.text = ((int) settings.cameraDefaultDistance).ToString(CultureInfo.InvariantCulture);
        }
    }
}