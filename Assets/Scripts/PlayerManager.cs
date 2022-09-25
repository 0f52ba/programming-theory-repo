using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public GameObject selectedCar;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }

        DontDestroyOnLoad(gameObject);

        InitData();
    }

    private void InitData()
    {
        Instance = this;
    }

    public void SaveSelectedCar()
    {
        selectedCar = SelectCarManager.currentCar;
    }

    [System.Serializable]
    public class PlayerData
    {
        public GameObject SelectedCar;
    }
}
