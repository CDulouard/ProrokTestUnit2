using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
    public class SettingsManager : MonoBehaviour
    {
        /*    Video settings    */
        public InputField camViewDistanceField;
        public Scrollbar camViewDistanceScroll;
        private const float CamViewDistanceMin = 1000f;
        private const float CamViewDistanceMax = 50000f;

        public InputField xSensivityField;
        public Scrollbar xSensivityScroll;
        private const float XSensivityMin = 1f;
        private const float XSensivityMax = 30f;

        public InputField ySensivityField;
        public Scrollbar ySensivityScroll;
        private const float YSensivityMin = 1f;
        private const float YSensivityMax = 30f;

        public InputField scrollSensivityField;
        public Scrollbar scrollSensivityScroll;
        private const float ScrollSensivityMin = 1f;
        private const float ScrollSensivityMax = 100f;

        public InputField cameraDistanceField;
        public Scrollbar cameraDistanceScroll;
        private const float CamDistanceMin = 100f;
        private const float CamDistanceMax = 1000f;

        public static Settings settings;

        /*    Robot settings    */

        public InputField legBotSpeedField;
        public Scrollbar legBotSpeedScroll;
        private const float LegBotSpeedMin = 10f;
        private const float LegBotSpeedMax = 200f;


        private void Start()
        {
            /*    Load actual settings    */
            settings = Settings.FromJson(Application.streamingAssetsPath + "/JsonFiles/settings.json");

            /*    Write settings values to the UI    */

            /*    Video settings    */

            camViewDistanceField.text = ((int) settings.cameraViewDistance).ToString(CultureInfo.InvariantCulture);
            xSensivityField.text = ((int) settings.xSensivity).ToString(CultureInfo.InvariantCulture);
            ySensivityField.text = ((int) settings.ySensivity).ToString(CultureInfo.InvariantCulture);
            scrollSensivityField.text = ((int) settings.scrollSensivity).ToString(CultureInfo.InvariantCulture);
            cameraDistanceField.text = ((int) settings.cameraDefaultDistance).ToString(CultureInfo.InvariantCulture);

            /*    Robot settings    */

            legBotSpeedField.text = ((int) settings.legBotSpeed).ToString(CultureInfo.InvariantCulture);

            /*    Convert the value of the settings to a value for scroll bars    */

            /*    Video settings    */

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

            /*    Robot settings    */

            legBotSpeedScroll.value = (settings.legBotSpeed - LegBotSpeedMin) /
                                      (LegBotSpeedMax - LegBotSpeedMin);
        }


        private void Update()
        {
            /*    Convert the value of the input fields to a value for scroll bars or  apply the value of the scroll bar to the UI   */

            /*    Video settings    */
            if (float.TryParse(camViewDistanceField.text == "" ? "0" : camViewDistanceField.text,
                    out var camViewDistanceFieldValue) &&
                camViewDistanceField.isFocused)
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
            else if (camViewDistanceField.text != "")
            {
                camViewDistanceField.text = ((int) settings.cameraViewDistance).ToString(CultureInfo.InvariantCulture);
            }
            else if (!camViewDistanceField.isFocused)
            {
                camViewDistanceField.text = 0 > CamViewDistanceMin
                    ? 0 < CamViewDistanceMax ? "0" : ((int) CamViewDistanceMax).ToString(CultureInfo.InvariantCulture)
                    : ((int) CamViewDistanceMin).ToString(CultureInfo.InvariantCulture);
            }

            if (float.TryParse(xSensivityField.text == "" ? "0" : xSensivityField.text, out var xSensivityFieldValue) &&
                xSensivityField.isFocused)
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
            else if (xSensivityField.text != "")
            {
                xSensivityField.text = ((int) settings.xSensivity).ToString(CultureInfo.InvariantCulture);
            }
            else if (!xSensivityField.isFocused)
            {
                xSensivityField.text = 0 > XSensivityMin
                    ? 0 < XSensivityMax ? "0" : ((int) XSensivityMax).ToString(CultureInfo.InvariantCulture)
                    : ((int) XSensivityMin).ToString(CultureInfo.InvariantCulture);
            }

            if (float.TryParse(ySensivityField.text == "" ? "0" : ySensivityField.text, out var ySensivityFieldValue) &&
                ySensivityField.isFocused)
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
            else if (ySensivityField.text != "")
            {
                ySensivityField.text = ((int) settings.ySensivity).ToString(CultureInfo.InvariantCulture);
            }
            else if (!ySensivityField.isFocused)
            {
                ySensivityField.text = 0 > YSensivityMin
                    ? 0 < YSensivityMax ? "0" : ((int) YSensivityMax).ToString(CultureInfo.InvariantCulture)
                    : ((int) YSensivityMin).ToString(CultureInfo.InvariantCulture);
            }


            if (float.TryParse(scrollSensivityField.text == "" ? "0" : scrollSensivityField.text,
                    out var scrollSensivityFieldValue) &&
                scrollSensivityField.isFocused)
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
            else if (scrollSensivityField.text != "")
            {
                scrollSensivityField.text = ((int) settings.scrollSensivity).ToString(CultureInfo.InvariantCulture);
            }
            else if (!scrollSensivityField.isFocused)
            {
                scrollSensivityField.text = 0 > ScrollSensivityMin
                    ? 0 < ScrollSensivityMax ? "0" : ((int) ScrollSensivityMax).ToString(CultureInfo.InvariantCulture)
                    : ((int) ScrollSensivityMin).ToString(CultureInfo.InvariantCulture);
            }

            if (float.TryParse(cameraDistanceField.text == "" ? "0" : cameraDistanceField.text,
                    out var cameraDistanceFieldValue) &&
                cameraDistanceField.isFocused)
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
            else if (cameraDistanceField.text != "")
            {
                cameraDistanceField.text =
                    ((int) settings.cameraDefaultDistance).ToString(CultureInfo.InvariantCulture);
            }
            else if (!cameraDistanceField.isFocused)
            {
                cameraDistanceField.text = 0 > CamDistanceMin
                    ? 0 < CamDistanceMax ? "0" : ((int) CamDistanceMax).ToString(CultureInfo.InvariantCulture)
                    : ((int) CamDistanceMin).ToString(CultureInfo.InvariantCulture);
            }

            /*    Robot settings    */
            if (float.TryParse(legBotSpeedField.text == "" ? "0" : legBotSpeedField.text,
                    out var legBotSpeedFieldValue) && legBotSpeedField.isFocused)
            {
                if (legBotSpeedFieldValue > LegBotSpeedMax)
                {
                    if (!legBotSpeedField.isFocused)
                    {
                        legBotSpeedField.text = ((int) LegBotSpeedMax).ToString(CultureInfo.InvariantCulture);
                    }

                    legBotSpeedScroll.value = 1;
                }
                else if (legBotSpeedFieldValue < LegBotSpeedMin)
                {
                    if (!legBotSpeedField.isFocused)
                    {
                        legBotSpeedField.text = ((int) LegBotSpeedMin).ToString(CultureInfo.InvariantCulture);
                    }

                    legBotSpeedScroll.value = 0;
                }
                else
                {
                    legBotSpeedScroll.value = (legBotSpeedFieldValue - LegBotSpeedMin) /
                                              (LegBotSpeedMax - LegBotSpeedMin);
                }
            }
            else if (legBotSpeedField.text != "")
            {
                legBotSpeedField.text = ((int) settings.legBotSpeed).ToString(CultureInfo.InvariantCulture);
            }
            else if (!legBotSpeedField.isFocused)
            {
                legBotSpeedField.text = 0 > LegBotSpeedMin
                    ? 0 < LegBotSpeedMax ? "0" : ((int) LegBotSpeedMax).ToString(CultureInfo.InvariantCulture)
                    : ((int) LegBotSpeedMin).ToString(CultureInfo.InvariantCulture);
            }


            /*    Convert the value of scroll bars to setting values    */

            /*    Video settings    */
            settings.cameraViewDistance = camViewDistanceScroll.value * (CamViewDistanceMax - CamViewDistanceMin) +
                                          CamViewDistanceMin;

            settings.xSensivity = xSensivityScroll.value * (XSensivityMax - XSensivityMin) + XSensivityMin;

            settings.ySensivity = ySensivityScroll.value * (YSensivityMax - YSensivityMin) + YSensivityMin;

            settings.scrollSensivity = scrollSensivityScroll.value * (ScrollSensivityMax - ScrollSensivityMin) +
                                       ScrollSensivityMin;

            settings.cameraDefaultDistance = cameraDistanceScroll.value * (CamDistanceMax - CamDistanceMin) +
                                             CamDistanceMin;

            /*    Robot settings    */

            settings.legBotSpeed = legBotSpeedScroll.value * (LegBotSpeedMax - LegBotSpeedMin) +
                                   LegBotSpeedMin;
        }
    }
}