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

    private void Start()
    {
        IsFirstLap = true;
    }

    private void Update()
    {
        LapTimeMiliSeconds++;

        if (LapChange)
        {
            ResetAfterLapChange();
        }

        ValidateTimer();
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
    }
}
