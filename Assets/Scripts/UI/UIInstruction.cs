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
		UIRobotProg.Instance.DropInstruction(states);
	}
}
