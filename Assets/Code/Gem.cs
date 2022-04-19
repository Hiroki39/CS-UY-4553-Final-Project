using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public AudioClip gemEatingSound;
    ParticleSystem ps;
    Renderer rend;
    Collider coll;
    int rotateSpeed = 30;
    float hitVolume = 2f;

    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        rend = GetComponent<Renderer>();
        coll = GetComponent<Collider>();
    }
    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource objectSound = other.gameObject.GetComponent<PlayerMove>().objectSound;
            objectSound.PlayOneShot(gemEatingSound, hitVolume);
            ps.Play();
            rend.enabled = false;
            coll.enabled = false;
            Destroy(gameObject, 0.5f);
        }
    }
}

