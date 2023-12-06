using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomData
{
    public int playerMaxNum { get; set; } = 2;

    public void SetPlayerMaxNum(int playerMaxNum)
    {
        this.playerMaxNum = playerMaxNum;
    }
}

public class GameRoom : MonoBehaviour
{
    GameRoomData gameRoomData = new GameRoomData();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
