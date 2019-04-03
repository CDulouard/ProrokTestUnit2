using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class LegsStatusManager : MonoBehaviour
    {
        public Image expandButton;

        public Text frontLeftBotTarget;
        public Text frontLeftBotPosition;
        public Text frontLeftTopTarget;
        public Text frontLeftTopPosition;
        public Text frontLeftShoulderTarget;
        public Text frontLeftShoulderPosition;

        public Text frontRightBotTarget;
        public Text frontRightBotPosition;
        public Text frontRightTopTarget;
        public Text frontRightTopPosition;
        public Text frontRightShoulderTarget;
        public Text frontRightShoulderPosition;

        public Text backLeftBotTarget;
        public Text backLeftBotPosition;
        public Text backLeftTopTarget;
        public Text backLeftTopPosition;
        public Text backLeftShoulderTarget;
        public Text backLeftShoulderPosition;

        public Text backRightBotTarget;
        public Text backRightBotPosition;
        public Text backRightTopTarget;
        public Text backRightTopPosition;
        public Text backRightShoulderTarget;
        public Text backRightShoulderPosition;

        private void LateUpdate()
        {
            if (!expandButton.enabled)
            {
                var robotDatas = Controller.GetProrokUnitTest2();

                /*    Display target positions    */
                frontLeftBotTarget.text =
                    Manager.TargetPositions.legFrontLeftBot.ToString(CultureInfo.InvariantCulture);
                frontLeftTopTarget.text =
                    Manager.TargetPositions.legFrontLeftTop.ToString(CultureInfo.InvariantCulture);
                frontLeftShoulderTarget.text =
                    Manager.TargetPositions.shoulderFrontLeft.ToString(CultureInfo.InvariantCulture);

                frontRightBotTarget.text =
                    Manager.TargetPositions.legFrontRightBot.ToString(CultureInfo.InvariantCulture);
                frontRightTopTarget.text =
                    Manager.TargetPositions.legFrontRightTop.ToString(CultureInfo.InvariantCulture);
                frontRightShoulderTarget.text =
                    Manager.TargetPositions.shoulderFrontRight.ToString(CultureInfo.InvariantCulture);

                backLeftBotTarget.text = Manager.TargetPositions.legBackLeftBot.ToString(CultureInfo.InvariantCulture);
                backLeftTopTarget.text = Manager.TargetPositions.legBackLeftTop.ToString(CultureInfo.InvariantCulture);
                backLeftShoulderTarget.text =
                    Manager.TargetPositions.shoulderBackLeft.ToString(CultureInfo.InvariantCulture);

                backRightBotTarget.text =
                    Manager.TargetPositions.legBackRightBot.ToString(CultureInfo.InvariantCulture);
                backRightTopTarget.text =
                    Manager.TargetPositions.legBackRightTop.ToString(CultureInfo.InvariantCulture);
                backRightShoulderTarget.text =
                    Manager.TargetPositions.shoulderBackRight.ToString(CultureInfo.InvariantCulture);

                /*    Display positions    */

                frontLeftBotPosition.text =
                    ((int) robotDatas.frontLeft.legBot.GetAngle()).ToString(CultureInfo.InvariantCulture);
                frontLeftBotPosition.color =
                    (int) robotDatas.frontLeft.legBot.GetAngle() == (int) Manager.TargetPositions.legFrontLeftBot
                        ? Color.green
                        : Color.red;
                
                frontLeftTopPosition.text =
                    ((int) robotDatas.frontLeft.legTop.GetAngle()).ToString(CultureInfo.InvariantCulture);
                frontLeftTopPosition.color =
                    (int) robotDatas.frontLeft.legTop.GetAngle() == (int) Manager.TargetPositions.legFrontLeftTop
                        ? Color.green
                        : Color.red;
                
                frontLeftShoulderPosition.text =
                    ((int) robotDatas.frontLeft.shoulder.GetAngle()).ToString(CultureInfo.InvariantCulture);
                frontLeftShoulderPosition.color =
                    (int) robotDatas.frontLeft.shoulder.GetAngle() == (int) Manager.TargetPositions.shoulderFrontLeft
                        ? Color.green
                        : Color.red;
                
                frontRightBotPosition.text =
                    ((int) robotDatas.frontRight.legBot.GetAngle()).ToString(CultureInfo.InvariantCulture);
                frontRightBotPosition.color =
                    (int) robotDatas.frontRight.legBot.GetAngle() == (int) Manager.TargetPositions.legFrontRightBot
                        ? Color.green
                        : Color.red;
                
                frontRightTopPosition.text =
                    ((int) robotDatas.frontRight.legTop.GetAngle()).ToString(CultureInfo.InvariantCulture);
                frontRightTopPosition.color =
                    (int) robotDatas.frontRight.legTop.GetAngle() == (int) Manager.TargetPositions.legFrontRightTop
                        ? Color.green
                        : Color.red;
                
                frontRightShoulderPosition.text =
                    ((int) robotDatas.frontRight.shoulder.GetAngle()).ToString(CultureInfo.InvariantCulture);
                frontRightShoulderPosition.color =
                    (int) robotDatas.frontRight.shoulder.GetAngle() == (int) Manager.TargetPositions.shoulderFrontRight
                        ? Color.green
                        : Color.red;
                
                backLeftBotPosition.text =
                    ((int) robotDatas.backLeft.legBot.GetAngle()).ToString(CultureInfo.InvariantCulture);
                backLeftBotPosition.color =
                    (int) robotDatas.backLeft.legBot.GetAngle() == (int) Manager.TargetPositions.legBackLeftBot
                        ? Color.green
                        : Color.red;
                
                backLeftTopPosition.text =
                    ((int) robotDatas.backLeft.legTop.GetAngle()).ToString(CultureInfo.InvariantCulture);
                backLeftTopPosition.color =
                    (int) robotDatas.backLeft.legTop.GetAngle() == (int) Manager.TargetPositions.legBackLeftTop
                        ? Color.green
                        : Color.red;
                
                backLeftShoulderPosition.text =
                    ((int) robotDatas.backLeft.shoulder.GetAngle()).ToString(CultureInfo.InvariantCulture);
                backLeftShoulderPosition.color =
                    (int) robotDatas.backLeft.shoulder.GetAngle() == (int) Manager.TargetPositions.shoulderBackLeft
                        ? Color.green
                        : Color.red;
                
                backRightBotPosition.text =
                    ((int) robotDatas.backRight.legBot.GetAngle()).ToString(CultureInfo.InvariantCulture);
                backRightBotPosition.color =
                    (int) robotDatas.backRight.legBot.GetAngle() == (int) Manager.TargetPositions.legBackRightBot
                        ? Color.green
                        : Color.red;
                
                backRightTopPosition.text =
                    ((int) robotDatas.backRight.legTop.GetAngle()).ToString(CultureInfo.InvariantCulture);
                backRightTopPosition.color =
                    (int) robotDatas.backRight.legTop.GetAngle() == (int) Manager.TargetPositions.legBackRightTop
                        ? Color.green
                        : Color.red;
                
                backRightShoulderPosition.text =
                    ((int) robotDatas.backRight.shoulder.GetAngle()).ToString(CultureInfo.InvariantCulture);
                backRightShoulderPosition.color =
                    (int) robotDatas.backRight.shoulder.GetAngle() == (int) Manager.TargetPositions.shoulderBackRight
                        ? Color.green
                        : Color.red;
            }
        }
    }
}