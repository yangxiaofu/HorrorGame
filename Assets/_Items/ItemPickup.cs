using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;
using Game.Characters;

namespace Game.Items
{
    [ExecuteInEditMode]
	public class ItemPickup : MonoBehaviour, IPointerClickHandler {
		[SerializeField] ItemConfig _item;
		Player _player;

        void Awake()
        {
            if (!GetComponent<BoxCollider>())
            {
                var bc = this.gameObject.AddComponent<BoxCollider>();
                bc.isTrigger = true;
            }
        }

		void Start()
		{
			_player = GameObject.FindObjectOfType<Player>();
			Assert.IsNotNull(_player);
		}
        void Update()
        {
            DestroyChildObjects();
            InstiantiateItem();
        }

        private void InstiantiateItem()
        {
            if (_item == null) return;
            
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

        public void OnPointerClick(PointerEventData eventData)
        {
            float distanceFromPlayer = Vector3.Distance(_player.transform.position, this.transform.position);
			if (distanceFromPlayer < _player.pickupDistance){
                _item.AddToInventory(_player.GetComponent<Inventory>());
				Destroy(this.gameObject);
			} else {
				Debug.Log("This item is too far away from the player.");
                //TODO: UI that tellsthe player that you are too far away from the player. 
			}
        }

		void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(this.transform.position, _player.pickupDistance);
		}
    }

}
