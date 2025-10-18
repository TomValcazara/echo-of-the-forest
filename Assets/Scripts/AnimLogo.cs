using UnityEngine;

public class AnimLogo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scale = 1f + Mathf.Sin(Time.time * 2f) * 0.02f;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
