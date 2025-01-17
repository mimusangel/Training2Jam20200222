﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
public class UIOperatorNot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public OpNot operatorOP;
	public TextMeshProUGUI textOP;

	public Transform aTransform;
	[HideInInspector]
	public GameObject aGo;

	public void GenerateContent()
	{
		if (aGo != null)
		{
			Destroy(aGo);
			aGo = null;
		}
		if (operatorOP.A != null) aGo = UIRobotProg.Instance.MakeGameObjectOperator(operatorOP.A, aTransform);
	}


	public void DropOperator(int index)
	{
		if (operatorOP.A == null) operatorOP.A = UIRobotProg.Instance.MakeOperator(operatorOP);
		UIRobotProg.Instance.ChangeProgram();
	}


	public void OnBeginDrag(PointerEventData eventData)
	{
		UIDragToTrash.StartDrag(operatorOP);
	}

	public void OnDrag(PointerEventData eventData)
	{

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		UIDragToTrash.EndDrag();
	}
}
