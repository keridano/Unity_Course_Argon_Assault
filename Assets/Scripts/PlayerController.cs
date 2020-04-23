using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float controlSpeed = 15f;
    [Tooltip("In m")] [SerializeField] float xRange = 20f;
    [Tooltip("In m")] [SerializeField] float yRange = 9f;

    [Header("Screen Position")]
    [SerializeField] float positionPitchFactor = -1.5f;
    [SerializeField] float positionYawFactor = 2.5f;

    [Header("Control Throw")]
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float controlRollFactor = -30f;

    float xThrow;
    float yThrow;

    bool isControlEnabled;

    private void Start()
    {
        isControlEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isControlEnabled) return;

        ProcessTranslation();
        ProcessRotation();
    }

    /// <summary>
    /// Called by string reference
    /// </summary>
    void OnPlayerDeath() 
    {
        print("Received Dead message");
        isControlEnabled = false;
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