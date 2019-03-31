using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class StopServer : MonoBehaviour
    {
        // Start is called before the first frame update
        public void Stop(Text error)
        {
#pragma warning disable 618
            Server.StopServer();
            error.text = "";
#pragma warning restore 618
        }

        public void EnableStartButton(Button startButton)
        {
            if (!Server.isActive)
            {
                GetComponent<Button>().interactable = false;
                startButton.interactable = true;
            }
        }
        
    }
}
