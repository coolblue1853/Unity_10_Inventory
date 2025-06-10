using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

    [Header("Mixer Settings")]
    public AudioMixer AudioMixer; // 오디오 믹서 (BGM, SFX 볼륨 제어)

    [Header("Background Music")]
    public AudioSource BgmSource; // BGM 재생용 AudioSource
    public AudioClip[] BgmClips;  // 재생 가능한 BGM 클립 배열

    [Header("Sound Effects (Auto Register)")]
    public AudioSource SfxSource; // SFX 재생용 AudioSource
    private Dictionary<string, AudioClip> _sfxDict = new Dictionary<string, AudioClip>(); // SFX 이름-클립 매핑 딕셔너리

    private AudioSource _loopSource; // 루프용 AudioSource

    private void Awake()
    {
        LoadAllSFX();
    }

    // Resources/Audio/SFX 폴더 내 모든 오디오 클립을 자동 등록
    void LoadAllSFX()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio/SFX");
        foreach (AudioClip clip in clips)
        {
            if (!_sfxDict.ContainsKey(clip.name))
            {
                _sfxDict.Add(clip.name, clip);
                Debug.Log($"SFX 로드 완료: {clip.name}");
            }
        }
    }

    // 인덱스로 BGM 재생
    public void PlayBGM(int index)
    {
        if (index >= 0 && index < BgmClips.Length)
        {
            BgmSource.clip = BgmClips[index];
            BgmSource.loop = true;
            BgmSource.Play();
        }
    }

    // 이름으로 SFX 재생
    public void PlaySFX(string name)
    {
        if (_sfxDict.TryGetValue(name, out AudioClip clip))
        {
            SfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX '{name}' 을(를) 찾을 수 없습니다.");
        }
    }

    // 피치와 볼륨 설정하여 SFX 재생
    public void PlaySFX(string name, float volume, float pitch)
    {
        if (_sfxDict.TryGetValue(name, out AudioClip clip))
        {
            SfxSource.pitch = pitch;
            SfxSource.PlayOneShot(clip, volume);
            SfxSource.pitch = 1f; // 재생 후 피치 초기화
        }
    }

    // 위치 기반으로 SFX 재생 (3D 공간)
    public void PlaySFXAtPosition(string name, Vector3 position)
    {
        if (_sfxDict.TryGetValue(name, out AudioClip clip))
        {
            AudioSource.PlayClipAtPoint(clip, position);
        }
    }

    // 루프 사운드 시작 (지속 재생 효과음)
    public void PlaySFXLoop(string name)
    {
        if (_loopSource == null)
        {
            _loopSource = gameObject.AddComponent<AudioSource>();
            _loopSource.loop = true;
            _loopSource.playOnAwake = false;
        }

        if (_sfxDict.TryGetValue(name, out AudioClip clip))
        {
            _loopSource.clip = clip;
            _loopSource.Play();
        }
    }

    // 루프 사운드 정지
    public void StopSFXLoop()
    {
        if (_loopSource != null && _loopSource.isPlaying)
        {
            _loopSource.Stop();
        }
    }

    // BGM 볼륨 설정 (0.0 ~ 1.0 범위, dB 변환)
    public void SetBGMVolume(float value)
    {
        AudioMixer.SetFloat("BGM", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }

    // SFX 볼륨 설정 (0.0 ~ 1.0 범위, dB 변환)
    public void SetSFXVolume(float value)
    {
        AudioMixer.SetFloat("SFX", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }
}
