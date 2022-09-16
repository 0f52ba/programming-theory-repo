using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCarUISettings : MonoBehaviour
{
    public void ShowNextCar()
    {
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
    }

    public void ShowPrevCar()
    {
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
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SelectCar()
    {
        SelectCarManager.isCarSelected = true;
    }
}
