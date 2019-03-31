using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;


public class Server : MonoBehaviour
{
    public static bool isActive;
    public static string targetPositions;
#pragma warning disable 618
    public static void SetupServer(int portNumber)
    {
        /*    Function used to start the server    */
        NetworkServer.Listen(portNumber);
        RegisterHandlers();
        isActive = NetworkServer.active;
        
    }
    
    public static void StopServer()
    {
        /*    Function used to start the server    */
        NetworkServer.Shutdown();
        isActive = NetworkServer.active;

    }

    private static void RegisterHandlers()
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

    private static void SendAskedDatas(NetworkMessage netMsg)
    {
        if (netMsg.ReadMessage<StringMessage>().value.ToLower() == "status")
        {
            NetworkServer.SendToAll(1000, new StringMessage(Manager.status));
        }
    }
    
#pragma warning restore 618
}