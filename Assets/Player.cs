using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] float speed = 20f;

    [Tooltip("In m")] [SerializeField] float xRange = 11.5f;
    [Tooltip("In m")] [SerializeField] float yRange = 7f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(GetXPosition(), GetYPosition(), transform.localPosition.z);
    }

    private float GetXPosition()
    {
        var xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        var xOffset = xThrow * speed * Time.deltaTime;
        var xRawPos = transform.localPosition.x + xOffset;
        var xClampPos = Mathf.Clamp(xRawPos, -xRange, xRange);
        return xClampPos;
    }

    private float GetYPosition()
    {
        var yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        var yOffset = yThrow * speed * Time.deltaTime;
        var yRawPos = transform.localPosition.y + yOffset;
        var yClampPos = Mathf.Clamp(yRawPos, -yRange, yRange);
        return yClampPos;
    }

}
