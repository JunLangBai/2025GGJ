using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // 获取AudioSource组件
        audioSource = GetComponent<AudioSource>();

        // 保证BGM对象在场景切换时不会销毁
        DontDestroyOnLoad(gameObject);

        // 如果没有播放音频，开始播放
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        // 如果音频停止播放且没有被循环，重新播放
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
