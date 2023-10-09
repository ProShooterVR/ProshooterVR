using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VFXManager : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip hoverTickSound;
    public AudioClip ClickTickSound;

    public void OnHoverFx()
    {
        myFx.PlayOneShot(hoverTickSound);
    }

    public void OnClickFx()
    {
        myFx.PlayOneShot(ClickTickSound);
    }
}
