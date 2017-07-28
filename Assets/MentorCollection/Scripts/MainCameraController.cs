using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainCameraController 
	: SingletonMonoBehaviour<MainCameraController> 
{

	[SerializeField] private Transform mainCamera;

	private float 
		easing = 6f,
		maxSpeed = 2f,
		stopDistance = 0.2f;

	private UnityAction onZoomInFinish;

	private AvatorController targetAvatar;
	private Transform target;
	public void ToZoomIn (AvatorController nextTarget, UnityAction OnZoomInFinish) {
		targetAvatar = nextTarget;
		target = targetAvatar.MainCameraPoint;
		onZoomInFinish = OnZoomInFinish;
	}

	public void ToZoomOut () {
		onZoomInFinish = null;
		target = this.transform;
	}

	private void Start ()
	{
		target = this.transform;
	}

	private void Update ()
	{
		// position
		Vector3 v = Vector3.Lerp(
			            mainCamera.position, 
			            target.position, 
			            Time.deltaTime * easing) - mainCamera.position;
		if (v.magnitude > maxSpeed) v = v.normalized * maxSpeed;
		mainCamera.position += v;

		// rotation
		mainCamera.rotation = Quaternion.Lerp(
			mainCamera.rotation,
			target.rotation,
			Time.deltaTime * easing);

		float distance = Vector3.Distance(mainCamera.position, target.position);
		if (stopDistance > distance) 
		{
			if (onZoomInFinish != null) 
			{
				onZoomInFinish();
				onZoomInFinish = null;
			}
		}
	}

}