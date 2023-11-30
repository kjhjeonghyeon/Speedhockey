using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        MyServer myServer = new MyServer();

        myServer.Start();
    
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
