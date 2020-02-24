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
	public enum Statestype
	{
		None,
		Error
	}

	public static Robot focusedRobot = null;


	public Rigidbody robotRigidbody;
	OriginalData origin;
	Coroutine routine;

	public Dictionary<string, List<States>> programs = new Dictionary<string, List<States>>();
	public Statestype type { get; private set; } = Statestype.None;

	public bool isError
	{
		get
		{
			return type == Statestype.Error;
		}
	}

	public bool forwardIsBlocked = false;
	public LayerMask colliderMask;
	public Rigidbody box = null;

	public LayerMask paintMask;
	public PaintStain paintStain = null;
	public PaintStain paintStainForward = null;

	void Start()
    {
		robotRigidbody = GetComponent<Rigidbody>();

		origin = new OriginalData();
		origin.Save(this);
		
		NewProgram("main");
	}

	private void Update()
	{
		forwardIsBlocked = false;
		Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward + Vector3.up * 0.6f, Vector3.one * 0.25f, Quaternion.identity, colliderMask.value);
		forwardIsBlocked = colliders.Length > 0;
		box = null;
		foreach(Collider coll in colliders)
		{
			box = coll.gameObject.GetComponent<Rigidbody>();
			if (box) break;
		}
		paintStain = null;
		colliders = Physics.OverlapBox(transform.position, Vector3.one * 0.25f, Quaternion.identity, paintMask.value);
		foreach (Collider coll in colliders)
		{
			paintStain = coll.gameObject.GetComponent<PaintStain>();
			if (paintStain != null) break;
		}
		paintStainForward = null;
		colliders = Physics.OverlapBox(transform.position + transform.forward, Vector3.one * 0.25f, Quaternion.identity, paintMask.value);
		foreach (Collider coll in colliders)
		{
			paintStainForward = coll.gameObject.GetComponent<PaintStain>();
			if (paintStainForward != null) break;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position + transform.forward + Vector3.up * 0.6f, Vector3.one * 0.25f);
	}

	IEnumerator Execute()
	{
		type = Statestype.None;
		if (programs.ContainsKey("main"))
		{
			foreach (States state in GetProgram("main"))
			{
				yield return state.Execute(this);
				if (type == Statestype.Error)
				{
					if (focusedRobot == this) UIMenu.Instance.stateText.text = "Error!";
					break;
				}
			}
		}
		else
		{
			type = Statestype.Error;
			if (focusedRobot == this) UIMenu.Instance.stateText.text = "Main Program Not exist!";
		}
		if (type != Statestype.Error)
		{
			if (focusedRobot == this) UIMenu.Instance.stateText.text = "End Program!";
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

	private void OnCollisionEnter(Collision collision)
	{
		type = Statestype.Error;
		Debug.Log("Collision, Fin de du programme");
	}

	public List<string> GetProgramNameList()
	{
		return new List<string>(programs.Keys);
	}

	public List<States> GetProgram(string key)
	{
		key = key.ToLower();
		if (programs.ContainsKey(key))
			return programs[key];
		return new List<States>();
	}

	public void NewProgram(string programName)
	{
		programs.Add(programName.ToLower(), new List<States>());
	}

	public void AddInstruction(string programName, States states, int index = -1)
	{
		programName = programName.ToLower();
		if (programs.ContainsKey(programName))
		{
			if (index >= 0 && index < programs[programName].Count)
			{
				programs[programName].Insert(index, states);
			}
			else
			{
				programs[programName].Add(states);
			}
		}
	}

	public void DeleteProgram(string programName)
	{
		programName = programName.ToLower();
		programs.Remove(programName);
		if (programName.Equals("main"))
			NewProgram(programName);
	}

	public void DeleteInstruction(string programName, States states)
	{
		programName = programName.ToLower();
		if (programs.ContainsKey(programName))
		{
			programs[programName].Remove(states);
		}
	}
	
}
