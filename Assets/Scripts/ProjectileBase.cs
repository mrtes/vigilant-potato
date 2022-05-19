using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileBase : MonoBehaviour
{
    public UnityEvent impacted = new UnityEvent();
    [SerializeField]
    protected AudioSource _bounceAudio, _explosionAudio;

    [SerializeField]
    protected GameObject explosionPrefab;

    protected virtual void Impact()
    {
        StartCoroutine(WaitBeforeDestroy());
    }

    private IEnumerator WaitBeforeDestroy()
    {
        _explosionAudio.Play();
        var explosion = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponent<SphereCollider>().enabled = false;
        impacted.Invoke();

        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
        explosion.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _bounceAudio.Play();
    }
}
