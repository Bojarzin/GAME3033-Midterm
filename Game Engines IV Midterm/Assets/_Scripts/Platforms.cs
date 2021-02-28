using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    PlayerController player;

    public enum PlatformType
    {
        BASIC,
        MOVING,
        MOVINGWAIT,
        FALLING,
        ROTATING,
        CRUMBLING,
        BLINKING
    };

    [SerializeField] public PlatformType platformType;

    Vector3 startScale;
    Vector3 positionOne;
    Vector3 positionTwoV;
    [SerializeField] Transform positionTwoT;
    [SerializeField] float moveSpeed = 1.0f;

    public bool playerSet = false;
    public bool shake = false;
    public bool on = false;

    public float blinkTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();

        startScale = transform.localScale;
        positionOne = transform.position;
        positionTwoV = positionTwoT.position;

        if (platformType == PlatformType.BLINKING)
        {
            if (on == false)
            {
                transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Shake();
        Blink();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (platformType == PlatformType.MOVINGWAIT)
        {
            if (playerSet)
            {
                gameObject.transform.position = Vector3.Lerp(positionOne, positionTwoV, Mathf.PingPong(Time.time * moveSpeed, 1.0f));
            }
        }
        else if (platformType == PlatformType.MOVING)
        {
            gameObject.transform.position = Vector3.Lerp(positionOne, positionTwoV, Mathf.PingPong(Time.time * moveSpeed, 1.0f));
        }
        else if (platformType == PlatformType.ROTATING)
        {
            gameObject.transform.Rotate(new Vector3(5.0f, 0.0f, 0.0f));
        }
    }

    void Shake()
    {
        if (platformType == PlatformType.CRUMBLING)
        {
            if (shake)
            {
                Vector3 shakeLeft = positionOne - new Vector3(0.08f, 0.0f, 0.0f);
                Vector3 shakeRight = positionOne + new Vector3(0.08f, 0.0f, 0.0f);
                transform.position = Vector3.Lerp(shakeLeft, shakeRight, Mathf.PingPong(Time.time * moveSpeed * 10, 1.0f));
            }
        }
    }

    public IEnumerator Crumble()
    {
        if (platformType == PlatformType.CRUMBLING)
        {
            yield return new WaitForSeconds(0.5f);
            shake = false;

            yield return new WaitForSeconds(0.3f);
            transform.parent.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.8f);
            transform.parent.gameObject.SetActive(true);
        }
    }

    void Blink()
    {
        if (platformType == PlatformType.BLINKING)
        {
            blinkTime -= Time.deltaTime;

            if (blinkTime <= 0)
            {
                if (on)
                {
                    gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
                    on = false;
                }
                else if (!on)
                {
                    gameObject.transform.localScale = startScale;
                    on = true;
                }
                blinkTime = 2.0f;
            }
        }
    }
}
