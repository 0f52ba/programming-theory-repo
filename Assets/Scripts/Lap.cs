using UnityEngine;

public class Lap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag(CarType.Eclipse) ||
           other.gameObject.CompareTag(CarType.Lotus) ||
           other.gameObject.CompareTag(CarType.Shadow)) 
           && TimeManager.IsFirstLap)
        {
            ValidateLapNumber();
            TimeManager.IsFirstLap = false;
        }
        else if ((other.gameObject.CompareTag(CarType.Eclipse) ||
            other.gameObject.CompareTag(CarType.Lotus) ||
            other.gameObject.CompareTag(CarType.Shadow)) 
            && TimeManager.IsFirstLap == false)
        {
            ValidateLapNumber();
            TimeManager.LapChange = true;
        }
    }

    private void ValidateLapNumber()
    {
        if(TimeManager.LapNumber > TimeManager.TotalLapNumber)
        {
            TimeManager.LapNumber = TimeManager.TotalLapNumber;
        }
        else
        {
            TimeManager.LapNumber++;
        }
    }
}
