using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnColorScript : MonoBehaviour {
	
	void OnEnable(){
		EventManager.myClicked += TurnColor;
	}
	
	void OnDisable(){
		EventManager.myClicked -= TurnColor;
	}

	void TurnColor(){
		Color col = new Color(Random.value,Random.value,Random.value);
		GetComponent<MeshRenderer>().material.color = col;
	}
}
