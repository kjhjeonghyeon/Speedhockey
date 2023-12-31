using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class StartPoint
{
    public Transform[] startPoint;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text[] text = new Text[2];
    public int redScore = 0;
    public int blueScore = 0;

    GameObject[] player = new GameObject[4];
    [SerializeField] GameObject PlayerPrefab_Red_Host;
    [SerializeField] GameObject PlayerPrefab_Red;
    [SerializeField] GameObject PlayerPrefab_Blue;
    //   List<Rigidbody> m_players = new List<Rigidbody>();
    //  public Rigidbody[] m_game = new Rigidbody[4];

    List<Rigidbody> r_players = new List<Rigidbody>();
    public Rigidbody[] r_game = new Rigidbody[4];

    List<Transform> t_players = new List<Transform>();
    public Transform[] t_game = new Transform[4];
    public GameObject t_ball;
    Texture color;
    [SerializeField] StartPoint[] startPoints_Red;
    [SerializeField] StartPoint[] startPoints_Blue;

    [NonSerialized]
    public int totalPlayerNum = 0;

    [SerializeField] List<List<Transform>> startPoint;
    int speed = 3;

    public bool isStart = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < t_game.Length; i++)
        //{
        //    t_players.Add(r_game[i]);


        //}

        Screen.SetResolution(960, 540, false);
        //Time.timeScale = 0;
    }

    public void SetBallPosition(Vector3 pos)
    {
        t_ball.transform.position = pos;
    }

    public void GameStartButton()
    {
        MyClient.instance.Send("START:1");

	}

    public void GameStart()
    {
        //Time.timeScale = 1;
        isStart = true;
    }

    public void GameEnd()
    {
        //Time.timeScale = 0;
        isStart = false;
    }

    public void PlayerCreate(int totalPlayerNum)
    {
        this.totalPlayerNum = totalPlayerNum;

        for (int i = 0; i < player.Length; i++)
        {
            Destroy(player[i]);
        }

        if (totalPlayerNum <= 2)
        {
            for (int i = 0; i < totalPlayerNum; i++)
            {
                if (i % 2 == 0)
                    player[i] = Instantiate(PlayerPrefab_Red_Host, startPoints_Red[0].startPoint[0].position, Quaternion.identity);
                else
                    player[i] = Instantiate(PlayerPrefab_Blue, startPoints_Blue[0].startPoint[0].position, Quaternion.identity);
                player[i].GetComponent<PlayerMove>().SetPlayerNum(i);
                r_game[i] = player[i].GetComponent<Rigidbody>();
                t_game[i] = player[i].transform;
            }
        }
        else
        {
            int redPointNum = 0;
            int bluePointNum = 0;
            for (int i = 0; i < totalPlayerNum; i++)
            {
                if (i == 0)
                {
                    player[i] = Instantiate(PlayerPrefab_Red_Host, startPoints_Red[1].startPoint[redPointNum++].position, Quaternion.identity);
                }
                else if (i % 2 == 0)
                    player[i] = Instantiate(PlayerPrefab_Red, startPoints_Red[1].startPoint[redPointNum++].position, Quaternion.identity);
                else
                    player[i] = Instantiate(PlayerPrefab_Blue, startPoints_Blue[1].startPoint[bluePointNum++].position, Quaternion.identity);
                player[i].GetComponent<PlayerMove>().SetPlayerNum(i);
                r_game[i] = player[i].GetComponent<Rigidbody>();
                t_game[i] = player[i].transform;
            }
        }
    }


    public void ToClientSendFromHostMove(int playerNum = -1, float moveX = 0f, float moveY = 0f)
    {
        if (playerNum != -1)
        {
            //    Vector3 randomPosition = new Vector3();

            //m_players.Add(Instantiate(m_game));
            //if (m_players.Count %2 == 0)
            //{//적팀 색만들기
            //    m_players[m_players.Count / 2].GetComponent<Material>().mainTexture = color;

            //}

            //} 
            Debug.Log(playerNum + " 번 플레이어 움직여야해" + moveX + " " + moveY);
            r_game[playerNum].velocity = new Vector3(moveX, 0, moveY).normalized * speed;
            //r_game[playerNum].velocity = Vector3.zero;
			//r_game[playerNum].AddForce(new Vector3(moveX, 0, moveY).normalized * speed, ForceMode.VelocityChange);
			// Debug.Log("recieve:" + playerNum + moveX + moveX);
		}
        Debug.Log("to client");
    }


    public void ClientMove(int playerNum = -1, float moveX = 0f, float moveY = 0f)
    {
        if (playerNum != -1)
        {
            //    Vector3 randomPosition = new Vector3();

            //m_players.Add(Instantiate(m_game));
            //if (m_players.Count %2 == 0)
            //{//적팀 색만들기
            //    m_players[m_players.Count / 2].GetComponent<Material>().mainTexture = color;

            //}
            t_game[playerNum].position = new Vector3(moveX, 0, moveY);
            // Debug.Log("recieve:" + playerNum + moveX + moveX);
        }
    }


    public  void originPosisition()
    {
        StartCoroutine(ioriginPosition());
    }
    IEnumerator ioriginPosition()
    {

        //yield return null;
        //t_ball.GetComponent<MeshRenderer>().enabled = false;
        //yield return new WaitForSeconds(1);
        //t_ball.transform.position =new Vector3(0,0.604f,0);
        //t_ball.GetComponent<MeshRenderer>().enabled = true;


		yield return null;
        t_ball.gameObject.SetActive(false);
		yield return new WaitForSeconds(1);
		t_ball.transform.position = new Vector3(0, 0.604f, 0);
        t_ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        t_ball.gameObject.SetActive(true);

	}
}
