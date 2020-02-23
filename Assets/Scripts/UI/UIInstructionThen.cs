using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class UIInstructionThen : MonoBehaviour, IDropHandler
{
	public UIInstructionIfThen ifThen;

	public void OnDrop(PointerEventData eventData)
	{
		UIRobotProg.Instance.DropInstruction(ifThen.states/*, ifThen.transform.parent*/);
	}
}
