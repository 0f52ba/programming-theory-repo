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
        // TODO - load scene
    }

    public void Quit()
    {
        PlayClickSound();
        // TODO - save data

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
