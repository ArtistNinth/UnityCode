using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCGroundGroupPool {

	private Dictionary<BCGroundGroup.BCGroundGroupType,BCGroundGroup> groups;

	private static BCGroundGroupPool instance;

	private BCGroundGroupPool(){
		groups = new Dictionary<BCGroundGroup.BCGroundGroupType,BCGroundGroup> ();
	}

	public static BCGroundGroupPool getInstance(){
		if (instance == null) {
			instance = new BCGroundGroupPool ();
		}
		return instance;
	}

	public BCGroundGroup getGroup(BCGroundGroup.BCGroundGroupType groupType){
		if(!groups.ContainsKey(groupType)){			
			groups.Add(groupType,new BCGroundGroup(groupType));
		}
		return groups[groupType];
	}
}
