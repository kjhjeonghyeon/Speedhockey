using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ClientManager : MonoBehaviour
{
    [SerializeField] GameObject startButton;

    void Awake()
    {
        //MyClient.instance.Connect();
    }

    private void Start()
    {
        MyClient.instance.Connect();

        startButton.SetActive(false);

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

    public void StartButton_SetActive_True()
    {
        startButton.SetActive(true);
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