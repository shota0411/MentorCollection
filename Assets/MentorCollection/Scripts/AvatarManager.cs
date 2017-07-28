using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager 
	: SingletonMonoBehaviour<AvatarManager> 
{
	[SerializeField] private GameObject avatarPrefab;
	private List<AvatorController> avatars = new List<AvatorController>();

	[SerializeField] private Transform spawnPoint, startPoint;

	private GameObject[] points;

	public Transform SpawnPoint { get { return spawnPoint; } }
	public Transform StartPoint { get { return startPoint; } }

	public Transform GetTarget()
	{
		int target = Random.Range(0, points.Length-1);
		return points[target].transform;
	}

	private void Start()
	{
		points = GameObject.FindGameObjectsWithTag("WalkPoints");
	}

	public AvatorController GetAvatar (int uniqueId)
	{
		return avatars.Find(a => a.Character.UniqueID == uniqueId);
	}

	public void Setup () { StartCoroutine( SetupCoroutine() ); }
	private IEnumerator SetupCoroutine () 
	{
		var characters = GameManager.instance.User.Characters;
		foreach (var c in characters)
		{
			SpawnAvatar(c);
			yield return new WaitForSeconds(5.0f);
		}
	}

	public void SpawnAvatar(Character data)
	{
		var obj = Instantiate(
			avatarPrefab, 
			spawnPoint.position, 
			spawnPoint.rotation) as GameObject;
		var avatar = obj.GetComponent<AvatorController>();
		avatar.SetValue(data);
		avatars.Add(avatar);
	}

}
