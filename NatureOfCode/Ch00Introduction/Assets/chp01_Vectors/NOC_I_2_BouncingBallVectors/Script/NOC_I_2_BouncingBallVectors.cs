using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chp01_NOC1_2
{
	public class NOC_I_2_BouncingBallVectors : MonoBehaviour {

		private Transform trans;

		private Vector3 position;
		private Vector3 velocity;
		
		private float xLimit = 50;
		private float zLimit = 25;

		// Use this for initialization
		void Start () {
			trans = GetComponent<Transform>();
			position = trans.position;
			velocity = new Vector3(0.25f,0,0.2f);
		}
		
		// Update is called once per frame
		void Update () {
			position += new Vector3(velocity.x,0,velocity.z);
			
			if((position.x > xLimit) || (position.x < -xLimit)){
				Vector3 newVelocity = new Vector3(velocity.x*-1,velocity.y,velocity.z);
				velocity = newVelocity;
			}
			
			if((position.z > zLimit) || (position.z < -zLimit)){
				Vector3 newVelocity = new Vector3(velocity.x,velocity.y,velocity.z*-1);
				velocity = newVelocity;
			}
			
			trans.position = position;
		}
	}
}

