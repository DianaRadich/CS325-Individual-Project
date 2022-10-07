using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Structure", order = 1)]
public class StructureScriptableObject : ScriptableObject
{
	public GameObject Structure;
	public int cost;
}
