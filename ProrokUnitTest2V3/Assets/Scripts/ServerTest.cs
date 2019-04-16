using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ServerTest : MonoBehaviour
{
    private static Thread _thEcoute;
    
    void Start()
    {
        _thEcoute = new Thread(new ThreadStart(Ecouter));
        _thEcoute.Start();
    }
    
    private static void Ecouter()
    {
        Debug.Log("Préparation à l'écoute...");

        //On crée le serveur en lui spécifiant le port sur lequel il devra écouter.
        UdpClient serveur = new UdpClient(5035);

        //Création d'une boucle infinie qui aura pour tâche d'écouter.
        while (true)
        {
            //Création d'un objet IPEndPoint qui recevra les données du Socket distant.
            IPEndPoint client = null;
            Debug.Log("ÉCOUTE...");

            //On écoute jusqu'à recevoir un message.
            byte[] data = serveur.Receive(ref client);
            Debug.Log("Données reçues en provenance de ." + client.Address + " "+ client.Port);

            //Décryptage et affichage du message.
            string message = Encoding.Default.GetString(data);
            Debug.Log("CONTENU DU MESSAGE : " +  message + "\n");
        }
    }
}
