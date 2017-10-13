using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Items{
	[ExecuteInEditMode]
	public class ItemPickup : MonoBehaviour {
		[SerializeField] ItemConfig _item;

        void Update()
		{
			DestroyChildObjects();

			var itemObject = Instantiate(
				_item.GetItemPrefab(), 
				this.transform.position, 
				Quaternion.identity
			) as GameObject;

			itemObject.transform.SetParent(
				this.transform
			);		
		}
		private void DestroyChildObjects()
        {
            foreach (Transform child in this.transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
	}

}
