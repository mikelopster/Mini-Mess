﻿using UnityEngine;
using System.Collections;

public class UserController_test : MonoBehaviour
{

	[System.Serializable]
	public class MoveSettings
	{
		public float walkVel = 7;
		public float jumpVel = 0.7f;
		public float distToGrounded = 0.51f;
		public float distForWalk = 0.7f;
		public float distToFace = 2;
		public LayerMask ground;
	}

	[System.Serializable]
	public class InputSettings
	{
		public float inputDelay = 0.1f;
		public string FORWARD_AXIS = "Vertical";
		public string SIDE_AXIS = "Horizontal";
		public string JUMP_AXIS = "Jump";
		public string SELFIE = "Selfie";
	}

	[System.Serializable]
	public class MouseSettings
	{
		public float minimumX = -360;
		public float maximumX = 360;
		public float rotationX = 0;
		public float sensitivityX = 5;
		public string MOUSE_X = "Mouse X";
		public int LEFT_CLICK = 0;
	}
		

	public MoveSettings moveSetting = new MoveSettings();
	public InputSettings inputSetting = new InputSettings();
	public MouseSettings mouseSetting = new MouseSettings();

	Vector3 velocity = Vector3.zero;
	Rigidbody rBody;
	float forwardInput, sideInput, jumpInput, selfieInput;
	bool clickInput;
	Quaternion originalRotation;
	RaycastHit hit;
	Vector3 fwd, dwn_ground, dwn_walk;
	Vector3 mid = new Vector3 (0, 0.5f, 0);
	bool openTurn;
	Animator anim;
	Vector3 velo;

	bool Grounded()
	{
		dwn_ground = transform.TransformDirection (Vector3.down);
		Debug.DrawRay(transform.position + mid, dwn_ground * moveSetting.distToGrounded, Color.blue);
		return Physics.Raycast (transform.position + mid, dwn_ground, moveSetting.distToGrounded, moveSetting.ground);
	}

	bool CanWalk()
	{
		dwn_walk = transform.TransformDirection (Vector3.down);
		Debug.DrawRay(transform.position + mid, dwn_walk * moveSetting.distForWalk, Color.red);
		return Physics.Raycast (transform.position + mid, dwn_walk, moveSetting.distForWalk, moveSetting.ground);
	}

	void Start()
	{
		// Set Turn as default
		openTurn = true;

		// Set mouse in center
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = true;


		if (GetComponent<Rigidbody> ()) 
		{
			rBody = GetComponent<Rigidbody> ();
			rBody.freezeRotation = true;
			originalRotation = transform.localRotation;
		}	
		else
			Debug.LogError ("The Character needs a rigidbody.");
		
		forwardInput = sideInput = jumpInput = 0;
		fwd = transform.TransformDirection(Vector3.forward);
		anim = GetComponent<Animator> ();
	}

	void GetInput()
	{
		forwardInput = Input.GetAxis (inputSetting.FORWARD_AXIS);
		sideInput = Input.GetAxis (inputSetting.SIDE_AXIS);
		jumpInput = Input.GetAxisRaw (inputSetting.JUMP_AXIS);
		clickInput = Input.GetMouseButtonDown (mouseSetting.LEFT_CLICK);
		if (Input.GetAxis (inputSetting.SELFIE) > 0)
			anim.SetBool ("selfie", true);
		else
			anim.SetBool ("selfie", false);
	}

	void Update()
	{

		GetInput ();
		if(openTurn){
		  Turn ();
		  Face ();
		}

	}

	void FixedUpdate()
	{
		if (CanWalk()) {
			Forward ();
		}
		Jump ();

		velo = rBody.velocity;
		velo.x = velocity.x;
		velo.z = velocity.z;
		rBody.velocity = transform.TransformDirection (velo);
	}

	void Forward()
	{
		if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Selfie"))
		{
			if (Mathf.Abs (forwardInput) > inputSetting.inputDelay || Mathf.Abs (sideInput) > inputSetting.inputDelay) 
			{
				anim.SetBool ("walking", true);
				velocity.z = moveSetting.walkVel * forwardInput;
				velocity.x = moveSetting.walkVel * sideInput;
			}
			else 
			{
				anim.SetBool ("walking", false);
				velocity.z = 0;
				velocity.x = 0;
			}
		}
		else 
		{
			anim.SetBool ("walking", false);
			velocity.z = 0;
			velocity.x = 0;
		}
	}

	void Jump()
	{
		if (jumpInput > 0 && CanWalk ()) 
		{
			rBody.AddForce (0, moveSetting.jumpVel, 0, ForceMode.Impulse);
		}
	}

	void Turn()
	{
		mouseSetting.rotationX += Input.GetAxis(mouseSetting.MOUSE_X) * mouseSetting.sensitivityX;

		if (mouseSetting.rotationX < -360F)
			mouseSetting.rotationX += 360F;
		if (mouseSetting.rotationX > 360F)
			mouseSetting.rotationX -= 360F;
		
		mouseSetting.rotationX = Mathf.Clamp (mouseSetting.rotationX, mouseSetting.minimumX, mouseSetting.maximumX);
		Quaternion xQuaternion = Quaternion.AngleAxis (mouseSetting.rotationX, Vector3.up);
		transform.localRotation = originalRotation * xQuaternion;
	}

	void Face()
	{
		fwd = transform.TransformDirection(Vector3.forward);
		Debug.DrawRay(transform.position + mid, fwd * moveSetting.distToFace, Color.green);
		if (Physics.Raycast (transform.position + mid, fwd, out hit , moveSetting.distToFace)) 
		{
			if (clickInput) Action (hit);
		}
	}

	void Action(RaycastHit hit) 
	{
		string tag = hit.transform.tag;
		string name = hit.transform.name;

		if (tag == "NPC")
		{
			NPCEvent evt = hit.transform.gameObject.GetComponent<NPCEvent> ();
			QuestManager.instance.CheckQuest (evt.npcIndex, evt.npcMain);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			openTurn = false;
		}
	}

	public void OpenMouseTurn() {
		openTurn = true;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void OnCollisionEnter (Collision col)
	{
		anim.SetBool ("walking", false);
		velocity.x = 0;
		velocity.z = 0;
	}
}