using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chp00_NOC1_3{
	public class Walker{
		private int _x;
		private int _y;
		
		public int x{
			get{return _x;}
		}		
		public int y{
			get{return _y;}	
		}
		
		private int limit = 50;
		
		public Walker(){
			_x = 0;
			_y = 0;
		}
		
		public void step(){
			float r = Random.Range(0.0f,1.0f);
			if(r > 0.4){
				_x++;
			}else if(r > 0.6){
				_x--;
			}else if(r > 0.8){
				_y++;
			}else{
				_y--;
			}
			
			_x = Mathf.Clamp(_x,-limit,limit);
			_y = Mathf.Clamp(_y,-limit,limit);
		}
	}
}

