using TMPro;
using UnityEngine;

public class RaceUISettings : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI lapNumberText;
    [SerializeField] private TextMeshProUGUI lapTimeMinutesText;
    [SerializeField] private TextMeshProUGUI lapTimeSecondsText;
    [SerializeField] private TextMeshProUGUI totalTimeMinutesText;
    [SerializeField] private TextMeshProUGUI totalTimeSecondsText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private int totalLaps;

    private Rigidbody carRb;
    private float speed;

    private void Start()
    {
        carRb = PlayerManager.Instance.selectedCarData.Car?.GetComponent<Rigidbody>();
       
        totalLaps = TimeManager.TotalLapNumber;

        lapNumberText.text = "0 / " + totalLaps.ToString();
        lapTimeSecondsText.text = "00";
        lapTimeMinutesText.text = "00";
        totalTimeMinutesText.text = "00";
        totalTimeSecondsText.text = "00";
        speedText.text = "0";
        gameOverText.gameObject.SetActive(false);

    }

    private void Update()
    {
       if(TimeManager.IsGameOver == false)
        {
            SetSpeed();
            SetLapTime();
            SetLapNumber();
            SetTotalTime();
        }
        else
        {
            GameOver();
        }
    }

    private void SetSpeed()
    {
        var convertToKmH = 3.6f;
        speed = carRb?.velocity.magnitude * convertToKmH ?? 0;
        speedText.text = ((int)speed).ToString();
    }

    private void SetLapTime()
    {
        if (TimeManager.LapTimeSeconds < 10)
        {
            lapTimeSecondsText.text = "0" + TimeManager.LapTimeSeconds.ToString();
        }
        else
        {
            lapTimeSecondsText.text = TimeManager.LapTimeSeconds.ToString();
        }

        if (TimeManager.LapTimeMinutes < 10)
        {
            lapTimeMinutesText.text = "0" + TimeManager.LapTimeMinutes.ToString();
        }
        else
        {
            lapTimeMinutesText.text = TimeManager.LapTimeMinutes.ToString();
        }
    }

    private void SetTotalTime()
    {
        if (TimeManager.TotalTimeSeconds < 10)
        {
            totalTimeSecondsText.text = "0" + TimeManager.TotalTimeSeconds.ToString();
        }
        else
        {
            totalTimeSecondsText.text = TimeManager.TotalTimeSeconds.ToString();
        }

        if (TimeManager.TotalTimeMinutes < 10)
        {
            totalTimeMinutesText.text = "0" + TimeManager.TotalTimeMinutes.ToString();
        }
        else
        {
            totalTimeMinutesText.text = TimeManager.TotalTimeMinutes.ToString();
        }
    }

    private void SetLapNumber()
    {
        lapNumberText.text = TimeManager.LapNumber.ToString() + " / " + TimeManager.TotalLapNumber.ToString();
    }

    private void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        speedText.text = "0";
        lapNumberText.text = totalLaps.ToString() + " / " + totalLaps.ToString();
    }
}
