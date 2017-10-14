using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	[CreateAssetMenu(menuName = "Game/Item")]
	public abstract class ItemConfig : ScriptableObject {
		[Header("Item General")]
		[SerializeField] GameObject _itemPrefab;
		public GameObject GetItemPrefab()
		{
			return _itemPrefab;
		}

		public abstract void AddToInventory(Inventory inv);

	}

}
