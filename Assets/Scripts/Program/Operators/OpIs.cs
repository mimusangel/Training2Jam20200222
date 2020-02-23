using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpIs : Operator
{
	public Block A;

	public OpIs(Block a = null)
	{
		A = a;
	}

	public override bool Execute(Robot robot)
	{
		if (A == null) return false;
		switch (A.type)
		{
			case BlockType.Bool:
				return A.GetBool(robot);
		}
		return false;
	}

	public override void RemoveChild(Block blo)
	{
		if (A == blo) A = null;
	}
}