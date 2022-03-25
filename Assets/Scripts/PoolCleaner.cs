using UnityEngine;

public class PoolCleaner : MonoBehaviour
{

    [SerializeField]
    private Transform _target; //Object to be followed0 (Generally Player)

    [SerializeField]
    private float _smoothness = 1000; //Camera smoothness speed

    [SerializeField]
    private Vector3 _offset = new Vector3(0, 0, 0); //Small adjustments

    void LateUpdate()
    {
        Follow();
    }

    void Follow()
    {
        if (_target == null)
            return;

        //Follow Position
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _smoothness * Time.deltaTime); // Slow effect
    }

}
