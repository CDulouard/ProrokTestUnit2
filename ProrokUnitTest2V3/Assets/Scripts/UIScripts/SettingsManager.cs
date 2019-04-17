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

        

        /*    Robot settings    */

        public InputField legBotSpeedField;
        public Scrollbar legBotSpeedScroll;
        private const float LegBotSpeedMin = 10f;
        private const float LegBotSpeedMax = 200f;
        
        public InputField legTopSpeedField;
        public Scrollbar legTopSpeedScroll;
        private const float LegTopSpeedMin = 10f;
        private const float LegTopSpeedMax = 200f;
        
        public InputField shoulderSpeedField;
        public Scrollbar shoulderSpeedScroll;
        private const float ShoulderSpeedMin = 10f;
        private const float ShoulderSpeedMax = 200f;
        
        
        public InputField legBotTorqueField;
        public Scrollbar legBotTorqueScroll;
        private const float LegBotTorqueMin = 1000f;
        private const float LegBotTorqueMax = 50000f;
        
        public InputField legTopTorqueField;
        public Scrollbar legTopTorqueScroll;
        private const float LegTopTorqueMin = 1000f;
        private const float LegTopTorqueMax = 50000f;
        
        public InputField shoulderTorqueField;
        public Scrollbar shoulderTorqueScroll;
        private const float ShoulderTorqueMin = 1000f;
        private const float ShoulderTorqueMax = 50000f;
        
        
        public InputField legBotAngleMinField;
        public Scrollbar legBotAngleMinScroll;
        private const float LegBotAngleMinMin = -170f;
        private const float LegBotAngleMinMax = -1f;
        
        public InputField legTopAngleMinField;
        public Scrollbar legTopAngleMinScroll;
        private const float LegTopAngleMinMin = -170f;
        private const float LegTopAngleMinMax = -1f;
        
        public InputField shoulderAngleMinField;
        public Scrollbar shoulderAngleMinScroll;
        private const float ShoulderAngleMinMin = -170f;
        private const float ShoulderAngleMinMax = -1f;

        
        
        public static Settings settings;
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
            legTopSpeedField.text = ((int) settings.legTopSpeed).ToString(CultureInfo.InvariantCulture);
            shoulderSpeedField.text = ((int) settings.shoulderSpeed).ToString(CultureInfo.InvariantCulture);
            
            legBotTorqueField.text = ((int) settings.legBotTorque).ToString(CultureInfo.InvariantCulture);
            legTopTorqueField.text = ((int) settings.legTopTorque).ToString(CultureInfo.InvariantCulture);
            shoulderTorqueField.text = ((int) settings.shoulderTorque).ToString(CultureInfo.InvariantCulture);
            
            legBotAngleMinField.text = ((int) settings.legBotAngleMin).ToString(CultureInfo.InvariantCulture);
            legTopAngleMinField.text = ((int) settings.legTopAngleMin).ToString(CultureInfo.InvariantCulture);
            shoulderAngleMinField.text = ((int) settings.shoulderAngleMin).ToString(CultureInfo.InvariantCulture);
            
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
            
            legTopSpeedScroll.value = (settings.legTopSpeed - LegTopSpeedMin) /
                                      (LegTopSpeedMax - LegTopSpeedMin);
            
            shoulderSpeedScroll.value = (settings.shoulderSpeed - ShoulderSpeedMin) /
                                      (ShoulderSpeedMax - ShoulderSpeedMin);
            
            
            legBotTorqueScroll.value = (settings.legBotTorque - LegBotTorqueMin) /
                                      (LegBotTorqueMax - LegBotTorqueMin);
            
            legTopTorqueScroll.value = (settings.legTopTorque - LegTopTorqueMin) /
                                       (LegTopTorqueMax - LegTopTorqueMin);
            
            shoulderTorqueScroll.value = (settings.shoulderTorque - ShoulderTorqueMin) /
                                       (ShoulderTorqueMax - ShoulderTorqueMin);
            
            
            legBotAngleMinScroll.value = (settings.legBotAngleMin - LegBotAngleMinMin) /
                                       (LegBotAngleMinMax - LegBotAngleMinMin);
            
            legTopAngleMinScroll.value = (settings.legTopAngleMin - LegTopAngleMinMin) /
                                         (LegTopAngleMinMax - LegTopAngleMinMin);
            
            shoulderAngleMinScroll.value = (settings.shoulderAngleMin - ShoulderAngleMinMin) /
                                         (ShoulderAngleMinMax - ShoulderAngleMinMin);
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

            if (float.TryParse(legTopSpeedField.text == "" ? "0" : legTopSpeedField.text,
                    out var legTopSpeedFieldValue) && legTopSpeedField.isFocused)
            {
                if (legTopSpeedFieldValue > LegTopSpeedMax)
                {
                    if (!legTopSpeedField.isFocused)
                    {
                        legTopSpeedField.text = ((int) LegTopSpeedMax).ToString(CultureInfo.InvariantCulture);
                    }

                    legTopSpeedScroll.value = 1;
                }
                else if (legTopSpeedFieldValue < LegTopSpeedMin)
                {
                    if (!legTopSpeedField.isFocused)
                    {
                        legTopSpeedField.text = ((int) LegTopSpeedMin).ToString(CultureInfo.InvariantCulture);
                    }

                    legTopSpeedScroll.value = 0;
                }
                else
                {
                    legTopSpeedScroll.value = (legTopSpeedFieldValue - LegTopSpeedMin) /
                                              (LegTopSpeedMax - LegTopSpeedMin);
                }
            }
            else if (legTopSpeedField.text != "")
            {
                legTopSpeedField.text = ((int) settings.legTopSpeed).ToString(CultureInfo.InvariantCulture);
            }
            else if (!legTopSpeedField.isFocused)
            {
                legTopSpeedField.text = 0 > LegTopSpeedMin
                    ? 0 < LegTopSpeedMax ? "0" : ((int) LegTopSpeedMax).ToString(CultureInfo.InvariantCulture)
                    : ((int) LegTopSpeedMin).ToString(CultureInfo.InvariantCulture);
            }
            
            if (float.TryParse(shoulderSpeedField.text == "" ? "0" : shoulderSpeedField.text,
                    out var shoulderSpeedFieldValue) && shoulderSpeedField.isFocused)
            {
                if (shoulderSpeedFieldValue > ShoulderSpeedMax)
                {
                    if (!shoulderSpeedField.isFocused)
                    {
                        shoulderSpeedField.text = ((int) ShoulderSpeedMax).ToString(CultureInfo.InvariantCulture);
                    }

                    shoulderSpeedScroll.value = 1;
                }
                else if (shoulderSpeedFieldValue < ShoulderSpeedMin)
                {
                    if (!shoulderSpeedField.isFocused)
                    {
                        shoulderSpeedField.text = ((int) ShoulderSpeedMin).ToString(CultureInfo.InvariantCulture);
                    }

                    shoulderSpeedScroll.value = 0;
                }
                else
                {
                    shoulderSpeedScroll.value = (shoulderSpeedFieldValue - ShoulderSpeedMin) /
                                              (ShoulderSpeedMax - ShoulderSpeedMin);
                }
            }
            else if (shoulderSpeedField.text != "")
            {
                shoulderSpeedField.text = ((int) settings.shoulderSpeed).ToString(CultureInfo.InvariantCulture);
            }
            else if (!shoulderSpeedField.isFocused)
            {
                shoulderSpeedField.text = 0 > ShoulderSpeedMin
                    ? 0 < ShoulderSpeedMax ? "0" : ((int) ShoulderSpeedMax).ToString(CultureInfo.InvariantCulture)
                    : ((int) ShoulderSpeedMin).ToString(CultureInfo.InvariantCulture);
            }
            
            if (float.TryParse(legBotTorqueField.text == "" ? "0" : legBotTorqueField.text,
                    out var legBotTorqueFieldValue) && legBotTorqueField.isFocused)
            {
                if (legBotTorqueFieldValue > LegBotTorqueMax)
                {
                    if (!legBotTorqueField.isFocused)
                    {
                        legBotTorqueField.text = ((int) LegBotTorqueMax).ToString(CultureInfo.InvariantCulture);
                    }

                    legBotTorqueScroll.value = 1;
                }
                else if (legBotTorqueFieldValue < LegBotTorqueMin)
                {
                    if (!legBotTorqueField.isFocused)
                    {
                        legBotTorqueField.text = ((int) LegBotTorqueMin).ToString(CultureInfo.InvariantCulture);
                    }

                    legBotTorqueScroll.value = 0;
                }
                else
                {
                    legBotTorqueScroll.value = (legBotTorqueFieldValue - LegBotTorqueMin) /
                                               (LegBotTorqueMax - LegBotTorqueMin);
                }
            }
            else if (legBotTorqueField.text != "")
            {
                legBotTorqueField.text = ((int) settings.legBotTorque).ToString(CultureInfo.InvariantCulture);
            }
            else if (!legBotTorqueField.isFocused)
            {
                legBotTorqueField.text = 0 > LegBotTorqueMin
                    ? 0 < LegBotTorqueMax ? "0" : ((int) LegBotTorqueMax).ToString(CultureInfo.InvariantCulture)
                    : ((int) LegBotTorqueMin).ToString(CultureInfo.InvariantCulture);
            }
            
            if (float.TryParse(legTopTorqueField.text == "" ? "0" : legTopTorqueField.text,
                    out var legTopTorqueFieldValue) && legTopTorqueField.isFocused)
            {
                if (legTopTorqueFieldValue > LegTopTorqueMax)
                {
                    if (!legTopTorqueField.isFocused)
                    {
                        legTopTorqueField.text = ((int) LegTopTorqueMax).ToString(CultureInfo.InvariantCulture);
                    }

                    legTopTorqueScroll.value = 1;
                }
                else if (legTopTorqueFieldValue < LegTopTorqueMin)
                {
                    if (!legTopTorqueField.isFocused)
                    {
                        legTopTorqueField.text = ((int) LegTopTorqueMin).ToString(CultureInfo.InvariantCulture);
                    }

                    legTopTorqueScroll.value = 0;
                }
                else
                {
                    legTopTorqueScroll.value = (legTopTorqueFieldValue - LegTopTorqueMin) /
                                               (LegTopTorqueMax - LegTopTorqueMin);
                }
            }
            else if (legTopTorqueField.text != "")
            {
                legTopTorqueField.text = ((int) settings.legTopTorque).ToString(CultureInfo.InvariantCulture);
            }
            else if (!legTopTorqueField.isFocused)
            {
                legTopTorqueField.text = 0 > LegTopTorqueMin
                    ? 0 < LegTopTorqueMax ? "0" : ((int) LegTopTorqueMax).ToString(CultureInfo.InvariantCulture)
                    : ((int) LegTopTorqueMin).ToString(CultureInfo.InvariantCulture);
            }
            
            if (float.TryParse(shoulderTorqueField.text == "" ? "0" : shoulderTorqueField.text,
                    out var shoulderTorqueFieldValue) && shoulderTorqueField.isFocused)
            {
                if (shoulderTorqueFieldValue > ShoulderTorqueMax)
                {
                    if (!shoulderTorqueField.isFocused)
                    {
                        shoulderTorqueField.text = ((int) ShoulderTorqueMax).ToString(CultureInfo.InvariantCulture);
                    }

                    shoulderTorqueScroll.value = 1;
                }
                else if (shoulderTorqueFieldValue < ShoulderTorqueMin)
                {
                    if (!shoulderTorqueField.isFocused)
                    {
                        shoulderTorqueField.text = ((int) ShoulderTorqueMin).ToString(CultureInfo.InvariantCulture);
                    }

                    shoulderTorqueScroll.value = 0;
                }
                else
                {
                    shoulderTorqueScroll.value = (shoulderTorqueFieldValue - ShoulderTorqueMin) /
                                                 (ShoulderTorqueMax - ShoulderTorqueMin);
                }
            }
            else if (shoulderTorqueField.text != "")
            {
                shoulderTorqueField.text = ((int) settings.shoulderTorque).ToString(CultureInfo.InvariantCulture);
            }
            else if (!shoulderTorqueField.isFocused)
            {
                shoulderTorqueField.text = 0 > ShoulderTorqueMin
                    ? 0 < ShoulderTorqueMax ? "0" : ((int) ShoulderTorqueMax).ToString(CultureInfo.InvariantCulture)
                    : ((int) ShoulderTorqueMin).ToString(CultureInfo.InvariantCulture);
            }

            if (float.TryParse(legBotAngleMinField.text == "" ? "0" : legBotAngleMinField.text == "-" ? "0" : legBotAngleMinField.text,
                    out var legBotAngleMinFieldValue) && legBotAngleMinField.isFocused)
            {
                if (legBotAngleMinFieldValue > LegBotAngleMinMax)
                {
                    if (!legBotAngleMinField.isFocused)
                    {
                        legBotAngleMinField.text = ((int) LegBotAngleMinMax).ToString(CultureInfo.InvariantCulture);
                    }

                    legBotAngleMinScroll.value = 1;
                }
                else if (legBotAngleMinFieldValue < LegBotAngleMinMin)
                {
                    if (!legBotAngleMinField.isFocused)
                    {
                        legBotAngleMinField.text = ((int) LegBotAngleMinMin).ToString(CultureInfo.InvariantCulture);
                    }

                    legBotAngleMinScroll.value = 0;
                }
                else
                {
                    legBotAngleMinScroll.value = (legBotAngleMinFieldValue - LegBotAngleMinMin) /
                                                 (LegBotAngleMinMax - LegBotAngleMinMin);
                }
            }
            else if (legBotAngleMinField.text != "" || legBotAngleMinField.text != "-")
            {
                legBotAngleMinField.text = ((int) settings.legBotAngleMin).ToString(CultureInfo.InvariantCulture);
            }
            else if (!legBotAngleMinField.isFocused)
            {
                legBotAngleMinField.text = 0 > LegBotAngleMinMin
                    ? 0 < LegBotAngleMinMax ? "0" : ((int) LegBotAngleMinMax).ToString(CultureInfo.InvariantCulture)
                    : ((int) LegBotAngleMinMin).ToString(CultureInfo.InvariantCulture);
            }
            
            if (float.TryParse(legTopAngleMinField.text == "" ? "0" : legTopAngleMinField.text == "-" ? "0" : legTopAngleMinField.text,
                    out var legTopAngleMinFieldValue) && legTopAngleMinField.isFocused)
            {
                if (legTopAngleMinFieldValue > LegTopAngleMinMax)
                {
                    if (!legTopAngleMinField.isFocused)
                    {
                        legTopAngleMinField.text = ((int) LegTopAngleMinMax).ToString(CultureInfo.InvariantCulture);
                    }

                    legTopAngleMinScroll.value = 1;
                }
                else if (legTopAngleMinFieldValue < LegTopAngleMinMin)
                {
                    if (!legTopAngleMinField.isFocused)
                    {
                        legTopAngleMinField.text = ((int) LegTopAngleMinMin).ToString(CultureInfo.InvariantCulture);
                    }

                    legTopAngleMinScroll.value = 0;
                }
                else
                {
                    legTopAngleMinScroll.value = (legTopAngleMinFieldValue - LegTopAngleMinMin) /
                                                 (LegTopAngleMinMax - LegTopAngleMinMin);
                }
            }
            else if (legTopAngleMinField.text != "" || legTopAngleMinField.text != "-")
            {
                legTopAngleMinField.text = ((int) settings.legTopAngleMin).ToString(CultureInfo.InvariantCulture);
            }
            else if (!legTopAngleMinField.isFocused)
            {
                legTopAngleMinField.text = 0 > LegTopAngleMinMin
                    ? 0 < LegTopAngleMinMax ? "0" : ((int) LegTopAngleMinMax).ToString(CultureInfo.InvariantCulture)
                    : ((int) LegTopAngleMinMin).ToString(CultureInfo.InvariantCulture);
            }
            
            if (float.TryParse(shoulderAngleMinField.text == "" ? "0" : shoulderAngleMinField.text == "-" ? "0" : shoulderAngleMinField.text,
                    out var shoulderAngleMinFieldValue) && shoulderAngleMinField.isFocused)
            {
                if (shoulderAngleMinFieldValue > ShoulderAngleMinMax)
                {
                    if (!shoulderAngleMinField.isFocused)
                    {
                        shoulderAngleMinField.text = ((int) ShoulderAngleMinMax).ToString(CultureInfo.InvariantCulture);
                    }

                    shoulderAngleMinScroll.value = 1;
                }
                else if (shoulderAngleMinFieldValue < ShoulderAngleMinMin)
                {
                    if (!shoulderAngleMinField.isFocused)
                    {
                        shoulderAngleMinField.text = ((int) ShoulderAngleMinMin).ToString(CultureInfo.InvariantCulture);
                    }

                    shoulderAngleMinScroll.value = 0;
                }
                else
                {
                    shoulderAngleMinScroll.value = (shoulderAngleMinFieldValue - ShoulderAngleMinMin) /
                                                   (ShoulderAngleMinMax - ShoulderAngleMinMin);
                }
            }
            else if (shoulderAngleMinField.text != "" || shoulderAngleMinField.text != "-")
            {
                shoulderAngleMinField.text = ((int) settings.shoulderAngleMin).ToString(CultureInfo.InvariantCulture);
            }
            else if (!shoulderAngleMinField.isFocused)
            {
                shoulderAngleMinField.text = 0 > ShoulderAngleMinMin
                    ? 0 < ShoulderAngleMinMax ? "0" : ((int) ShoulderAngleMinMax).ToString(CultureInfo.InvariantCulture)
                    : ((int) ShoulderAngleMinMin).ToString(CultureInfo.InvariantCulture);
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
            
            settings.legTopSpeed = legTopSpeedScroll.value * (LegTopSpeedMax - LegTopSpeedMin) +
                                   LegTopSpeedMin;
            
            settings.shoulderSpeed = shoulderSpeedScroll.value * (ShoulderSpeedMax - ShoulderSpeedMin) +
                                     ShoulderSpeedMin;
            
            
            settings.legBotTorque = legBotTorqueScroll.value * (LegBotTorqueMax - LegBotTorqueMin) +
                                    LegBotTorqueMin;
            
            settings.legTopTorque = legTopTorqueScroll.value * (LegTopTorqueMax - LegTopTorqueMin) +
                                    LegTopTorqueMin;
            
            settings.shoulderTorque = shoulderTorqueScroll.value * (ShoulderTorqueMax - ShoulderTorqueMin) +
                                      ShoulderTorqueMin;
            
            
            settings.legBotAngleMin = legBotAngleMinScroll.value * (LegBotAngleMinMax - LegBotAngleMinMin) +
                                      LegBotAngleMinMin;
            
            settings.legTopAngleMin = legTopAngleMinScroll.value * (LegTopAngleMinMax - LegTopAngleMinMin) +
                                      LegTopAngleMinMin;
            
            settings.shoulderAngleMin = shoulderAngleMinScroll.value * (ShoulderAngleMinMax - ShoulderAngleMinMin) +
                                        ShoulderAngleMinMin;
        }
    }
}