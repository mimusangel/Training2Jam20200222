using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class UITrash : MonoBehaviour, IDropHandler
{
	public void OnDrop(PointerEventData eventData)
	{
		UIDragToTrash.ToTrash();
	}
}
