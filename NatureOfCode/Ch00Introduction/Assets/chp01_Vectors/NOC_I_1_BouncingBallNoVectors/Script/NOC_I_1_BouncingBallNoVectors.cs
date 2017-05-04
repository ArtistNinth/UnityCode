using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chp01_NOC1_1
{
	public class NOC_I_1_BouncingBallNoVectors : MonoBehaviour {

		private Transform trans;

		private float x = 0;
		private float y = 10;
		private float xSpeed = 0.25f;
		private float ySpeed = 0.2f;
		private float xLimit = 50;
		private float yLimit = 25f;

		// Use this for initialization
		void Start () {
			trans = GetComponent<Transform>();
		}
		
		// Update is called once per frame
		void Update () {
			
			x = x + xSpeed;
			y = y + ySpeed;
			
			if((x > xLimit) || (x < -xLimit)){
				xSpeed = xSpeed*-1;
			}
			if((y > yLimit) || (y < -yLimit)){
				ySpeed = ySpeed*-1;
			}
			
			Vector3 newpos = new Vector3(x,0,y);
			trans.position = newpos;
		}
	}
}

