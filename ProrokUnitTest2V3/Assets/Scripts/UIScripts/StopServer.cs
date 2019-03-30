using UnityEngine;

namespace UIScripts
{
    public class StopServer : MonoBehaviour
    {
        // Start is called before the first frame update
        public void Stop()
        {
#pragma warning disable 618
            Server.StopServer();
#pragma warning restore 618
        }
    }
}
