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

    private void Start()
    {
        SetCenterOfMass();
        RaceManager.Racing = false;
    }

    private void FixedUpdate()
    {
        if (TimeManager.IsGameOver == false && RaceManager.Racing)
        {
            GetInput();
            UpdateWheelPoses();

            // ABSTRACTION
            Move();
            Steer();
            Brake();
        }
        else if(TimeManager.IsGameOver)
        {
            steerInput = 0f;
            moveInput = 0f;
            isBraking = true;
            ApplyBraking();
        }
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
        RaceManager.RaceCar.SetAcceleration(moveInput, maxAcceleration);
        RaceManager.RaceCar.Move(frontLeftWheelCollider, frontRightWheelCollider);
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
        if (brakeAcceleration == 0)
        {
            brakeAcceleration = 300 * brakeAcceleration * Time.deltaTime;
        }
        
        RaceManager.RaceCar.SetBrake(brakeAcceleration);
        RaceManager.RaceCar.ApplyBrake(frontLeftWheelCollider, frontRightWheelCollider, rearRightWheelCollider, rearLeftWheelCollider);
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


// INHERITANCE
public abstract class Car
{
    public abstract string[] AvailableColors { get; }
    public abstract string Color { get; set; }
    public abstract string Type { get; }
    public abstract float Acceleration { get; }
    public abstract float Brake { get; }

    public abstract float AccelerationTwoWheels { get; set; }
    public abstract float BrakeAllWheels { get; set; }

    // POLYMORPHISM
    public virtual void SetAcceleration(float moveInput, float maxAcceleration)
    {
        var twoWheels = 2;
        AccelerationTwoWheels = moveInput * (maxAcceleration / twoWheels);
    }

    public float GetAcceleration(float moveInput, float maxAcceleration)
    {
        var twoWheels = 2;
        return moveInput * (maxAcceleration / twoWheels);
    }

    public void Move(WheelCollider frontLeftWheelCollider, WheelCollider frontRightWheelCollider)
    {
        frontLeftWheelCollider.motorTorque = AccelerationTwoWheels;
        frontRightWheelCollider.motorTorque = AccelerationTwoWheels;
    }

    // POLYMORPHISM
    public virtual void SetBrake(float brakeAcceleration)
    {
        BrakeAllWheels = 300 * brakeAcceleration * Time.deltaTime;
    }

    public float GetBrakeValue(float brakeAcceleration)
    {
        return 300 * brakeAcceleration * Time.deltaTime;
    }

    public void ApplyBrake(
        WheelCollider frontLeftWheelCollider,
        WheelCollider frontRightWheelCollider,
        WheelCollider rearRightWheelCollider,
        WheelCollider rearLeftWheelCollider)
    {
        frontRightWheelCollider.brakeTorque = BrakeAllWheels;
        frontLeftWheelCollider.brakeTorque = BrakeAllWheels;
        rearRightWheelCollider.brakeTorque = BrakeAllWheels;
        rearLeftWheelCollider.brakeTorque = BrakeAllWheels;
    }
}

// INHERITANCE
public class Eclipse : Car
{
    // ENCAPSULATION
    public override string[] AvailableColors { get; } = new string[]
   {
        CarColor.Blue,
        CarColor.White,
        CarColor.Black
   };
    public override string Color { get; set; }
    public override string Type { get; } = CarType.Eclipse;
    public override float Acceleration { get; } = 0.73f;
    public override float Brake { get; } = 0.85f;
    public override float AccelerationTwoWheels { get; set; }
    public override float BrakeAllWheels { get; set; }

    // POLYMORPHISM
    public override void SetAcceleration(float moveInput, float maxAcceleration)
    {
        AccelerationTwoWheels = base.GetAcceleration(moveInput, maxAcceleration) * 70;
    }

    // POLYMORPHISM
    public override void SetBrake(float brakeAcceleration)
    {
        BrakeAllWheels = base.GetBrakeValue(brakeAcceleration) * 80;
    }
}

// INHERITANCE
public class Lotus : Car
{
    // ENCAPSULATION
    public override string[] AvailableColors { get; } = new string[]
    {
        CarColor.Blue,
        CarColor.Black,
        CarColor.Green,
        CarColor.Yellow
    };
    public override string Color { get; set; }
    public override string Type { get; } = CarType.Lotus;
    public override float Acceleration { get; } = 0.63f;
    public override float Brake { get; } = 0.7f;
    public override float AccelerationTwoWheels { get; set; }
    public override float BrakeAllWheels { get; set; }

    // POLYMORPHISM
    public override void SetAcceleration(float moveInput, float maxAcceleration)
    {
        AccelerationTwoWheels = base.GetAcceleration(moveInput, maxAcceleration) * 60;
    }

    // POLYMORPHISM
    public override void SetBrake(float brakeAcceleration)
    {
        BrakeAllWheels = base.GetBrakeValue(brakeAcceleration) * 70;
    }
}

// INHERITANCE
public class Shadow : Car
{
    // ENCAPSULATION
    public override string[] AvailableColors { get; } = new string[]
    {
        CarColor.Blue,
        CarColor.Gray,
        CarColor.Red,
        CarColor.Yellow
    };
    public override string Color { get; set; }
    public override string Type { get; } = CarType.Shadow;
    public override float Acceleration { get; } = 0.5f;
    public override float Brake { get; } = 0.63f;
    public override float AccelerationTwoWheels { get; set; }
    public override float BrakeAllWheels { get; set; }

    // POLYMORPHISM
    public override void SetAcceleration(float moveInput, float maxAcceleration)
    {
        AccelerationTwoWheels = base.GetAcceleration(moveInput, maxAcceleration) * 50;
    }

    // POLYMORPHISM
    public override void SetBrake(float brakeAcceleration)
    {
        BrakeAllWheels = base.GetBrakeValue(brakeAcceleration) * 60;
    }
}

public static class CarColor
{
    public static string Blue = "#2D52BB";
    public static string White = "#FFFFFF";
    public static string Black = "#000000";
    public static string Green = "#81DD36";
    public static string Yellow = "#FFC012";
    public static string Gray = "#8A8A7F";
    public static string Red = "#D10809";
}

public static class CarType
{
    public const string Eclipse = "Eclipse";
    public const string Lotus = "Lotus";
    public const string Shadow = "Shadow";
}
