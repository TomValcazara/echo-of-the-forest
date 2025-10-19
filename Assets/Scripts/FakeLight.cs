using UnityEngine;

public class FakeLight : MonoBehaviour
{
    private float speed = 0.5f;     // How fast it pulses
    private float amount = 0.1f;  // How much it grows/shrinks

    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * speed) * amount;
        transform.localScale = startScale * scale;
    }
}
