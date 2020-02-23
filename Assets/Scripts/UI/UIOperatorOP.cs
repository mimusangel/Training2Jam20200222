using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
public class UIOperatorOP : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Operator operatorOP;
	public TextMeshProUGUI textOP;

	public Transform aTransform;
	[HideInInspector]
	public GameObject aGo;
	public Transform bTransform;
	[HideInInspector]
	public GameObject bGo;

	public void GenerateContent()
	{
		if (aGo != null)
		{
			Destroy(aGo);
			aGo = null;
		}
		if (bGo != null)
		{
			Destroy(bGo);
			bGo = null;
		}
		if (operatorOP.GetType() == typeof(OpAnd))
		{
			OpAnd and = operatorOP as OpAnd;
			aGo = UIRobotProg.Instance.MakeGameObjectOperator(and.A, aTransform);
			bGo = UIRobotProg.Instance.MakeGameObjectOperator(and.B, bTransform);
		}
		else if (operatorOP.GetType() == typeof(OpOr))
		{
			OpOr or = operatorOP as OpOr;
			aGo = UIRobotProg.Instance.MakeGameObjectOperator(or.A, aTransform);
			bGo = UIRobotProg.Instance.MakeGameObjectOperator(or.B, bTransform);
		}
	}
	
	public void DropOperator(int index)
	{
		if (operatorOP.GetType() == typeof(OpAnd))
		{
			OpAnd and = operatorOP as OpAnd;
			if (index == 0 && and.A == null) and.A = UIRobotProg.Instance.MakeOperator(and);
			if (index == 1 && and.B == null) and.B = UIRobotProg.Instance.MakeOperator(and);
		}
		else if (operatorOP.GetType() == typeof(OpOr))
		{
			OpOr or = operatorOP as OpOr;
			if (index == 0 && or.A == null) or.A = UIRobotProg.Instance.MakeOperator(or);
			if (index == 1 && or.B == null) or.B = UIRobotProg.Instance.MakeOperator(or);
		}
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
