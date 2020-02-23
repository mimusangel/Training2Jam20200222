using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;


public class UIOperator : MonoBehaviour, IDropHandler
{
	public GameObject link;
	public int index = 0;

	public void OnDrop(PointerEventData eventData)
	{
		link.SendMessage("DropOperator", index);
	}

}
