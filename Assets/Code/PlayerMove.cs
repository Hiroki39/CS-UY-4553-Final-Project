using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public int playerNum = 1;
    public int speed = 10;
    public int jumpSpeed = 10;
    public Material Mat1;
    public Material Mat2;
    Renderer rend;

    Rigidbody rb;

    bool isAlive = true;
    bool isBlue = true;

    bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(WaitToMove());

        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isBlue)
            {
                rend.material = Mat2;
            }
            else
            {
                rend.material = Mat1;
            }
            isBlue = !isBlue;
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }

        float zSpeed = Input.GetAxis("Vertical") * speed;
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        rb.AddForce(new Vector3(xSpeed, 0, zSpeed));

        if (transform.position.y < -10)
        {
            isAlive = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if ((other.gameObject.CompareTag("Ground1") && !isBlue) || (other.gameObject.CompareTag("Ground2") && isBlue))
        {
            isAlive = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (other.gameObject.CompareTag("Ground1") || other.gameObject.CompareTag("Ground2"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground1") || other.gameObject.CompareTag("Ground2"))
        {
            grounded = false;
        }
    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(.2f);
        isAlive = true;
    }
}