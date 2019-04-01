using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class StopServer : MonoBehaviour
    {
        // Start is called before the first frame update
        public void Stop(Text error)
        {
            /*    Stop the server    */
#pragma warning disable 618
            Server.StopServer();
            error.text = "";
#pragma warning restore 618
        }

        public void EnableStartButton(Button startButton)
        {
            /*    Disable top button and enable the start button    */
            if (Server.isActive) return;
            GetComponent<Button>().interactable = false;
            startButton.interactable = true;
        }
        
    }
}
