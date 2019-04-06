using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Serialization;


namespace UIScripts
{
    public class ServerDisplayManager : MonoBehaviour
    {
        public Text statusDisplay;
        public GameObject inputPortNumber;
        public static int portNumber;
        public Image expandButton;
        public InputField portInput;
        public Button startButton;
        public Button stopButton;
        public Image reducedRunning;
        public Image reducedStopped;
        
        private Text _text;
        private Image _reducedRunningImage;
        private Image _reducedStoppedImage;

        private void Start()
        {
            portNumber = SettingsLoader.settings.portDefault;
            portInput.text = portNumber.ToString();
            startButton.interactable = !Server.isActive;
            stopButton.interactable = Server.isActive;
            _reducedRunningImage = reducedRunning.GetComponent<Image>();
            _reducedStoppedImage = reducedStopped.GetComponent<Image>();
            expandButton.enabled = false;
            portNumber = -1;
            _text = inputPortNumber.GetComponent<Text>();
        }

        private void Update()
        {
            if (int.TryParse(_text.text, out var x))
            {
                portNumber = x;
            }
            
            if (Server.isActive)
            {
                statusDisplay.text = "Running";
                statusDisplay.color = Color.green;
            }
            else
            {
                statusDisplay.text = "Stoped";
                statusDisplay.color = Color.red;
            }

            if (expandButton.enabled)
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
