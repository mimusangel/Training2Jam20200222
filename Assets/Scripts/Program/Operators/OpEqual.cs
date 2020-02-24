using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpEqual : Operator
{
	public Block A;
	public Block B;

	public OpEqual(Block a = null, Block b = null)
	{
		A = a;
		B = b;
	}

	public override bool Execute(Robot robot)
	{
		if (A == null || B == null) return false;
		if (A.type == B.type)
		{
			switch (A.type)
			{
				case BlockType.Bool:
					return A.GetBool(robot) == B.GetBool(robot);
				case BlockType.Color:
					return A.GetColor(robot) == B.GetColor(robot);
			}
		}
		return false;
	}

	public override void RemoveChild(Block blo)
	{
		if (A == blo) A = null;
		if (B == blo) B = null;
	}
}