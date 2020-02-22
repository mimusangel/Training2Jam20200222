using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpEqual : Operator
{
	public Block A;
	public Block B;

	public OpEqual(Block a, Block b)
	{
		A = a;
		B = b;
	}

	public override bool Execute(Robot robot)
	{
		if (A.type == B.type)
		{
			switch (A.type)
			{
				case BlockType.Bool:
					return A.GetBool(robot) == B.GetBool(robot);
			}
		}
		return false;
	}
}