using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class UIBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public TextMeshProUGUI textBlock;
	[HideInInspector]
	public Block block;

	private void Start()
	{
		if (block.GetType() == typeof(BlockDetectCollider))
		{
			textBlock.text = "Detection de la Collision";
		}
		else
		{
			switch (block.type)
			{
				case BlockType.Bool:
					textBlock.text = block.GetBool() ? "True" : "False";
					break;
			}
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		UIDragToTrash.StartDrag(block);
	}

	public void OnDrag(PointerEventData eventData)
	{

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		UIDragToTrash.EndDrag();
	}
}
