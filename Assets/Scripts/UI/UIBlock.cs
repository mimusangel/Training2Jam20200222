using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBlock : MonoBehaviour
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
}
