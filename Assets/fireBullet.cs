using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class fireBullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform firePoint;
    public GameObject bullet;
    public bool canFire = true;
    public float firingCooldown;
    public float spawnDistance;
    private Animator animator;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard kb = Keyboard.current;

        if (canFire && kb.spaceKey.wasPressedThisFrame)
        {
            FindObjectOfType<SoundController>().PlayFireBullet(true);

            canFire = false;

            animator.SetTrigger("Throw");

            Instantiate(bullet, firePoint.position + GetComponent<Rigidbody2D>().transform.up * spawnDistance, GetComponent<Rigidbody2D>().transform.rotation);
        }
    }
    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(firingCooldown);
        canFire = true;
    }
}
