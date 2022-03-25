using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SpikesController : MonoBehaviour
{
    private bool _isFinished;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //transform.GetComponent<MeshRenderer>().enabled = false;

            if (FindObjectOfType<PlayerPrefData>().GetHeart() == 0)
            {
                transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                DOTween.Kill(collision.gameObject.transform, false);
                _isFinished = true;
                StartCoroutine(ResetCoroutine());
            }
            else
            {
                transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                FindObjectOfType<PlayerPrefData>().HeartDecrease();
                _isFinished = false;
                StartCoroutine(ResetCoroutine());
            }
        }
    }

    public void ResetFunction()
    {
        if (gameObject.activeSelf) // If object is active
        {
            transform.GetComponent<MeshRenderer>().enabled = enabled;
            gameObject.SetActive(false);
            if (_isFinished)
                FindObjectOfType<UIManager>().Finish(false);
        }

    }

    private IEnumerator ResetCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        ResetFunction();
    }


}
