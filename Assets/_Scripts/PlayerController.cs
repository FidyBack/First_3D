using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	CharacterController characterController;
	GameObject playerCamera;

	float _baseSpeed = 10.0f;
	float _gravidade = 9.8f;
	float cameraRotation;


	void Start() {
		characterController = GetComponent<CharacterController>();
		playerCamera = GameObject.Find("Main Camera");
		cameraRotation = 0.0f;
	}

	void Update() {
		
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		float mouse_dX = Input.GetAxis("Mouse X");
		float mouse_dY = Input.GetAxis("Mouse Y");

		float y = 0;
		if(!characterController.isGrounded){
			y = -_gravidade;
		}

		Vector3 direction = transform.right * x + transform.up * y + transform.forward * z;
		cameraRotation -= mouse_dY;
		Mathf.Clamp(cameraRotation, -75.0f, 75.0f);

		characterController.Move(direction * _baseSpeed * Time.deltaTime);
		transform.Rotate(Vector3.up, 2*mouse_dX);
		playerCamera.transform.localRotation = Quaternion.Euler(2*cameraRotation, 0.0f, 0.0f);
   }


	void LateUpdate() {
		RaycastHit hit;
		Debug.DrawRay(playerCamera.transform.position, transform.forward*10.0f, Color.magenta);
	
		if(Physics.Raycast(playerCamera.transform.position, transform.forward, out hit, 100.0f)) {
			Debug.Log(hit.collider.name);
		}
	}
}