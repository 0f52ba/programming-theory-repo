using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerManager;

public class SelectCarUISettings : MonoBehaviour
{
    public AudioClip clickButtonSound;
    public SelectCarManager selectCarManager;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        selectCarManager = GameObject.Find("SelectCarManager").GetComponent<SelectCarManager>();
    }

    private GameObject LoadSelectedCar(int colorId, string tag)
    {
        switch (tag)
        {
            case CarType.Eclipse:
                return SelectCarManager.availableEclipseCars[colorId];
            case CarType.Lotus:
                return SelectCarManager.availableLotusCars[colorId];
            case CarType.Shadow:
                return SelectCarManager.availableShadowCars[colorId];
            default:
                Debug.Log("unknown car");
                return null;
        }
    }

    public void SelectFirstColor()
    {
        SelectColor(0);
    }

    public void SelectSecondColor()
    {
        SelectColor(1);
    }

    public void SelectThirdColor()
    {
        SelectColor(2);
    }

    public void SelectFourthColor()
    {
        SelectColor(3);
    }

    public void ShowNextCar()
    {
        PlayClickSound();

        var currentCar = SelectCarManager.currentCar;
        var currentCarId = SelectCarManager.currentCarId;
        var availableCars = SelectCarManager.availableCars;

        currentCar.SetActive(false);

        currentCarId++;

        if (currentCarId >= availableCars.Length)
        {
            currentCarId = 0;
        }

        currentCar = availableCars[currentCarId];
        SelectCarManager.currentCar = currentCar;
        SelectCarManager.currentCarId = currentCarId;
        currentCar.SetActive(true);

        selectCarManager.LoadCarData();
    }

    public void ShowPrevCar()
    {
        PlayClickSound();

        var currentCar = SelectCarManager.currentCar;
        var currentCarId = SelectCarManager.currentCarId;
        var availableCars = SelectCarManager.availableCars;

        currentCar.SetActive(false);

        currentCarId--;

        if (currentCarId < 0)
        {
            currentCarId = availableCars.Length - 1;
        }

        currentCar = availableCars[currentCarId];
        SelectCarManager.currentCar = currentCar;
        SelectCarManager.currentCarId = currentCarId;
        currentCar.SetActive(true);

        selectCarManager.LoadCarData();
    }

    public void BackToMenu()
    {
        PlayClickSound();
        SceneManager.LoadScene(0);
    }

    public void StartRace()
    {
        PlayClickSound();

        SelectCarManager.isCarSelected = true;
        string carType = SelectCarManager.currentCar.tag.ToString();

        PlayerManager.Instance.selectedCarType = carType;
        PlayerManager.Instance.selectedCarData = new PlayerData { Car = SelectCarManager.currentCar };

        DontDestroyOnLoad(SelectCarManager.currentCar);

        SceneManager.LoadScene(2);
    }

    private void SelectColor(int colorId)
    {
        PlayClickSound();

        var currentCar = SelectCarManager.currentCar;
        currentCar.SetActive(false);

        currentCar = LoadSelectedCar(colorId, SelectCarManager.currentCar.tag);

        if (currentCar != null)
        {
            SelectCarManager.currentCar = currentCar;
            currentCar.SetActive(true);
        }
    }

    private void PlayClickSound()
    {
        audioSource.PlayOneShot(clickButtonSound);
    }
}
