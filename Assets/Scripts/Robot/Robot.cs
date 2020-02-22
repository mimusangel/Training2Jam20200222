using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
	public class OriginalData
	{
		public Vector3 position;
		public Quaternion rotation;

		public void Save(Robot robot)
		{
			position = robot.transform.position;
			rotation = robot.transform.rotation;
		}

		public void Load(Robot robot)
		{
			robot.transform.position = position;
			robot.transform.rotation = rotation;
		}
	}

	OriginalData origin;
	Coroutine routine;

	public List<States> program = new List<States>();

	void Start()
    {
		origin = new OriginalData();
		origin.Save(this);

		program.Add(new StatesForward());
		program.Add(new StatesRotate());
		program.Add(new StatesForward());
		program.Add(new StatesRotate(-90.0f));
		program.Add(new StatesForward());
		program.Add(new StatesRotate());
		program.Add(new StatesBackward());

	}

	IEnumerator Execute()
	{
		foreach (States state in program)
		{
			yield return state.Execute(this);
		}
	}

	public void ResetObject()
	{
		origin.Load(this);
	}

	public void Play()
	{
		routine = StartCoroutine(Execute());
	}

	public void Stop()
	{
		if (routine != null)
		{
			StopCoroutine(routine);
			routine = null;
		}
	}
}
