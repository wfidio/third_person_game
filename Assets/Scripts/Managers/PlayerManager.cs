using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour,IGameManager {

	public ManagerStatus status{ get; private set;}
	public int health{ get; private set; }
	public int maxHealth{ get; private set; }


	public void startUp(NetworkService service){
		Debug.Log ("Player Manager Starting....");
		health = 50;
		maxHealth = 100;
		status = ManagerStatus.started;
	}

	public void ChangeHealth(int value){
		this.health += value;
		if (health > maxHealth) {
			health = maxHealth;
		} else if (health < 0) {
			health = 0;
		}

		Debug.Log ("health: " + health + "/" + maxHealth);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
