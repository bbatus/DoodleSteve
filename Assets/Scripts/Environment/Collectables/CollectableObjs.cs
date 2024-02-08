using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class CollectableObjs : MonoBehaviour
{
    public ObjType objType;
    [SerializeField]

    Collider collider;
   private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            collider.enabled = false;
            other.transform.root.GetComponentInChildren<PlayerController>().ObjCollectJump(this.gameObject, objType);
        }
    }
}
