using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class UIInstructionRepeatThen : MonoBehaviour, IDropHandler
{
	public UIInstructionRepeat repeat;

	public void OnDrop(PointerEventData eventData)
	{
		UIRobotProg.Instance.DropInstruction(repeat.states, repeat.transform.parent.gameObject);
	}
}
