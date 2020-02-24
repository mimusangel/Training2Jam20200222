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
	public TMP_Dropdown dropdown;

	[HideInInspector]
	public Block block;

	private void Start()
	{
		dropdown.gameObject.SetActive(false);
		if (block.GetType() == typeof(BlockDetectCollider))
		{
			textBlock.text = "Detection de la Collision";
		}
		else if (block.GetType() == typeof(BlockDetectColor))
		{
			textBlock.text = "Detection de la Couleur";
		}
		else
		{
			switch (block.type)
			{
				case BlockType.Bool:
					textBlock.text = block.GetBool() ? "True" : "False";
					break;
				case BlockType.Color:
					textBlock.text = "Couleur";
					dropdown.gameObject.SetActive(true);
					dropdown.ClearOptions();
					List<string> colors = StatesColorExtends.ToList();
					dropdown.AddOptions(colors);
					StatesColor.StatesColorType color = block.GetColor();
					dropdown.SetValueWithoutNotify((int)color);
					break;
			}
		}
	}

	public void SelectDropdown()
	{
		if (block.type == BlockType.Color)
		{
			block.SetColor((StatesColor.StatesColorType)dropdown.value);
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
