using UnityEngine;
using System.Globalization;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class ClientTest : MonoBehaviour
{
    public string serverIp;
    public int serverPort;
    public bool showDebugMessage;
    public MotorController[] motors;
#pragma warning disable 618
    private NetworkClient _client;
#pragma warning restore 618


    private void Start()
    {
        SetupClient();
    }

    private void Update()
    {
        if (_client.isConnected) SendRequests();
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

    private void SendRequests()
    {
        /*    Send the requests to test the server    */
        _client.Send(1000, new StringMessage(MotorControllerToJson()));

        _client.Send(2000, new StringMessage("status"));
    }

    private void SetupClient()
    {
        /*    Create a client and connect to the asked ip and port   */
        _client = new NetworkClient();

        RegisterHandlers();
        _client.Connect(serverIp, serverPort);
    }

    private void RegisterHandlers()
    {
        /*    All RegisterHandlers    */
        _client.RegisterHandler(MsgType.Connect, OnConnected);
        _client.RegisterHandler(1000, ReadReceivedDatas);
    }

    private void ReadReceivedDatas(NetworkMessage netMsg)
    {
        if (showDebugMessage) Debug.Log(netMsg.ReadMessage<StringMessage>().value);
    }

    private void OnConnected(NetworkMessage netMsg)

    {
        if (showDebugMessage) Debug.Log("Connected to server");
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