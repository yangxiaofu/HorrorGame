using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Items{
	[RequireComponent(typeof(WeaponSystem))]	
	public class Inventory : MonoBehaviour {
		[SerializeField] List<WeaponConfig> _weapons = new List<WeaponConfig>();
		[SerializeField] List<ItemConfig> _items = new List<ItemConfig>();
		[SerializeField] List<KeyConfig> _keys = new List<KeyConfig>();
		[SerializeField] List<FoodConfig> _foods = new List<FoodConfig>();
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

		public void AddFood(FoodConfig food){
			_foods.Add(food);
		}

		public void AddKey(KeyConfig key)
		{
			_keys.Add(key);
		}

		public KeyConfig FindKey(string passCode)
		{
			return _keys.Find(a => a.passCode == passCode);
		}

		public FoodConfig GetFood()
		{
			var food = _foods.FirstOrDefault();
			
			if (_foods.Count > 0)
			{
				_foods.RemoveAt(0);
			}
			
			return food;
		}
	}
}


