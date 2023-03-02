using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class GroundManager : MonoBehaviour
{
    public static GroundManager Instance;

    public float X_MIN;
    public float X_MAX;
    public float Y_MIN;
    public float Y_MAX;
    public float Z_MIN;
    public float Z_MAX;

    [SerializeField] private Transform ground;
    [SerializeField] private NavMeshSurface nav_mesh_surface;

    private void Awake()
    {
        Instance = this;

        Vector3 ground_position = ground.position;
        Bounds nav_mesh_bounds = nav_mesh_surface.navMeshData.sourceBounds;

        X_MIN = ground_position.x + nav_mesh_bounds.min.x;
        X_MAX = ground_position.x + nav_mesh_bounds.max.x;

        Y_MIN = ground_position.y + nav_mesh_bounds.min.y;
        Y_MAX = ground_position.y + nav_mesh_bounds.max.y;

        Z_MIN = ground_position.z + nav_mesh_bounds.min.z;
        Z_MAX = ground_position.z + nav_mesh_bounds.max.z;
    }
}