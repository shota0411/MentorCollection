using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class BasePopupController : MonoBehaviour, IPopupController {

	private Transform m_Origin;
	public Transform origin { get { return m_Origin; } }

	protected virtual void OnOpenBeforeActive() {}
	protected virtual void OnOpenAfterActive() {}
	protected virtual void OnCloseBeforeActive() {}
	protected virtual void OnCloseAfterActive() {}

	private Animator anim;
	private UnityEngine.Events.UnityAction m_OnOpenFinish;
	private UnityEngine.Events.UnityAction m_OnCloseFinish;

	private void Setup () {
		m_Origin = transform.Find("Popup");
		anim = GetComponent<Animator>();
		transform.localScale = Vector3.one;
	}

	public void Open (UnityAction onOpenFinish = null) {
		Setup();
		m_OnOpenFinish = onOpenFinish;
		this.OnOpenBeforeActive();
	}

	public void Close (UnityAction onCloseFinish = null) {
		m_OnCloseFinish = onCloseFinish;
		this.OnCloseBeforeActive();
		anim.SetTrigger("Close");
	}

	// Animatorから呼ばれる
	private void OnOpenFinish() {
		if (m_OnOpenFinish != null) {
			m_OnOpenFinish();
		}
		this.OnOpenAfterActive();
	}

	// Animatorから呼ばれる
	private void OnCloseFinish() {
		PopupManager.instance.RemoveLastPopup();
		if (m_OnCloseFinish != null) {
			m_OnCloseFinish();
		}
		this.OnCloseAfterActive();
		Destroy(gameObject);
	}
}