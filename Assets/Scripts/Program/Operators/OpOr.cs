using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpOr: Operator
{
	public Operator A;
	public Operator B;

	public OpOr(Operator a, Operator b)
	{
		A = a;
		B = b;
	}

	public override bool Execute(Robot robot)
	{
		return A.Execute(robot) || B.Execute(robot);
	}
}