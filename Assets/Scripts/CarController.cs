using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const string JUMP = "Jump";

    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;

    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;

    [SerializeField] private bool isBraking;
    [SerializeField] private float maxSteerAngle = 30f;
    [SerializeField] private float maxAcceleration = 1500f;
    [SerializeField] private float brakeAcceleration = 2500f;

    [SerializeField] private Vector3 centerOfMass = new Vector3(0, -0.9f, 0);

    private float steerInput;
    private float moveInput;
    private Rigidbody carRb;

    internal enum DriveType
    {
        frontWheelDrive,
        rearWheelDrive,
        allWheelDrive
    }

    private DriveType drive;

    private void Start()
    {
        SetCenterOfMass();
    }

    private void FixedUpdate()
    {
        GetInput();
        UpdateWheelPoses();
        Move();
        Steer();
        Brake();
    }

    private void SetCenterOfMass()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = centerOfMass;
    }

    public void GetInput()
    {
        steerInput = Input.GetAxis(HORIZONTAL);
        moveInput = Input.GetAxis(VERTICAL);
        isBraking = (Input.GetAxis(JUMP) != 0) ? true : false;
    }

    private void Move()
    {
        var allWheels = 4;
        var twoWheels = 2;
        var accelerationAllWheels = moveInput * (maxAcceleration / allWheels);
        var accelerationTwoWheels = moveInput * (maxAcceleration / twoWheels);

        if (drive == DriveType.allWheelDrive)
        {
            frontLeftWheelCollider.motorTorque = accelerationAllWheels;
            frontRightWheelCollider.motorTorque = accelerationAllWheels;
            rearLeftWheelCollider.motorTorque = accelerationAllWheels;
            rearRightWheelCollider.motorTorque = accelerationAllWheels;
        }
        else if(drive == DriveType.rearWheelDrive)
        {
            rearLeftWheelCollider.motorTorque = accelerationTwoWheels;
            rearRightWheelCollider.motorTorque = accelerationTwoWheels;
        }
        else
        {
            frontLeftWheelCollider.motorTorque = accelerationTwoWheels;
            frontRightWheelCollider.motorTorque = accelerationTwoWheels;
        }
    }

    private void Steer()
    {
        var maxSteeringAngle = maxSteerAngle * steerInput;

        if (steerInput != 0)
        {
            frontLeftWheelCollider.steerAngle = maxSteeringAngle;
            frontRightWheelCollider.steerAngle = maxSteeringAngle;
        }
        else
        {
            frontLeftWheelCollider.steerAngle = 0;
            frontRightWheelCollider.steerAngle = 0;
        }
    }

    private void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyBraking();
        }
        else
        {
            NoBraking();
        }
    }

    private void ApplyBraking()
    {
        var brakeTorque = 300 * brakeAcceleration * Time.deltaTime;

        frontRightWheelCollider.brakeTorque = brakeTorque;
        frontLeftWheelCollider.brakeTorque = brakeTorque;
        rearRightWheelCollider.brakeTorque = brakeTorque;
        rearLeftWheelCollider.brakeTorque = brakeTorque;
    }

    private void NoBraking()
    {
        var noBrakeTorque = 0;

        frontRightWheelCollider.brakeTorque = noBrakeTorque;
        frontLeftWheelCollider.brakeTorque = noBrakeTorque;
        rearRightWheelCollider.brakeTorque = noBrakeTorque;
        rearLeftWheelCollider.brakeTorque = noBrakeTorque;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPose(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPose(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPose(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        Vector3 pos = transform.position;
        Quaternion quat = transform.rotation;

        collider.GetWorldPose(out pos, out quat);

        transform.position = pos;
        transform.rotation = quat;
    }
}
