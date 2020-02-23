using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class States
{
	public States parent = null;

	public States() { }
	public abstract IEnumerator Execute(Robot robot);

	public virtual void RemoveChild(States state) { }
	public virtual void RemoveChild(Operator ope) { }
}
