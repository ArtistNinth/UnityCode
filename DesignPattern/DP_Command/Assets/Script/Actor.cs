using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
	
	Transform trans;
	
	Vector3 startPos;

	void Start () {		
		trans = transform;		
	}
	
	public void Move(Vector3 deltaVector){
		trans.Translate(deltaVector);
	}
	
	public void Remember(){
		startPos = transform.position;
	}
	
	public void Reset(){
		transform.position = startPos;
	}
}
