using UnityEngine;
using UnityEngine.UI;
using System;


namespace UIScripts
{
    public class ServerDisplayManager : MonoBehaviour
    {
        public Text statusDisplayer;
        public GameObject inputPortNumber;
        public static int portNumber;
        private Text _text;
    
        void Start()
        {
            portNumber = 4444;
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

        }
    }
}
