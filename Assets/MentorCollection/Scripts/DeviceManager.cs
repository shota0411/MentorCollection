using UnityEngine;
using System.Collections;

public class DeviceManager 
	: SingletonMonoBehaviour<DeviceManager>
{
	[SerializeField] 
	private GameObject mainCamera, diveCamera, gvrViewer;

	private AvatorController avatarController;

	public void Setup () 
	{ 
		Screen.autorotateToPortrait = false; 
		Screen.autorotateToLandscapeLeft = false; 
		Screen.autorotateToLandscapeRight = false; 
		Screen.autorotateToPortraitUpsideDown = false;
		ToPortrate(); 
	}

	public void ToVR(AvatorController avatar = null) {
		StartCoroutine(ToVRCoroutine(avatar));
	}
	private IEnumerator ToVRCoroutine (AvatorController avatar)
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		avatarController = avatar;
		mainCamera.SetActive(false);
		mainCamera.GetComponent<Camera>().enabled = false;
		diveCamera.SetActive(true);
		if (avatar != null) diveCamera.transform.SetParentWithReset(avatar.VRView());
		yield return null;  // 1フレーム遅らせる
		gvrViewer.SetActive(true);
	}

	public void ToPortrate()
	{
		if (avatarController != null) avatarController.InactiveVR();
		mainCamera.SetActive(true);
		mainCamera.GetComponent<Camera>().enabled = true;
		diveCamera.SetActive(false);
		gvrViewer.SetActive(false);
		Screen.orientation = ScreenOrientation.Portrait;
	}

	private void EnableGvrView ()
	{
		gvrViewer.SetActive(true);
	}

	private void UnenableGvrView ()
	{
		gvrViewer.SetActive(false);
	}
}