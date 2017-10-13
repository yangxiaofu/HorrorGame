using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	[CreateAssetMenu(menuName = "Game/Item")]
	public class ItemConfig : ScriptableObject {
		[SerializeField] GameObject _itemPrefab;
		public GameObject GetItemPrefab()
		{
			return _itemPrefab;
		}
	}

}
