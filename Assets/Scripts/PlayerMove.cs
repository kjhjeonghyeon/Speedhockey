using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Vector3 movePosition;
    int speed = 3;
    Rigidbody rb;
    int playerNum;
    float mouse_X;

    float mouse_Y;

	// Start is called before the first frame update
	void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isStart)
            return;

        InputMove();

        //Debug.Log(rb.velocity);
        if (MyClient.instance.playerNum == 0)
            DataSend();


    }
    void InputMove()
    {
        mouse_X = Input.GetAxis("Mouse X");
        mouse_Y = Input.GetAxis("Mouse Y");

        if (MyClient.instance != null)
        {
            //Debug.Log(MyClient.client + " " + MyClient.client.Connected);
            movePosition.x = mouse_X;
            movePosition.z = mouse_Y;
            
            //Debug.Log(rb.velocity);
            //transform.position += movePosition * Time.deltaTime * speed;

            try
            {
                if (MyClient.instance.playerNum == 0)
                {
                    rb.velocity = movePosition.normalized * speed;
                    //rb.velocity = Vector3.zero;
					//rb.AddForce(movePosition.normalized * speed, ForceMode.VelocityChange);
                    
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
		string command="";
        command += "BALL_POSITION:" + GameManager.instance.t_ball.transform.position.x + ":" + GameManager.instance.t_ball.transform.position.y + ":" + GameManager.instance.t_ball.transform.position.z + ";";


		//플레이어 데이터 보내기
		for (int i = 0; i < GameManager.instance.totalPlayerNum; i++)
        {
            
            if (GameManager.instance.t_game[i] == null)
                break;
            Vector3 pos = GameManager.instance.t_game[i].position;
            command += "PLAYER_POSITION:" + i + ":" + pos.x + ":" + pos.y + ":" + pos.z+";";
        }
        MyClient.instance.Send(command);
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
