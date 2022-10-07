using UnityEngine;

public class AntiRollBar : MonoBehaviour
{
    [SerializeField] private WheelCollider rearLeftWheel;
    [SerializeField] private WheelCollider rearRightWheel;
    [SerializeField] private float antiRoll = 5000.0f;

    private Rigidbody carRb;

    private void Start()
    {
        carRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PreventCarFlipOver();
    }

    private void PreventCarFlipOver()
    {
        WheelHit hit;

        float travelLeft = 1.0f;
        float travelRight = 1.0f;

        bool groundedLeft = rearLeftWheel.GetGroundHit(out hit);
        bool groundedRight = rearRightWheel.GetGroundHit(out hit);

        AdjustInverseTransform(groundedLeft, groundedRight);

        float antiRollForce = (travelLeft - travelRight) * antiRoll;
        AddForce(groundedLeft, groundedRight);

        void AdjustInverseTransform(bool groundedLeft, bool groundedRight)
        {
            if (groundedLeft)
            {
                travelLeft = (-rearLeftWheel.transform.InverseTransformPoint(hit.point).y - rearLeftWheel.radius)
                    / rearLeftWheel.suspensionDistance;
            }

            if (groundedRight)
            {
                travelRight = (-rearRightWheel.transform.InverseTransformPoint(hit.point).y - rearRightWheel.radius)
                    / rearRightWheel.suspensionDistance;
            }
        }

        void AddForce(bool groundedLeft, bool groundedRight)
        {
            if (groundedLeft)
            {
                carRb.AddForceAtPosition(rearLeftWheel.transform.up * -antiRollForce, rearLeftWheel.transform.position);
            }

            if (groundedRight)
            {
                carRb.AddForceAtPosition(rearRightWheel.transform.up * antiRollForce, rearRightWheel.transform.position);
            }
        }
    }
}
