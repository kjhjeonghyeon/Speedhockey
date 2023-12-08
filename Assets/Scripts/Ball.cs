using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, 0.55f, transform.position.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        {
            if (MyClient.instance.playerNum == 0)
            {

                if (other.name == "Goal1")
                {
                    GameManager.instance.redScore++;
                    MyClient.instance.Send("GOAL1" + ":" + GameManager.instance.redScore + ":" + "0" + ":" + "0.604" + ":" + "0");

                }
                else if (other.name == "Goal2")
                {
                    GameManager.instance.blueScore++;
                    MyClient.instance.Send("GOAL2" + ":" + GameManager.instance.blueScore + ":" + "0" + ":" + "0.604" + ":" + "0");
                }
            }
        }

    }
}