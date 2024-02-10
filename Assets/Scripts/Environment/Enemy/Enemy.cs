using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform enemyCharacter;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 6) {
            other.transform.root.GetComponentInChildren<PlayerController>().Aim(enemyCharacter);
            Debug.Log($"Enemy");
        }
    }
}
