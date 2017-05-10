using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MyText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text text = GetComponent<Text>();
		text.DOText("下一章正在加载，想看看吗，那就继续玩吧。GO GO GO! Wonderful",4.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
