using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    List<Rigidbody> m_players = new List<Rigidbody>();
    public Rigidbody[] m_game = new Rigidbody[4];
    Texture color;
    [SerializeField] List<List<Transform>> startPoint;

    private void Awake()
    {
        if(instance == null)
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
