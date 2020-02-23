using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class UIOperatorBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Operator operatorBlock;
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
		if (operatorBlock.GetType() == typeof(OpEqual))
		{
			OpEqual ope = operatorBlock as OpEqual;
			if (ope.A != null)
			{
				GameObject go = Instantiate(Resources.Load<GameObject>("UI/Block"), aTransform);
				UIBlock ui = go.GetComponent<UIBlock>();
				ui.block = ope.A;
			}
			if (ope.B != null)
			{
				GameObject go = Instantiate(Resources.Load<GameObject>("UI/Block"), bTransform);
				UIBlock ui = go.GetComponent<UIBlock>();
				ui.block = ope.B;
			}
		}
		else if (operatorBlock.GetType() == typeof(OpInf))
		{
			OpInf ope = operatorBlock as OpInf;
			if (ope.A != null)
			{
				GameObject go = Instantiate(Resources.Load<GameObject>("UI/Block"), aTransform);
				UIBlock ui = go.GetComponent<UIBlock>();
				ui.block = ope.A;
			}
			if (ope.B != null)
			{
				GameObject go = Instantiate(Resources.Load<GameObject>("UI/Block"), bTransform);
				UIBlock ui = go.GetComponent<UIBlock>();
				ui.block = ope.B;
			}

		}
		else if (operatorBlock.GetType() == typeof(OpSup))
		{
			OpSup ope = operatorBlock as OpSup;
			if (ope.A != null)
			{
				GameObject go = Instantiate(Resources.Load<GameObject>("UI/Block"), aTransform);
				UIBlock ui = go.GetComponent<UIBlock>();
				ui.block = ope.A;
			}
			if (ope.B != null)
			{
				GameObject go = Instantiate(Resources.Load<GameObject>("UI/Block"), bTransform);
				UIBlock ui = go.GetComponent<UIBlock>();
				ui.block = ope.B;
			}

		}
	}

	

	public void DropOperator(int index)
	{
		if (operatorBlock.GetType() == typeof(OpEqual))
		{
			OpEqual ope = operatorBlock as OpEqual;
			if (index == 0 && ope.A == null) ope.A = UIRobotProg.Instance.MakeBlock(ope);
			if (index == 1 && ope.B == null) ope.B = UIRobotProg.Instance.MakeBlock(ope);
		}
		else if (operatorBlock.GetType() == typeof(OpInf))
		{
			OpInf ope = operatorBlock as OpInf;
			if (index == 0 && ope.A == null) ope.A = UIRobotProg.Instance.MakeBlock(ope);
			if (index == 1 && ope.B == null) ope.B = UIRobotProg.Instance.MakeBlock(ope);

		}
		else if (operatorBlock.GetType() == typeof(OpSup))
		{
			OpSup ope = operatorBlock as OpSup;
			if (index == 0 && ope.A == null) ope.A = UIRobotProg.Instance.MakeBlock(ope);
			if (index == 1 && ope.B == null) ope.B = UIRobotProg.Instance.MakeBlock(ope);

		}
		UIRobotProg.Instance.ChangeProgram();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		UIDragToTrash.StartDrag(operatorBlock);
	}

	public void OnDrag(PointerEventData eventData)
	{

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		UIDragToTrash.EndDrag();
	}
}
