using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintStain : MonoBehaviour
{
	public StatesColor.StatesColorType color;

	Renderer render;
	private void Start()
	{
		render = GetComponent<Renderer>();
	}

	public void Paint(StatesColor.StatesColorType col)
	{
		color = col;
		render.material.SetColor("_Color", color.GetColor());
	}
}
