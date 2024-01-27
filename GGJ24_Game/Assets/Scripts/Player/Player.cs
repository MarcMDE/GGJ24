using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    [SerializeField] float speed;

    public Vector3 Position => transform.position;
    public Vector3 Forward => transform.forward;
    public float Speed => speed;
}