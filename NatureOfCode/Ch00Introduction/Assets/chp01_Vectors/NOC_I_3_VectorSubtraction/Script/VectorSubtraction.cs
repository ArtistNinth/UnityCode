using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorSubtraction : MonoBehaviour {

	private LineRenderer line;

	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 lineScreenPos = Camera.main.WorldToScreenPoint(line.transform.position);
		Vector3 mousePos = Input.mousePosition;
		
		Vector3 deltaPos = mousePos - lineScreenPos;
		
		
		line.SetPosition(1,deltaPos.normalized);
	}
}
