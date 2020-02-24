using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class UIInstructionColor : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public TMP_Dropdown colorDD;

	[HideInInspector]
	public StatesColor states = null;

	private void Start()
	{
		colorDD.ClearOptions();
		List<string> colors = StatesColorExtends.ToList();
		colorDD.AddOptions(colors);
		colorDD.SetValueWithoutNotify((int)states.color);
	}

	public void SelectDropdown()
	{
		if (states != null)
		{
			states.color = (StatesColor.StatesColorType)colorDD.value;
		}
	}

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
