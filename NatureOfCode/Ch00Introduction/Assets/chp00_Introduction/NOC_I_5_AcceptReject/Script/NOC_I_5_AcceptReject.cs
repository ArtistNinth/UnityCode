using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chp00_NOC1_5
{
	public class NOC_I_5_AcceptReject : MonoBehaviour {
	
		private Transform trans;	
		private int count;
		private float[] randomCounts;

		// Use this for initialization
		void Start () {
			trans = GetComponent<Transform>();
			count = 10;
			randomCounts = new float[count];
			
			for(int i = 0;i<count;i++){
				GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);			
				obj.transform.SetParent(trans);
				obj.transform.localScale = new Vector3(1.0f,randomCounts[i],1.0f);
				obj.transform.position = new Vector3(trans.childCount - count/2 - 0.5f,obj.transform.localScale.y/2,0);
				
				float r = Random.Range(0.0f,1.0f);
				float g = Random.Range(0.0f,1.0f);
				float b = Random.Range(0.0f,1.0f);
				MeshRenderer render = obj.GetComponent<MeshRenderer>();
				render.material.color = new Color(r,g,b);
			}		
		}
		
		// Update is called once per frame
		void Update () {
			int index = (int)(AcceptReject()*count);
			
			randomCounts[index]+= 0.1f;
			
			GameObject single = trans.GetChild(index).gameObject; 
			single.transform.localScale = new Vector3(1.0f,randomCounts[index],1.0f);
			single.transform.position = new Vector3(single.transform.position.x,single.transform.localScale.y/2,single.transform.position.z);
		}
		
		//可能是蒙特卡洛方法
		float AcceptReject(){
			bool foundone = false;
			int hack = 0;
			while(!foundone && hack < 10000){
				float r1 = Random.Range(0.0f,1.0f);
				float r2 = Random.Range(0.0f,1.0f);
				float y = r1 * r1;
				
				if(r2 < y){
					foundone = true;

					return r1;
				}
				hack++;
			}
			
			return 0;
		}
	}
}

