using UnityEngine;

public class PoolController : MonoBehaviour
{
    
    public GameObject[] elements, parents; // Object to pool and its parent for location information
    public int[] Quantities;
    private PoolRaw[] objectPools; // A pool is created for each object

    // For placement of elements
    Vector3[][] elementsNodes;
    int[] elementsNodeCounter;
    
    void Start()
    {
        objectPools = new PoolRaw[elements.Length];
        elementsNodeCounter = new int[elements.Length];

        // Get the placement information of Elements
        GetElementNodes();
        
        // Create the pool and fill it
        GetPools();

        FillPools();

        //Pull objects from the pool and perform initial placement
        MakeFirstPlacement();

    }

    // Send the object back to the pool
    public void PoolAddElement(int poolNumber, GameObject element)
    {
        objectPools[poolNumber].AddElementInPool(element);
    }

    // Pull object from the pool
    public void PoolPullElement(int poolNumber)
    {
        if (elementsNodeCounter[poolNumber] < elementsNodes[poolNumber].Length)
        {
            GameObject tempElement = objectPools[poolNumber].PullElementFromPool();
            tempElement.transform.position = elementsNodes[poolNumber][elementsNodeCounter[poolNumber]];
            tempElement.transform.rotation = Quaternion.identity;
            tempElement.transform.parent = parents[poolNumber].transform;
            elementsNodeCounter[poolNumber] = elementsNodeCounter[poolNumber] + 1;
        }  
    }

    public void ThrowInPool(int poolNumber, GameObject element)
    {
        PoolAddElement(poolNumber, element);
        PoolPullElement(poolNumber);
    }

    void GetPools()
    {
        for (int i = 0; i < objectPools.Length; i++)
        {
            objectPools[i] = new PoolRaw(elements[i]);
        } 
    }

    void FillPools()
    {
        for (int i = 0; i < objectPools.Length; i++)
        {
            objectPools[i].FillPool(Quantities[i]);
        } 
    }

    void MakeFirstPlacement()
    {
        for (int i = 0; i < objectPools.Length; i++)
        {
            for (int j = 0; j < Quantities[i]; j++)
            {
                // Pull an object from the pool and change its position
                PoolPullElement(i);
            }
        }
    }

    void GetElementNodes()
    {
        Vector3[][] tempElements = new Vector3[elements.Length][];
        for (int i = 0; i < tempElements.Length; i++)
        {
            Vector3[] elementNodes = new Vector3[parents[i].transform.childCount];
            for (int j = 0; j < elementNodes.Length; j++)
                elementNodes[j] = parents[i].transform.GetChild(j).position;
            tempElements[i] = elementNodes;
            elementsNodeCounter[i] = 0;
        }
        elementsNodes = tempElements;
    }

}
