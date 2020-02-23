using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesIfElse : StatesIf
{
	public List<States> elseProgram = new List<States>();

	public StatesIfElse(Operator cond = null) : base(cond)
	{ }

	public override IEnumerator Execute(Robot robot)
	{
		if (condition != null && condition.Execute(robot))
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

	public void AddInstructionElse(States state, int index)
	{
		state.parent = this;
		if (index >= 0 && index < elseProgram.Count)
		{
			elseProgram.Insert(index, state);
		}
		else
		{
			elseProgram.Add(state);
		}
	}

	public override void RemoveChild(States state)
	{
		ifProgram.Remove(state);
		elseProgram.Remove(state);
	}

	public override void RemoveChild(Operator ope)
	{
		if (condition == ope)
		{
			condition = null;
		}
	}
}

