using UnityEngine;

public class ProjectileSelectionPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _projectilePrefabs;
    [SerializeField]
    private CannonManager _cannon;

    private void Start()
    {
        _cannon.stateChanged.AddListener(OnCamStateChanged);
    }

    public void SelectProjectile(int index)
    {
        _cannon.CannonBallPrefab = _projectilePrefabs[index];
        _cannon.State = CannonManager.CannonState.CanFire;
    }

    private void OnCamStateChanged()
    {
        if (_cannon.State == CannonManager.CannonState.Loading
            || _cannon.State == CannonManager.CannonState.CanFire)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
