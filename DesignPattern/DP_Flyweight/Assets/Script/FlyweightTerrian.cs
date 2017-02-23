using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightTerrian : MonoBehaviour {

	public Material hardMat;
	public Material normalMat;

	private FlyweightTile hardTile;
	private FlyweightTile normalTile;

	private FlyweightTile[,] tiles;
	private const int width = 5;
	private const int height = 5;
	int[,] terrain = {
		{ 0, 1, 0, 0, 0 },
		{ 0, 0, 0, 1, 0 },
		{ 1, 0, 0, 1, 0 },
		{ 1, 0, 0, 0, 0 },
		{ 0, 0, 1, 0, 0 }
	};
		
	void Start () {
		hardTile = new FlyweightTile (hardMat, true);
		normalTile = new FlyweightTile (normalMat, false);

		this.draw ();
	}

	void draw () {
		tiles = new FlyweightTile[width,height];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				if (terrain [i, j] == 0) {
					tiles [i, j] = normalTile;
				} else {
					tiles [i, j] = hardTile;
				}
			}
		}

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				GameObject obj = GameObject.CreatePrimitive (PrimitiveType.Cube);
				obj.transform.position = new Vector3 (i,0, j);
				obj.GetComponent<MeshRenderer> ().material = tiles [i, j].mat;
				obj.transform.parent = this.transform;
			}
		}
	}
}
