using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
//public class MyData
//{
//    public List<string> standerdBall = new List<string>();

//    public byte[] standerdPlayerData(Vector3 standerdPosition)
//    {
//        string positon = standerdPosition.ToString();
//        //string positon = JsonUtility.ToJson(standerdPosition);
//        byte[] buf = Encoding.Default.GetBytes(positon);

//        return buf;
//    }
//    public byte[] clientstanderdPlayerData(Vector3 standerdPosition)
//    {
//        //string positon = JsonUtility.ToJson(standerdPosition);
//        string positon = standerdPosition.ToString();
//        byte[] buf = Encoding.Default.GetBytes(positon);

//        return buf;
//    }
//    public byte[] standerdBallData(Vector3 standerdPosition, Quaternion standerdRotate)
//    {

//        string positon = standerdRotate.ToString();
//        //string positon = JsonUtility.ToJson(standerdPosition);
//        string rotate = JsonUtility.ToJson(standerdPosition);

//        standerdBall.Add(positon);
//        standerdBall.Add(rotate);
//        string transfromData = JsonUtility.ToJson(standerdBall);

//        byte[] buf = Encoding.Default.GetBytes(transfromData);
//        return buf;
//    }


//}
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
    PlayerMove playerMove = new PlayerMove();
    //MyData myData = new MyData();
    Socket mainSock;
    //  public bool isHost = false;
    public int playerNum = -1;
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

        //byte[] buffer = new byte[received];

        //Array.Copy(obj.Buffer, 0, buffer, 0, received);

        //Debug.Log("client receive : " + Encoding.Default.GetString(buffer));

        int bytesRead = obj.WorkingSocket.EndReceive(ar);
        if (bytesRead > 0)
        {
            byte[] receivedData = new byte[bytesRead];
            Array.Copy(obj.Buffer, 0, receivedData, 0, bytesRead);

            // 여기서 receivedData를 활용하여 필요한 작업 수행
            // 예시: 문자열로 변환하여 출력
            string[] split_receivedData = Encoding.Default.GetString(receivedData).Split(";");
            Debug.Log(split_receivedData.Length);
            for (int siter = 0; siter < split_receivedData.Length - 1; siter++)
            {
                string receivedString = split_receivedData[siter];
                Debug.Log(receivedString);
                if (receivedString != "")
                {
                    string[] commands = receivedString.Split(":");
                    if (commands.Length > 0)
                    {
                        if (commands[0] == "MOVE")
                        {
                            int clientNum = int.Parse(commands[1]);
                            float moveX = float.Parse(commands[2]);
                            float moveY = float.Parse(commands[3]);
                            if (playerNum == 0)
                            {
                                GameManager.instance.ToClientSendFromHostMove(clientNum, moveX, moveY);
                            }
                            //else
                            //{
                            //	GameManager.instance.ClientMove(clientNum, moveX, moveY);

                            //}

                        }
                        else if (commands[0] == "NUM")
                        {
                            Debug.Log(receivedString + " NUM 초기화");
                            playerNum = int.Parse(commands[1]);

                            if (playerNum == 0)
                            {
                                ClientManager.instance.StartButton_SetActive_True();
                            }
                        }
                        else if (commands[0] == "BALL_POSITION" && MyClient.instance.playerNum != 0)
                        {
                            float posX = float.Parse(commands[1]);
                            float posY = float.Parse(commands[2]);
                            float posZ = float.Parse(commands[3]);
                                if (playerNum == 0)
                                {
                                    ClientManager.instance.StartButton_SetActive_True();
                                }
                            }
                            else if (commands[0] == "GOAL")
                            {
                                Debug.Log(receivedString + " GAOL 초기화");
                                int redScore = int.Parse(commands[1]);
                                int blueScore = int.Parse(commands[2]);

                                GameManager.instance.text[0].text = redScore.ToString();
                                GameManager.instance.text[1].text = blueScore.ToString();
                            
                                GameManager.instance.t_ball.gameObject.GetComponent<MeshRenderer>().enabled = false;


                            }
                            else if (commands[0] == "BALL_POSITION" && MyClient.instance.playerNum != 0)
                            {
                                float posX = float.Parse(commands[1]);
                                float posY = float.Parse(commands[2]);
                                float posZ = float.Parse(commands[3]);


                                GameManager.instance.t_ball.position = new Vector3(posX, posY, posZ);


                        }
                        else if (commands[0] == "PLAYER_POSITION" && MyClient.instance.playerNum != 0)
                        {
                            int player_num = int.Parse(commands[1]);
                            float posX = float.Parse(commands[2]);
                            float posY = float.Parse(commands[3]);
                            float posZ = float.Parse(commands[4]);


                                GameManager.instance.t_game[player_num].position = new Vector3(posX, posY, posZ);


                        }
                        else if (commands[0] == "START_POSSIBILITY")
                        {
                            if (playerNum == 0)
                            {
                                if (int.Parse(commands[1]) == 0)
                                {
                                    ClientManager.instance.StartButton_Interactable_False();
                                }
                                else
                                {
                                    ClientManager.instance.StartButton_Interactable_True();
                                }
                            }
                        }
                        else if (commands[0] == "TOTAL")
                        {
                            Debug.Log("total test11");
                            GameManager.instance.PlayerCreate(int.Parse(commands[1]));
                            Debug.Log("total test22");
                        }
                        else if (commands[0] == "START")
                        {
                            if (int.Parse(commands[1]) == 1)
                            {
                                GameManager.instance.GameStart();
                            }
                        }
                    }
                }
                //Debug.Log("Received: " + receivedString);
                Debug.Log("Received: " + Encoding.Default.GetString(receivedData));
            }

        }

        // 다음 데이터 수신 대기
        obj.WorkingSocket.BeginReceive(obj.Buffer, 0, obj.Buffer.Length, 0, DataReceived, obj);
        //try
        //{
            
        //}
        //catch (Exception e)
        //{
        //    Debug.LogError("Error in DataReceived: " + e.Message);
        //}

        mainSock.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);
    }


    public void Send(byte[] msg)
    {
        mainSock.Send(msg);
        //List<byte[]> data = new List<byte[]>();
        //data.Add(standerdPlayerData());
        //data.Add(clientstanderdPlayerData());
        //data.Add(standerdBallData());


        //mainSock.Send(data[0]);
        //mainSock.Send(data[1]);
        //mainSock.Send(data[2]);

    }

    public void Send(string msg)
    {
        mainSock.Send(Encoding.Default.GetBytes(msg + ";"));

    }

    //    byte[] standerdPlayerData()
    //    {

    //        Vector3 position = GameObject.FindGameObjectWithTag("RedPlayer").GetComponent<Transform>().position;

    //        return myData.standerdPlayerData(position);
    //    }
    //    byte[] clientstanderdPlayerData()
    //    {
    //        Vector3 position = GameObject.FindGameObjectWithTag("BluePlayer").GetComponent<Transform>().position;
    //        return myData.clientstanderdPlayerData(position);
    //    }
    //    byte[] standerdBallData()
    //    {
    //        Vector3 position = GameObject.FindGameObjectWithTag("BluePlayer").GetComponent<Transform>().position;
    //        Quaternion rotate = GameObject.FindGameObjectWithTag("BluePlayer").GetComponent<Transform>().rotation;
    //        return myData.standerdBallData(position, rotate);
    //    }



}
