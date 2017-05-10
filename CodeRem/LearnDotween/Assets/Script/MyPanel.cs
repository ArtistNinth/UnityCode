using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RectTransform rt = GetComponent<RectTransform>();
		Tweener tw = rt.DOLocalMoveX(0,1);
		tw.SetEase(Ease.Linear);
		tw.SetLoops(2);
		tw.OnComplete(TweenFinished);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void TweenFinished(){
		Debug.Log("TweenFinished");
	}
}
