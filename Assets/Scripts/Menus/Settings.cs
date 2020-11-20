﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace DoodleJump.Menus
{
    [AddComponentMenu("Menus/Audio Settings")]
    public class Settings : MonoBehaviour
    {
        #region Variables
        [SerializeField, Tooltip("Connect AudioMixer here.")] private AudioMixer mixer;
        #endregion
        #region Start
        private void Start()
        {
            //if music has been saved previously, load those settings
            if (PlayerPrefs.HasKey("music"))
            {
                SetMusicVolume(PlayerPrefs.GetFloat("music"));
            }
            if (PlayerPrefs.HasKey("sfx"))
            {
                SetSFXVolume(PlayerPrefs.GetFloat("sfx"));
            }
            if (PlayerPrefs.HasKey("master"))
            {
                if (PlayerPrefs.GetInt("master") == 0)
                {
                    MuteToggle(true);
                }
                else
                {
                    MuteToggle(false);
                }
            }
        }
        #endregion
        #region Functions
        /// <summary>
        /// Set music volume from slider.
        /// </summary>
        public void SetMusicVolume(float value)
        {
            mixer.SetFloat("MusicVolume", value);
            PlayerPrefs.SetFloat("music", value);
        }
        /// <summary>
        /// Set sfx volume from slider.
        /// </summary>
        public void SetSFXVolume(float value)
        {
            mixer.SetFloat("SFXVolume", value);
            PlayerPrefs.SetFloat("sfx", value);
        }
        /// <summary>
        /// Mute all sound.
        /// </summary>
        public void MuteToggle(bool mute)
        {
            if (mute)
            {
                mixer.SetFloat("MasterVolume", -80);
                PlayerPrefs.SetInt("master", 0);
            }
            else
            {
                mixer.SetFloat("MasterVolume", 0);
                PlayerPrefs.SetInt("master", 1);
            }
        }
        #endregion
    }
}