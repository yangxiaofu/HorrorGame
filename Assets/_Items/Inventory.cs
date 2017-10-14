using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Items{
	public class Inventory : MonoBehaviour {
		[SerializeField] List<ItemConfig> _items = new List<ItemConfig>();
		[SerializeField] List<KeyConfig> _keys = new List<KeyConfig>();

		public void AddItem(ItemConfig item)
		{
			_items.Add(item);
		}

		public void AddKey(KeyConfig key)
		{
			_keys.Add(key);
		}

		public KeyConfig FindKey(string passCode)
		{
			return _keys.Find(a => a.passCode == passCode);
		}
	}
}


