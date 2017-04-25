using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chp00_NOC1_1{
	public class Walker {
		private int _x;
		private int _y;
		
		private int limit = 5;
		
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
			
			_x = Mathf.Clamp(_x,-limit,limit);
			_y = Mathf.Clamp(_y,-limit,limit);
		}
	}
}


