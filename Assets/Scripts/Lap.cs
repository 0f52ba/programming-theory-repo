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
            TimeManager.LapNumber++;
            TimeManager.IsFirstLap = false;
        }
        else if ((other.gameObject.CompareTag(CarType.Eclipse) ||
            other.gameObject.CompareTag(CarType.Lotus) ||
            other.gameObject.CompareTag(CarType.Shadow)) 
            && TimeManager.IsFirstLap == false)
        {
            TimeManager.LapNumber++;
            TimeManager.LapChange = true;
        }
    }
}
