using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	public delegate void MyClickAction();
	public static event MyClickAction myClicked;
	
	public void handleClick(){
		if(myClicked!=null){
			myClicked();
		}
	}
}
