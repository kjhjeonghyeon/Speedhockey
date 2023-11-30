using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        MyClient myClient = new MyClient();
        myClient.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
