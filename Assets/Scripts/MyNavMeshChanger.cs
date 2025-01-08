using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyNavMeshChanger : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]
    NavMeshSurface surface;

    public void Update()
    {
        surface.UpdateNavMesh(surface.navMeshData);
    }
}

