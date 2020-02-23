using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class UIInstructionRepeat : MonoBehaviour
{
	public Transform contentTransform;
	public UIOperator uIOperator;

	[HideInInspector]
	public StatesRepeat states = null;

	GameObject operatorItem = null;
	List<GameObject> contentItems = new List<GameObject>();

	public void GenerateContent()
	{
		foreach (GameObject go in contentItems)
		{
			Destroy(go);
		}
		contentItems.Clear();
		if (states == null) return;

		if (operatorItem != null)
		{
			Destroy(operatorItem);
			operatorItem = null;
		}
		if (states.condition != null)
		{
			operatorItem = UIRobotProg.Instance.MakeGameObjectOperator(states.condition, uIOperator.transform);
		}

		foreach (States state in states.ifProgram)
		{
			GameObject go = UIRobotProg.Instance.MakeGameObject(state, contentTransform);
			if (go != null)
			{
				contentItems.Add(go);
			}
		}
	}

	public void DropOperator(int index)
	{
		if (states.condition != null) return;
		Operator oper = UIRobotProg.Instance.MakeOperator(states);
		if (oper != null)
		{
			states.condition = oper;
		}
		UIRobotProg.Instance.ChangeProgram();
	}

}
