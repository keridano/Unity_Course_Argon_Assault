using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float controlSpeed = 15f;
    [Tooltip("In m")] [SerializeField] float xRange = 20f;
    [Tooltip("In m")] [SerializeField] float yRange = 9f;
    [SerializeField] AudioClip shipThrust;
    [SerializeField] GameObject[] guns;

    [Header("Screen Position")]
    [SerializeField] float positionPitchFactor = -1.5f;
    [SerializeField] float positionYawFactor = 2.5f;

    [Header("Control Throw")]
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float controlRollFactor = -30f;

    AudioSource audioSource;

    float xThrow;
    float yThrow;
    bool isControlEnabled;

    private void Start()
    {
        isControlEnabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && audioSource.clip != shipThrust)
        {
            audioSource.clip = shipThrust;
            audioSource.loop = true;
            audioSource.Play();
            isControlEnabled = true;
        }

        if (!isControlEnabled) return;

        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    /// <summary>
    /// Called by string reference in CollisionHandler.cs
    /// </summary>
    void OnPlayerDeath() 
    {
        isControlEnabled = false;
        audioSource.Stop();
    }

    private void ProcessRotation()
    {
        var pitchDueToPosition  = transform.localPosition.y * positionPitchFactor;
        var pitchDueToControl   = yThrow * controlPitchFactor;

        var pitch               = pitchDueToPosition + pitchDueToControl;
        var yaw                 = transform.localPosition.x * positionYawFactor;
        var roll                = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        transform.localPosition = new Vector3(GetXPosition(), GetYPosition(), transform.localPosition.z);
    }

    private void ProcessFiring()
    {
        GunsOnOff(CrossPlatformInputManager.GetButton("Fire"));
    }

    private void GunsOnOff(bool isOn)
    {
        foreach (var gun in guns)
        {
            gun.SetActive(isOn);
        }
    }

    private float GetXPosition()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        var xOffset = xThrow * controlSpeed * Time.deltaTime;
        var xRawPos = transform.localPosition.x + xOffset;
        var xClampPos = Mathf.Clamp(xRawPos, -xRange, xRange);
        return xClampPos;
    }

    private float GetYPosition()
    {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        var yOffset = yThrow * controlSpeed * Time.deltaTime;
        var yRawPos = transform.localPosition.y + yOffset;
        var yClampPos = Mathf.Clamp(yRawPos, -yRange, yRange);
        return yClampPos;
    }

}