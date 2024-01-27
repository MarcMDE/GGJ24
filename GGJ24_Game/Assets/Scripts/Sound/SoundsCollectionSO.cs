using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundsCollection", menuName = "ScriptableObjects/Sounds Collection", order = 1)]
public class SoundsCollectionSO : ScriptableObject
{
    [SerializeField]
    private AudioClip [] clips;

    public int Length => clips.Length;

    public AudioClip GetClip(int i)
    {
        if (i >= Length) return null;

        return clips[i];
    }

    public AudioClip RandomClip => clips[Random.Range(0, Length)];
}
