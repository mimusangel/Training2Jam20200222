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


	public Rigidbody robotRigidbody;
	OriginalData origin;
	Coroutine routine;

	public List<States> program = new List<States>();
	public Statestype type { get; private set; } = Statestype.None;

	public bool isError
	{
		get
		{
			return type == Statestype.Error;
		}
	}

	public bool forwardIsBlocked = false;

	public static Robot focusedRobot = null;

	void Start()
    {
		robotRigidbody = GetComponent<Rigidbody>();

		origin = new OriginalData();
		origin.Save(this);

		program.Add(new StatesForward());
		BlockDetectCollider detect = new BlockDetectCollider();
		Block blockTrue = new Block(BlockType.Bool, true);
		OpEqual equal = new OpEqual(detect, blockTrue);
		StatesIf sIf = new StatesIf(equal);
		sIf.ifProgram.Add(new StatesRotate());
		program.Add(sIf);
		program.Add(new StatesForward());


	}

	private void Update()
	{
		forwardIsBlocked = false;
		Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward + Vector3.up * 0.6f, Vector3.one * 0.25f);
		forwardIsBlocked = colliders.Length > 0;
		
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position + transform.forward + Vector3.up * 0.6f, Vector3.one * 0.25f);
	}

	IEnumerator Execute()
	{
		type = Statestype.None;
		foreach (States state in program)
		{
			if (focusedRobot == this) UIMenu.Instance.stateText.text = state.ToString();
			yield return state.Execute(this);
			if (type == Statestype.Error)
			{
				if (focusedRobot == this) UIMenu.Instance.stateText.text = "Error!";
				break;
			}
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
}
