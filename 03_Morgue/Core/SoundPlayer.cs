using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioClip bgClip;

    private void Start()
    {
        SoundManager.PlayMusic(bgClip, 0.1f, true);
    }
}
