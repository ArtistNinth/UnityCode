using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour {
	
	public Actor actor;
	public bool playback;
	
	bool actorReseted = false;
	
	Queue<Command> commandQueue;
	float callbackTime;
	
	void Start(){
		commandQueue = new Queue<Command>();
		
		Reset();
	}
	
	void Update(){
		if(!playback){
			Control();
		}else{
			PlayAgain();
		}
	}
	
	void Reset(){
		actor.Remember();
			
		callbackTime = 0;
		playback = false;
		actorReseted = false;
	}
	
	void Control(){
		callbackTime += Time.deltaTime;
		
		Command cmd = InputHandler();
		if(cmd!=null){
			commandQueue.Enqueue(cmd);
			cmd.execute(actor);
		}
	}
	
	void PlayAgain(){
		if(!actorReseted){
			actor.Reset();
			callbackTime = 0;			
			
			actorReseted = true;
		}
		
		callbackTime += Time.deltaTime;
		
		if (commandQueue.Count > 0){			
			Command cmd = commandQueue.Peek();
			
			if(callbackTime > cmd.time){
				commandQueue.Dequeue();
				cmd.execute(actor);
			}
		}else{
			Reset();
		}
	}
	
	
	Command InputHandler(){
		Command cmd = null;
		
		if(Input.GetKey(KeyCode.W)){
			cmd = new CommandMove(new Vector3(0,1,0),callbackTime);	
		}else if(Input.GetKey(KeyCode.S)){
			cmd = new CommandMove(new Vector3(0,-1,0),callbackTime);
		}else if(Input.GetKey(KeyCode.A)){
			cmd = new CommandMove(new Vector3(-1,0,0),callbackTime);
		}else if(Input.GetKey(KeyCode.D)){
			cmd = new CommandMove(new Vector3(1,0,0),callbackTime);
		}
		
		return cmd;
	}
}
