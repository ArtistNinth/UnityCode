using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chp01_NOC1_2_1
{
	public class NOC_I_2_1_BouncingBallVectorsObject : MonoBehaviour {

		private Transform trans;

		private Ball ball;
		

		// Use this for initialization
		void Start () {
			trans = GetComponent<Transform>();
			ball = new Ball(trans.position);
		}
		
		// Update is called once per frame
		void Update () {
			ball.step();			
			trans.position = ball.Pos;
		}
	}
}

