using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chp00_NOC1_1{
	public class Walker {
		private int _x;
		private int _y;
		
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
		
		public void step(){
			int choice = Random.Range(0,4);
			if(choice == 0){
				_x++;
			}else if(choice == 1){
				_x--;
			}else if(choice == 2){
				_y++;
			}else{
				_y--;
			}
			
			int limit = 5;
			if(_x > limit){
				_x = limit;
			}
			if(_x < -limit){
				_x = -limit;
			}
			if(_y > limit){
				_y = limit;
			}
			if(_y < -limit){
				_y = -limit;
			}
		}
	}
}


