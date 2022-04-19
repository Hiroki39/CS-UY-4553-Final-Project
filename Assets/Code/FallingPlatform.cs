using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public AudioClip platformLandingSound;
    Rigidbody rb;
    ParticleSystem ps;

    bool landed = false;
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
            objectSound.PlayOneShot(platformLandingSound, 2f);
            StartCoroutine(Fall());
        }
    }
    IEnumerator Fall()
    {
        landed = true;
        ps.Play();
        yield return new WaitForSeconds(0.5f);
        rb.isKinematic = false;
    }
}
