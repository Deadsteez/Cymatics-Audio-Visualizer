using UnityEngine;

public class AudioToShader : MonoBehaviour
{
    public AudioSource audioSource; 
    public Renderer targetRenderer; 
    private Material mat; 

    void Start()
    {
        mat = targetRenderer.material; 
    }

    void Update()
    {
        float[] spectrum = new float[64]; // Here we take 64 for better performance since shaders are heavy
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        float intensity = spectrum[5] * 20f; // Again here for bass we take 5th freq bin
        mat.SetFloat("_WaveIntensity", intensity); // Send data to Shader
    }
}
