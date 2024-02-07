using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

 public enum ObjType
{
    Red,
    Green

}
public class CollectableObjs : MonoBehaviour
{
    public ObjType objType;
    [SerializeField]
   private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            other.transform.root.GetComponentInChildren<PlayerController>().ObjCollectJump(this.gameObject, objType);
        }
    }
}
