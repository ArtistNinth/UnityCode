using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerProperty : MonoBehaviour {

	public string ip;
    public string name;
	public int count;

    void Start() {
        GetComponentInChildren<Text>().text = name;
    }
}
