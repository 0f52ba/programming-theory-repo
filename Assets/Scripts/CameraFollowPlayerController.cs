using UnityEngine;

public class CameraFollowPlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;

    private Vector3 startCarPosition = new Vector3(-2.9f, 0.47f, 17.3f);
    private Quaternion startCarRotation = Quaternion.Euler(0, -90, 0);
    private Vector3 cameraOffset = new Vector3(0, 1.8f, -6.22f);

    private void Start()
    {
        LoadCar();
        SetCameraOffset();
    }

    void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }

    private void LoadCar()
    {
        target = PlayerManager.Instance.selectedCarData.SelectedCar.transform;
        target.position = startCarPosition;
        target.rotation = startCarRotation;
    }

    private void SetCameraOffset()
    {
        offset = cameraOffset;
    }

    private void HandleTranslation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        var targetPosition = target.position + target.forward * offset.z + target.right * offset.x + target.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }
}
