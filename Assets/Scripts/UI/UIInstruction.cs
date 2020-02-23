using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIInstruction : MonoBehaviour, IDropHandler
{
	public TextMeshProUGUI textInstruction;

	[HideInInspector]
	public States states;

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
