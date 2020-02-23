using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class UIInstructionRotate : MonoBehaviour, IDropHandler
{
	public TMP_Dropdown rotateDD;

	private float[] angles = { 90.0f, 180.0f, -90.0f };

	[HideInInspector]
	public StatesRotate states = null;

	private void Start()
	{
		rotateDD.ClearOptions();
		rotateDD.AddOptions(angles.Select((x) => Mathf.RoundToInt(x).ToString()).ToList<string>());
		for (int i = 0; i < angles.Length; i++)
		{
			if (angles[i] == states.rotate)
			{
				rotateDD.SetValueWithoutNotify(i);
				break;
			}
		}
	}

	public void SelectDropdown()
	{
		if (states != null)
		{
			states.rotate = angles[rotateDD.value];
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		UIInstructionIfContent content = transform.parent.gameObject.GetComponent<UIInstructionIfContent>();
		if (content)
		{
			UIRobotProg.Instance.DropInstruction(content.ifThen, states);
		}
		else
		{
			UIRobotProg.Instance.DropInstruction(states);
		}
	}
}
