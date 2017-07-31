using System.Collections;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;
	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	public AudioMixerSnapshot menuSnapshot;
	public AudioMixerSnapshot level1Snapshot;
	public AudioMixerSnapshot level2Snapshot;
	public AudioMixerSnapshot level3Snapshot;
	public AudioMixerSnapshot level4Snapshot;
	public AudioMixerSnapshot level5Snapshot;
	public AudioMixerSnapshot endgameSnapshot;
	public AudioMixerSnapshot storySpinnerSnapshot;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
            AudioMixerGroup mg = null;
            if (s.mixerGroup != null) {
                mg = s.mixerGroup;
            }

			s.source.outputAudioMixerGroup = mg;
		}
	}

    public void ResetSound(string sound) {
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

        s.source.Stop();
    }

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

        if (s.source.isPlaying) {
			Debug.LogWarning("Sound: " + name + " Already playing!");
            return;
        }

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

    public void GoToMenu() {
        Play("Menu");
        menuSnapshot.TransitionTo(1.0f);
    }

    public void GoToGame() {
        StartCoroutine("DelayStartTerm");
    }

    IEnumerator DelayStartTerm() {
        yield return new WaitForSeconds(0.5f);
        level1Snapshot.TransitionTo(1.8f);
        yield return new WaitForSeconds(2.3f);
        Play("Term1");
    }

    public void GoToSpinStory() {
        Play("SpinTheStory");
        storySpinnerSnapshot.TransitionTo(1.0f);
    }

    public void PlayTerm(int term) {
        switch (term) {
            case 1:
            case 2:
                level1Snapshot.TransitionTo(1f);
                Play("Term1");
                break;
            case 3:
            case 4:
                level2Snapshot.TransitionTo(1f);
                Play("Term2");
                break;
            case 5:
            case 6:
                level3Snapshot.TransitionTo(1f);
                Play("Term3");
                break;
            case 7:
            case 8:
                level4Snapshot.TransitionTo(1f);
                Play("Term4");
                break;
            default:
                level5Snapshot.TransitionTo(1f);
                Play("Term5");
                break;

        }
    }

}
