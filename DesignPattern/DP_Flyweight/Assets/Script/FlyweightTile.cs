using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightTile {

	public Material mat;

	bool _isHard = false;
	public bool isHard{
		get{ return _isHard;}
	}

	public FlyweightTile(Material mat,bool isHard){
		this.mat = mat;
		this._isHard = isHard;
	}
}
