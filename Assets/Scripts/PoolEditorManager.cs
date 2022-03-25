using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PoolEditorManager : MonoBehaviour
{
    public Transform parent;
    public int quantity = 12;
    [Range(-2.5f, 2.5f)] public float borderXMin = -2.5f, borderXMax = 2.5f;
    public float zPostion = 5f;

    public void CreateObjects()
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newObj.transform.position = CreateRandomPosition();
            newObj.transform.parent = parent;
            newObj.transform.localScale = new Vector3(1, 0.3f, 1);
        }
    }

    public void DeleteObjects()
    {
        int p = parent.childCount;
        for (int i = 0; i < p; i++)
        {
            DestroyImmediate(parent.GetChild(0).gameObject);
        }
        zPostion = 5f;
    }

    public Vector3 CreateRandomPosition()
    {
        zPostion += 15f;
        Vector3 randomPosition = new Vector3(Random.Range(borderXMin, borderXMax + 1), 0, zPostion);
        return randomPosition;
    }

    public void DeleteMesh()
    {
        int p = parent.childCount;
        for (int i = 0; i < p; i++)
        {
            Transform child = parent.GetChild(i);
            DestroyImmediate(child.GetComponent<MeshRenderer>());
            DestroyImmediate(child.GetComponent<MeshFilter>());
            DestroyImmediate(child.GetComponent<BoxCollider>());
        }
    }

    public void CreateMesh()
    {
        GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.GetComponent<MeshRenderer>() == null)
            {
                child.gameObject.AddComponent<MeshRenderer>();
                child.gameObject.GetComponent<MeshRenderer>().sharedMaterial = newObj.GetComponent<MeshRenderer>().sharedMaterial;
                child.gameObject.AddComponent<MeshFilter>();
                child.gameObject.GetComponent<MeshFilter>().sharedMesh = newObj.GetComponent<MeshFilter>().sharedMesh;
                child.gameObject.AddComponent<BoxCollider>();
            }
        }

        DestroyImmediate(newObj);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PoolEditorManager))]
class EditorManagerSubClass : Editor
{
    public override void OnInspectorGUI()
    {
        PoolEditorManager myScript = (PoolEditorManager)target; //The main class object is created
        DrawDefaultInspector(); //The default editor window is drawn

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("CREATE", EditorStyles.miniButton))
            myScript.CreateObjects();

        if (GUILayout.Button("DELETE", EditorStyles.miniButton))
            myScript.DeleteObjects();

        if (GUILayout.Button("DEL. MESH", EditorStyles.miniButton))
            myScript.DeleteMesh();

        if (GUILayout.Button("CRE. MESH", EditorStyles.miniButton))
            myScript.CreateMesh();
        EditorGUILayout.EndHorizontal();

    }
}

#endif