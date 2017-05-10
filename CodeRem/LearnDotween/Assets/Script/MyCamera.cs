using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.DOShakePosition(1.0f,new Vector3(1.0f,1.0f,0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
