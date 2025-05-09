using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameDev.tv Challenge Club. Got questions or want to share your nifty solution?
// Head over to - http://community.gamedev.tv

public class Teleport : MonoBehaviour
{
    [SerializeField] Transform[] teleportTarget;
    [SerializeField] GameObject player;
    [SerializeField] Light areaLight;
    [SerializeField] Light mainWorldLight;

    void Start()
    {
        // Ensure all lights are off at the start
        if (areaLight != null) areaLight.enabled = false;
        if (mainWorldLight != null) mainWorldLight.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            TeleportPlayer();
            IlluminateArea();
            StartCoroutine(BlinkWorldLight());
        }
    }

    void TeleportPlayer()
    {
        if (teleportTarget.Length == 0)
        {
            Debug.LogWarning("No teleport targets assigned.");
            return;
        }

        int randomIndex = UnityEngine.Random.Range(0, teleportTarget.Length);
        Transform target = teleportTarget[randomIndex];

        if (player != null && target != null)
        {
            player.transform.position = target.position;
            player.transform.rotation = target.rotation;
        }
        else
        {
            Debug.LogWarning("Player or Teleport Target is not assigned.");
        }
    }

    void DeactivateObject()
    {
        gameObject.SetActive(false);
    }

    void IlluminateArea()
    {
        if (teleportTarget.Length == 0)
        {
            Debug.LogWarning("No teleport targets assigned.");
            return;
        }

        int randomIndex = UnityEngine.Random.Range(0, teleportTarget.Length);
        Transform target = teleportTarget[randomIndex];

        if (areaLight != null && target != null)
        {
            areaLight.transform.position = target.position;
            areaLight.transform.rotation = target.rotation;
            areaLight.enabled = true;
        }
        else
        {
            Debug.LogWarning("Area Light or Teleport Target is not assigned.");
        }
    }

    IEnumerator BlinkWorldLight()
    {
        if (mainWorldLight == null)
        {
            Debug.LogWarning("Main World Light is not assigned.");
            yield break;
        }

        mainWorldLight.enabled = false;
        Debug.Log("Light off!");

        yield return new WaitForSeconds(1.5f);

        mainWorldLight.enabled = true;
        Debug.Log("Light on!");

        DeactivateObject();
    }
}
