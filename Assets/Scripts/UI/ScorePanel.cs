using TMPro;
using UnityEngine;

public class ScorePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _shotsText;
    [SerializeField]
    private TextMeshProUGUI _destructionText;

    public void UpdateShots(int shots)
    {
        _shotsText.text = "Shots: " + shots;
    }

    public void UpdateDestruction(int destruction)
    {
        _destructionText.text = "Destruction: " + destruction;
    }
}
