using UnityEngine;

public class CommandMove : Command {

	Vector3 deltaVector;
	
	public CommandMove(Vector3 d,float t){
		deltaVector = d;
		m_time = t;
	}
	
	public override void execute(Actor actor){
		actor.Move(deltaVector);
	}
}
