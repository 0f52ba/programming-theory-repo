using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectCarManager : MonoBehaviour
{
    public static GameObject currentCar;
    public static int currentCarId;
    public static bool isCarSelected;
    public static GameObject[] availableCars;

    [SerializeField] private GameObject platform;
    [SerializeField] private float rotateSeconds;
    [SerializeField] private float rotateAngle;
    [SerializeField] private GameObject[] availableCarsUser;

    [SerializeField] private GameObject color1;
    [SerializeField] private GameObject color2;
    [SerializeField] private GameObject color3;
    [SerializeField] private GameObject color4;

    void Start()
    {
        InitCars();
        LoadCarData();

        isCarSelected = false;
        rotateSeconds = 0.05f;
        rotateAngle = 2.55f;

        StartCoroutine(RotatePlatform());
    }

    private void Update()
    {
        if (isCarSelected)
        {
            StopCoroutine(RotatePlatform());
        }
    }

    private void InitCars()
    {
        availableCars = availableCarsUser;

        currentCar = availableCars[0];
        currentCarId = 0;

        var availableCarsCount = availableCars.Length;
        availableCars[0].SetActive(true);

        for (int i = 1; i < availableCarsCount; i++)
        {
            availableCars[i].SetActive(false);
        }
    }

    public void LoadCarData()
    {
        ICar car;

        switch (currentCar.tag)
        {
            case "Eclipse":
                car = new Eclipse();

                color1.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[0]);
                color2.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[1]);
                color3.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[2]);
                color4.SetActive(false);
                break;
            case "Lotus":
                car = new Lotus();

                color1.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[0]);
                color2.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[1]);
                color3.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[2]);
                color4.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[3]);
                color4.SetActive(true);
                break;
            case "Shadow":
                car = new Shadow();

                color1.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[0]);
                color2.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[1]);
                color3.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[2]);
                color4.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[3]);
                color4.SetActive(true);
                break;
            default:
                Debug.Log("unknown car");
                break;
        }
    }
    private static Color ConvertHexToColor(string hex)
    {
        Color convertedColor;

        ColorUtility.TryParseHtmlString(hex, out Color color);
        convertedColor = color;
        return convertedColor;
    }

    private IEnumerator RotatePlatform()
    {
        while (!isCarSelected)
        {
            platform.transform.Rotate(-Vector3.down, rotateAngle);
            currentCar.transform.Rotate(-Vector3.down, rotateAngle);
            yield return new WaitForSeconds(rotateSeconds);
        }
    }
}

public interface ICar
{
    string[] AvailableColors { get; }
    string Color { get; set; }
    CarType Type { get; }
    float TopSpeed { get; }
    float Acceleration { get; }
}

// 1202
public class Eclipse : ICar
{
    string[] ICar.AvailableColors { get; } = new string[] { CarColor.Blue, CarColor.White, CarColor.Black };
    public string Color { get; set; }
    CarType ICar.Type { get; } = CarType.Eclipse;
    public float TopSpeed { get; }
    public float Acceleration { get; }
}

// 1203
public class Lotus : ICar
{
    string[] ICar.AvailableColors { get; } = new string[] { CarColor.Blue, CarColor.Black, CarColor.Green, CarColor.Yellow };
    public string Color { get; set; }
    CarType ICar.Type { get; } = CarType.Lotus;
    public float TopSpeed { get; }
    public float Acceleration { get; }
}

// ARCADE
public class Shadow : ICar
{
    string[] ICar.AvailableColors { get; } = new string[] { CarColor.Blue, CarColor.Gray, CarColor.Red, CarColor.Yellow };
    public string Color { get; set; }
    CarType ICar.Type { get; } = CarType.Shadow;
    public float TopSpeed { get; }
    public float Acceleration { get; }
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

public enum CarType
{
    Eclipse = 0,
    Lotus = 1,
    Shadow = 2
}