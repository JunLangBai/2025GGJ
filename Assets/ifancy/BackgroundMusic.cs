using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // ��ȡAudioSource���
        audioSource = GetComponent<AudioSource>();

        // ��֤BGM�����ڳ����л�ʱ��������
        DontDestroyOnLoad(gameObject);

        // ���û�в�����Ƶ����ʼ����
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        // �����Ƶֹͣ������û�б�ѭ�������²���
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
