using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class AttitudeDisplayManager : MonoBehaviour
    {
        public Image expandButton;

        public Text xpos;
        public Text ypos;
        public Text zpos;
        public Text pitch;
        public Text yaw;
        public Text roll;


        // Update is called once per frame
        private void LateUpdate()
        {
            /*    Display attitude information    */
            if (expandButton.enabled) return;
            var attitude = Controller.GetSensorValues().ToDictionary(x => x.Key, x => x.Value);
            xpos.text = ((int)attitude["posX"]).ToString(CultureInfo.InvariantCulture);
            ypos.text = ((int)attitude["posY"]).ToString(CultureInfo.InvariantCulture);
            zpos.text = ((int)attitude["posZ"]).ToString(CultureInfo.InvariantCulture);
            pitch.text = ": " + ((int)attitude["pitch"]).ToString(CultureInfo.InvariantCulture);
            yaw.text = ": " + ((int)attitude["yaw"]).ToString(CultureInfo.InvariantCulture);
            roll.text = ": " + ((int)attitude["roll"]).ToString(CultureInfo.InvariantCulture);
        }
    }
}