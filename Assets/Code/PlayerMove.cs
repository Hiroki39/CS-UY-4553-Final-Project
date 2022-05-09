using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public Material Mat1;
    public Material Mat2;
    public GameObject blueGemPicked;
    public AudioSource objectSound;
    public TMP_Text slowmoText;
    public int dieLimit;
    [HideInInspector] public float jumpForce = 6f;
    [HideInInspector] public bool isDying = false;

    Renderer rend;
    TrailRenderer trend;
    Rigidbody rb;
    CameraFollow cf;
    ParticleSystem[] ps;
    float force = 12f;
    float maxSpeed = 12f;
    float slowdownFactor = 0.2f;
    bool isAlive = true;
    bool isBlue = true;
    bool grounded = false;
    bool readyToJump = false;
    bool infiniteJump = false;
    bool isSlowmoActive = false;
    bool beforeDieWaitStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        trend = GetComponent<TrailRenderer>();
        ps = GetComponentsInChildren<ParticleSystem>();
        cf = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();

        Physics.defaultContactOffset = 0.00000001f;
        slowmoText.text = PublicVars.slowmoCount.ToString();

        isBlue = PublicVars.checkPointIsBlue;

        transform.localScale = new Vector3(PublicVars.checkPointScale, PublicVars.checkPointScale, PublicVars.checkPointScale);
        transform.position = PublicVars.checkPointPosition + new Vector3(0, 1.5f, 0);

        GameObject[] checkPoints = GameObject.FindGameObjectsWithTag("Gem2");

        foreach (GameObject checkpoint in checkPoints)
        {
            if (checkpoint.transform.position == PublicVars.checkPointPosition)
            {
                Destroy(checkpoint);
            }
        }


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

        StartCoroutine(WaitToMove());
        StartCoroutine(AutoSave());
    }

    void Update()
    {
        if (!isAlive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (transform.position.y < dieLimit)
        {
            if (!beforeDieWaitStarted)
            {
                StartCoroutine(WaitToDie());
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && !isSlowmoActive)
        {
            if (PublicVars.slowmoCount > 0)
            {
                isSlowmoActive = true;
                PublicVars.slowmoCount -= 1;
                slowmoText.text = PublicVars.slowmoCount.ToString();
                ps[0].Play();
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
                StartCoroutine(DoSlowmo(0.5f, 0.5f));
            }
        }
    }

    IEnumerator Die()
    {
        isDying = true;
        GetComponent<TimeBody>().StopRewind();

        yield return new WaitForSeconds(0.5f);
        rb.isKinematic = true;
        rb.freezeRotation = true;
        rend.enabled = false;
        trend.enabled = false;

        ps[9].Play();
        yield return new WaitForSeconds(3.0f);
        isAlive = false;
    }

    IEnumerator WaitToDie()
    {
        beforeDieWaitStarted = true;

        // player have three seconds to rewind
        yield return new WaitForSeconds(3.0f);

        // if the ball after slowmo is still under the limit
        if (transform.position.y < dieLimit)
        {
            StartCoroutine(Die());
        }
        else
        {
            beforeDieWaitStarted = false;
        }
    }

    IEnumerator DoSlowmo(float delayStartSlowmo, float delayStopSlowmo)
    {
        yield return new WaitForSeconds(delayStartSlowmo);
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        yield return new WaitForSeconds(delayStopSlowmo * slowdownFactor);
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        isSlowmoActive = false;
    }

    private void FixedUpdate()
    {
        if (isDying) return;

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
        if (((other.gameObject.CompareTag("Ground1") && !isBlue) || (other.gameObject.CompareTag("Ground2") && isBlue)) && !isDying)
        {
            StartCoroutine(Die());
        }
        if (other.relativeVelocity.y > 6 && !cf.shaking)
        {
            StartCoroutine(cf.ShakeCamera());
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if (((other.gameObject.CompareTag("Ground1") && !isBlue) || (other.gameObject.CompareTag("Ground2") && isBlue)) && !isDying)
        {
            StartCoroutine(Die());
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
            CheckPointAttribute checkPointAttr = other.gameObject.GetComponent<CheckPointAttribute>();
            PublicVars.checkPointIsBlue = checkPointAttr.isBlue;
            PublicVars.checkPointScale = checkPointAttr.scale;
            PublicVars.checkPointPosition = other.gameObject.transform.position;
        }

        if (other.gameObject.CompareTag("Gem3"))
        {
            //transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);

            var scaleTo = transform.localScale - new Vector3(0.1f, 0.1f, 0.1f);
            StartCoroutine(ScaleOverSeconds(gameObject, scaleTo, 1f));
        }

        if (other.gameObject.CompareTag("Gem4"))
        {
            //transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);

            var scaleTo = transform.localScale + new Vector3(0.1f, 0.1f, 0.1f);
            StartCoroutine(ScaleOverSeconds(gameObject, scaleTo, 1f));
        }
    }

    IEnumerator ScaleOverSeconds(GameObject objectToScale, Vector3 scaleTo, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingScale = objectToScale.transform.localScale;
        while (elapsedTime < seconds)
        {
            objectToScale.transform.localScale = Vector3.Lerp(startingScale, scaleTo, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
        }
        objectToScale.transform.localScale = scaleTo;
        yield return null;
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