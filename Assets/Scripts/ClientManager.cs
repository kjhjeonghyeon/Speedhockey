using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ClientManager : MonoBehaviour
{
    public static ClientManager instance;

	private static readonly ConcurrentQueue<System.Action> _executionQueue = new ConcurrentQueue<System.Action>();

	public static void Enqueue(System.Action action)
	{
		_executionQueue.Enqueue(action);
	}

    

	[SerializeField] GameObject startButton;
    //bool startPossibility = false;

    void Awake()
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

    private void Start()
    {
        MyClient.instance.Connect();

        //startButton.SetActive(false);

        //myClient.Connect();
    }

	private void Update()
	{
		while (_executionQueue.TryDequeue(out var action))
		{
			action?.Invoke();
		}
	}


	public void StartButton_SetActive_True()
    {
        startButton.SetActive(true);
    }

    public void StartButton_Interactable_True()
    {
        startButton.GetComponent<Button>().interactable = true;
    }

    public void StartButton_Interactable_False()
    {
        startButton.GetComponent<Button>().interactable = false;
    }

    private void OnDestroy()
    {
        MyClient.instance.Send("USER_DISCONNECTED");

        MyClient.instance.Close();
    }

    //void clientNum()
    //{
    //    for (int i = 0; i < myServer.socketList.Count; i++)
    //    {
    //        string c = i.ToString();
    //        byte[] buff = Encoding.Default.GetBytes(c);

    //        myServer.socketList[i].Send(buff);
    //    }


    //}
  

}