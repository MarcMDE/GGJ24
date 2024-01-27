using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class EnemyAudioPlayer : MonoBehaviour
{
    [SerializeField] private Transform soundParent;
    private List<AudioClipInfo> clipInfoArray;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    private void InitArray()
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
