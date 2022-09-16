using System.Collections;
using UnityEngine;

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
   
    void Start()
    {
        InitCars();

        isCarSelected = false;
        rotateSeconds = 0.05f;
        rotateAngle = 2.55f;

        StartCoroutine(RotatePlatform());
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
