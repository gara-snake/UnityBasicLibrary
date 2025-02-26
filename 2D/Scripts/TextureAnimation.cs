using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Snake.Gara.Unity.Basic.Library._2D
{

    /// <summary>
    /// RawImageのテクスチャを切り替えてアニメーションさせる
    /// </summary>
    public class TextureAnimation : MonoBehaviour
    {
        public RawImage RawImage; // アニメーションを表示するRawImage
        public int FrameRate = 10; // アニメーションのフレームレート

        private Texture2D[] textures;
        private Coroutine coroutine;

        public Texture alternativeTexture;  // テクスチャがない場合に表示する代替テクスチャ

        private int currentFrame = 0;

        /// <summary>
        /// 現在フレームカウントを1originで取得
        /// </summary>
        /// <value></value>
        public int CurrentFrame
        {
            get
            {
                if (textures == null || textures.Length == 0)
                {
                    return 0;
                }
                return currentFrame;
            }
        }

        public bool IsCursorLastFrame
        {
            get
            {
                if (textures == null || textures.Length == 0)
                {
                    return true;
                }

                return currentFrame == textures.Length - 1;
            }
        }

        public bool IsPlaying
        {
            get;
            private set;
        }

        // リピートするかどうか
        public bool IsRepeat
        {
            get;
            set;
        }

        public Texture2D CurrentTexture
        {
            get
            {
                if (textures == null || textures.Length == 0)
                {
                    return null;
                }

                return textures[currentFrame];
            }
        }

        private bool isInitialized = false;

        private void Init()
        {
            if (RawImage == null)
            {
                RawImage = GetComponent<RawImage>();
            }

            isInitialized = true;
        }

        public void LoadAndPlay(ITexture2DLoader loader)
        {
            if (!isInitialized)
            {
                Init();
            }

            Stop();

            Load(loader);
            currentFrame = 0;
            coroutine = StartCoroutine(AnimateTextures());

        }

        public void Load(ITexture2DLoader loader)
        {
            if (!isInitialized)
            {
                Init();
            }

            textures = loader.LoadTexture2D();

        }

        public void LoadAndClear(ITexture2DLoader loader)
        {
            Load(loader);
            currentFrame = 0;
        }

        public void LoadAndPlayOnce(ITexture2DLoader loader, Action completeCallback = null)
        {
            if (!isInitialized)
            {
                Init();
            }

            Stop();

            Load(loader);
            currentFrame = 0;
            coroutine = StartCoroutine(AnimateTextures(completeCallback));
        }

        public void PlayOnce(Action completeCallback = null)
        {
            if (!isInitialized)
            {
                Init();
            }

            Stop();

            currentFrame = 0;
            coroutine = StartCoroutine(AnimateTextures(completeCallback));
        }

        public void Pause()
        {
            IsPlaying = false;
        }

        public void Resume()
        {
            if (!isInitialized)
            {
                Init();
            }

            IsPlaying = true;
            coroutine = StartCoroutine(AnimateTextures());
        }

        public void NextFrame()
        {
            if (!isInitialized)
            {
                Init();
            }

            if (!isTextureOK())
            {
                return;
            }

            currentFrame++;

            Refresh();
        }

        public void PrevFrame()
        {
            if (!isInitialized)
            {
                Init();
            }

            if (!isTextureOK())
            {
                return;
            }

            currentFrame--;

            Refresh();
        }

        public void LastFrame()
        {
            if (!isInitialized)
            {
                Init();
            }

            if (!isTextureOK())
            {
                return;
            }

            currentFrame = textures.Length - 1;

            Refresh();
        }

        public void Refresh()
        {
            if (currentFrame < 0)
            {
                currentFrame = textures.Length - 1;
            }

            if (textures.Length <= currentFrame)
            {
                currentFrame = 0;
            }

            RawImage.texture = textures[currentFrame];
        }

        public void View()
        {
            if (!isInitialized)
            {
                Init();
            }

            if (!isTextureOK())
            {
                return;
            }

            Stop();

            currentFrame = 0;
            RawImage.texture = textures[currentFrame];
        }

        public void Stop()
        {

            if (!isInitialized)
            {
                Init();
            }

            IsPlaying = false;

            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

        }

        public void SetTexture(Texture2D tex)
        {
            RawImage.texture = tex;
        }

        private bool isTextureOK()
        {
            if (textures == null || textures.Length == 0)
            {
                if (alternativeTexture != null)
                {
                    RawImage.texture = alternativeTexture;
                }

                return false;
            }

            return true;
        }

        IEnumerator AnimateTextures(Action completeCallback = null)
        {

            if (textures == null || textures.Length == 0)
            {
                if (alternativeTexture != null)
                {
                    RawImage.texture = alternativeTexture;
                }

                yield break;
            }

            IsPlaying = true;

            for (; currentFrame < textures.Length; currentFrame++)
            {
                if (!IsPlaying) yield break;

                RawImage.texture = textures[currentFrame];
                yield return new WaitForSeconds(1f / FrameRate - UnityEngine.Time.deltaTime);
            }

            IsPlaying = false;

            if (completeCallback != null)
            {
                completeCallback();
            }
            else
            {
                if (IsRepeat)
                {
                    currentFrame = 0;
                    coroutine = StartCoroutine(AnimateTextures());
                }
            }
        }
    }

}