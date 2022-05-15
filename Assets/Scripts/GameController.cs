using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CannonManager _cannon;
    [SerializeField]
    private Transform _cameraTarget;

    protected void Start()
    {
        StartGame();
        _cannon.projectileFired.AddListener(OnCannonShot);
        _cannon.projectileImpacted.AddListener(OnProjectileImpact);
    }

    public void StartGame()
    {
        _cannon.CanFire = true;
        _cameraTarget.SetParent(_cannon.transform, true);
        _cameraTarget.DOLocalMove(Vector3.zero, .4f);
    }

    public void OnCannonShot()
    {
        _cannon.CanFire = false;
        if (_cannon.CurrentProjectile != null)
        {
            _cameraTarget.SetParent(_cannon.CurrentProjectile.transform, true);
            _cameraTarget.DOLocalMove(Vector3.zero, .4f);
        }
    }

    public void OnProjectileImpact()
    {
        _cannon.CanFire = true;
        _cameraTarget.SetParent(_cannon.transform, true);
        _cameraTarget.DOLocalMove(Vector3.zero, .4f);
    }

    public void EndGame()
    {
        // show score etc
    }
}
