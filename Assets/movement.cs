using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed;
    private GameManager GameManager;
    [SerializeField] private float maxHeight;
    private bool isPaused = false;
    private bool isDark = false;

    [SerializeField] GameObject flashlight1;
    [SerializeField] GameObject flashlight2;
    [SerializeField] GameObject areaLight;

    void Start()
    {
        GameManager = FindAnyObjectByType<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        Keyboard kb = Keyboard.current;

        UpdateMovement();

        if (isDark)
        {
            checkFlashlightToggle();
        }

        if (kb.escapeKey.wasPressedThisFrame && !isPaused)
        {
            FindObjectOfType<GameManager>().Pause();
            isPaused = true;
        } else if (isPaused && kb.escapeKey.wasPressedThisFrame)
        {
            FindObjectOfType<GameManager>().Resume();
            isPaused = false;
        }
    }

    private void UpdateMovement()
    {
        Keyboard kb = Keyboard.current;

        if (kb.aKey.isPressed)
        {
            GetComponent<Rigidbody2D>().position += Vector2.left * Time.deltaTime * movementSpeed;
        }
        if (kb.dKey.isPressed)
        {
            GetComponent<Rigidbody2D>().position += Vector2.right * Time.deltaTime * movementSpeed;
        }
        if (kb.wKey.isPressed)
        {
            GetComponent<Rigidbody2D>().position += Vector2.up * Time.deltaTime * movementSpeed;
            if (transform.position.y >= maxHeight)
            {
                transform.localPosition = new Vector2(transform.position.x, maxHeight);
            }
        }
        if (kb.sKey.isPressed)
        {
            GetComponent<Rigidbody2D>().position += Vector2.down * Time.deltaTime * movementSpeed;
        }
    }

    private void checkFlashlightToggle()
    {
        Keyboard kb = Keyboard.current;

        if (kb.digit1Key.wasPressedThisFrame)
        {
            flashlight2.SetActive(false);
            flashlight1.SetActive(true);
        } else if(kb.digit2Key.wasPressedThisFrame)
        {
            flashlight2.SetActive(true);
            flashlight1.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            GameManager.loseLife();
            if (GameManager.lifeCount <= 0)
            {
                GameManager.GameOver();
            }
        }
    }

    public void LightsOn(bool shouldTurnOn)
    {
        if (shouldTurnOn)
        {
            flashlight1.SetActive(true);
            flashlight2.SetActive(false);
            areaLight.SetActive(true);
            FindObjectOfType<SoundController>().PlayFlashlight();
            isDark = true;
        }
        else
        {
            flashlight1.SetActive(false);
            flashlight2.SetActive(false);
            areaLight.SetActive(false);
            FindObjectOfType<SoundController>().PlayFlashlight();
            isDark = false;
        }
    }

    public IEnumerator FlickerSprite()
    {
        bool boolean = false;

        for (int i = 0; i < 10; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = boolean;
            boolean = !boolean;
            if (boolean)
            {
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
