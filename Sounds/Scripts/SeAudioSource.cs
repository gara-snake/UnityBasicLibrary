using System.Collections;
using System.Collections.Generic;
using Snake.Gara.Unity.Basic.Library.Util;
using UnityEngine;

namespace Snake.Gara.Unity.Basic.Library.Sound
{

	public class SeAudioSource : MonoBehaviour, IPoolable
	{

		public AudioSource source;

		void Start()
		{
			if (source == null)
			{
				source = GetComponent<AudioSource>();
			}
		}

		public void Play(AudioClip clip)
		{
			if (source != null)
			{
				WakeUp();
				source.clip = clip;
				source.Play();
			}
		}

		public void Update()
		{
			if (source != null)
			{
				if (!source.isPlaying)
				{
					Extinguish();
				}
			}
		}

		public void Extinguish()
		{
			this.gameObject.SetActive(false);
		}

		public bool IsActive()
		{
			return this.gameObject.activeInHierarchy;
		}

		public void WakeUp()
		{
			this.gameObject.SetActive(true);
		}
	}

}
