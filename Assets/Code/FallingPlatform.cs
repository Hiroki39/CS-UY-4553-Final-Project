using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public AudioClip platformLandingSound;
    Rigidbody rb;
    ParticleSystem ps;
    bool landed = false;
    float waitTime = 0.8f;
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
    }
    IEnumerator Fall()
    {
        landed = true;
        ps.Play();
        yield return new WaitForSeconds(waitTime);
        rb.isKinematic = false;
    }
}
