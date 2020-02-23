using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UINewTool : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public enum Tool
	{
		None = -1,

		I_Forward = 0,
		I_Backward,
		I_Rotate,
		I_If,
		I_IfElse,
		I_Paint,
		I_Repeat,

		O_Or = 100,
		O_And,
		O_Not,
		O_Equal,
		O_Sup,
		O_Inf,
		O_Is,

		B_True = 200,
		B_False,
		B_DetectCollid,
		B_DetectColor
	}

	public static Tool ToolDragAndDrop = Tool.None;

	public Tool tool;

	public void OnBeginDrag(PointerEventData eventData)
	{
		ToolDragAndDrop = tool;
	}

	public void OnDrag(PointerEventData eventData)
	{

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		ToolDragAndDrop = Tool.None;
	}
}
