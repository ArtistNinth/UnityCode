using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chp00_NOC1_6
{
	public class NOC_I_6_NoiseWalk : MonoBehaviour {

		private Transform trans;

		private Walker walker;

		// Use this for initialization
		void Start () {
			trans = GetComponent<Transform>();
			
			walker = new Walker();
		}
		
		// Update is called once per frame
		void Update () {
			walker.step();
			
			Debug.Log(walker.x + " " + walker.y);
			Vector3 newpos = new Vector3(walker.x,0,walker.y);
			trans.position = newpos;
		}
	}
}

