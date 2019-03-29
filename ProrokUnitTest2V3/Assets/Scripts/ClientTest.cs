using UnityEngine;
using System.Globalization;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class ClientTest : MonoBehaviour
{
    public string serverIp;
    public int serverPort;
    public MotorController[] motors;
    private static string _messageToSend;
#pragma warning disable 618
    private NetworkClient _client;
#pragma warning restore 618
    private short _requestId;


    private void Start()
    {
        SetupClient();
    }

    private void Update()
    {
        _requestId = 1000;
        _messageToSend = MotorControllerToJson();
        var request = new Request();
        request.content = _messageToSend;
        if (_client.isConnected) _client.Send(_requestId, request);
    }

    private string MotorControllerToJson()
    {
        /*    Transform the inputs datas in a json string    */
        var json = motors.Aggregate("{\n",
            (current, motor) => current + ("    \"" + motor.name + "\"" + ": " +
                                           motor.targetPosition.ToString(CultureInfo.InvariantCulture)
                                               .Replace(",", ".") + ","));

        json = json.Remove(json.Length - 1);
        json += "\n    }";
        return json;
    }

#pragma warning disable 618
    public void SetupClient()
    {
        /*    Create a client and connect to the asked ip and port   */


        _client = new NetworkClient();
        _client.RegisterHandler(MsgType.Connect, OnConnected);

        _client.Connect(serverIp, serverPort);
    }


    private static void OnConnected(NetworkMessage netMsg)

    {
        Debug.Log("Connected to server");
    }
#pragma warning restore 618
}


[System.Serializable]
public class MotorController
{
    /*    Class used to get the input values of target positions    */
    public string name;
    [Range(-30, 30)] public float targetPosition;
}