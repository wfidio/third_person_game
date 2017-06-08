using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class RelativeMovement : MonoBehaviour {

	[SerializeField] private Transform target;


	private float rotSpeed = 15.0f;
	public float moveSpeed = 6.0f;
	public float jumpSpeed = 15.0f;
	public float gravity = -9.8f;
	public float minFall = -1.5f;
	public float terminalVelocity = -10.0f;
	public float pushForce = 3.0f;


	private float vertSpeed;
	private ControllerColliderHit _contact;

	private CharacterController controller;
	private Animator _animator;

	// Use this for initialization
	void Start () {
		vertSpeed = minFall;
		controller = GetComponent<CharacterController> ();
		_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = Vector3.zero;
		bool hitGround = false;
		RaycastHit hit;

		float horInput = Input.GetAxis ("Horizontal");
		float verInput = Input.GetAxis ("Vertical");
		if (horInput != 0 || verInput != 0) {
			movement.x = horInput * moveSpeed;
			movement.z = verInput * moveSpeed;
			movement = Vector3.ClampMagnitude (movement, moveSpeed);


			Quaternion tmp = target.rotation;
			target.eulerAngles = new Vector3 (0, target.eulerAngles.y, 0);
			movement = target.TransformDirection (movement);
//			transform.rotation = Quaternion.LookRotation (movement);
			Quaternion direction = Quaternion.LookRotation(movement);
			transform.rotation = Quaternion.Lerp (transform.rotation, direction, rotSpeed*Time.deltaTime);

		}

		if (vertSpeed < 0 && Physics.Raycast (transform.position, Vector3.down, out hit)) {
			float check = (controller.height + controller.radius) / 1.9f;
			hitGround = hit.distance < check;
		}

		_animator.SetFloat ("Speed", movement.sqrMagnitude);

//		if (controller.isGrounded) {
		if(hitGround){	
			if (Input.GetButtonDown ("Jump")) {
				vertSpeed = jumpSpeed;	
			} else {
				vertSpeed = minFall;
			}
			_animator.SetBool ("Jumping", false);
		} else {
			vertSpeed += gravity * 5 * Time.deltaTime;
			if (vertSpeed <= terminalVelocity) {
				vertSpeed = terminalVelocity;
			}

			if (_contact != null) {
				_animator.SetBool ("Jumping", true);
			}
			if (controller.isGrounded) {
				if (Vector3.Dot (movement, _contact.normal) < 0) {
					movement = _contact.normal * moveSpeed;
//					Debug.Log (_contact.normal);
				} else {
					movement += _contact.normal * moveSpeed;
				}
			}
		}

		movement.y = vertSpeed;
		movement *= Time.deltaTime;
		controller.Move(movement);
	}

	void OnControllerColliderHit(ControllerColliderHit hit){
		_contact = hit;
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body != null && !body.isKinematic) {
			body.velocity = hit.moveDirection * pushForce;
		}
	}
}
