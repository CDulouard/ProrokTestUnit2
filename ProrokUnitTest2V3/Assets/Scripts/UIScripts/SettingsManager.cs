using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class SettingsManager : MonoBehaviour
    {
        public InputField camViewDistanceField;
        public InputField xSensivityField;
        public InputField ySensivityField;
        public InputField scrollSensivityField;
        public InputField cameraDistanceField;

        public Scrollbar camViewDistanceScroll;
        public Scrollbar xSensivityScroll;
        public Scrollbar ySensivityScroll;
        public Scrollbar scrollSensivityScroll;
        public Scrollbar cameraDistanceScroll;

        public static Settings settings;

        private const float CamViewDistanceMin = 1000f;
        private const float CamViewDistanceMax = 50000f;

        private const float XSensivityMin = 1f;
        private const float XSensivityMax = 30f;

        private const float YSensivityMin = 1f;
        private const float YSensivityMax = 30f;

        private const float ScrollSensivityMin = 1f;
        private const float ScrollSensivityMax = 100f;

        private const float CamDistanceMin = 100f;
        private const float CamDistanceMax = 1000f;

        private void Start()
        {
            /*    Load actual settings    */
            settings = Settings.FromJson(Application.streamingAssetsPath + "/JsonFiles/settings.json");

            /*    Write settings values to the UI    */
            camViewDistanceField.text = ((int) settings.cameraViewDistance).ToString(CultureInfo.InvariantCulture);
            xSensivityField.text = ((int) settings.xSensivity).ToString(CultureInfo.InvariantCulture);
            ySensivityField.text = ((int) settings.ySensivity).ToString(CultureInfo.InvariantCulture);
            scrollSensivityField.text = ((int) settings.scrollSensivity).ToString(CultureInfo.InvariantCulture);
            cameraDistanceField.text = ((int) settings.cameraDefaultDistance).ToString(CultureInfo.InvariantCulture);

            // x = (v * (max - min)) + min
            //    (x - min) / (max - min) = v

            /*    Convert the value of the settings to a value for scroll bars    */

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
            /*    Convert the value of the input fields to a value for scroll bars or  apply the value of the scroll bar to the UI   */

            if (float.TryParse(camViewDistanceField.text, out var camViewDistanceFieldValue) && camViewDistanceField.isFocused)
            {
                if (camViewDistanceFieldValue > CamViewDistanceMax)
                {
                    if (!camViewDistanceField.isFocused)
                    {
                        camViewDistanceField.text = ((int) CamViewDistanceMax).ToString(CultureInfo.InvariantCulture);
                    }

                    camViewDistanceScroll.value = 1;
                }
                else if (camViewDistanceFieldValue < CamDistanceMin)
                {
                    if (!camViewDistanceField.isFocused)
                    {
                        camViewDistanceField.text = ((int) CamDistanceMin).ToString(CultureInfo.InvariantCulture);
                    }
                    camViewDistanceScroll.value = 0;
                }
                else
                {
                    camViewDistanceScroll.value = (camViewDistanceFieldValue - CamViewDistanceMin) /
                                                  (CamViewDistanceMax - CamViewDistanceMin);
                }
            }
            else
            {
                camViewDistanceField.text = ((int) settings.cameraViewDistance).ToString(CultureInfo.InvariantCulture);
            }
            
            if (float.TryParse(xSensivityField.text, out var xSensivityFieldValue) && xSensivityField.isFocused)
            {
                if (xSensivityFieldValue > XSensivityMax)
                {
                    if (!xSensivityField.isFocused)
                    {
                        xSensivityField.text = ((int) XSensivityMax).ToString(CultureInfo.InvariantCulture);
                    }

                    xSensivityScroll.value = 1;
                }
                else if (xSensivityFieldValue < XSensivityMin)
                {
                    if (!xSensivityField.isFocused)
                    {
                        xSensivityField.text = ((int) XSensivityMin).ToString(CultureInfo.InvariantCulture);
                    }
                    xSensivityScroll.value = 0;
                }
                else
                {
                    xSensivityScroll.value = (xSensivityFieldValue - XSensivityMin) /
                                                  (XSensivityMax - XSensivityMin);
                }
            }
            else
            {
                xSensivityField.text = ((int) settings.xSensivity).ToString(CultureInfo.InvariantCulture);
            }
            
            if (float.TryParse(ySensivityField.text, out var ySensivityFieldValue) && ySensivityField.isFocused)
            {
                if (ySensivityFieldValue > YSensivityMax)
                {
                    if (!ySensivityField.isFocused)
                    {
                        ySensivityField.text = ((int) YSensivityMax).ToString(CultureInfo.InvariantCulture);
                    }

                    ySensivityScroll.value = 1;
                }
                else if (ySensivityFieldValue < YSensivityMin)
                {
                    if (!ySensivityField.isFocused)
                    {
                        ySensivityField.text = ((int) YSensivityMin).ToString(CultureInfo.InvariantCulture);
                    }
                    ySensivityScroll.value = 0;
                }
                else
                {
                    ySensivityScroll.value = (ySensivityFieldValue - YSensivityMin) /
                                             (YSensivityMax - YSensivityMin);
                }
            }
            else
            {
                ySensivityField.text = ((int) settings.ySensivity).ToString(CultureInfo.InvariantCulture);
            }
            
            if (float.TryParse(scrollSensivityField.text, out var scrollSensivityFieldValue) && scrollSensivityField.isFocused)
            {
                if (scrollSensivityFieldValue > ScrollSensivityMax)
                {
                    if (!scrollSensivityField.isFocused)
                    {
                        scrollSensivityField.text = ((int) ScrollSensivityMax).ToString(CultureInfo.InvariantCulture);
                    }

                    scrollSensivityScroll.value = 1;
                }
                else if (scrollSensivityFieldValue < ScrollSensivityMin)
                {
                    if (!scrollSensivityField.isFocused)
                    {
                        scrollSensivityField.text = ((int) ScrollSensivityMin).ToString(CultureInfo.InvariantCulture);
                    }
                    scrollSensivityScroll.value = 0;
                }
                else
                {
                    scrollSensivityScroll.value = (scrollSensivityFieldValue - ScrollSensivityMin) /
                                             (ScrollSensivityMax - ScrollSensivityMin);
                }
            }
            else
            {
                scrollSensivityField.text = ((int) settings.scrollSensivity).ToString(CultureInfo.InvariantCulture);
            }

            if (float.TryParse(cameraDistanceField.text, out var cameraDistanceFieldValue) && cameraDistanceField.isFocused)
            {
                if (cameraDistanceFieldValue > CamDistanceMax)
                {
                    if (!cameraDistanceField.isFocused)
                    {
                        cameraDistanceField.text = ((int) CamDistanceMax).ToString(CultureInfo.InvariantCulture);
                    }

                    cameraDistanceScroll.value = 1;
                }
                else if (cameraDistanceFieldValue < CamDistanceMin)
                {
                    if (!cameraDistanceField.isFocused)
                    {
                        cameraDistanceField.text = ((int) CamDistanceMin).ToString(CultureInfo.InvariantCulture);
                    }
                    cameraDistanceScroll.value = 0;
                }
                else
                {
                    cameraDistanceScroll.value = (cameraDistanceFieldValue - CamDistanceMin) /
                                                  (CamDistanceMax - CamDistanceMin);
                }
            }
            else
            {
                cameraDistanceField.text = ((int) settings.cameraDefaultDistance).ToString(CultureInfo.InvariantCulture);
            }
            /*    Convert the value of scroll bars to setting values    */
            settings.cameraViewDistance = camViewDistanceScroll.value * (CamViewDistanceMax - CamViewDistanceMin) +
                                          CamViewDistanceMin;

            settings.xSensivity = xSensivityScroll.value * (XSensivityMax - XSensivityMin) + XSensivityMin;

            settings.ySensivity = ySensivityScroll.value * (YSensivityMax - YSensivityMin) + YSensivityMin;

            settings.scrollSensivity = scrollSensivityScroll.value * (ScrollSensivityMax - ScrollSensivityMin) +
                                       ScrollSensivityMin;

            settings.cameraDefaultDistance = cameraDistanceScroll.value * (CamDistanceMax - CamDistanceMin) +
                                             CamDistanceMin;
        }

    }
}