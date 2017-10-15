using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	[CreateAssetMenu(menuName = "Game/Items/Key")]
	public class KeyConfig : ItemConfig {
		[Header("Key Specific")]
		[SerializeField] string _passCode;
		public string passCode{get{return _passCode;}}
        public override void AddToInventory(Inventory inv)
        {
            inv.AddKey(this);
        }
    }
}