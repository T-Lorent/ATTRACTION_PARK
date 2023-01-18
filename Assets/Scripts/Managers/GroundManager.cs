using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundManager : MonoBehaviour
{
    public static GroundManager Instance;

    void Awake() => Instance = this;

    
}