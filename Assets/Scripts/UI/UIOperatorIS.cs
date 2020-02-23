using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class UIOperatorIS : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public OpIs operatorOP;
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

		if (operatorOP.A != null)
		{
			aGo = Instantiate(Resources.Load<GameObject>("UI/Block"), aTransform);
			UIBlock ui = aGo.GetComponent<UIBlock>();
			ui.block = operatorOP.A;
		}
	}


	public void DropOperator(int index)
	{
		if (operatorOP.A == null) operatorOP.A = UIRobotProg.Instance.MakeBlock(operatorOP);
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
