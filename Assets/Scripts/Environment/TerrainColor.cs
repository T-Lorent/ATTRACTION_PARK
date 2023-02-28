using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainColor : MonoBehaviour
{
    private Mesh mesh;

    [SerializeField] private float threashold_0 = 0.1f;
    [SerializeField] private float threashold_1 = 0.5f;
    [SerializeField] private float threashold_2 = 0.9f;
    

    // Start is called before the first frame update
    void Start()
    {
        mesh = this.GetComponent<MeshFilter>().mesh;
        //this.GetComponent<MeshRenderer>().material = null;

        List<Vector3> vao = new List<Vector3>();
        List<int> vbo = new List<int>(mesh.triangles);
        Vector3[] vertices = mesh.vertices;
        int[] indices = mesh.triangles;
        Color[] colors = new Color[vbo.Count];

        float height_min = mesh.bounds.min.y;
        float height_max = mesh.bounds.max.y;

        for (int i = 0; i < vbo.Count; i += 3)
        {
            Vector3 pos_1 = vertices[indices[i]];
            Vector3 pos_2 = vertices[indices[i + 1]];
            Vector3 pos_3 = vertices[indices[i + 2]];

            vao.Add(pos_1);
            vao.Add(pos_2);
            vao.Add(pos_3);

            vbo[i] = i;
            vbo[i + 1] = i + 1;
            vbo[i + 2] = i + 2;

            Vector3 medium_pos = (pos_1 + pos_2 + pos_3) / 3;

            float current_height = Mathf.InverseLerp(height_min, height_max, medium_pos.y);

            if(current_height <= threashold_0)
            {
                // SAND
                colors[i] = new Color(0.799103f, 0.603827f, 0.266356f);
                colors[i + 1] = new Color(0.799103f, 0.603827f, 0.266356f);
                colors[i + 2] = new Color(0.799103f, 0.603827f, 0.266356f);
            }
            else if(current_height <= threashold_1)
            {
                // GRASS
                colors[i] = new Color(0.799103f, 0.40724f, 0.181164f);
                colors[i + 1] = new Color(0.799103f, 0.40724f, 0.181164f);
                colors[i + 2] = new Color(0.799103f, 0.40724f, 0.181164f);
            }
            else if (current_height <= threashold_2)
            {
                // ROCK
                colors[i] = new Color(0.045186f, 0.045186f, 0.045186f);
                colors[i + 1] = new Color(0.045186f, 0.045186f, 0.045186f);
                colors[i + 2] = new Color(0.045186f, 0.045186f, 0.045186f);
            }
            else
            {
                // SNOW
                colors[i] = new Color(1, 1, 1);
                colors[i + 1] = new Color(1, 1, 1);
                colors[i + 2] = new Color(1, 1, 1);
            }
        }

        mesh.Clear();

        mesh.vertices = vao.ToArray();
        mesh.triangles = vbo.ToArray();
        mesh.colors = colors;


        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }
}
