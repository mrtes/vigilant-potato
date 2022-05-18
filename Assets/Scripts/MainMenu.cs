using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audio;

    public void StartGame()
    {
        _audio.Play();
        SceneManager.LoadScene("Game");
    }

    public void CloseGame()
    {
        _audio.Play();
        Application.Quit();
    }
}
