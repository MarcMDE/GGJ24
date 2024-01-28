using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField] float t;
    void Start()
    {
        Destroy(gameObject, t);   
    }

}
