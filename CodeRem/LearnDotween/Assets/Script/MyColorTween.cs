using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MyColorTween : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text text = GetComponent<Text>();
		//text.DOColor(Color.red,2.0f);
		text.DOFade(1.0f,2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
