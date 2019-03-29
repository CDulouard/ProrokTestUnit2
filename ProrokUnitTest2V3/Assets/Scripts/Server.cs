using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    public static string targetPositions;
    private short _id = 1000;

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

        NetworkServer.RegisterHandler(_id, ReadTargetPositions);
    }


    private void ReadTargetPositions(NetworkMessage request)
    {
        /*    Function use to read and refresh target positions    */
        targetPositions = request.ReadMessage<Request>().content;
    }
#pragma warning restore 618
}