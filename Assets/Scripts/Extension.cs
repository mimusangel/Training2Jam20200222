using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Extension
{
	public static string Capitalize(this string str)
	{
		char[] strChar = str.ToCharArray();
		for (int i = 0; i < strChar.Length; i++)
		{
			if (i - 1 < 0 || strChar[i - 1] == ' ')
			{
				strChar[i] = char.ToUpper(strChar[i]);
			}
		}
		return new string(strChar);
	}
}
