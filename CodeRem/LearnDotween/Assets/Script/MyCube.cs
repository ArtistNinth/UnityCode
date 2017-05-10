using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.DOMoveX(5,4).From(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
