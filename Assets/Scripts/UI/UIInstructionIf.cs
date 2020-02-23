using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class UIInstructionIf : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public UIInstructionIfThen ifThen;

	public void OnDrop(PointerEventData eventData)
	{
		UIRobotProg.Instance.DropInstruction(ifThen, null);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		UIDragToTrash.StartDrag(ifThen.states);
	}

	public void OnDrag(PointerEventData eventData)
	{

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		UIDragToTrash.EndDrag();
	}
}
