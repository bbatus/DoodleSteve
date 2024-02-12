using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;
using System.ComponentModel;

public class PlayerController : MonoBehaviour
{
    [Space(20)]
    [Header("Player Controls")]
    [SerializeField][Range(1f, 10f)] private float moveForwardSpeed = 5f;
    [SerializeField] private bool isMovingForward = false;
    private PlayerAnim playerAnim;
    [Space(20)]
    [Header("Ground Checking")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField][Range(1f, 10f)] float groundCheckDistance = 0.2f;
    private Rigidbody playerRb;
    public PlayerData playerData;
    [Space(20)]
    [Header("Collectables")]
    [SerializeField] private int greenAmount;
    [SerializeField] private int redAmount;
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
            Debug.LogError("IM Yok");
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

        if (isMovingForward)
        {
            transform.position += new Vector3(0, 0, moveForwardSpeed * Time.deltaTime);
        }
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

    public void FinishLineControll()
    {
        UIManager.instance.levelCompletePanel.SetActive(true);
        UIManager.instance.ActivateLevelCompletePanel(1f);
        Debug.Log("Finish Line");
        CameraFollow.instance.CameraFinish();
        UIManager.instance.ConfettiSetActive(true);
        isMovingForward = true;
        playerData.movementSpeed = 1.25f;
        //playerAnim.FinishAnim();
    }
    public void Aim(Transform enemy)
    {
        if (greenAmount == 0 || redAmount == 0)

            return;
        greenAmount--;
        redAmount--;

        GameObject _bullet = Instantiate(bulletObject, transform.position, Quaternion.identity);
        _bullet.transform.parent = enemy;

        _bullet.transform.DOLocalJump(new Vector3(0, -2, -0.75f), 2f, 1, .5f).OnComplete(() =>
        {
            int distributedGemAmount = 100;
            UIManager.instance.GemTextUpdate(true, distributedGemAmount);
            Debug.Log($"Dagitildi");
            Taptic.Light();
        });
    }
}
