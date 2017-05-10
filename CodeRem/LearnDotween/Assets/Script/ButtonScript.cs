using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonScript : MonoBehaviour {

	public RectTransform rt;
	
	private bool isIn = false;
	
	public void Start(){
		Tweener tw = rt.DOLocalMove(new Vector3(0,0,0),0.3f,true);
		tw.SetAutoKill(false);
		tw.Pause();
	}
	
	public void OnClick(){
		if(isIn){			
			isIn = false;
			rt.DOPlayBackwards();			
		}else{
			isIn = true;
			rt.DOPlayForward();
		}
	}
}
