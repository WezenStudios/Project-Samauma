using UnityEngine;

public class code_SaveAudioInScenes : MonoBehaviour
{
    static readonly string FirstPlay = "FirstPlay";
    static readonly string MusicPref = "MusicPref";
    static readonly string EffectsPref = "EffectsPref";

    private int firstPlayInt;
    float floatMusic, floatEffects;

    public AudioSource sourceMusic;
    public AudioSource[] sourceEffects;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0)
        {
            floatMusic = 0.3f;
            floatEffects = 1f;

            PlayerPrefs.SetInt(FirstPlay, -1);
            PlayerPrefs.SetFloat(MusicPref, floatMusic);
            PlayerPrefs.SetFloat(EffectsPref, floatEffects);
        }
        else
        {
            floatMusic = PlayerPrefs.GetFloat(MusicPref);
            floatEffects = PlayerPrefs.GetFloat(EffectsPref);
        }
    }

    void Awake()
    {
        ContinueSettings();
    }

    void ContinueSettings()
    {
        floatMusic = PlayerPrefs.GetFloat(MusicPref);
        floatEffects = PlayerPrefs.GetFloat(EffectsPref);

        if(sourceMusic != null)
        {
            sourceMusic.volume = floatMusic;
            return;
        }
        
        for (int i = 0; i < sourceEffects.Length; i++)
        {
            sourceEffects[i].volume = floatEffects;
        }
    }
}
