using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesIf : States
{
	public Operator condition = null;
	public List<States> ifProgram = new List<States>();

	public StatesIf(Operator cond = null)
	{
		this.condition = cond;
	}

	public override IEnumerator Execute(Robot robot)
	{
		if (condition != null && condition.Execute(robot))
		{
			foreach (States state in ifProgram)
			{
				yield return state.Execute(robot);
			}
		}
	}

	public void AddInstruction(States state, int index)
	{
		if (index >= 0 && index < ifProgram.Count)
		{
			ifProgram.Insert(index, state);
		}
		else
		{
			ifProgram.Add(state);
		}
	}
}

