using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCGround  {

	private const int groundSize = 26;
	private BCGroundElement[][] elements;

	public BCGround(){
		for (int i = 0; i < groundSize; i++) {
			for (int j = 0; j < groundSize; j++) {
				elements [i] [j] = new BCGroundElement ();
			}
		}
	}

	public void setElement(int x,int y,BCGroundElement.BCGroundElementType elementType){
		if (x >= groundSize || y >= groundSize || x < 0 || y < 0) {
			return;
		}
		elements[x][y] = BCGroundElementPool.getInstance().getElement(elementType);
	}
	
	public void setElementGroup(int x,int y,BCGroundGroup.BCGroundGroupType groupType){
		if (x % 2 != 0 || y % 2 != 0 || x >= groundSize || y >= groundSize || x < 0 || y < 0) {
			return;
		}

		BCGroundGroup group = BCGroundGroupPool.getInstance ().getGroup (groupType);
		elements [x] [y] = group.Elements [0];
		elements [x + 1] [y] = group.Elements [1];
		elements [x] [y + 1] = group.Elements [2];
		elements [x + 1] [y + 1] = group.Elements [3];
	}
}
