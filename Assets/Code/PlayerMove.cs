using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public int playerNum = 1;
    public int speed = 10;
    public Material Mat1;
    public Material Mat2;
    Renderer rend;

    Rigidbody rb;

    bool isAlive = true;
    bool isBlue = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(WaitToMove());

        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
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
    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(.2f);
        isAlive = true;
    }
}
