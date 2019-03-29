using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class Server : MonoBehaviour
{
    public static string targetPositions;

    void Start()
    {
        SetupServer();
        /*    Initialization of targetPositions (all motors to 0)    */
        targetPositions =
            "{\"legBackRightBot\": 0,    \"legBackRightTop\": 0,    \"shoulderBackRight\": 0,    \"legBackLeftBot\": 0,    \"legBackLeftTop\": 0,    \"shoulderBackLeft\": 0,    \"legFrontRightBot\": 0,    \"legFrontRightTop\": 0,    \"shoulderFrontRight\": 0,    \"legFrontLeftBot\": 0,    \"legFrontLeftTop\": 0,    \"shoulderFrontLeft\": 0}";
    }
#pragma warning disable 618
    private void SetupServer()
    {
        /*    Function used to start the server    */
        NetworkServer.Listen(4444);

        RegisterHandlers();
    }

    private void RegisterHandlers()
    {
        /*    All RegisterHandlers    */

        NetworkServer.RegisterHandler(1000, ReadTargetPositions);
        NetworkServer.RegisterHandler(2000, SendAskedDatas);
    }


    private static void ReadTargetPositions(NetworkMessage netMsg)
    {
        /*    Function use to read and refresh target positions    */
        targetPositions = netMsg.ReadMessage<StringMessage>().value;
    }

    private void SendAskedDatas(NetworkMessage netMsg)
    {
        if (netMsg.ReadMessage<StringMessage>().value.ToLower() == "status")
        {
            NetworkServer.SendToAll(1000, new StringMessage(Manager.status));
        }
    }
    
#pragma warning restore 618
}