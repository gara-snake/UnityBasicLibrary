using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake.Gara.Unity.Basic.Library.Util;

namespace Snake.Gara.Unity.Basic.Library.Sound
{

    public static class SoundEffect
    {
        protected class SoundEffectController : MonoBehaviour
        {
            public ObjectPool<SeAudioSource> objectPool = null;

            public void Play(AudioClip audioClip)
            {
                var se = objectPool.GetObjectFromPool();
                se.Play(audioClip);
            }
        }

        private static SoundEffectController instance;

        private static Dictionary<int, SECell> cells = new Dictionary<int, SECell>();

        public static void Init(ISEAssets assets = null)
        {
            var obj = new GameObject("SoundEffectController");
            obj.transform.position = Vector3.zero;

            instance = obj.AddComponent<SoundEffectController>();

            if (assets != null)
            {
                foreach (var asset in assets.GetSEs())
                {
                    cells.Add(asset.ID, asset);
                }
            }
        }

        /// <summary>
        /// SE鳴動に使用するAudioSourceを付与したゲームオブジェクトを指定します
        /// </summary>
        /// <param name="source"></param>
        /// <param name="buffer"></param>
        public static void SetSauce(SeAudioSource source, int buffer = 10)
        {
            instance.objectPool = new ObjectPool<SeAudioSource>(source, buffer);
        }

        public static void Play(int id)
        {
            if (instance.objectPool == null)
            {
                var ses = new GameObject("SoundEffectSource");
                var source = ses.AddComponent<SeAudioSource>();
                source.source = ses.AddComponent<AudioSource>();

                instance.objectPool = new ObjectPool<SeAudioSource>(source, 5);
            }

            var cell = cells[id];

            if (cell != null)
            {
                instance.Play(cell.audioClip);
            }
        }

    }
}
