using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUISettings : MonoBehaviour
{
    public AudioClip clickButtonSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartRace()
    {
        PlayClickSound();
        SceneManager.LoadScene(1);
    }

    public void HighScore()
    {
        PlayClickSound();
    }

    public void Quit()
    {
        PlayClickSound();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void PlayClickSound()
    {
        audioSource.PlayOneShot(clickButtonSound);
    }
}
