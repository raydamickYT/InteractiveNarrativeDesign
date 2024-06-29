using System.Collections;
using System.Collections.Generic;
using BlackBoard;
using UnityEngine;
using UnityEngine.UI;

namespace VNCreator
{
    public class ThoughtUIManager : MonoBehaviour
    {
        public Slider slider;
        public Canvas canvas;
        // Start is called before the first frame update
        void Start()
        {
            // EnableUI();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void EnableUI()
        {
            canvas.gameObject.SetActive(true);
        }

        public void DisableUI()
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
