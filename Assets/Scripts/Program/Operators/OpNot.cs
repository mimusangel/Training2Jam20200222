using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpNot : Operator
{
	public Operator A;

	public OpNot(Operator a)
	{
		A = a;
	}

	public override bool Execute(Robot robot)
	{
		return !(A.Execute(robot));
	}
}