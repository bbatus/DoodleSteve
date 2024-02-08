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
            Finish();
        }
    }

    private void Finish() {
        Debug.Log("Finish Line");
        CameraFollow.instance.CameraFinish();
        UIManager.instance.ConfettiSetActive(true);
    }
}
