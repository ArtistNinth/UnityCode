using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chp00_NOC1_6{
	public class Walker {
		private int _x;
		private int _y;
		
		private int limit = 50;		
		
		public int x{
			get{return _x;}
		}		
		public int y{
			get{return _y;}	
		}
		
		public Walker(){
			_x = 0;
			_y = 0;
		}
		
		//Perlin噪声的使用
		public void step(){
			_x = (int)(limit * Mathf.PerlinNoise(Time.time,0.0f));
			_y = (int)(limit * Mathf.PerlinNoise(0.0f,Time.time));
		}
	}
}


