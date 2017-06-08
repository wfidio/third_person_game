using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour {

	[SerializeField] private string itemName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
//		Debug.Log ("Item collected: " + itemName);
		Managers.Inventory.AddItem(this.itemName);
		Destroy (this.gameObject);
	}
}
