using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Items{
	public class Inventory : MonoBehaviour {
		[SerializeField] List<Item> _items = new List<Item>();
		[SerializeField] List<Key> _keys = new List<Key>();

		public void AddItem(Item item)
		{
			_items.Add(item);
		}

		public Key FindKey(string passCode)
		{
			return _keys.Find(a => a.passcode == passCode);
		}
	}
}


