public class Command  {

	protected float m_time;
	public float time{
		get{return m_time;}
	}
	
	public virtual void execute(Actor actor){}
}
