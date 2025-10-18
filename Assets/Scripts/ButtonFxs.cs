using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonFxs : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    public float hoverScaleIncrease = 0.05f; // how much to grow on hover
    public float scaleSpeed = 5f; // how fast it scales
    private Vector3 targetScale;


    public Texture2D pointerCursor; // only need to set this one
    public Vector2 pointerHotspot = Vector2.zero;
    private Texture2D defaultCursor;
    private Vector2 defaultHotspot;
    public AudioClip clickClip;
    public List<Sprite> buttonDesigns;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;

        defaultCursor = null;           // since it’s already set by Player Settings
        defaultHotspot = Vector2.zero;  // uses the default hotspot (0,0)
        GetComponent<Image>().sprite = buttonDesigns[Random.Range(0, buttonDesigns.Count)];

    }

    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // System hand/pointer
    // }

    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // Back to normal arrow
    // }
    void Update()
    {
        // Smoothly interpolate scale
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * (1f + hoverScaleIncrease); //size
        Cursor.SetCursor(pointerCursor, pointerHotspot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale; //size
        // Reverts back to whatever is defined in Player Settings
        Cursor.SetCursor(defaultCursor, defaultHotspot, CursorMode.Auto);
    }

    public void ResetCursor()
    {
        // When clicked, restore the default cursor too
        Cursor.SetCursor(defaultCursor, defaultHotspot, CursorMode.Auto);
    }
    public void ClickSound()
    {
        //Debug.Log("Sound");
        //GetComponent<AudioSource>().enabled = true;
        //GetComponent<AudioSource>().Play();
        AudioSource.PlayClipAtPoint(clickClip, Vector3.zero); //Used this instead of having the AudioSource on the button, because when the canvas become inactive, all children elements get deactivate too, making the sound never play.

    }

}
