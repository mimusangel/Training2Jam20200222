﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIInstruction : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public TextMeshProUGUI textInstruction;

	[HideInInspector]
	public States states;

	public void OnDrop(PointerEventData eventData)
	{
		UIRobotProg.Instance.DropInstruction(states, transform.parent.gameObject);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		UIDragToTrash.StartDrag(states);
	}

	public void OnDrag(PointerEventData eventData)
	{

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		UIDragToTrash.EndDrag();
	}
}
