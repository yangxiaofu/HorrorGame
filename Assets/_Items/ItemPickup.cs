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
				var item = _item.GetItemPrefab().GetComponent<Item>();
				_player.GetComponent<Inventory>().AddItem(item);
				Destroy(this.gameObject);
			} else {
				Debug.Log("This item is too far away from the player.");
			}
        }

		void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(this.transform.position, _player.pickupDistance);
		}
    }

}
