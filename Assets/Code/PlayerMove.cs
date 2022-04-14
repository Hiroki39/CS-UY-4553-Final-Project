using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public Material Mat1;
    public Material Mat2;
    public GameObject[] checkPoints;
    public bool[] checkPointIsBlues;
    public float[] checkPointScales;
    public GameObject bluePicked;
    public AudioSource objectSound;
    public AudioClip gemEatingSound;
    public TimeManager timeManager;

    Renderer rend;
    TrailRenderer trend;
    Rigidbody rb;

    [HideInInspector] public float jumpForce = 5f;
    [HideInInspector] public bool jumped = false;
    float speed = 12f;
    bool isAlive = true;
    bool isBlue;
    bool grounded = false;
    bool isBallSlowmo = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        trend = GetComponent<TrailRenderer>();

        isBlue = checkPointIsBlues[PublicVars.checkPoint];
        if (isBlue)
        {
            rend.material = Mat1;
            trend.startColor = Mat1.color;
            trend.endColor = Mat1.color;
        }
        else
        {
            rend.material = Mat2;
            trend.startColor = Mat2.color;
            trend.endColor = Mat2.color;
        }
        trend.enabled = false;
        trend.time = 0.5f;

        transform.localScale = new Vector3(checkPointScales[PublicVars.checkPoint], checkPointScales[PublicVars.checkPoint], checkPointScales[PublicVars.checkPoint]);
        transform.position = checkPoints[PublicVars.checkPoint].transform.position + new Vector3(0, 1.5f, 0);
        StartCoroutine(WaitToMove());
        for (int i = 0; i <= PublicVars.checkPoint; ++i)
        {
            Destroy(checkPoints[i]);
        }
    }

    void Update()
    {
        if (!isAlive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isBlue)
            {
                rend.material = Mat2;
                trend.startColor = Mat2.color;
                trend.endColor = Mat2.color;
            }
            else
            {
                rend.material = Mat1;
                trend.startColor = Mat1.color;
                trend.endColor = Mat1.color;
            }
            isBlue = !isBlue;

            if (isBallSlowmo)
            {
                timeManager.StopSlowmotion();
                isBallSlowmo = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump") && (grounded || PublicVars.infinteJump))
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }

        float zSpeed = Input.GetAxis("Vertical") * speed;
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        rb.AddForce(new Vector3(xSpeed, 0, zSpeed));

        if (transform.position.y < -12)
        {
            isAlive = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.CompareTag("Ground1") && !isBlue) || (other.gameObject.CompareTag("Ground2") && isBlue))
        {
            isAlive = false;
        }
        if (other.gameObject.CompareTag("Ground1") || other.gameObject.CompareTag("Ground2"))
        {
            grounded = true;
            trend.enabled = false;
        }
        if (other.gameObject.CompareTag("Ground2") && !PublicVars.movedPlatformFirstTime)
        {
            PublicVars.movedPlatformFirstTime = true;
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if ((other.gameObject.CompareTag("Ground1") && !isBlue) || (other.gameObject.CompareTag("Ground2") && isBlue))
        {
            isAlive = false;
        }
        if (other.gameObject.CompareTag("Ground1") || other.gameObject.CompareTag("Ground2"))
        {
            grounded = true;
            trend.enabled = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground1") || other.gameObject.CompareTag("Ground2"))
        {
            grounded = false;
            trend.Clear();
            trend.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Note: we use colliders here, not collisions
        if (other.gameObject.CompareTag("Gem1") || other.gameObject.CompareTag("Gem2") || other.gameObject.CompareTag("Gem3") || other.gameObject.CompareTag("Gem4"))
        {
            objectSound.PlayOneShot(gemEatingSound);
            other.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            other.gameObject.GetComponentInChildren<Renderer>().enabled = false;
            other.gameObject.GetComponent<Collider>().enabled = false;
            Destroy(other.gameObject, 0.5f);
        }

        if (other.gameObject.CompareTag("Gem1"))
        {
            if (!PublicVars.infinteJump)
            {
                StartCoroutine(BlueGemFirstTime());
                PublicVars.infinteJump = true;
            }
        }

        if (other.gameObject.CompareTag("Gem2"))
        {
            ++PublicVars.checkPoint;
            if (!PublicVars.pickedRed)
            {
                PublicVars.pickedRed = true;
            }
        }

        if (other.gameObject.CompareTag("Gem3"))
        {
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            if (!PublicVars.pickedGreen)
            {
                PublicVars.pickedGreen = true;
            }
        }

        if (other.gameObject.CompareTag("Gem4"))
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            if (!PublicVars.pickedYellow)
            {
                PublicVars.pickedYellow = true;
            }
        }

        if (other.gameObject.CompareTag("SlowmoPlane"))
        {
            timeManager.DoSlowmotion();
            isBallSlowmo = true;
        }

        if (other.gameObject.CompareTag("JumpPlane"))
        {
            jumped = true;
        }
    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(.2f);
    }

    public IEnumerator BlueGemFirstTime()
    {
        bluePicked.SetActive(true);
        yield return new WaitForSeconds(3f);
        bluePicked.SetActive(false);
    }
}