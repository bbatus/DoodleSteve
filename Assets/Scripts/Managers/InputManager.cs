using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector2 GetPlayerInput()
    {
#if UNITY_EDITOR
        return new Vector2(Input.GetAxis("Horizontal"), 0);
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                // Dokunmatik ekranın yatay hareketini algılayıp, bu değeri oyuncunun hareketi için kullan.
                // Dokunmatik ekran genişliğine göre bir değer döndürmek için ekranın genişliğini kullanarak bir oran hesapla.
                // Bu basit bir yaklaşımdır ve oyununuz için daha detaylı bir ayarlama gerektirebilir.
                float move = touch.deltaPosition.x / Screen.width * 2 - 1; // -1 ile 1 arasında bir değer döndürür.
                return new Vector2(move, 0);
            }
        }
        return Vector2.zero; // Dokunmatik girdi yoksa veya hareket etmiyorsa (0,0) döndür.
#endif
    }

    public bool IsJumpRequested()
    {
#if UNITY_EDITOR
        return Input.GetKeyDown(KeyCode.Space);
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // Eğer dokunma başladıysa, zıplama isteği olarak kabul et.
            return touch.phase == TouchPhase.Began;
        }
        return false;
#endif
    }
}