using UIScripts;
using UnityEngine;
using UnityEngine.Networking;
public class StartServer : MonoBehaviour
{

    public void Launch()
    {
        Server.targetPositions =
            "{\"legBackRightBot\": 0,    \"legBackRightTop\": 0,    \"shoulderBackRight\": 0,    \"legBackLeftBot\": 0,    \"legBackLeftTop\": 0,    \"shoulderBackLeft\": 0,    \"legFrontRightBot\": 0,    \"legFrontRightTop\": 0,    \"shoulderFrontRight\": 0,    \"legFrontLeftBot\": 0,    \"legFrontLeftTop\": 0,    \"shoulderFrontLeft\": 0}";
#pragma warning disable 618
        if(!NetworkServer.active) Server.SetupServer(ServerDisplayManager.portNumber);
#pragma warning restore 618
    }
}
