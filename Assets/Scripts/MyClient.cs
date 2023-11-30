using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class MyClient
{
    public static TcpClient client;
    // Start is called before the first frame update
    public void Start()
    {

        Debug.Log("클라이언트콘솔창. \n\n\n");

        client = new TcpClient();

        //client.Connect("172.16.6.5", 5000);
        client.Connect("127.0.0.1", 11000);

        //입력및 출력
        byte[] buf = Encoding.Default.GetBytes("USER_IN");
        client.GetStream().Write(buf, 0, buf.Length);

        byte[] buf2 = Encoding.Default.GetBytes("USER_IN2");
        client.GetStream().Write(buf2, 0, buf2.Length);


        //client.Close();



    }

   
}
