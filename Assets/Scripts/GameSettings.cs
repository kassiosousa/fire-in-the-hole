using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {

    public bool fullScreen;
    public int resolutionIndex;
    public int textureQuality;
    public int antiAliasing;
    public int vSync;
    public float musicVolume;

    public Toggle fullScreenToggle;
    public Slider resolutionSlider;
    public Slider textureQualitySlider;
    public Slider antiAliasingSlider;
    public Slider vSyncSlider;
    public Slider musicVolumeSlider;

	
    public Resolution[] resolutions;

    void OnEnable()
    {

        /*fullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        resolutionSlider.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualitySlider.onValueChanged.AddListener(delegate { OnTextQualityChange(); });
        antiAliasingSlider.onValueChanged.AddListener(delegate { OnAntiAliasingChange(); });
        vSyncSlider.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });

        resolutions = Screen.resolutions;*/
    }

	void OnFullScreenToggle () {
        fullScreen = Screen.fullScreen = fullScreenToggle.isOn;
	}

    void OnResolutionChange()
    {
        
    }

    void OnTextQualityChange() {
        QualitySettings.masterTextureLimit = textureQuality = (int)textureQualitySlider.value; 
    }
    void OnAntiAliasingChange() { }
    void OnVSyncChange() { }
    void OnMusicVolumeChange() {
        AudioListener.volume = musicVolume = musicVolumeSlider.value;
    }

    void SaveSettings() { }
    void LoadSettings() { }
	
}
