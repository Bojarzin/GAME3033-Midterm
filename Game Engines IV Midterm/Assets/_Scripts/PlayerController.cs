using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    PlayerControls controls;

    public Canvas pauseCanvas;

    Respawn respawn;
    [SerializeField] Animator animator;
    
    public float moveSpeed;
    public float diveSpeed;
    public float rotationSpeed;
    public float divingRotationSpeed;
    public float jumpStrength;

    Vector2 inputDirection;

    Quaternion rotationToCamera;
    Quaternion rotationToMoveDirection;
    Vector3 moveDirection;
    Vector3 externalMovement = Vector3.zero;
    public Vector2 yDirection;

    bool isMoving;
    bool isDiving;

    bool isLoading;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        respawn = GetComponent<Respawn>();
        pauseCanvas.enabled = false;
        Time.timeScale = 1.0f;
        isLoading = false;

        Cursor.visible = false;
    }

    private void Update()
    {
        Movement();
        Rotation();
    }
    
    private void FixedUpdate()
    {
        yControl();
    }

    public void OnMove(InputValue _value)
    {
        inputDirection = _value.Get<Vector2>();
    }

    public void OnJump(InputValue _button)
    {
        if (characterController.isGrounded)
        {
            Jump();
        }
        else
        {
            Dive();
        }
    }

    public void OnPause(InputValue _button)
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        pauseCanvas.enabled = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        pauseCanvas.enabled = false;
    }

    public void Movement()
    {
        moveDirection = Vector3.forward * inputDirection.y + Vector3.right * inputDirection.x;

        Vector3 cameraForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
        rotationToCamera = Quaternion.LookRotation(cameraForward, Vector3.up);

        moveDirection = rotationToCamera * moveDirection;
        if (inputDirection.y != 0 || inputDirection.x != 0)
        {
            if (!isDiving)
            {
                characterController.Move(moveDirection * moveSpeed * Time.deltaTime + externalMovement);
                isMoving = true;
            }
        }
        else
        {
            isMoving = false;
        }

        if (isDiving && !characterController.isGrounded && !isLoading)
        {
            characterController.Move(transform.forward * diveSpeed * Time.deltaTime);
        }

        if (characterController.isGrounded)
        {
            isDiving = false;
            animator.SetBool("Dive", false);
        }

        characterController.Move(yDirection * Time.deltaTime);
        UpdateAnimation();
    }

    public void Rotation()
    {
        if (inputDirection.x != 0 || inputDirection.y != 0)
        {
            if (!isDiving)
            {
                rotationToMoveDirection = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToMoveDirection, rotationSpeed * Time.deltaTime);
            }
            else
            {
                rotationToMoveDirection = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToMoveDirection, divingRotationSpeed * Time.deltaTime);
            }
        }
    }

    public void UpdateAnimation()
    {
        float animationSpeed = isMoving ? 1.0f : 0.0f;
        animator.SetFloat("Speed", animationSpeed);
    }

    public void yControl()
    {
        if (!characterController.isGrounded)
        {
            yDirection.y -= 10.04f * Time.deltaTime;
        }
    }

    public void Jump()
    {
        yDirection.y = jumpStrength / 50f;
    }

    public void Dive()
    {
        isDiving = true;
        animator.SetBool("Dive", true);
    }

    public void Respawn()
    {
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Platform"))
        {
            if (hit.gameObject.GetComponent<Platforms>().platformType == Platforms.PlatformType.CRUMBLING &&
                !hit.gameObject.GetComponent<Platforms>().shake)
            {
                hit.gameObject.GetComponent<Platforms>().shake = true;
                StartCoroutine(hit.gameObject.GetComponent<Platforms>().Crumble());
            }
        }

        if (hit.gameObject.CompareTag("Enemy"))
        {
            transform.position = respawn.currentSpawnPoint.transform.position;
        }

        if (hit.gameObject.CompareTag("Gem"))
        {
            hit.gameObject.GetComponent<Gem>().collected = true;
            if (hit.gameObject.GetComponent<Gem>().gemColour == Gem.GemColour.RED)
            {
                GameManager.Instance.redCollected = true;
                GameManager.Instance.redPortal = true;
            }
            else if (hit.gameObject.GetComponent<Gem>().gemColour == Gem.GemColour.ORANGE)
            {
                GameManager.Instance.orangeCollected = true;
                GameManager.Instance.orangePortal = true;
            }
            else if (hit.gameObject.GetComponent<Gem>().gemColour == Gem.GemColour.YELLOW)
            {
                GameManager.Instance.yellowCollected = true;
                GameManager.Instance.yellowPortal = true;
            }
            else if (hit.gameObject.GetComponent<Gem>().gemColour == Gem.GemColour.GREEN)
            {
                GameManager.Instance.greenCollected = true;
                GameManager.Instance.greenPortal = true;
            }
            else if (hit.gameObject.GetComponent<Gem>().gemColour == Gem.GemColour.BLUE)
            {
                GameManager.Instance.blueCollected = true;
                GameManager.Instance.bluePortal = true;
            }
            else if (hit.gameObject.GetComponent<Gem>().gemColour == Gem.GemColour.INDIGO)
            {
                GameManager.Instance.indigoCollected = true;
                GameManager.Instance.indigoPortal = true;
            }
            else if (hit.gameObject.GetComponent<Gem>().gemColour == Gem.GemColour.VIOLET)
            {
                GameManager.Instance.violetCollected = true;
                GameManager.Instance.violetPortal = true;
            }
            Destroy(hit.gameObject);

            GameManager.Instance.gemsCollected++;

            StartCoroutine(LoadScene());
        }

        if (hit.gameObject.CompareTag("SpawnPoint"))
        {
            respawn.currentSpawnPoint.transform.position = hit.gameObject.transform.position;
            Destroy(hit.gameObject);
        }
        if (hit.gameObject.CompareTag("DeathPlane"))
        {
            transform.position = respawn.currentSpawnPoint.transform.position;
        }

        if (hit.gameObject.CompareTag("Portal"))
        {
            if (!hit.gameObject.GetComponent<Portal>().isUsed)
            {
                isLoading = true;
                hit.gameObject.GetComponent<Portal>().isUsed = true;
                StartCoroutine(hit.gameObject.GetComponent<Portal>().LoadScene());
            }
        }

        if (hit.gameObject.CompareTag("Win"))
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Over World");
    }
}
