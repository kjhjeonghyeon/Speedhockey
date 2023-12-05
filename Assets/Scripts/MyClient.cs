using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class MyClient
{
    //public static TcpClient client;
    //// Start is called before the first frame update
    //public void Start()
    //{

    //    Debug.Log("클라이언트콘솔창. \n\n\n");

    //    client = new TcpClient();

    //    //client.Connect("172.16.6.5", 5000);
    //    client.Connect("127.0.0.1", 11000);

    //    //입력및 출력
    //    byte[] buf = Encoding.Default.GetBytes("USER_IN");
    //    client.GetStream().Write(buf, 0, buf.Length);

    //    byte[] buf2 = Encoding.Default.GetBytes("USER_IN2");
    //    client.GetStream().Write(buf2, 0, buf2.Length);


    //    //client.Close();



    //}
    //public void Send(byte[] msg)
    //{
    //    client.GetStream().Write(msg, 0, msg.Length);
    //}


    public static MyClient instance = new MyClient();

    Socket mainSock;
    int m_port = 11000;
    public void Connect()
    {
        mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress serverAddr = IPAddress.Parse("127.0.0.1");//"10.0.0.10");
        IPEndPoint clientEP = new IPEndPoint(serverAddr, m_port);
        mainSock.BeginConnect(clientEP, new AsyncCallback(ConnectCallback), mainSock);
    }
    public void Close()
    {
        if (mainSock != null)
        {
            mainSock.Close();
            mainSock.Dispose();
        }
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

    void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Socket client = (Socket)ar.AsyncState;
            client.EndConnect(ar);
            AsyncObject obj = new AsyncObject(4096);
            obj.WorkingSocket = mainSock;
            mainSock.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);

            Debug.Log("연결");
        }
        catch (Exception e)
        {
            Debug.LogError("Error in accept callback: " + e.Message);
        }
    }

    void DataReceived(IAsyncResult ar)
    {
        AsyncObject obj = (AsyncObject)ar.AsyncState;

        int received = obj.WorkingSocket.EndReceive(ar);

        byte[] buffer = new byte[received];

        Array.Copy(obj.Buffer, 0, buffer, 0, received);

        Debug.Log("client receive : " + Encoding.Default.GetString(buffer));
    }
    public void Send(byte[] msg)
    {
        mainSock.Send(msg);
    }

}
