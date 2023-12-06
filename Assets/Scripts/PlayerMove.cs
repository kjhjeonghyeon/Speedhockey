using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Vector3 movePosition;
    public int speed = 7;
    Rigidbody rb;
    int playerNum;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMove();

        //Debug.Log(rb.velocity);

        DataSend();


    }
    void InputMove()
    {
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        if ((mouse_X != 0 || mouse_Y != 0) && MyClient.instance != null)
        {
            //Debug.Log(MyClient.client + " " + MyClient.client.Connected);
            movePosition = new Vector3(mouse_X, 0, mouse_Y);
            //Debug.Log(rb.velocity);
            //transform.position += movePosition * Time.deltaTime * speed;

            try
            {
                if (MyClient.instance.playerNum == 0)
                {
                        rb.velocity = movePosition.normalized * speed;
                        //Debug.Log("호스트임!");
                        //byte[] buf = Encoding.Default.GetBytes("MOVE:" + MyClient.instance.playerNum + ":" + mouse_X + ":" + mouse_Y + ":");
                        //입력및 출력
                        //MyClient.client.GetStream().Write(buf, 0, buf.Length);
                        //MyClient.client.GetStream().Flush();
                        //MyClient.instance.Send(buf);
                }
                else
                {
                    //입력및 출력
                    byte[] buf = Encoding.Default.GetBytes("MOVE:" + MyClient.instance.playerNum + ":" + mouse_X + ":" + mouse_Y + ";");
                    //MyClient.client.GetStream().Write(buf, 0, buf.Length);
                    //MyClient.client.GetStream().Flush();
                    MyClient.instance.Send(buf);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Exception during network write: " + e.Message);
            }
            //MyClientclient.GetStream().Write(buf, 0, buf.Length);
        }

    }

    public void DataSend()
    {
        //공 데이터 보내기
        byte[] buf1 = Encoding.Default.GetBytes("BALL_POSITION:" + GameManager.instance.t_ball.position.x+ ":" + GameManager.instance.t_ball.position.y + ":" + GameManager.instance.t_ball.position.z+";");
        MyClient.instance.Send(buf1);

        //플레이어 데이터 보내기
        for (int i = 0; i < GameManager.instance.t_game.Length; i++)
        {
            string command;
            Vector3 pos = GameManager.instance.t_game[i].position;
            command = "PLAYER_POSITION:"+ i + ":" + pos.x + ":" + pos.y + ":" + pos.z;
            MyClient.instance.Send(command);
        }
    }

    public void SetPlayerNum(int num)
    {
        playerNum = num;
    }

    void HostData()
    {

    }
    void ClientData()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
