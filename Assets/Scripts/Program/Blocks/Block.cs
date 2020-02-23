using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
	public Operator parent = null;
	public BlockType type { get; private set; }
	public byte[] data { get; private set; }

	public Block(BlockType type)
	{
		this.type = type;
	}

	public Block(BlockType type, bool value)
	{
		this.type = type;
		data = BitConverter.GetBytes(value);
	}

	public virtual bool GetBool(Robot robot = null)
	{
		return BitConverter.ToBoolean(data, 0);
	}

}
