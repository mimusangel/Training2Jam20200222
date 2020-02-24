using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetectColor : Block
{
	public BlockDetectColor() : base(BlockType.Color)
	{

	}

	public override StatesColor.StatesColorType GetColor(Robot robot = null)
	{
		if (robot.paintStain != null) return robot.paintStain.color;
		return StatesColor.StatesColorType.None;
	}
}
