using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class EnemyAudioPlayer : MonoBehaviour
{
    const float SpeedThreshold = 0.1f;

    [SerializeField] private Transform soundParent;

    [SerializeField] RandomSoundsPlayer stepsSound;

    EnemyBehaviour enemyBehaviour;

    private List<AudioClipInfo> clipInfoArray;

    private void Awake()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitList();

        stepsSound.Volume = enemyBehaviour.CurrentSpeed > SpeedThreshold ? 1f : 0f;
        stepsSound.Enable();

    }

    private void Update()
    {
        stepsSound.Volume = enemyBehaviour.CurrentSpeed > SpeedThreshold ? 1f : 0f;
    }

    private void InitList()
    {
        clipInfoArray = new List<AudioClipInfo>();
        foreach (Transform child in soundParent)
        {
            AudioClipInfo info = child.GetComponent<AudioClipInfo>();
            if (info != null)
            {
                clipInfoArray.Add(info);
            }
        }
    }

    public void PlaySound(EnemyAudio audioType)
    {
        AudioClipInfo clipInfo = clipInfoArray.FirstOrDefault(info => info.AudioType == audioType);
        if(clipInfo != null) clipInfo.Play();
    }
}
