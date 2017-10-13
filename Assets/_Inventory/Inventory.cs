using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Items;

namespace Game.Core{
	public class Inventory : MonoBehaviour {

		[SerializeField] List<Item> _items = new List<Item>();

		public void AddItem(Item item)
		{
			_items.Add(item);
		}
	}
}


