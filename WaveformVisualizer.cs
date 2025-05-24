using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WaveformVisualizer : MonoBehaviour
{
    public AudioSource audioSource;
    private LineRenderer lineRenderer;
    private int sampleSize = 256;
    private float[] samples;
    private int frameCount = 0; 

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = sampleSize;
        samples = new float[sampleSize];

        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.useWorldSpace = false;
    }

    void Update()
    {
        frameCount++;

        if (frameCount % 2 == 0)  // <-- Skip alternate frames
            return;

        if (audioSource.isPlaying)
        {
            audioSource.GetOutputData(samples, 0);

            for (int i = 0; i < sampleSize; i++)
            {
                float x = (i / (float)sampleSize) * 10f - 5f;
                float y = samples[i] * 2f;
                lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            }
        }
    }
}
