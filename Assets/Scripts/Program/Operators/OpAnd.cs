using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpAnd : Operator
{
	public Operator A;
	public Operator B;

	public OpAnd(Operator a = null, Operator b = null)
	{
		A = a;
		B = b;
	}

	public override bool Execute(Robot robot)
	{
		if (A == null || B == null) return false;
		return A.Execute(robot) && B.Execute(robot);
	}
}