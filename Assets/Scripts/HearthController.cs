using UnityEngine;
using System.Collections;

public class HearthController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            transform.GetComponent<MeshRenderer>().enabled = false;
            FindObjectOfType<PlayerPrefData>().HeartIncrease();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            StartCoroutine(ResetCoroutine());
    }

    public void ResetFunction()
    {
        if (gameObject.activeSelf) // If object is active
        {
            transform.GetComponent<MeshRenderer>().enabled = enabled;
            gameObject.SetActive(false);
        }

    }

    private IEnumerator ResetCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        ResetFunction();
    }



}
