

public static class UIDragToTrash
{
	public enum UIDragToTrashType
	{
		None,
		State,
		Operator,
		Block
	}

	private static UIDragToTrashType Type = UIDragToTrashType.None;
	private static States State = null;
	private static Operator Operat = null;
	private static Block Block = null;

	public static void StartDrag(States state)
	{
		Type = UIDragToTrashType.State;
		State = state;
	}

	public static void StartDrag(Operator ope)
	{
		Type = UIDragToTrashType.Operator;
		Operat = ope;
	}

	public static void StartDrag(Block block)
	{
		Type = UIDragToTrashType.Block;
		Block = block;
	}

	public static void EndDrag()
	{
		Type = UIDragToTrashType.None;
		State = null;
		Operat = null;
		Block = null;
	}

	public static void ToTrash()
	{
		switch(Type)
		{
			case UIDragToTrashType.State:
				if (State != null)
				{
					if (State.parent != null)
						State.parent.RemoveChild(State);
					else
						UIRobotProg.Instance.DeleteState(State);
				}
				break;
			case UIDragToTrashType.Operator:
				if (Operat != null)
				{
					if (Operat.parentOP != null) Operat.parentOP.RemoveChild(Operat);
					if (Operat.parentST != null) Operat.parentST.RemoveChild(Operat);
				}
				break;
			case UIDragToTrashType.Block:
				if (Block != null)
				{
					if (Block.parent != null) Block.parent.RemoveChild(Block); 
				}
				break;
		}
		UIRobotProg.Instance.ChangeProgram();
	}
}