using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.Networking;

public class ConnectionManager 
	: SingletonMonoBehaviour<ConnectionManager> {

	public void ConnectionAPI (
		string url,
		UnityAction<string> onFinish = null,
		UnityAction<string> errorFinish = null
	) {
		StartCoroutine(ConnectionAPICoroutine(url, onFinish, errorFinish));
	}

	private IEnumerator ConnectionAPICoroutine (
		string url,
		UnityAction<string> onFinish = null,
		UnityAction<string> errorFinish = null
	) {
		var request = UnityWebRequest.Get(url);
		GameManager.Log("通信開始 : " + url);
		yield return request.Send();
		if (!request.isError) {
			GameManager.Log( "url:"+ url + "\nSuccess : " + request.downloadHandler.text );
			if (onFinish != null) {
				onFinish( request.downloadHandler.text );
			}
		}
		else {
			GameManager.Log("url:"+ url + "\nFaild : " + request.error);
			if (errorFinish != null) {
				errorFinish( request.error );
			}
		}
	}
}