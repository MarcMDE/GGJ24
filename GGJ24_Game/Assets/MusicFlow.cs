using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MusicController))]
public class MusicFlow  : SingletonMonoBehaviour<MusicFlow>
{
    private MusicController musicController;

    private GampelayStates state;
    private MusicTrackNames currentTrack;
    // Start is called before the first frame update
    void Start()
    {
        musicController = GetComponent<MusicController>();
    }

    // Update is called once per frame
    public void TryPlayMusic(MusicTrackNames track)
    {
        if (track != currentTrack && (currentTrack <= MusicTrackNames.Chase || track > currentTrack  ) )
        {
            musicController.PlayTrack(track);
            currentTrack = track;
        }
        
    }

}

public enum GampelayStates
{
    Terror,
    TerrorFunny,
    Funny
}
