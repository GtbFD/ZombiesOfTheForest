using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Text;
using System;
using UnityEngine.UI;
using TMPro;

public class ConnectServer : MonoBehaviour
{
    // Start is called before the first frame update
    public Text quantityPlayers;
    void Start()
    {
        StartClient();
    }

    void StartClient() 
    {
        byte[] bytes = new byte[1024];

        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

        Socket sender = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

        sender.Connect(remoteEP);

        Console.WriteLine("Socket connected to {0}",
            sender.RemoteEndPoint.ToString());

        byte[] msg = Encoding.ASCII.GetBytes("0");

        int bytesSent = sender.Send(msg);

        int bytesRec = sender.Receive(bytes);


        string connections = Encoding.ASCII.GetString(bytes, 0, bytesRec);

        quantityPlayers.text = connections;

        /*sender.Shutdown(SocketShutdown.Both);
        sender.Close();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
