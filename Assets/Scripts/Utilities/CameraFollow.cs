using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    public Transform Target; // Karakterin transformu
    public Transform camTransform; // Kameranın transformu
    public Vector3 Offset; // Kamera ile karakter arasındaki mesafe offset'i
    public float SmoothTime = 0.3f; // Kameranın hedefe "yumuşak" bir şekilde hareket etme süresi

    private Vector3 velocity = Vector3.zero;

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

        // Kamera ile karakter arasındaki başlangıç mesafesini hesapla
        Offset = camTransform.position - Target.position;
    }

    private void LateUpdate()
    {
        // Karakterin Y pozisyonu dikkate alınarak hedef pozisyonu hesapla
        Vector3 targetPosition = Target.position + Offset;

        // Kameranın Y pozisyonunu, hedefin Y pozisyonuna yumuşak bir geçiş yaparak güncelle
        Vector3 newPosition = new Vector3(camTransform.position.x, targetPosition.y, camTransform.position.z);
        camTransform.position = Vector3.SmoothDamp(camTransform.position, newPosition, ref velocity, SmoothTime);
    }

    public void CameraFinish()
    {
        transform.DOMoveX(20f, .15f);
        transform.DOMoveY(40f, .15f);
        transform.DOMoveZ(0f, .15f); // move 3f to right in .15 seconds 
        transform.DORotate(new Vector3(transform.eulerAngles.x, -75, 0), .15f);
    }
}
