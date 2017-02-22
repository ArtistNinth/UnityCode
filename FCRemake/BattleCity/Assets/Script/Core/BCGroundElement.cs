using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCGroundElement {

	public enum BCGroundElementType
	{
		Empty,
		MetalWall,
		BrickWall,
		River,
		Sand,
		Forest
	}

	private BCGroundElementType elementType;
	public BCGroundElementType ElementType{
		get{ return elementType;}
		set{ elementType = value;}
	}

	public BCGroundElement(){
		this.elementType = BCGroundElementType.Empty;
	}
}

