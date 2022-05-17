using DG.Tweening;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CannonManager _cannon;
    [SerializeField]
    private Transform _cameraTarget;

    [SerializeField]
    private ScorePanel _scorePanel;

    private int _currentShots = 0;
    private int _currentDestruction = 0;

    protected void Start()
    {
        StartGame();
        _cannon.projectileFired.AddListener(OnCannonShot);
        _cannon.projectileImpacted.AddListener(OnProjectileImpact);
    }

    protected void UpdateUI()
    {
        if (_scorePanel != null)
        {
            _scorePanel.UpdateShots(_currentShots);
        }
        if (_scorePanel != null)
        {
            _scorePanel.UpdateDestruction(_currentDestruction);
        }
    }

    public void StartGame()
    {
        _cannon.CanFire = true;
        _cameraTarget.SetParent(_cannon.transform, true);
        _cameraTarget.DOLocalMove(Vector3.zero, .4f);
        UpdateUI();
    }

    public void OnCannonShot()
    {
        _cannon.CanFire = false;
        if (_cannon.CurrentProjectile != null)
        {
            _cameraTarget.SetParent(_cannon.CurrentProjectile.transform, true);
            _cameraTarget.DOLocalMove(Vector3.zero, .4f);
        }

        _currentShots++;
        UpdateUI();
    }

    public void OnProjectileImpact()
    {
        _cannon.CanFire = true;
        _cameraTarget.SetParent(_cannon.transform, true);
        _cameraTarget.DOLocalMove(Vector3.zero, .4f);
    }

    public void OnDestructionCaused() // TODO attack to destruction of obstacles
    {
        _currentDestruction++;
        UpdateUI();
    }

    public void EndGame()
    {
        // show score etc
    }
}
