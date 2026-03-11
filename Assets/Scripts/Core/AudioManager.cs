using UnityEngine;

namespace Core
{
    /// <summary>
    /// BGM・SE管理
    /// </summary>
    public class AudioManager : SingletonMonoBehaviour<AudioManager>
    {
        [SerializeField] private AudioSource bgmSource;
        [SerializeField] private AudioSource seSource;

        public float BgmVolume
        {
            get => bgmSource.volume;
            set => bgmSource.volume = value;
        }

        public float SeVolume
        {
            get => seSource.volume;
            set => seSource.volume = value;
        }

        public void PlayBgm(AudioClip clip, bool loop = true)
        {
            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.Play();
        }

        public void StopBgm()
        {
            bgmSource.Stop();
        }

        public void PlaySe(AudioClip clip)
        {
            seSource.PlayOneShot(clip);
        }
    }
}
