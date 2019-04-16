using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;

public class Server : MonoBehaviour
{
    public static bool isActive;
    public static string targetPositions;

    private static Thread _listener;

#pragma warning disable 618
    public static void SetupServer(int portNumber)
    {
        var test = new TargetPositions();
        test.shoulderBackLeft = 10;
        test.shoulderBackRight = 10;
        test.shoulderFrontLeft = 10;
        test.shoulderFrontRight = 10;
        Debug.Log(JsonUtility.ToJson(test));
        /*    Function used to start the server    */
        /*=========================*/
        _listener = new Thread(() => Listen(portNumber));
        _listener.Start();
        /*=========================*/


        

        isActive = _listener.IsAlive;
    }

    public static void StopServer()
    {
        /*    Function used to start the server    */
        /*=========================*/
        _listener.Abort();
        Debug.Log("Server stopped");
        /*=========================*/
        
        isActive = _listener.IsAlive;
    }

    private static void RegisterHandlers(int id, string content)
    {
        /*    All RegisterHandlers    */
        switch (id)
        {
            case 1000:
                ReadTargetPositions(content);
                break;
            default:
                Debug.Log("Unknown id");
                break;
        }

    }


    private static void ReadTargetPositions(string message)
    {
        /*    Function use to read and refresh target positions    */
        targetPositions = message;
    }

    private static void SendAskedDatas(NetworkMessage netMsg)
    {
        if (netMsg.ReadMessage<StringMessage>().value.ToLower() == "status")
        {
            NetworkServer.SendToAll(1000, new StringMessage(Manager.status));
        }
    }

#pragma warning restore 618

    [SuppressMessage("ReSharper", "FunctionNeverReturns")]
    private static void Listen(int portNumber)
    {
        Debug.Log("Server is starting...");

        //On crée le serveur en lui spécifiant le port sur lequel il devra écouter.
        var server = new UdpClient(portNumber);

        //Création d'une boucle infinie qui aura pour tâche d'écouter.
        while (true)
        {
            //Création d'un objet IPEndPoint qui recevra les données du Socket distant.
            IPEndPoint client = null;
            Debug.Log("Listening...");

            //On écoute jusqu'à recevoir un message.
            var data = server.Receive(ref client);
            Debug.Log("Received datas from :" + client.Address + " " + client.Port);

            //Décryptage et affichage du message.
            var message = Encoding.Default.GetString(data);
            
            ReadMessage(message, out var id, out var content);
            RegisterHandlers(id, content);
            
            Debug.Log("id : " + id + "  message : " + content + "\n");
        }
    }

    private static void ReadMessage(string message, out int id, out string content)
    {
        var rx = new Regex(@"^[0-9]*",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
        if (int.TryParse(rx.Match(message).ToString(), out var x))
        {
            id = x;
            var idLength = id.ToString().Length;
            content = message.Substring(idLength, message.Length - idLength);
            return;
        }
        content = message;
        id = -1;
    }
}