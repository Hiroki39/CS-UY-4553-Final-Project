using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public Material Mat1;
    public Material Mat2;
    public GameObject[] checkPoints;
    public bool[] checkPointIsBlues;
    public float[] checkPointScales;
    public GameObject blueGemPicked;
    public AudioSource objectSound;
    public float slowdownFactor = 0.2f;
    public int slowmoCount = 3;
    public TMP_Text slowmoText;
    [HideInInspector] public float jumpForce = 6f;

    Renderer rend;
    TrailRenderer trend;
    Rigidbody rb;
    CameraFollow cf;
    ParticleSystem ps;
    float force = 12f;
    float maxSpeed = 12f;
    bool isAlive = true;
    bool isBlue;
    bool grounded = false;
    bool readyToJump = false;
    bool infiniteJump = false;
    bool isSlowmoActive = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        trend = GetComponent<TrailRenderer>();
        ps = GetComponentInChildren<ParticleSystem>();
        cf = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();

        Physics.defaultContactOffset = 0.00000001f;

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

        StartCoroutine(AutoSave());
    }

    void Update()
    {
        if (transform.position.y < -12)
        {
            isAlive = false;
        }
        if (!isAlive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.E) && !isSlowmoActive)
        {
            if (slowmoCount > 0)
            {
                isSlowmoActive = true;
                slowmoCount -= 1;
                slowmoText.text = "Slowmo Remaining: " + slowmoCount.ToString();
                ps.Play();
            }
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
        }
        if (grounded != isGrounded())
        {
            if (grounded == true)
            {
                trend.Clear();
                trend.enabled = true;
            }
            else
            {
                trend.enabled = false;
            }
            grounded = isGrounded();
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            readyToJump = true;
            if (isSlowmoActive)
            {
                StartCoroutine(DoSlowmo());
            }
        }
    }

    IEnumerator DoSlowmo()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        yield return new WaitForSeconds(0.6f * slowdownFactor);
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        isSlowmoActive = false;
    }

    private void FixedUpdate()
    {
        float zSpeed = Input.GetAxis("Vertical") * force;
        float xSpeed = Input.GetAxis("Horizontal") * force;
        rb.AddForce(new Vector3(xSpeed, 0, zSpeed));
        Vector3 projectedSpeed = Vector3.ProjectOnPlane(rb.velocity, new Vector3(0f, 1f, 0f));
        if (projectedSpeed.magnitude > maxSpeed)
        {
            projectedSpeed = projectedSpeed.normalized * maxSpeed;
            rb.velocity = new Vector3(projectedSpeed.x, rb.velocity.y, projectedSpeed.z);
        }
        if (readyToJump)
        {
            readyToJump = false;
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, transform.localScale.y / 2 + 0.2f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.CompareTag("Ground1") && !isBlue) || (other.gameObject.CompareTag("Ground2") && isBlue))
        {
            isAlive = false;
        }
        if (other.relativeVelocity.y > 6 && !cf.shaking)
        {
            StartCoroutine(cf.ShakeCamera());
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if ((other.gameObject.CompareTag("Ground1") && !isBlue) || (other.gameObject.CompareTag("Ground2") && isBlue))
        {
            isAlive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gem1"))
        {
            if (!infiniteJump)
            {
                StartCoroutine(BlueGemFirstTime());
                infiniteJump = true;
            }
        }

        if (other.gameObject.CompareTag("Gem2"))
        {
            ++PublicVars.checkPoint;
            Debug.Log(PublicVars.checkPoint);
        }

        if (other.gameObject.CompareTag("Gem3"))
        {
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        }

        if (other.gameObject.CompareTag("Gem4"))
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }

        // if (other.gameObject.CompareTag("SlowmoPlane"))
        // {
        //     timeManager.DoSlowmotion();
        //     isBallSlowmo = true;
        // }
    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(.2f);
    }

    public IEnumerator BlueGemFirstTime()
    {
        blueGemPicked.SetActive(true);
        yield return new WaitForSeconds(3f);
        blueGemPicked.SetActive(false);
    }

    IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            SaveLoad.Save();
        }
    }
}