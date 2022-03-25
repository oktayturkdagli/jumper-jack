using System.Collections.Generic;
using UnityEngine;

public class PoolRaw : MonoBehaviour
{
    private GameObject element; // The object to be pooled
    private Stack<GameObject> objectPool = new Stack<GameObject>(); // Pool of this object

    // Object is selected
    public PoolRaw(GameObject element)
    {
        this.element = element;
    }

    // The pool is filled with the desired number of objects
    public void FillPool(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject tempElement = Object.Instantiate(element);
            AddElementInPool(tempElement);
        }
    }

    // An object is pulled from the pool
    public GameObject PullElementFromPool()
    {
        if (objectPool.Count > 0)
        {
            GameObject tempElement = objectPool.Pop();
            tempElement.gameObject.SetActive(true);

            return tempElement;
        }

        return Object.Instantiate(element);
    }

    // An object is thrown into the pool
    public void AddElementInPool(GameObject tempElement)
    {
        tempElement.gameObject.SetActive(false);
        objectPool.Push(tempElement);
    }

}
