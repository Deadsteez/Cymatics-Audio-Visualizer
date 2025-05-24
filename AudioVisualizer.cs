using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform targetObject;
    public float scaleMultiplier = 10f;
    public float smoothTime = 0.1f; // Controls how fast it adapts

    private float targetScale = 1f;
    private float currentScale = 1f;
    private float velocity = 0f; // Used for SmoothDamp

    void Update()
    {
        float[] spectrum = new float[256];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);

        // Take an average of multiple frequencies for stability
        int startFreq = 4, endFreq = 10;// 344 Hz to 860 Hz raneg for the frequency thats why we have taken
                                        // 4 and 10. We take lower and not higher freq bins because
                                        // we wanted high bass but less treble
        float sum = 0f;
        for (int i = startFreq; i < endFreq; i++)
        {
            sum += spectrum[i];
        }

        // Calculate target scale dynamically
        float intensity = Mathf.Clamp((sum / (endFreq - startFreq)) * scaleMultiplier * 1000f, 0.1f, 3f);
        targetScale = 1 + intensity;

        // Smoothly transition to the target scale
        currentScale = Mathf.SmoothDamp(currentScale, targetScale, ref velocity, smoothTime);

        // Apply the scale
        targetObject.localScale = new Vector3(currentScale, currentScale, currentScale);
    }
}
