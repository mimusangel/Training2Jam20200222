using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Operator
{
	public Operator parentOP = null;
	public States parentST = null;

	public abstract bool Execute(Robot robot);

	public virtual void RemoveChild(Operator ope) { }
	public virtual void RemoveChild(Block blo) { }
}