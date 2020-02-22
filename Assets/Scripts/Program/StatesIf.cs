using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesIf : States
{
	public Operator condition;
	public List<States> ifProgram = new List<States>();

	public StatesIf(Operator cond)
	{
		this.condition = cond;
	}

	public override IEnumerator Execute(Robot robot)
	{
		if (condition.Execute(robot))
		{
			foreach (States state in ifProgram)
			{
				yield return state.Execute(robot);
			}
		}
	}
}

