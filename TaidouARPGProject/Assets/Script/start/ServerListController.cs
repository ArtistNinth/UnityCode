using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerListController : MonoBehaviour {
	
	public GameObject serverRed;
	public GameObject serverGreen;

	// Use this for initialization
	void Start () {
		for(int i=0;i<5;i++){
			string ip = "127.0.0.1";
			string name = i + " 区 马达加斯加";
            int count = Random.Range(1, 100);
			
			GameObject serverInstance;
			if(count > 50)
			{
				serverInstance = Instantiate(serverRed);	
			}else{
				serverInstance = Instantiate(serverGreen);
			}
            ServerProperty sp = serverInstance.GetComponent<ServerProperty>();
            sp.ip = ip;
            sp.name = name;
            sp.count = count;
            serverInstance.transform.SetParent(transform);
            serverInstance.transform.localScale = Vector3.one;  //很重要的一句，不然大小会乱
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
