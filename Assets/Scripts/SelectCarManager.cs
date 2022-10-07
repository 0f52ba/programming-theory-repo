using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectCarManager : MonoBehaviour
{
    private static readonly string accelerationSliderPath = "/Canvas/CarSettings/AccelerationSlider";
    private static readonly string brakeSliderPath = "/Canvas/CarSettings/BrakeSlider";

    public static GameObject currentCar;
    public static int currentCarId;
    public static bool isCarSelected;

    public static GameObject[] availableCars;
    public static GameObject[] availableShadowCars;
    public static GameObject[] availableLotusCars;
    public static GameObject[] availableEclipseCars;

    [SerializeField] private GameObject platform;
    [SerializeField] private float rotateSeconds;
    [SerializeField] private float rotateAngle;
    [SerializeField] private GameObject[] availableCarsUser;

    [SerializeField] private GameObject[] shadowCars;
    [SerializeField] private GameObject[] lotusCars;
    [SerializeField] private GameObject[] eclipseCars;

    [SerializeField] private GameObject color1Field;
    [SerializeField] private GameObject color2Field;
    [SerializeField] private GameObject color3Field;
    [SerializeField] private GameObject color4Field;

    private Slider accelerationSlider;
    private Slider brakeSlider;

    void Start()
    {
        InitCars();
        LoadSceneData();
        LoadCarData();

        StartCoroutine(RotatePlatform());
    }

    private void Update()
    {
        if (isCarSelected)
        {
            StopCoroutine(RotatePlatform());
        }
    }

    public void LoadCarData()
    {
        ICar car;

        switch (currentCar.tag)
        {
            case CarType.Eclipse:
                car = new Eclipse();

                color1Field.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[0]);
                color2Field.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[1]);
                color3Field.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[2]);
                color4Field.SetActive(false);

                accelerationSlider.value = car.Acceleration;
                brakeSlider.value = car.Brake;

                break;
            case CarType.Lotus:
                car = new Lotus();

                color1Field.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[0]);
                color2Field.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[1]);
                color3Field.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[2]);
                color4Field.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[3]);
                color4Field.SetActive(true);

                accelerationSlider.value = car.Acceleration;
                brakeSlider.value = car.Brake;

                break;
            case CarType.Shadow:
                car = new Shadow();

                color1Field.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[0]);
                color2Field.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[1]);
                color3Field.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[2]);
                color4Field.GetComponent<Image>().color = ConvertHexToColor(car.AvailableColors[3]);
                color4Field.SetActive(true);

                accelerationSlider.value = car.Acceleration;
                brakeSlider.value = car.Brake;

                break;
            default:
                Debug.Log("unknown car");
                break;
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

        availableShadowCars = shadowCars;
        availableLotusCars = lotusCars;
        availableEclipseCars = eclipseCars;
    }

    private void LoadSceneData()
    {
        accelerationSlider = GameObject.Find(accelerationSliderPath).GetComponent<Slider>();
        brakeSlider = GameObject.Find(brakeSliderPath).GetComponent<Slider>();

        isCarSelected = false;
        rotateSeconds = 0.05f;
        rotateAngle = 2.55f;
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
    string Type { get; }
    float Acceleration { get; }
    float Brake { get; }
}

// 1202
public class Eclipse : ICar
{
    string[] ICar.AvailableColors { get; } = new string[]
    {
        CarColor.Blue,
        CarColor.White,
        CarColor.Black
    };
    public string Color { get; set; }
    string ICar.Type { get; } = CarType.Eclipse;
    public float Acceleration { get; } = 0.73f;
    public float Brake { get; } = 0.85f;
}

// 1203
public class Lotus : ICar
{
    string[] ICar.AvailableColors { get; } = new string[]
    {
        CarColor.Blue,
        CarColor.Black,
        CarColor.Green,
        CarColor.Yellow
    };
    public string Color { get; set; }
    string ICar.Type { get; } = CarType.Lotus;
    public float Acceleration { get; } = 0.63f;
    public float Brake { get; } = 0.7f;
}

// ARCADE
public class Shadow : ICar
{
    string[] ICar.AvailableColors { get; } = new string[]
    {
        CarColor.Blue,
        CarColor.Gray,
        CarColor.Red,
        CarColor.Yellow
    };
    public string Color { get; set; }
    string ICar.Type { get; } = CarType.Shadow;
    public float Acceleration { get; } = 0.5f;
    public float Brake { get; } = 0.63f;
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