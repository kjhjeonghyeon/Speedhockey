using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Text;
using System;


public class MyServer
{
    public static MyServer instance = new MyServer();

    Socket mainSock;
    List<Socket> connectedClients = new List<Socket>();
    int m_port = 11000;
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
            AsyncObject obj = new AsyncObject(30);

            Socket client = mainSock.EndAccept(ar);
            obj.WorkingSocket = client;
            //Socket client = mainSock.EndAccept(ar);
            

            // 다음 데이터 수신 대기
            client.BeginReceive(obj.Buffer, 0, obj.Buffer.Length, 0, DataReceived, obj);


            //AsyncObject obj = new AsyncObject(1920 * 1080 * 3);
            //obj.WorkingSocket = client;
            //connectedClients.Add(client);
            //client.BeginReceive(obj.Buffer, 0, 1920 * 1080 * 3, 0, DataReceived, obj);
            //string stringData = Encoding.Default.GetString(obj.Buffer);
            //Debug.Log(stringData);
            //mainSock.BeginAccept(AcceptCallback, null);
        }
        catch (Exception e)
        { Debug.LogError("Error in accept callback: " + e.Message); }
    }

    void DataReceived(IAsyncResult ar)
    {
        AsyncObject obj = (AsyncObject)ar.AsyncState;

        try
        {
            int bytesRead = obj.WorkingSocket.EndReceive(ar);

            if (bytesRead > 0)
            {
                byte[] receivedData = new byte[bytesRead];
                Array.Copy(obj.Buffer, 0, receivedData, 0, bytesRead);

                // 여기서 receivedData를 활용하여 필요한 작업 수행
                // 예시: 문자열로 변환하여 출력
                string receivedString = Encoding.Default.GetString(receivedData);
                Debug.Log("Received: " + receivedString);
            }

            // 다음 데이터 수신 대기
            obj.WorkingSocket.BeginReceive(obj.Buffer, 0, obj.Buffer.Length, 0, DataReceived, obj);
        }
        catch (Exception e)
        {
            Debug.LogError("Error in DataReceived: " + e.Message);
        }
    }

    public void Send(byte[] msg)
    {
        mainSock.Send(msg);
    }

}


