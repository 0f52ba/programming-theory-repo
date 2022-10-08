using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static float LapNumber;
    public static bool IsFirstLap;
    public static int TotalLapNumber = 3;
    public static bool LapChange = false;

    public static float LapTimeMinutes;
    public static float LapTimeSeconds;
    public static float LapTimeMiliSeconds;

    public static float TotalTimeMinutes;
    public static float TotalTimeSeconds;
    public static float TotalTimeMiliSeconds;

    public static bool IsGameOver = false;

    private void Start()
    {
        IsFirstLap = true;
    }

    private void FixedUpdate()
    {
        if (IsGameOver == false)
        {
            LapTimeMiliSeconds++;

            if (LapChange)
            {
                UpdateTotalTime();
                ResetAfterLapChange();
            }

            ValidateTimer();
            ValidateLapNumber();
        }
    }

    private void UpdateTotalTime()
    {
        TotalTimeMinutes += LapTimeMinutes;
        TotalTimeSeconds += LapTimeSeconds;
        TotalTimeMiliSeconds += LapTimeMiliSeconds;
    }

    private void ResetAfterLapChange()
    {
        LapChange = false;
        LapTimeMinutes = 0f;
        LapTimeSeconds = 0f;
        LapTimeMiliSeconds = 0f;
    }

    private void ValidateTimer()
    {
        // lap 
        if (LapTimeMiliSeconds > 59)
        {
            LapTimeMiliSeconds = 0f;
            LapTimeSeconds++;
        }

        if (LapTimeSeconds > 59)
        {
            LapTimeSeconds = 0f;
            LapTimeMinutes++;
        }

        // total
        if (TotalTimeMiliSeconds > 59)
        {
            TotalTimeMiliSeconds = 0f;
            TotalTimeSeconds++;
        }

        if (TotalTimeSeconds > 59)
        {
            TotalTimeSeconds = 0f;
            TotalTimeMinutes++;
        }
    }

    private void ValidateLapNumber()
    {
        if (LapNumber > TotalLapNumber)
        {
            LapNumber = TotalLapNumber;
            IsGameOver = true;
        }
    }
}
