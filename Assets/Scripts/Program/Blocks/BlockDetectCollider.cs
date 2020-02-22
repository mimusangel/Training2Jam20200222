using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetectCollider : Block
{
	public BlockDetectCollider() : base(BlockType.Bool)
	{
	}

	public override bool GetBool(Robot robot)
	{
		return robot.forwardIsBlocked;
	}
}
