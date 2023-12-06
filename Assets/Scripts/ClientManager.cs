using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class ClientManager : MonoBehaviour
{
    //MyClient myClient = new MyClient();
    //MyServer myServer = new MyServer();


    void Awake()
    {
        //MyClient.instance.Connect();
    }

    private void Start()
    {
        MyClient.instance.Connect();
        //myClient.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            byte[] buf = Encoding.Default.GetBytes("Client -> Server asdasdasdasdasdasdasdasasd");
            MyClient.instance.Send(buf);
            Debug.Log("Q ´©¸§");
            //.myClient.Send(buf);
        }
    }

    private void OnDestroy()
    {
        MyClient.instance.Send("USER_DISCONNECTED");

        MyClient.instance.Close();
    }

    //void clientNum()
    //{
    //    for (int i = 0; i < myServer.socketList.Count; i++)
    //    {
    //        string c = i.ToString();
    //        byte[] buff = Encoding.Default.GetBytes(c);

    //        myServer.socketList[i].Send(buff);
    //    }


    //}


}