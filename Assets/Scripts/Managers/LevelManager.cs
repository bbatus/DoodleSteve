using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

    [SerializeField] GameObject finishAreaPrefab;
    [SerializeField] Transform endTransform;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start() {
        for (int i = 0; i < 50; i++) {
            GameObject _go = Instantiate(finishAreaPrefab);
            _go.transform.position = endTransform.position;
            endTransform.position = new Vector3(-4, 37, endTransform.position.z + 3);
        }
    }

}
