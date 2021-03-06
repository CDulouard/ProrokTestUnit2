﻿using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class StartServer : MonoBehaviour
    {
        /*    Functions used for the start server button    */
        public void Launch(Text error)
        {
            /*    Launch the server and display an error message if it can't    */
#pragma warning disable 618
            if (ServerDisplayManager.portNumber >= 49152 & ServerDisplayManager.portNumber <= 65535)
            {
                Server.targetPositions =
                    "{\"legBackRightBot\": 0,    \"legBackRightTop\": 0,    \"shoulderBackRight\": 0,    \"legBackLeftBot\": 0,    \"legBackLeftTop\": 0,    \"shoulderBackLeft\": 0,    \"legFrontRightBot\": 0,    \"legFrontRightTop\": 0,    \"shoulderFrontRight\": 0,    \"legFrontLeftBot\": 0,    \"legFrontLeftTop\": 0,    \"shoulderFrontLeft\": 0}";

                if (!Server.isActive)
                {
                    Server.SetupServer(ServerDisplayManager.portNumber);
                    error.text = "";


                }
                else
                {
                    error.text = "Can't creat the server. Maybe it's already running.";
                }
            }
            else
            {
                error.text = "Enter a correct port number.";
            }
#pragma warning restore 618
        }

        public void EnableButtonStop(Button stopButton)
        {
            /*    If the server is launched, enable the stop button and disable the start button    */
            if (!Server.isActive) return;
            GetComponent<Button>().interactable = false;
            stopButton.interactable = true;

        }


    }
}
