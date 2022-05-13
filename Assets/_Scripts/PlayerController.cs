using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	CharacterController characterController;
	GameManager gm;
	GameObject playerCamera;
	public TextMeshProUGUI notEnough;
	// get panel from UI
	public GameObject win, lose;


	float _mouseSensitivity = 100.0f;
	float cameraRotation = 0.0f;

	float _baseSpeed = 13.0f;
	float _baseJumpHeight = 3.0f;
	float _gravity = 9.8f;

	Vector3 _moveDirection = Vector3.zero;

	void Start() {
		gm = GameManager.GetInstance();
		characterController = GetComponent<CharacterController>();
		playerCamera = GameObject.Find("Main Camera");
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update() {
		if (gm._runningTime == 0) {
			lose.SetActive(true);
			StartCoroutine(WaitEnd());
		}
		
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

			if (Input.GetButtonDown("Jump") && characterController.isGrounded) {
				_moveDirection.y = _baseJumpHeight;
			}

			if(!characterController.isGrounded){
				_moveDirection.y -= _gravity * Time.deltaTime;
			}
			
			Vector3 direction = transform.right * x + transform.up * _moveDirection.y + transform.forward * z;
			characterController.Move(direction * _baseSpeed * Time.deltaTime);
		} 
		else {
			Cursor.lockState = CursorLockMode.None;
		}
   }

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("estrela")) {
			Destroy(other.gameObject);
			gm._objective += 1;
		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.name == "Barrier" && gm._objective != 4) {
			notEnough.gameObject.SetActive(true);
			StartCoroutine(Wait());
		} else if (hit.gameObject.name == "Barrier" && gm._objective == 4) {
			win.SetActive(true);
			StartCoroutine(WaitEnd());
		}
	}

	IEnumerator WaitEnd() {
		yield return new WaitForSeconds(5);
		win.SetActive(false);
		lose.SetActive(false);
		gm.Reset();
		SceneManager.LoadScene(0);
	}

	IEnumerator WaitLose() {
		yield return new WaitForSeconds(5);
		lose.gameObject.SetActive(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	IEnumerator Wait() {
		yield return new WaitForSeconds(2);
		notEnough.gameObject.SetActive(false);
	}


	void LateUpdate() {
		RaycastHit hit;
		Debug.DrawRay(playerCamera.transform.position, transform.forward*10.0f, Color.magenta);
	
		if(Physics.Raycast(playerCamera.transform.position, transform.forward, out hit, 100.0f)) {
			// Debug.Log(hit.collider.name);
		}
	}
}