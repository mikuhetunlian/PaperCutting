using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicMgr : BaseManager<MusicMgr>
{
    //���ű������ֵ�AudioSource
    private AudioSource bkMusic;
    //�������ֵĴ�С 0-1֮��
    public float bkValue;

    //�ڳ����б�������Ч�ǵ�GameObject
    private GameObject efMusicObj;
    //��Ч��
    private List<AudioSource> sourceList;
    //��Ч�Ĵ�С 0-1֮��
    public float efValue;



    public MusicMgr()
    {
        if (sourceList == null)
        {
            sourceList = new List<AudioSource>();
        }

        //��ӵ�Mono��Update��ִ��
        MonoManager.GetInstance().AddUpdateLinstener(Update);
    }

    /// <summary>
    /// ÿһ֡��һ���Ƿ�����Ч������ϣ�����У���efMusicObj���Ƴ���AudioSource���
    /// ��Mono����ģ�����ִ��
    /// </summary>
    private void Update()
    {
        for (int i = sourceList.Count - 1; i >= 0; i--)
        {
            if (!sourceList[i].isPlaying)
            {
                //�������Чģ����ϣ���efMusicObj���Ƴ���Ӧ����ЧAudioSource���
                GameObject.Destroy(sourceList[i]);
                sourceList.RemoveAt(0);
            }
        }
    }

    /// <summary>
    /// ���ű�������
    /// </summary>
    /// <param name="musicName">����������</param>
    public void PlayBkMusic(string musicName)
    {
        if (bkMusic == null)
        {
            GameObject obj = new GameObject("bkMusic");
            bkMusic = obj.AddComponent<AudioSource>();
        }

        //�첽������Դ
        ResMgr.GetInstance().LoadResAsync<AudioClip>("Music/bkMusic/" + musicName, (clip) =>
         {
             bkMusic.clip = clip;
             bkMusic.volume = bkValue;
             bkMusic.Play();
         });

    }

    /// <summary>
    /// ��ֹ���ű�������
    /// </summary>
    public void StopBkMusic()
    {
        if (bkMusic != null)
        {
            bkMusic.Stop();
        }
    }

    /// <summary>
    /// ��ͣ���ű�������
    /// </summary>
    public void PauseBkMusic()
    {
        if (bkMusic != null)
        {
            bkMusic.Pause();
        }
    }


    /// <summary>
    /// ������Ч
    /// </summary>
    /// <param name="effectName">��Ч����</param>
    /// <param name="isLoop">��Ч�Ƿ�ѭ��</param>
    /// <param name="callback">��������Ч��Ļص�������Ĭ��Ϊnull</param>
    public void PlayMusicEffect(string effectName, bool isLoop, UnityAction<AudioSource> callback = null)
    {
        if (efMusicObj == null)
        {
            GameObject obj = new GameObject("effectMusic");
            efMusicObj = obj;
        }

        //�첽������Ч
        ResMgr.GetInstance().LoadResAsync<AudioClip>("Music/effectMusic/" + effectName, (clip) =>
         {
             AudioSource efMusic = efMusicObj.AddComponent<AudioSource>();
             sourceList.Add(efMusic);
             efMusic.clip = clip;
             efMusic.volume = efValue;
             efMusic.loop = isLoop;
             efMusic.Play();
             if (callback != null)
             {
                 callback(efMusic);
             }
         });

    }

    /// <summary>
    /// ��ͣĳ����Ч����
    /// </summary>
    /// <param name="sourse"></param>
    public void PauseMusicEffect(AudioSource sourse)
    {
        if (sourceList.Contains(sourse))
        {
            sourse.Pause();
        }
      
    }

    /// <summary>
    /// ֹͣĳ����Ч
    /// </summary>
    /// <param name="sourse"></param>
    public void StopMusicEffect(AudioSource sourse)
    {
        if (sourceList.Contains(sourse))
        {
            sourceList.Remove(sourse);
            sourse.Stop();
            GameObject.Destroy(sourse);
        }
    }
}
