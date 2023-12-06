using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StartPoint
{
    public Transform[] startPoint;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] player;
    [SerializeField] GameObject PlayerPrefab_Red;
    [SerializeField] GameObject PlayerPrefab_Blue;
    List<Rigidbody> m_players = new List<Rigidbody>();
    public Rigidbody[] m_game = new Rigidbody[4];
    Texture color;
    [SerializeField] StartPoint[] startPoints_Red;
    [SerializeField] StartPoint[] startPoints_Blue;

    int totalPlayerNum = 0;

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
        for (int i = 0; i < m_game.Length; i++)
        {
            m_players.Add(m_game[i]);


        }

        Screen.SetResolution(960, 540, false);

    }

    public void PlayerCreate(int totalPlayerNum)
    {
        this.totalPlayerNum = totalPlayerNum;

        if (totalPlayerNum <= 2)
        {
            for (int i = 0; i < totalPlayerNum; i++)
            {
                if (i % 2 == 0)
                {
                    player[i] = Instantiate(PlayerPrefab_Red, startPoints_Red[0].startPoint[0].position, Quaternion.identity);
                }
                else
                {
                    player[i] = Instantiate(PlayerPrefab_Blue, startPoints_Blue[0].startPoint[0].position, Quaternion.identity);
                }
                player[i].GetComponent<PlayerMove>().SetPlayerNum(i);
            }
        }
        else
        {
            int redPointNum = 0;
            int bluePointNum = 0;
            for (int i = 0; i < totalPlayerNum; i++)
            {
                if (i % 2 == 0)
                    player[i] = Instantiate(PlayerPrefab_Red, startPoints_Red[1].startPoint[redPointNum++].position, Quaternion.identity);
                else
                    player[i] = Instantiate(PlayerPrefab_Blue, startPoints_Blue[1].startPoint[bluePointNum++].position, Quaternion.identity);
                player[i].GetComponent<PlayerMove>().SetPlayerNum(i);
            }
        }
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
            m_game[playerNum].velocity = new Vector3(moveX, 0, moveY);


            // Debug.Log("recieve:" + playerNum + moveX + moveX);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
