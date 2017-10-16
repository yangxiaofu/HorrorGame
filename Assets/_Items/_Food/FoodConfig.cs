using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{

	[CreateAssetMenu(menuName = "Game/Items/Food")]
	public class FoodConfig : ItemConfig{
		[SerializeField] float _energyBoost = 50f;
        public float energyBoost{get{return _energyBoost;}}
        public override void AddToInventory(Inventory inv)
        {
            inv.AddItem(this);
        }
    }

}
