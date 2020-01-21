using UnityEditor;
using UnityEngine;

public class GenerateCell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreateCube();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateCube()
    {

        GameObject obj = new GameObject("cube");
        MeshFilter mf = obj.AddComponent<MeshFilter>();
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();


        Vector3[] vertices = new Vector3[24];
        int[] triangles = new int[36];

        //forward
        vertices[0].Set(0.5f, 0f, - 0.5f);
        vertices[1].Set(-0.5f, 0f, -0.5f);
        vertices[2].Set(0.5f, 0f, 0.5f);
        vertices[3].Set(-0.5f, 0f, 0.5f);

        //3 2 
        //1 0
        //
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 2;
        triangles[4] = 1;
        triangles[5] = 3;

        mf.mesh.vertices = vertices;
        mf.mesh.triangles = triangles;

        Mesh mesh = obj.GetComponent<MeshFilter>().mesh;
        AssetDatabase.CreateAsset(mesh, "Assets/" + name + ".mesh.asset");
    }
}
