using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvatorController : MonoBehaviour {

    [SerializeField] private SpriteRenderer face;
    [SerializeField] private Transform diveCameraPoint, mainCameraPoint;
    public Transform MainCameraPoint { get { return mainCameraPoint; } }

    private Character characterData;
    public Character Character { get{ return characterData; } }

    private NavMeshAgent agent;
    private Transform target;

    public void SetValue (Character data)
    {
        characterData = data;
        face.sprite = Resources.Load<Sprite>("Face/" + data.Master.ImageId);
    }

    public Transform VRView ()
    {
        face.gameObject.SetActive(false);
        return diveCameraPoint;
    }

    public void InactiveVR ()
    {
        face.gameObject.SetActive(true);
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = AvatarManager.instance.StartPoint;
    }

    private void Update()
    {
        if (target == null) return;
        agent.SetDestination(target.position);

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < 0.2f) 
        {
            target = AvatarManager.instance.GetTarget();
        }
    }

}