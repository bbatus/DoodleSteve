using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            other.transform.root.GetComponentInChildren<PlayerController>().FinishLineControll();

            Debug.Log("finish");
            
        }
    }
}
