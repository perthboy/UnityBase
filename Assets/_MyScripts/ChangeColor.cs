using UnityEngine;
using System.Collections;


namespace ChangeTheColor
{
public  class ChangeColor 
	{

		Color col;
		public Color color
		{
			get
			{
				return this.col;
			}
			set
			{
				this.col = value;
			}
		}
	}
}

