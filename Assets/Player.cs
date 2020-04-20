using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] float speed = 15f;

    [Tooltip("In m")] [SerializeField] float xRange = 12f;
    [Tooltip("In m")] [SerializeField] float yRange = 8.5f;

    [SerializeField] float positionPitchFactor = -1.2f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 1.2f;
    [SerializeField] float controlRollFactor = -30f;

    float xThrow;
    float yThrow;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
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
        var xOffset = xThrow * speed * Time.deltaTime;
        var xRawPos = transform.localPosition.x + xOffset;
        var xClampPos = Mathf.Clamp(xRawPos, -xRange, xRange);
        return xClampPos;
    }

    private float GetYPosition()
    {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        var yOffset = yThrow * speed * Time.deltaTime;
        var yRawPos = transform.localPosition.y + yOffset;
        var yClampPos = Mathf.Clamp(yRawPos, -yRange, yRange);
        return yClampPos;
    }
}