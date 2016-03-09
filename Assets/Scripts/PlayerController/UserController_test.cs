using UnityEngine;
using System.Collections;

public class UserController_test : MonoBehaviour
{

	[System.Serializable]
	public class MoveSettings
	{
		public float forwardVel = 7;
		public float sideVel = 7;
		public float jumpVel = 10;
		public float distToGrounded = 0.51f;
		public float distToFace = 2;
		public LayerMask ground;
	}

	[System.Serializable]
	public class PhysSettings
	{
		public float downAccel = 0.75f;
	}

	[System.Serializable]
	public class InputSettings
	{
		public float inputDelay = 0.1f;
		public string FORWARD_AXIS = "Vertical";
		public string SIDE_AXIS = "Horizontal";
		public string JUMP_AXIS = "Jump";
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
	public PhysSettings physSetting = new PhysSettings();
	public InputSettings inputSetting = new InputSettings();
	public MouseSettings mouseSetting = new MouseSettings();

	Vector3 velocity = Vector3.zero;
	Rigidbody rBody;
	float forwardInput, sideInput, jumpInput;
	bool clickInput;
	Quaternion originalRotation;
	RaycastHit hit;
	Vector3 fwd, dwn;
	Vector3 mid = new Vector3 (0, 0.5f, 0);

	bool Grounded()
	{
		dwn = transform.TransformDirection (Vector3.down);
		Debug.DrawRay(transform.position + mid, dwn * moveSetting.distToGrounded, Color.blue);
		return Physics.Raycast (transform.position + mid, dwn, moveSetting.distToGrounded, moveSetting.ground);
	}

	void Start()
	{
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
	}

	void GetInput()
	{
		forwardInput = Input.GetAxis (inputSetting.FORWARD_AXIS);
		sideInput = Input.GetAxis (inputSetting.SIDE_AXIS);
		jumpInput = Input.GetAxisRaw (inputSetting.JUMP_AXIS);
		clickInput = Input.GetMouseButtonDown (mouseSetting.LEFT_CLICK);
	}

	void Update()
	{
		GetInput ();
		Turn ();
		Face ();
	}

	void FixedUpdate()
	{
		if (Grounded ()) {
			Forward ();
			Side ();
		}
		Jump ();

		rBody.velocity = transform.TransformDirection (velocity);
	}

	void Forward()
	{
		if (Mathf.Abs (forwardInput) > inputSetting.inputDelay)
		{
			//move
			velocity.z = moveSetting.forwardVel * forwardInput;
		}
		else
			//zero velocity
			velocity.z = 0;
	}

	void Side()
	{
		if (Mathf.Abs (sideInput) > inputSetting.inputDelay)
		{
			velocity.x = moveSetting.sideVel * sideInput;
		}
		else
			//zero velocity
			velocity.x = 0;
	}

	void Jump()
	{
		if (jumpInput > 0 && Grounded ())
		{
			//jump
			velocity.y = moveSetting.jumpVel;
		} 
		else if (jumpInput == 0 && Grounded ()) 
		{
			//zero out our velocity.y
			velocity.y = 0;
			Debug.Log ("Grounded");
		} 
		else 
		{
			//decrease velocity.y
			velocity.y -= physSetting.downAccel;
			Debug.Log ("Jumping");
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
			//Debug.Log ("Face : " + hit.transform.name);
			if (clickInput) Action (hit);
		}
	}

	void Action(RaycastHit hit) 
	{
		string tag = hit.transform.tag;
		string name = hit.transform.name;
		if (tag == "NPC")
			Debug.Log ("Talk with " + name + ".");
		else if (tag == "Enemy") 
			Debug.Log ("Shoot " + name + "!!!");
	}
}