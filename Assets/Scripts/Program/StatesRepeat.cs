using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesRepeat : States
{
	public Operator condition = null;
	public List<States> ifProgram = new List<States>();

	public StatesRepeat(Operator cond = null)
	{
		this.condition = cond;
	}

	public override IEnumerator Execute(Robot robot)
	{
		if (condition != null)
		{
			while (condition.Execute(robot) && !robot.isError) 
			{
				foreach (States state in ifProgram)
				{
					yield return state.Execute(robot);
				}
				yield return new WaitForEndOfFrame();
			}
		}
	}

	public void AddInstruction(States state, int index)
	{
		state.parent = this;
		if (index >= 0 && index < ifProgram.Count)
		{
			ifProgram.Insert(index, state);
		}
		else
		{
			ifProgram.Add(state);
		}
	}
	public override void RemoveChild(States state)
	{
		ifProgram.Remove(state);
	}

	public override void RemoveChild(Operator ope)
	{
		if (condition == ope)
		{
			condition = null;
		}
	}
}

