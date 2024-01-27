using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent),typeof(CapsuleCollider))]
public class NavMeshController : MonoBehaviour
{
    

    private NavMeshAgent navMeshAgent;
    private CapsuleCollider capsuleCollider;
    
    [SerializeField] private NavMeshSurface surface;
    
    private float destinationThreshold = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        destinationThreshold = capsuleCollider.radius * Mathf.Abs(transform.lossyScale.x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }
    public bool HasAgentReachedDestination()
    {
        return !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= destinationThreshold;
    }
    public Vector3 GetRandomPoint()
    {
        
        float xMin = surface.navMeshData.sourceBounds.min.x;
        float xMax = surface.navMeshData.sourceBounds.max.x;
        float zMin = surface.navMeshData.sourceBounds.min.z;
        float zMax = surface.navMeshData.sourceBounds.max.z;
        float y = surface.navMeshData.sourceBounds.center.y;
        return new Vector3(Random.Range(xMin, xMax), y, Random.Range(zMin, zMax));
    }
    
}
