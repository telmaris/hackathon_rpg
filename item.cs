using Godot;
using System;
using System.Collections.Generic;

namespace rpg
{
	using itemId = System.Int32;

	public class modifier
	{
		List<string> modNames;
		List<int> modValues;
	}

	public class item
	{
		public item(itemId id)
		{
			this.id = id;
		}

		public itemId id;
		public List<modifier> mods;
		public bool stackable = false;
		public int maxStackSize = 1;
	}
}

