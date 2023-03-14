using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserGun : MonoBehaviour
{
    public float duration = 1.0f;  // Time duration for which the laser beam should appear
    public GameObject laserBeam;  // Reference to the laser beam GameObject

    private bool isActive = false; // Whether the laser beam is currently active or not
    private float timer = 0.0f;    // Timer to keep track of the time duration

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;

            if (timer <= 0.0f)
            {
                isActive = false;
                timer = duration;
                laserBeam.SetActive(false);
            }
        }
    }

    // Method to activate the laser beam
    public void ActivateLaserBeam()
    {
        isActive = true;
        laserBeam.SetActive(true);
    }

    // Method to deactivate the laser beam
    public void DeactivateLaserBeam()
    {
        isActive = false;
        laserBeam.SetActive(false);
    }
}
