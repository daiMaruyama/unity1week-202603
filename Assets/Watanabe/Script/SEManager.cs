using UnityEngine;

public class SEManager : MonoBehaviour
{
    public static SEManager Instance;
    [Header("SE一覧")]
    [SerializeField] private AudioClip correctSE;
    [SerializeField] private AudioClip wrongSE;
    [SerializeField] private AudioClip buttonSE;
    [SerializeField] private AudioClip selectSE;
    [SerializeField] private float correctVolume = 1.0f;
    [SerializeField] private float wrongVolume = 1.0f;
    [SerializeField] private float buttonVolume = 0.5f;
    [SerializeField] private float selectVolume = 0.3f;
    [SerializeField] private AudioSource audioSource;
    private void Awake()
    {
        Instance = this;
    }

    public void PlayCorrect()
    {
        audioSource.PlayOneShot(correctSE, correctVolume);
    }

    public void PlayWrong()
    {
        audioSource.PlayOneShot(wrongSE, wrongVolume);
    }

    public void PlayButton()
    {
        audioSource.PlayOneShot(buttonSE, buttonVolume);
    }

    public void PlaySelect()
    {
        audioSource.PlayOneShot(selectSE, selectVolume);
    }
}
