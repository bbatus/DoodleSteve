using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;
using System.ComponentModel;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public PlayerData playerData;
    public bool isGrounded;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.2f;
    private int greenAmount;
    private int redAmount;
    Sequence collectObjSequence;
    [SerializeField] GameObject bulletObject;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (InputManager.instance == null)
        {
            // InputManager objesi sahnede yoksa, bunu burada oluşturabilirsiniz.
            // Ancak, InputManager'ı bir prefab olarak sahnenize eklemeniz ve
            // bu kontrolü kaldırmanız daha temiz bir yaklaşım olacaktır.
            Debug.LogError("InputManager instance not found. Please add it to the scene.");
        }
    }

    private void Update()
    {
        CheckGroundStatus();
        if (InputManager.instance.IsJumpRequested() && isGrounded)
        {
            Jump();
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    void CheckGroundStatus()
    {
        isGrounded = IsGrounded();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 input = InputManager.instance.GetPlayerInput();
        Vector3 movement = new Vector3(input.x, 0, 0) * playerData.movementSpeed;
        playerRb.MovePosition(transform.position + movement * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        playerRb.AddForce(Vector3.up * playerData.jumpForce, ForceMode.Impulse);
        isGrounded = false;

        float targetRotation = transform.eulerAngles.y + 360f; // Y ekseni etrafında 360 derece dön
    DOTween.To(() => transform.eulerAngles.y, x => transform.eulerAngles = new Vector3(transform.eulerAngles.x, x, transform.eulerAngles.z), targetRotation, 1f);
    }

    public void ObjCollectJump(GameObject obj, ObjType objType)
    {
        if (objType == ObjType.Green)
        {
            greenAmount++;
            UIManager.instance.GreenAmountTextChange(greenAmount);
        }
        if (objType == ObjType.Red)
        {
            redAmount++;
            UIManager.instance.RedAmountTextChange(redAmount);
        }
        collectObjSequence = DOTween.Sequence().SetAutoKill(false);
        collectObjSequence.Append(obj.transform.DOLocalJump(gameObject.transform.localPosition + new Vector3(0, 3f, 0), 2f, 3, 2f).OnComplete(() =>
        {
            Debug.Log("Eklendi");
            Taptic.Light();
            obj.SetActive(false);
        }));
        collectObjSequence.Join(obj.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), .5f));
    }
    
    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = true;
    //     }
    // }

    // private void OnCollisionExit(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = false;
    //     }
    // }

        public void FinishLineControll() {
        Debug.Log("Finish Line");
        CameraFollow.instance.CameraFinish();
        UIManager.instance.ConfettiSetActive(true);
    }
    public void Aim(Transform enemy) {
        if (greenAmount == 0 || redAmount == 0)
            return;
        greenAmount--;
        redAmount--;

        GameObject _bullet = Instantiate(bulletObject, transform.position, Quaternion.identity);
        _bullet.transform.parent = enemy;

        _bullet.transform.DOLocalJump(new Vector3(-4, 37, -0.75f), 2f, 1, .5f).OnComplete(() =>
        {
            int distributedGemAmount = 100;
            UIManager.instance.GemTextUpdate(true, distributedGemAmount);
            Debug.Log($"Dagitildi");
            Taptic.Light();
        });


    }
    

}
