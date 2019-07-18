using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Manager : MonoBehaviour {

	public Dictionary<ObjectType, List<GameObject>> m_Objects = new Dictionary<ObjectType,List<GameObject>>();
	public static Manager inst;
	
	void Awake(){
		inst = this;
	}

	public enum ObjectType
	{
		WeaponLight,
		WeaponHeavy,
		Enemy,
		SomeMoreTagLikeObjectsHere,
	}
	
	public List<GameObject> GetType(ObjectType type)
	{
		if(m_Objects.ContainsKey(type))
		{
			return m_Objects[type];
		}
		return null;
	}
	
	public void Register(GameObject gObject, ObjectType type)
	{
		if(m_Objects.ContainsKey(type))
		{
			m_Objects[type].Add(gObject);
		}
		else
		{
			m_Objects.Add(type, new List<GameObject>());
			m_Objects[type].Add(gObject);
		}
	}
}
