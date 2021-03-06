using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public AudioClip platformLandingSound;
    Rigidbody rb;
    ParticleSystem ps;
    [HideInInspector] public bool landed = false;
    float waitTime = 0.5f;
    float hitVolume = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !landed)
        {
            AudioSource objectSound = other.gameObject.GetComponent<PlayerMove>().objectSound;
            objectSound.PlayOneShot(platformLandingSound, hitVolume);
            StartCoroutine(Fall());
        }
        if (other.gameObject.CompareTag("PlayerEx") && !landed)
        {
            AudioSource objectSound = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().objectSound;
            objectSound.PlayOneShot(platformLandingSound, hitVolume);
            StartCoroutine(Fall());
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !landed)
        {
            AudioSource objectSound = other.gameObject.GetComponent<PlayerMove>().objectSound;
            objectSound.PlayOneShot(platformLandingSound, hitVolume);
            StartCoroutine(Fall());
        }
        if (other.gameObject.CompareTag("PlayerEx") && !landed)
        {
            AudioSource objectSound = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().objectSound;
            objectSound.PlayOneShot(platformLandingSound, hitVolume);
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        landed = true;
        ps.Play();
        yield return new WaitForSeconds(waitTime);
        rb.isKinematic = false;
    }
}
