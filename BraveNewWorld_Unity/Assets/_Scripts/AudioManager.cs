using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRuby.SoundManagerNamespace
{
	public class AudioManager : MonoBehaviour
	{
        public static AudioManager instance = null;
		private AudioSource[] SoundAudioSources;
		private AudioSource[] MusicAudioSources;

		#region singleton


		void Awake()
		{
			if (instance == null) {
				instance = this;
			} else if (instance != this) {

				Destroy (gameObject);
			}
			DontDestroyOnLoad (gameObject);
		}
		#endregion

		private void Start()
		{
			SoundAudioSources = new AudioSource[GameObject.Find ("SfxSource").transform.childCount];
			MusicAudioSources = new AudioSource[GameObject.Find ("DialogueSounds").transform.childCount];
			for (int i = 0; i < SoundAudioSources.Length; i++)
			{
				SoundAudioSources[i] = transform.Find("SfxSource").GetChild(i).GetComponent<AudioSource>();
			}
			for (int i = 0; i < MusicAudioSources.Length; i++)
			{
				MusicAudioSources[i] = transform.Find("DialogueSounds").GetChild(i).GetComponent<AudioSource>();
			}
		}

		public void PlaySound(string Clip)
		{
			for (int i = 0; i < SoundAudioSources.Length; i++)
			{
				if (Clip == SoundAudioSources[i].name) { PlaySound(i); }
			}         
		}

		public void PlayMusic(string Clip)
		{
			for (int i = 0; i < MusicAudioSources.Length; i++)
			{
				if (Clip == MusicAudioSources[i].name) { PlayMusic(i); }
			}
		}

		public void StopMusic()
		{
			for (int i = 0; i < MusicAudioSources.Length; i++)
			{
				StopMusic(i);
			}
		}

        public void StopSound(string Clip)
        {
            for (int i = 0; i < SoundAudioSources.Length; i++)
            {
                if (Clip == SoundAudioSources[i].name)
                {

                }
            }
        }

        private void PlaySound(int index)
		{
			SoundAudioSources[index].PlayOneShotSoundManaged(SoundAudioSources[index].clip);
		}

		private void PlayMusic(int index)
		{
			MusicAudioSources[index].PlayOneShotSoundManaged(MusicAudioSources[index].clip);
		}

		private void StopMusic(int index)
		{
			MusicAudioSources[index].Stop();
		}
	}
}