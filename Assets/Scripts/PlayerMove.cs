using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Vector3 movePosition;
    public int speed = 3;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMove();


    }
    void InputMove()
    {
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        if ((mouse_X != 0 || mouse_Y != 0) && MyClient.instance != null)
        {
            //Debug.Log(MyClient.client + " " + MyClient.client.Connected);
            movePosition = new Vector3(mouse_X, 0, mouse_Y);
            rb.velocity = movePosition.normalized * speed;
            //Debug.Log(rb.velocity);
            //transform.position += movePosition * Time.deltaTime * speed;

            try
            {
                //입력및 출력
                byte[] buf = Encoding.Default.GetBytes("MOVE : " + mouse_X + " : " + mouse_Y);
                //MyClient.client.GetStream().Write(buf, 0, buf.Length);
                //MyClient.client.GetStream().Flush();
                //MyClient.instance.Send(buf);
            }
            catch (Exception e)
            {
                Debug.LogError("Exception during network write: " + e.Message);
            }
            //MyClientclient.GetStream().Write(buf, 0, buf.Length);
        }

    }


    private void OnTriggerEnter(Collider other)
    {

    }
}
