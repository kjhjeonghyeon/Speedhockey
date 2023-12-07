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
        if (other.name == "Goal1")
        {
            GameManager.instance.red++;

        }
        if (other.name == "Goal2")
        {
            GameManager.instance.blue++;
        }
        MyClient.instance.Send("GOAL" + ":" + GameManager.instance.red + ":" + GameManager.instance.blue);
        StartCoroutine(goal());
    }
    IEnumerator goal()
    {
        yield return  null;
        yield return new WaitForSeconds(1);
        gameObject.transform.position = new Vector3(0, 0.604f, 0);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}