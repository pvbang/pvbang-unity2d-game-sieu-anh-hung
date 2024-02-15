using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject destroyObject;
    public bool isActive = false;
    public bool isParentObject = false;
    public bool waitAnimation = false;

    private void Awake()
    {
        if (isParentObject)
        {
            destroyObject = transform.parent.gameObject;
        }
        else if (destroyObject == null)
        {
            destroyObject = gameObject;
        }
    }

    private void Start()
    {
        if (isActive == false) return;

        if (waitAnimation)
        {
            StartCoroutine(CoroutineHelper.DelaySeconds(() => Destroy(destroyObject), GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length));
        }
        else
        {
            Destroy(destroyObject);
        }
    }
}
