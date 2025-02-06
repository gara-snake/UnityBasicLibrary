using UnityEngine;

namespace Snake.Gara.Unity.Basic.Library.Control
{

    public class KeyHoldDetector
    {
        private float keyHoldTime = 0;
        private float holdThreshold;
        private KeyCode targetKey;

        public KeyHoldDetector(KeyCode targetKey, float holdThreshold = 0.5f)
        {
            this.targetKey = targetKey;
            this.holdThreshold = holdThreshold;
        }

        public void Update()
        {
            if (Input.GetKey(targetKey))
            {
                keyHoldTime += UnityEngine.Time.deltaTime;
            }
            else
            {
                keyHoldTime = 0;
            }
        }

        public bool IsKeyHeld()
        {
            return keyHoldTime >= holdThreshold;
        }

        public float HoldTimeRatio
        {
            get
            {
                return keyHoldTime / holdThreshold;
            }
        }
    }

}