using UnityEngine;

public class EnableDisableObject : MonoBehaviour
{
    public GameObject objectToToggle1;
    public GameObject objectToToggle2;

    private bool isObject1Active = true;

    // This method will be called when the button is clicked
    public void OnButtonClick()
    {
        // Toggle the visibility of the objects based on the current state
        isObject1Active = !isObject1Active;
        objectToToggle1.SetActive(isObject1Active);
        objectToToggle2.SetActive(!isObject1Active);
    }
}
