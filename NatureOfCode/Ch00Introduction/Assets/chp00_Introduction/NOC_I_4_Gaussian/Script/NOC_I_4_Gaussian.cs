using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chp00_NOC1_4
{
	public class NOC_I_4_Gaussian : MonoBehaviour {

		public Transform background;

		private float xRange = 10.0f;		

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {			
			float xloc = NextGaussian();
			GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			obj.transform.SetParent(background);
			obj.transform.position = new Vector3(xRange*xloc,0.5f,0);
			
			Destroy(obj,1.0f);
		}
		
		public static float NextGaussian() {
			float v1, v2, s;
			do {
				v1 = 2.0f * Random.Range(0f,1f) - 1.0f;
				v2 = 2.0f * Random.Range(0f,1f) - 1.0f;
				s = v1 * v1 + v2 * v2;
			} while (s >= 1.0f || s == 0f);

			s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);
		
			return v1 * s;
		}
	}
}

