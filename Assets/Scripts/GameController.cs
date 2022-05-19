using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CannonManager _cannon;
    [SerializeField]
    private Transform _cameraTarget;
    [SerializeField]
    private Target _target;

    [SerializeField]
    private ScorePanel _scorePanel;
    [SerializeField]
    private EndScreen _endScreen;

    private int _currentShots = 0;
    private int _currentDestruction = 0;

    protected void Start()
    {
        StartGame();
        _cannon.projectileFired.AddListener(OnCannonShot);
        _cannon.projectileImpacted.AddListener(OnProjectileImpact);
        _target.targetHit.AddListener(EndGame);
    }

    protected void UpdateUI()
    {
        if (_scorePanel != null)
        {
            _scorePanel.UpdateShots(_currentShots);
            _scorePanel.UpdateDestruction(_currentDestruction);
        }
        if (_endScreen != null)
        {
            _endScreen.UpdateScore(_currentDestruction, _currentShots);
        }
    }

    public void StartGame()
    {
        _cannon.State = CannonManager.CannonState.CanFire;
        _cameraTarget.SetParent(_cannon.transform, true);
        _cameraTarget.DOLocalMove(Vector3.zero, .4f);
        UpdateUI();
    }

    public void OnPointerUp(BaseEventData eventData)
    {
        Debug.Log("onpointerup");
        if (_cannon.State == CannonManager.CannonState.Firing)
        {
            var pointerEventData = (PointerEventData)eventData;
            if (pointerEventData.button == PointerEventData.InputButton.Right)
            {
                OnProjectileImpact();
            }
        }
    }

    public void OnCannonShot()
    {
        _cannon.State = CannonManager.CannonState.Firing;
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
        _cannon.State = CannonManager.CannonState.Loading;
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
        _cannon.State = CannonManager.CannonState.Idle;
        UpdateUI();
        _endScreen.gameObject.SetActive(true);
    }
}
