using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static Car RaceCar;
    public static bool Racing = false;

    void Start()
    {
        RaceCar = GetCar();
        Racing = true;
    }

    private Car GetCar()
    {
        var selectedCar = PlayerManager.Instance.selectedCarType;

        switch (selectedCar)
        {
            case CarType.Shadow:
                return new Shadow();
            case CarType.Lotus:
                return new Lotus();
            case CarType.Eclipse:
                return new Eclipse();
            default:
                return new Shadow();
        }
    }
}
