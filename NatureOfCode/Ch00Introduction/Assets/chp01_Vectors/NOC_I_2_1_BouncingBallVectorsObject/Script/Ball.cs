using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chp01_NOC1_2_1{
	
	public class Ball{
		private Vector3 position;
		private Vector3 velocity;
		public Vector3 Pos{
			get{return position;}	
		}
		
		private float xLimit = 50;
		private float zLimit = 25;
		
		public Ball(Vector3 pos){
			position = pos;
			velocity = new Vector3(0.25f,0,0.2f);
		}
		
		public void step () {
			position += new Vector3(velocity.x,0,velocity.z);
			
			if((position.x > xLimit) || (position.x < -xLimit)){
				Vector3 newVelocity = new Vector3(velocity.x*-1,velocity.y,velocity.z);
				velocity = newVelocity;
			}
			
			if((position.z > zLimit) || (position.z < -zLimit)){
				Vector3 newVelocity = new Vector3(velocity.x,velocity.y,velocity.z*-1);
				velocity = newVelocity;
			}
		}
	}
}
