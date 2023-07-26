

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMovement : MonoBehaviour
{
    public UIManager uiManager;
    public RaycastHit2D hit;
    public Rigidbody2D rb;
    public float farwordSpeed = 5f;
    public static int foundedStars;

    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float multiplier = -1f;
    [SerializeField] float timer = 5f;
    [SerializeField] float hitWallRotation = 140f;
    [SerializeField] float moveDitectionRange = 5f;

    void Start()
    {
                farwordSpeed = 0f;
        rb = GetComponent<Rigidbody2D>();
        timer = 5;
        uiManager = FindObjectOfType<UIManager>();
            }

    void Update()
    {
                timer -= Time.deltaTime;
        transform.Translate(Vector3.up * farwordSpeed * Time.deltaTime);
        transform.Rotate(0, 0, rotationSpeed);
                if (timer <= 0)
        {
                      ChangeAngle();
            timer = 5;
        }
    }

    private void FixedUpdate()
    {
                hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.up), 0.4f);
                if (hit.collider != null && hit.collider.tag == "Wall")
        {
            transform.Rotate(0, 0, transform.rotation.z + hitWallRotation);
                    }
    }

    private void OnDrawGizmos()
    {
                Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * moveDitectionRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "hit" || collision.collider.name == "Star")
        {

            Debug.Log("Hitcollider");


        }
        timer = 5;
        transform.Rotate(0, 0, transform.rotation.z + hitWallRotation);


    }



    private void ChangeAngle()
    {



        rotationSpeed = rotationSpeed * multiplier;





    }

    private void OnMouseDown()
    {
        farwordSpeed = 1f;
        foundedStars++;
        Debug.Log(foundedStars);
        if (foundedStars <= 11)
        {
            GameManager.Instance.PlaySFX("SratFound");

        }

        if (foundedStars == 11)
        {
            Debug.Log("counted 11 stars");
            uiManager.OpenMiddlePanel();

        }

    }
}



