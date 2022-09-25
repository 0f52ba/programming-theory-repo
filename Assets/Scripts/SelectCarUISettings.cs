using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void SelectFirstColor()
    {
        var currentCar = SelectCarManager.currentCar;
        currentCar.SetActive(false);

        switch (SelectCarManager.currentCar.tag)
        {
            case "Eclipse":
                currentCar = GameObject.Find("/Cars/Eclipse/EclipseBlue");
                break;
            case "Lotus":
                currentCar = GameObject.Find("/Cars/Lotus/LotusBlue");
                break;
            case "Shadow":
                currentCar = GameObject.Find("/Cars/Shadow/ShadowBlue");
                break;
            default:
                Debug.Log("unknown car");
                break;
        }

        SelectCarManager.currentCar = currentCar;
        currentCar.SetActive(true);
    }

    public void SelectSecondColor()
    {
        var currentCar = SelectCarManager.currentCar;
        currentCar.SetActive(false);

        switch (SelectCarManager.currentCar.tag)
        {
            case "Eclipse":
                currentCar = GameObject.Find("/Cars/Eclipse/EclipseWhite");
                break;
            case "Lotus":
                currentCar = GameObject.Find("/Cars/Lotus/LotusBlack");
                break;
            case "Shadow":
                currentCar = GameObject.Find("/Cars/Shadow/ShadowGray");
                break;
            default:
                Debug.Log("unknown car");
                break;
        }

        SelectCarManager.currentCar = currentCar;
        currentCar.SetActive(true);
    }

    public void SelectThirdColor()
    {
        var currentCar = SelectCarManager.currentCar;
        currentCar.SetActive(false);

        switch (SelectCarManager.currentCar.tag)
        {
            case "Eclipse":
                currentCar = GameObject.Find("/Cars/Eclipse/EclipseBlack");
                break;
            case "Lotus":
                currentCar = GameObject.Find("/Cars/Lotus/LotusGreen");
                break;
            case "Shadow":
                currentCar = GameObject.Find("/Cars/Shadow/ShadowRed");
                break;
            default:
                Debug.Log("unknown car");
                break;
        }

        SelectCarManager.currentCar = currentCar;
        currentCar.SetActive(true);
    }

    public void SelectFourthColor()
    {
        var currentCar = SelectCarManager.currentCar;
        currentCar.SetActive(false);

        switch (SelectCarManager.currentCar.tag)
        {
            case "Eclipse":
                break;
            case "Lotus":
                currentCar = GameObject.Find("/Cars/Lotus/LotusYellow");
                break;
            case "Shadow":
                currentCar = GameObject.Find("/Cars/Shadow/ShadowYellow");
                break;
            default:
                Debug.Log("unknown car");
                break;
        }

        SelectCarManager.currentCar = currentCar;
        currentCar.SetActive(true);
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
        SelectCarManager.isCarSelected = true;
        PlayClickSound();
        PlayerManager.Instance.SaveSelectedCar();
        SceneManager.LoadScene(2);
    }

    private void PlayClickSound()
    {
        audioSource.PlayOneShot(clickButtonSound);
    }
}
