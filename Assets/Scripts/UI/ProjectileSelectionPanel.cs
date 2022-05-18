using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileSelectionPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _projectilePrefabs;
    [SerializeField]
    private CannonManager _cannon;
    [SerializeField]
    private Transform _selector;
    private Transform[] _buttons;

    private void Start()
    {
        _cannon.stateChanged.AddListener(OnCamStateChanged);
        _buttons = GetComponentsInChildren<Button>().Select(b => b.transform).ToArray();
    }

    public void SelectProjectile(int index)
    {
        _cannon.CannonBallPrefab = _projectilePrefabs[index];
        _cannon.State = CannonManager.CannonState.CanFire;
        _selector.DOLocalMove(_buttons[index].localPosition, .4f);
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
