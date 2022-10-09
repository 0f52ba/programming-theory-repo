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
        Car car;

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
