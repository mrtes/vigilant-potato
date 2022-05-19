using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private AudioSource _audio;

    private void OnEnable()
    {
        _audio.Play();
    }

    public void UpdateScore(int destruction, int shotsFired)
    {
        _scoreText.text = $"Shots fired: {shotsFired}";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
