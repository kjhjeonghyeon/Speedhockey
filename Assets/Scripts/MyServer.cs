using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Text;
using System;


public class MyServer
{
    Socket mainSock;
    List<Socket> connectedClients = new List<Socket>();
    int m_port = 5000;
    public void Start()
    {
        try
        {
            mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Any, m_port);
            mainSock.Bind(serverEP);
            mainSock.Listen(10);
            mainSock.BeginAccept(AcceptCallback, null);
        }
        catch (Exception e)
        {
        }
    }

    
    public void Close()
    {
        if (mainSock != null)
        {
            mainSock.Close();
            mainSock.Dispose();
        }

        foreach (Socket socket in connectedClients)
        {
            socket.Close();
            socket.Dispose();
        }
        connectedClients.Clear();

        //mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
    }

    public class AsyncObject
    {
        public byte[] Buffer;
        public Socket WorkingSocket;
        public readonly int BufferSize;
        public AsyncObject(int bufferSize)
        {
            BufferSize = bufferSize;
            Buffer = new byte[(long)BufferSize];
        }

        public void ClearBuffer()
        {
            Array.Clear(Buffer, 0, BufferSize);
        }
    }

    void AcceptCallback(IAsyncResult ar)
    {
        try
        {
            Socket client = mainSock.EndAccept(ar);
            AsyncObject obj = new AsyncObject(1920 * 1080 * 3);
            obj.WorkingSocket = client;
            connectedClients.Add(client);
            client.BeginReceive(obj.Buffer, 0, 1920 * 1080 * 3, 0, DataReceived, obj);
            string stringData = Encoding.Default.GetString(obj.Buffer);
            Debug.Log(stringData);
            mainSock.BeginAccept(AcceptCallback, null);
        }
        catch (Exception e)
        { }
    }

    void DataReceived(IAsyncResult ar)
    {
        AsyncObject obj = (AsyncObject)ar.AsyncState;

        int received = obj.WorkingSocket.EndReceive(ar);

        byte[] buffer = new byte[received];
       
        Array.Copy(obj.Buffer, 0, buffer, 0, received);
    }


}


