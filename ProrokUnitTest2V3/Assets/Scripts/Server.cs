using UnityEngine;



public class Server : MonoBehaviour
{
    public static bool isActive;
    public static string targetPositions;

    private static UdpSocket _server;
    

    public static void SetupServer(int portNumber)
    {
        /*    Function used to start the server    */
       
        
        _server = new UdpSocket();
        _server.StartServer("127.0.0.1", portNumber);
        isActive = _server != null;
    }

    public static void StopServer()
    {
        /*    Function used to stop the server    */
        _server.Stop();
        isActive = false;
    }
}