using UnityEngine;

public class BlockController : MonoBehaviour
{

    private Animator _blockAnimator, _springAnimator;
    private PoolController _objectPool;
    private bool _isCenter = false;

    private void Start()
    {
        _objectPool = FindObjectOfType<PoolController>();
        _blockAnimator = transform.GetChild(0).GetComponent<Animator>();
        _springAnimator = transform.GetChild(1).GetComponent<Animator>();
        SetReward(); //Sets rewards on blocks
    }

    private void ThrowInPool(GameObject block)
    {
        Reset();
        SetReward();
        _objectPool.ThrowInPool(0, block);
    }

    private void Reset()
    {
        transform.GetChild(0).transform.rotation = Quaternion.identity;
        transform.GetChild(1).transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
    }

    private void SetReward()
    {
        int _randomNumber = Random.Range(1, 101);
        if (_randomNumber < 15)
            transform.GetChild(3).GetChild(0).gameObject.SetActive(true); // 15% chance of diamond
        else if (_randomNumber < 30)
            transform.GetChild(3).GetChild(1).gameObject.SetActive(true); // 15% chance of Spikes
        else if (_randomNumber < 35)
            transform.GetChild(3).GetChild(2).gameObject.SetActive(true); // 5% chance of Heart
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            if (_isCenter) //If touched on center, Play effect
                transform.GetChild(2).GetComponent<ParticleSystem>().Play();

            _isCenter = false;
            _blockAnimator.SetTrigger("Trigger"); //Applies the shaking effect
            _springAnimator.SetTrigger("Trigger"); //Applies the spring effect
        }

        if (collision.gameObject.CompareTag("Cleaner"))
        {
            Transform rewardChilds = transform.GetChild(3);
            rewardChilds.GetChild(0).GetComponent<DiamondController>().ResetFunction();
            rewardChilds.GetChild(1).GetComponent<SpikesController>().ResetFunction();
            rewardChilds.GetChild(2).GetComponent<HearthController>().ResetFunction();
            ThrowInPool(gameObject);
        }
            
    }

    private void OnTriggerEnter(Collider other) // when this collider triggered our character is center.
    {
        _isCenter = true;
    }

    

}
