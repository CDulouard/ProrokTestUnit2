using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Serialization;


namespace UIScripts
{
    public class ServerDisplayManager : MonoBehaviour
    {
        public Text statusDisplayer;
        public GameObject inputPortNumber;
        public static int portNumber;
        public static bool isReduced;
        public Image reducedRunning;
        public Image reducedStopped;
        
        private Text _text;
        private Image _reducedRunningImage;
        private Image _reducedStoppedImage;

        private void Start()
        {
            _reducedRunningImage = reducedRunning.GetComponent<Image>();
            _reducedStoppedImage = reducedStopped.GetComponent<Image>();
            isReduced = false;
            portNumber = -1;
            _text = inputPortNumber.GetComponent<Text>();
        }

        private void Update()
        {
            if (Int32.TryParse(_text.text, out var x))
            {
                portNumber = x;
            }
            
            if (Server.isActive)
            {
                statusDisplayer.text = "Running";
                statusDisplayer.color = Color.green;
            }
            else
            {
                statusDisplayer.text = "Stoped";
                statusDisplayer.color = Color.red;
            }

            if (isReduced)
            {
                if (Server.isActive & !_reducedRunningImage.enabled)
                {
                    _reducedRunningImage.enabled = true;
                    _reducedStoppedImage.enabled = false;
                }

                if (!Server.isActive & !_reducedStoppedImage.enabled)
                {
                    _reducedRunningImage.enabled = false;
                    _reducedStoppedImage.enabled = true;
                }
            }
            else
            {
                if (_reducedRunningImage.enabled || _reducedStoppedImage.enabled)
                {
                    _reducedRunningImage.enabled = false;
                    _reducedStoppedImage.enabled = false;
                }
            }

        }
    }
}
