using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpInf : Operator
{
	public Block A;
	public Block B;

	public OpInf(Block a = null, Block b = null)
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
					return A.GetBool(robot) != B.GetBool(robot);
			}
		}
		return false;
	}
}