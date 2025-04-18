using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]

    public int walkSpeed;
    public int currentSpeed;
    public int runSpeed;
    CharacterController characterController;

    [Header("Jump")]

    public int jumpForce;
    public int gravityY;
    Vector3 gravityVector;
    float gravity = -9.81f;

    [Header("MouseControl")]

    public Transform camPos;
    public float mouseSens = 1f;
    public int maxRotate = 80;
    float xRotation = 0f;

    [Header("Crouch")]
    public float normalHeight;
    public float crouchHeight;
    public int crouchSpeed;
    public bool isCrouch;
    public Transform front, back;
    public bool frontHead, backHead;

    [Header("Inventory")]    
    bool isOpen;
    public GameObject playerInventory;
    public SCinventory inventory;
    private Item itemNearby;


    void Start()
    {      

        characterController = GetComponent<CharacterController>();
        normalHeight = characterController.height;
        currentSpeed = walkSpeed;
        playerInventory.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

    }

    void FixedUpdate()
    {
        Movement();
        Gravity();
        Jump();
        MouseController();
        Crouch();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!isOpen)
            {
                playerInventory.SetActive(true);
                Cursor.lockState= CursorLockMode.None;
                isOpen = true;

            }
            else if (isOpen)
            {
                playerInventory.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;

                isOpen = false;
            }
        }
    }


    #region Movement
    void Movement()
    {
        float MoveX = Input.GetAxis("Horizontal");
        float MoveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * MoveX + transform.forward * MoveZ;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        characterController.Move(move * currentSpeed * Time.deltaTime);



    }
    #endregion

    #region Gravity
    void Gravity()
    {
        gravityVector.y += gravity * Time.deltaTime;
        characterController.Move(gravityVector * Time.deltaTime);
    }

    #endregion

    #region Jump      
    void Jump()
    {
        gravityVector.y = characterController.isGrounded && Input.GetKey(KeyCode.Space) ? jumpForce : gravityVector.y;
        gravityVector.y = gravityVector.y < 0 ? gravityY : gravityVector.y;
    }

    #endregion

    #region MouseController

    private void MouseController()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxRotate, maxRotate);

        camPos.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    #endregion

    #region Crouch
    void Crouch()
    {
        DrawCrouchRaycast();

        if (Input.GetKey(KeyCode.LeftControl))
        {
            characterController.height = crouchHeight;
            currentSpeed = crouchSpeed;
            isCrouch = true;




        }
        else
        {
            if (!frontHead && !backHead)
            {
                characterController.height = normalHeight;
                currentSpeed = walkSpeed;
                isCrouch = false;
            }
        }


    }

    void DrawCrouchRaycast()
    {
        Color frontColor = Color.green;
        Color backColor = Color.green;

        frontHead = Physics.Raycast(front.position, Vector3.up, normalHeight - crouchHeight);
        backHead = Physics.Raycast(back.position, Vector3.up, normalHeight - crouchHeight);

        if (frontHead)
        {
            frontColor = Color.red;
        }

        if (backHead)
        {
            backColor = Color.red;
        }

        Debug.DrawRay(front.position, Vector3.up * (normalHeight - crouchHeight), frontColor);
        Debug.DrawRay(back.position, Vector3.up * (normalHeight - crouchHeight), backColor);

    }

    #endregion

    #region TakeGun
   /* void TakeGun()
    {
        float Dis = Vector3.Distance(this.gameObject.transform.position, Gun.transform.position);
        if(Dis<=1 && Input.GetKey(KeyCode.E))
        {

            if (inventory.AddItem(Gun.GetComponent<Item>().item))
            {
                Destroy(Gun);
            }
        }
    }*/
    #endregion
}
