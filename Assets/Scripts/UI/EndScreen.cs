using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    public void UpdateScore(int destruction, int shotsFired)
    {
        var score = destruction - shotsFired;
        _scoreText.text = $"Score: {score}\nDestruction: {destruction}\nShots fired: {shotsFired}";
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
