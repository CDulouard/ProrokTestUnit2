﻿using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class RobotStatusManager : MonoBehaviour
    {
        public Image expandButton;

        private void Start()
        {
            expandButton.enabled = false;
        }


    }
}
