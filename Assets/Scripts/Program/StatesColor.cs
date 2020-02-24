using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class StatesColor : States
{
	public enum StatesColorType
	{
		None = -1,
		[Description("Blanc")]
		White = 0,
		[Description("Gris")]
		Gray,
		[Description("Noir")]
		Black,
		[Description("Rouge")]
		Red,
		[Description("Vert")]
		Green,
		[Description("Bleu")]
		Blue,
		[Description("Jaune")]
		Yellow,
		Magenta,
		Cyan,

		Count
	}

	public StatesColorType color;

	public StatesColor(StatesColorType col = StatesColorType.White)
	{
		color = col;
	}

	public override IEnumerator Execute(Robot robot)
	{
		Vector3 paintPos = robot.transform.position + robot.transform.forward;
		paintPos.y += 0.01f;
		yield return new WaitForSeconds(0.5f);
		if (robot.paintStainForward != null)
		{
			if (color != StatesColorType.None)
				robot.paintStainForward.Paint(color);
			else
				GameObject.Destroy(robot.paintStainForward.gameObject);
		}
		else
		{
			if (color != StatesColorType.None)
			{
				GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("PaintStain"), paintPos, Quaternion.Euler(90, 0, 0));
				PaintStain ps = go.GetComponent<PaintStain>();
				ps.Paint(color);
			}
		}
	}
}

public static class StatesColorExtends
{

	public static Color GetColor(this StatesColor.StatesColorType col)
	{
		switch (col)
		{
			case StatesColor.StatesColorType.Gray:
				return Color.gray;
			case StatesColor.StatesColorType.Black:
				return Color.black;
			case StatesColor.StatesColorType.Red:
				return Color.red;
			case StatesColor.StatesColorType.Green:
				return Color.green;
			case StatesColor.StatesColorType.Blue:
				return Color.blue;
			case StatesColor.StatesColorType.Yellow:
				return Color.yellow;
			case StatesColor.StatesColorType.Magenta:
				return Color.magenta;
			case StatesColor.StatesColorType.Cyan:
				return Color.cyan;
		}
		return Color.white;
	}

	public static string ToDescription(this Enum value)
	{
		DescriptionAttribute[] da = (DescriptionAttribute[])(value.GetType().GetField(value.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
		return da.Length > 0 ? da[0].Description : value.ToString();
	}

	public static List<string> ToList()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < (int)(StatesColor.StatesColorType.Count); i++)
		{
			StatesColor.StatesColorType e = (StatesColor.StatesColorType)i;
			list.Add(e.ToDescription());
		}
		return list;
	}

}

