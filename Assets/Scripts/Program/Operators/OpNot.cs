using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpNot : Operator
{
	public Operator A;

	public OpNot(Operator a = null)
	{
		A = a;
	}

	public override bool Execute(Robot robot)
	{
		if (A == null) return false;
		return !(A.Execute(robot));
	}

	public override void RemoveChild(Operator ope)
	{
		if (A == ope) A = null;
	}
}