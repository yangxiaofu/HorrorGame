using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Characters;

namespace Game.UI{
    [RequireComponent(typeof(RawImage))]
    public class PlayerHealthBar : Bar
    {
        // Use this for initialization
        void Start()
        {
            SetupBarVariables();
        }

        // Update is called once per frame
        void Update()
        {
            float percentage = _player.GetComponent<PlayerHealth>().healthAsPercentage;
            UpdateUIBar(percentage);
        }

    }

}
