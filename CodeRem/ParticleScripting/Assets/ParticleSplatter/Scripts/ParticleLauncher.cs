using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour {
	
	public ParticleSystem particleLauncher;
	public ParticleSystem splatterParticles;
	public Gradient particleColorGradient;
	public ParticleDecalPool splatDecalPool;
	
	List<ParticleCollisionEvent> collisionEvents;

	// Use this for initialization
	void Start () {
		collisionEvents = new List<ParticleCollisionEvent>();
	}
	
	void OnParticleCollision(GameObject other){
		ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher,other,collisionEvents);
		
		for (int i = 0; i < collisionEvents.Count; i++)
		{
			splatDecalPool.ParticleHit(collisionEvents[i],particleColorGradient);
			EmitAtLocation(collisionEvents[i]);	
		}		
		
	}
	
	void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent){
		ParticleSystem.MainModule psMain = splatterParticles.main;
		psMain.startColor = particleColorGradient.Evaluate(Random.Range(0.0f,1.0f));
		
		splatterParticles.transform.position = particleCollisionEvent.intersection;
		splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
		splatterParticles.Emit(1);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1")){
			ParticleSystem.MainModule psMain = particleLauncher.main;
			psMain.startColor = particleColorGradient.Evaluate(Random.Range(0.0f,1.0f));	//粒子颜色
			
			particleLauncher.Emit(1);	//发射一个粒子				
		}		
	}
}
