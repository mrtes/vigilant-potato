using UnityEngine;

public class ProjectileSelectionPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _projectilePrefabs;
    [SerializeField]
    private CannonManager _cannon;

    public void SelectProjectile(int index)
    {
        _cannon.CannonBallPrefab = _projectilePrefabs[index];
    }
}
