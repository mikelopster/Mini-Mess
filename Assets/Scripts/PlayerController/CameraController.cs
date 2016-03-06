﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Transform target;

	[System.Serializable]
	public class PositionSettings
	{
		public Vector3 targetPosOffset = new Vector3(0, 1.8f, 0);
		public float lookSmooth = 100f;
		public float distanceFromTarget = -3;
		public float zoomSmooth = 10;
		public float maxZoom = -2;
		public float minZoom = -15;
	}

	[System.Serializable]
	public class OrbitSettings
	{
		public float xRotation = -90;
		public float yRotation = -180;
		public float maxXRotation = 25;
		public float minXRotation = -85;
		public float vOrbitSmooth = 150;
		public float hOrbitSmooth = 150;
	}

	[System.Serializable]
	public class InputSettings
	{
		public string ORBIT_HORIZONTAL_SNAP = "OrbitHorizontalSnap";
		public string ORBIT_HORIZONTAL = "OrbitHorizontal";
		public string ORBIT_VERTICAL = "OrbitVertical";
		public string ZOOM = "Mouse ScrollWheel";
	}

	public PositionSettings position = new PositionSettings ();
	public OrbitSettings orbit = new OrbitSettings ();
	public InputSettings input = new InputSettings ();

	Vector3 destination = Vector3.zero;
	Vector3 targetPos = Vector3.zero;
	UserController charController;
	float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput;

	void Start()
	{
		SetCameraTarget (target);

		targetPos = target.position + position.targetPosOffset;
		destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * -Vector3.forward * position.distanceFromTarget;
		destination += target.position;
		transform.position = destination;
	}

	void SetCameraTarget(Transform t)
	{
		target = t;

		if (target != null) 
		{
			if (target.GetComponent<UserController> ()) 
			{
				charController = target.GetComponent<UserController> ();
			} 
			else
				Debug.LogError ("The camera's target needs a character controller.");
		} 
		else
			Debug.LogError ("Your camera needs a target.");
	}

	void GetInput()
	{
		vOrbitInput = Input.GetAxisRaw (input.ORBIT_VERTICAL);
		hOrbitInput = Input.GetAxisRaw (input.ORBIT_HORIZONTAL);
		hOrbitSnapInput = Input.GetAxisRaw (input.ORBIT_HORIZONTAL_SNAP);
		zoomInput = Input.GetAxisRaw (input.ZOOM);
	}

	void Update()
	{
		GetInput ();
		OrbitTarget ();
		ZoomInOnTarget ();
	}

	void LateUpdate()
	{
		//moving
		MoveToTarget();
		//rotation
		LookAtTarget();
	}

	void MoveToTarget()
	{
		targetPos = target.position + position.targetPosOffset;
		destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * -Vector3.forward * position.distanceFromTarget;
		destination += target.position;
		transform.position = destination;
	}

	void LookAtTarget()
	{
		Quaternion targetRotation = Quaternion.LookRotation (targetPos - transform.position);
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, position.lookSmooth * Time.deltaTime);
	}

	void OrbitTarget()
	{
		if (hOrbitSnapInput > 0) 
		{
			orbit.yRotation = -180;
		}

		orbit.xRotation += -vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
		orbit.yRotation += -hOrbitInput * orbit.hOrbitSmooth * Time.deltaTime;

		if (orbit.xRotation > orbit.maxXRotation)
		{
			orbit.xRotation = orbit.maxXRotation;
		}
		if (orbit.xRotation < orbit.minXRotation) 
		{
			orbit.xRotation = orbit.minXRotation;
		}
	}

	void ZoomInOnTarget()
	{
		position.distanceFromTarget += zoomInput * position.zoomSmooth * Time.deltaTime;

		if (position.distanceFromTarget > position.maxZoom)
		{
			position.distanceFromTarget = position.maxZoom;
		}
		if (position.distanceFromTarget < position.minZoom)
		{
			position.distanceFromTarget = position.minZoom;
		}
	}

}
