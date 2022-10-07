using TMPro;
using UnityEngine;

public class RaceUISettings : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText;

    private Rigidbody carRb;
    private float speed;

    private void Start()
    {
        carRb = PlayerManager.Instance.selectedCarData.Car?.GetComponent<Rigidbody>();
        speedText.text = "0";
    }

    private void Update()
    {
        speed = carRb?.velocity.magnitude * 3.6f ?? 0;
        speedText.text = ((int)speed).ToString();
    }
}
