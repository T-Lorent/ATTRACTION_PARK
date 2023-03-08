using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;

public class GroundManager : MonoBehaviour
{
    public static GroundManager Instance;

    public float X_MIN;
    public float X_MAX;
    public float Y_MIN;
    public float Y_MAX;
    public float Z_MIN;
    public float Z_MAX;


   

    /*====== PRIVATE ======*/
    [SerializeField] private Transform ground;
    private NavMeshSurface nav_mesh_surface;

    [Header("GRID MANAGEMENT")]
    [SerializeField] private int grid_size = 10;
    private Terrain terrain;
    [SerializeField] private GameObject lake;

    public List<Vector3> spawnable_cell {get; private set;} = new List<Vector3>();
    public float cellSizeX {get; private set;}
    public float cellSizeZ {get; private set;}

    private void Awake()
    {
        Instance = this;

        terrain = ground.GetComponent<Terrain>();
        nav_mesh_surface = ground.GetComponent<NavMeshSurface>();

        NavMesh.pathfindingIterationsPerFrame= 1000;
        CreateSpawnGrid();

        Vector3 ground_position = ground.position;
        Bounds nav_mesh_bounds = nav_mesh_surface.navMeshData.sourceBounds;

        X_MIN = ground_position.x + nav_mesh_bounds.min.x;
        X_MAX = ground_position.x + nav_mesh_bounds.max.x;

        Y_MIN = ground_position.y + nav_mesh_bounds.min.y;
        Y_MAX = ground_position.y + nav_mesh_bounds.max.y;

        Z_MIN = ground_position.z + nav_mesh_bounds.min.z;
        Z_MAX = ground_position.z + nav_mesh_bounds.max.z;
    }

    public void CreateSpawnGrid()
    {
        float lake_height = lake.transform.position.y + (lake.transform.localScale.y/2);

        //We compute the size of each cell of the grid
        float minCoordX = terrain.transform.position.x;
        float minCoordZ = terrain.transform.position.z;

        float maxCoordX = minCoordX + terrain.terrainData.size.x;
        float maxCoordZ = minCoordZ + terrain.terrainData.size.z;

        cellSizeX= (maxCoordX - minCoordX) / grid_size;
        cellSizeZ= (maxCoordZ - minCoordZ) / grid_size;

        Vector2 center = new Vector2(cellSizeX/2f, cellSizeZ/2f);

        for(int i=0; i< grid_size; ++i)
        {
            for(int j=0; j< grid_size; ++j)
            {
                float positionX = i*cellSizeX + center.x + minCoordX;
                float positionZ = j*cellSizeZ + center.y + minCoordZ;


                float height = terrain.SampleHeight(new Vector3(positionX, 0, positionZ));
                //We only take cells that are not in the lake
                if(height > lake_height)
                {
                    spawnable_cell.Add(new Vector3(positionX, height, positionZ));
                }
            }
        }
    }

    // public Vector2 get_cell_size(){
    //     return new Vector2(cellSizeX, cellSizeZ);
    // }
}