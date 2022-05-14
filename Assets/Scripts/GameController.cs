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
        _cameraTarget.SetParent(_cannon.transform, false); // TODO add tween?
    }

    public void OnCannonShot()
    {
        _cannon.CanFire = false;
        if (_cannon.CurrentProjectile != null)
        {
            _cameraTarget.SetParent(_cannon.CurrentProjectile.transform, false); // TODO add tween?
        }
    }

    public void OnProjectileImpact()
    {
        _cannon.CanFire = true;
        _cameraTarget.SetParent(_cannon.transform, false); // TODO add tween?
    }

    public void EndGame()
    {
        // show score etc
    }
}
