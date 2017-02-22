using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCGroundElementPool {

	private Dictionary<BCGroundElement.BCGroundElementType,BCGroundElement> elements;
	private static BCGroundElementPool instance;

	private BCGroundElementPool(){
		elements = new Dictionary<BCGroundElement.BCGroundElementType,BCGroundElement> ();
	}

	public static BCGroundElementPool getInstance(){
		if (instance == null) {
			instance = new BCGroundElementPool ();
		}
		return instance;
	}

	public BCGroundElement getElement(BCGroundElement.BCGroundElementType elementType){
		if (!elements.ContainsKey (elementType)) {
			BCGroundElement element = new BCGroundElement ();
			element.ElementType = elementType;
			elements [element.ElementType] = element;
		}

		return elements [elementType];
	}
}
