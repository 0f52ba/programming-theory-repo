using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public PlayerData selectedCarData;
    public string selectedCarType = CarType.Eclipse;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    [System.Serializable]
    public class PlayerData
    {
        public GameObject Car;
    }
}
