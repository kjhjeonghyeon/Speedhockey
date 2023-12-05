using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    //MyClient myClient = new MyClient();

    // Start is called before the first frame update
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
}
