using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class code_Audio : MonoBehaviour
{
    static readonly string FirstPlay = "FirstPlay";
    static readonly string MusicPref = "MusicPref";
    static readonly string EffectsPref = "EffectsPref";

    int firstPlayInt;
    float floatMusic, floatEffects;

    public Slider sliderMusic, sliderEffects;
    public TMP_Text musicLabel, effectsLabel;
    public AudioSource sourceMusic;
    public AudioSource[] sourceEffects;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if(firstPlayInt == 0)
        {
            floatMusic = 0.3f;
            floatEffects = 1f;

            sliderMusic.value = floatMusic;
            sliderEffects.value = floatEffects;

            musicLabel.text = (sliderMusic.value * 100f).ToString("F0") + "%";
            effectsLabel.text = (sliderEffects.value * 100f).ToString("F0") + "%";

            PlayerPrefs.SetFloat(MusicPref, floatMusic);
            PlayerPrefs.SetFloat(EffectsPref, floatEffects);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            floatMusic = PlayerPrefs.GetFloat(MusicPref);
            floatEffects = PlayerPrefs.GetFloat(EffectsPref);

            sliderMusic.value = floatMusic;
            sliderEffects.value = floatEffects;

            musicLabel.text = (sliderMusic.value * 100f).ToString("F0") + "%";
            effectsLabel.text = (sliderEffects.value * 100f).ToString("F0") + "%";
        }
    }

    public void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat(MusicPref, sliderMusic.value);
        PlayerPrefs.SetFloat(EffectsPref, sliderEffects.value);

        musicLabel.text = (sliderMusic.value * 100f).ToString("F0") + "%";
        effectsLabel.text = (sliderEffects.value * 100f).ToString("F0") + "%";
    }

    void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveAudioSettings();
        }
    }

    public void UpdateAudio()
    {
        sourceMusic.volume = sliderMusic.value;

        for (int i = 0; i < sourceEffects.Length; i++)
        {
            sourceEffects[i].volume = sliderEffects.value;
        }

        musicLabel.text = (sliderMusic.value * 100f).ToString("F0") + "%";
        effectsLabel.text = (sliderEffects.value * 100f).ToString("F0") + "%";
    }
}
