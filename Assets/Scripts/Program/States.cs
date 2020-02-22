using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class States
{
	public States() { }
	public abstract IEnumerator Execute(Robot robot);
}
