using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    //MyServer myServer = new MyServer();
   
    // Start is called before the first frame update
    void Awake()
    {

        MyServer.instance.Start();
        //myServer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            byte[] buf = Encoding.UTF8.GetBytes("Server -> Client asdasdasdasdasdasdasdasasd;");
            MyServer.instance.Send(buf);
            Debug.Log("W ����");
            //myServer.Send(buf);
        }
    }

	private void OnDestroy()
	{
        MyServer.instance.Close();
	}
}
