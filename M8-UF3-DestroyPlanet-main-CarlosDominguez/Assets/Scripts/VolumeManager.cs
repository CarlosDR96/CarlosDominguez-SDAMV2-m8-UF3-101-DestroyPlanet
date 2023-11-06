using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeManager : MonoBehaviour
{
    public Scrollbar volumeScrollbar;
    
    void Start()
    {
        // Configura el valor inicial del Scrollbar al volumen actual del AudioSource
        volumeScrollbar.value = AudioListener.volume;
    }

    void Update()
    {
        // Ajusta el volumen global del juego basándose en el valor del Scrollbar
        AudioListener.volume = volumeScrollbar.value;
    }
}

