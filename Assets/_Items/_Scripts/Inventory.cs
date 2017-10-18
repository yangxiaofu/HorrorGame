using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Items{
	[RequireComponent(typeof(WeaponSystem))]	
	public class Inventory : MonoBehaviour {
		[SerializeField] List<WeaponConfig> _weapons = new List<WeaponConfig>();
		[SerializeField] List<ItemConfig> _items = new List<ItemConfig>();
		[SerializeField] List<KeyConfig> _keys = new List<KeyConfig>();

		WeaponSystem _weaponSystem;
	
		void Start()
		{
			_weaponSystem = GetComponent<WeaponSystem>();
			_weaponSystem.SetEquippedWeapon(_weapons[0]);
		}
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


