using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace rpg
{

	public class modifier
	{
		List<string> modNames;
		List<int> modValues;
	}

	public class item
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<modifier> Mods { get; set; }
		public bool Stackable { get; set; } = false;
		public int MaxStackSize { get; set; } = 1;
	}

	public class ItemDatabase
	{
		

		private ItemDatabase(string path)
		{
			try
			{
				Debug.Print("Loading item database...");
				string json = File.ReadAllText(path, Encoding.UTF8);
				Debug.Print(json);
				var database = JsonConvert.DeserializeObject<Dictionary<int, item>>(json);
				items = database;
				Debug.Print($"Item database loaded. size: {items.Count}");
			}
			catch (FileNotFoundException ex)
			{
				Debug.Print($"Error: File not found - {ex.Message}");
			}
			catch (JsonException ex)
			{
				Debug.Print($"Error: Invalid JSON format - {ex.Message}");
			}
			catch (Exception ex)
			{
				Debug.Print($"Unexpected error: {ex.Message}");
			}
		}

		private static readonly ItemDatabase _instance = new ItemDatabase("items.json");

		public static ItemDatabase Instance => _instance;

		public item GetItem(int id)
		{
			return items[id];
		}

		public void ping()
		{
			Debug.Print("Database created");
		}
		private readonly Dictionary<int, item> items;
	}
}
