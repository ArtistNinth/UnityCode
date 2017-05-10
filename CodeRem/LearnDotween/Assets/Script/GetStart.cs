using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GetStart : MonoBehaviour {
	
	private RectTransform rt;
	public Vector3 myValue;
	public float myValue2;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();
		myValue = rt.localPosition;
		DOTween.To(()=>myValue,x=>myValue=x,new Vector3(0,0,0),2);
		
		DOTween.To(()=>myValue2,x=>myValue2=x,10.0f,2);
	}
	
	// Update is called once per frame
	void Update () {
		rt.localPosition = myValue;
		
	}
}
