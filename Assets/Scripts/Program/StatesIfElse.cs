using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesIfElse : States
{
	public Operator condition;
	public List<States> ifProgram = new List<States>();
	public List<States> elseProgram = new List<States>();

	public StatesIfElse(Operator cond)
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
		else
		{
			foreach (States state in elseProgram)
			{
				yield return state.Execute(robot);
			}
		}
	}
}

