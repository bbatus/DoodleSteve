using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; // Oyuncunun transformu
    private Vector3 cameraOffset; // Kamera ile oyuncu arasındaki başlangıç mesafesi
    public float smoothFactor = 0.5f; // Kameranın ne kadar "yumuşak" hareket edeceği
    public bool lookAtPlayer = false; // Kameranın oyuncuya bakıp bakmayacağı

    void Start()
    {
        // Kamera ile oyuncu arasındaki mesafeyi hesapla
        cameraOffset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        // Oyuncunun yeni pozisyonunu, kamera ile oyuncu arasındaki orijinal mesafeyi koruyarak hesapla
        Vector3 newPos = playerTransform.position + cameraOffset;

        // Kameranın pozisyonunu, oyuncunun pozisyonuna doğru "yumuşak" bir geçişle güncelle
        transform.position = Vector3.Lerp(transform.position, newPos, smoothFactor * Time.deltaTime);

        if (lookAtPlayer)
        {
            transform.LookAt(playerTransform);
        }
    }
}
