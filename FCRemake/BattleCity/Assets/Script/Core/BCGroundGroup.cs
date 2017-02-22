using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCGroundGroup {
	
	public enum BCGroundGroupType{
		MetalWallAll,
		MetalWallUp,
		MetalWallDown,
		MetalWallLeft,
		MetalWallRight,
		BrickWallAll,
		BrickWallUp,
		BrickWallDown,
		BrickWallLeft,
		BrickWallRight,
		RiverAll,
		SandAll,
		ForestAll
	};

	private BCGroundGroupType groupType;
	public BCGroundGroupType GroupType{
		get{ return groupType;}
		set{ groupType = value;}
	}

	private const int groupSize = 4;
	private List<BCGroundElement> elements;
	public List<BCGroundElement> Elements{
		get{ return elements; }
	}

	public BCGroundGroup(BCGroundGroupType groupType){
		elements = new List<BCGroundElement>();

		BCGroundElementPool pool = BCGroundElementPool.getInstance ();
		switch (groupType) {
		case BCGroundGroupType.MetalWallAll:
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.MetalWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.MetalWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.MetalWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.MetalWall));
			break;
		case BCGroundGroupType.MetalWallUp:
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.MetalWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.MetalWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Empty));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Empty));
			break;
		case BCGroundGroupType.MetalWallDown:
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Empty));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Empty));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.MetalWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.MetalWall));
			break;
		case BCGroundGroupType.MetalWallLeft:
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.MetalWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Empty));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.MetalWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Empty));
			break;
		case BCGroundGroupType.MetalWallRight:
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Empty));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.MetalWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Empty));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.MetalWall));
			break;
		case BCGroundGroupType.BrickWallAll:
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.BrickWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.BrickWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.BrickWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.BrickWall));
			break;
		case BCGroundGroupType.BrickWallUp:
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.BrickWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.BrickWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Empty));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Empty));
			break;
		case BCGroundGroupType.BrickWallDown:
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Empty));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Empty));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.BrickWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.BrickWall));
			break;
		case BCGroundGroupType.BrickWallLeft:
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.BrickWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Empty));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.BrickWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Empty));
			break;
		case BCGroundGroupType.BrickWallRight:
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Empty));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.BrickWall));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Empty));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.BrickWall));
			break;
		case BCGroundGroupType.RiverAll:
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.River));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.River));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.River));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.River));
			break;
		case BCGroundGroupType.SandAll:
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Sand));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Sand));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Sand));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Sand));
			break;
		case BCGroundGroupType.ForestAll:
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Forest));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Forest));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Forest));
			elements.Add(pool.getElement (BCGroundElement.BCGroundElementType.Forest));
			break;
		default:
			break;
		}
	}
}
