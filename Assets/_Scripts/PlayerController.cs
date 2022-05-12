using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	CharacterController characterController;
	GameObject playerCamera;

	float _mouseSensitivity = 125.0f;
	float cameraRotation = 0.0f;

	float _baseSpeed = 12.0f;
	float _baseJumpHeight = 103.0f;
	float _gravity = 9.8f;


	void Start() {
		characterController = GetComponent<CharacterController>();
		playerCamera = GameObject.Find("Main Camera");
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update() {
		
		if (!PauseMenu.GameIsPaused) {
			Cursor.lockState = CursorLockMode.Locked;
			// Rotation
			float mouse_dX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
			float mouse_dY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

			cameraRotation -= mouse_dY;
			cameraRotation = Mathf.Clamp(cameraRotation, -90, 90);
			playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);
			transform.Rotate(Vector3.up * mouse_dX);

			// Movement
			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");

			float y = 0;
			if(!characterController.isGrounded){
				y -= _gravity;
			}

			Vector3 direction = transform.right * x + transform.up * y + transform.forward * z;
			characterController.Move(direction * _baseSpeed * Time.deltaTime);

			// Jump
			if (Input.GetButtonDown("Jump") && characterController.isGrounded) {
				y = _baseJumpHeight;
			}
		} 
		else {
			Cursor.lockState = CursorLockMode.None;
		}
	



   }


	void LateUpdate() {
		RaycastHit hit;
		Debug.DrawRay(playerCamera.transform.position, transform.forward*10.0f, Color.magenta);
	
		if(Physics.Raycast(playerCamera.transform.position, transform.forward, out hit, 100.0f)) {
			// Debug.Log(hit.collider.name);
		}
	}
}