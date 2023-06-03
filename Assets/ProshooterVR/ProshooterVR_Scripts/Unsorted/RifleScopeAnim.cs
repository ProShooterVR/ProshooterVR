using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleScopeAnim : MonoBehaviour
{

    public GameObject scope1, scope2, fadeAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator playAnim()
    {
        fadeAnim.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        fadeAnim.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (string.Compare(other.gameObject.name, "Head") == 0)
        {
            Debug.Log("In");
           // StartCoroutine(playAnim());
            scope1.SetActive(false);
            scope2.SetActive(true);
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (string.Compare(other.gameObject.name, "Head") == 0)
        {
            //StartCoroutine(playAnim());
            scope1.SetActive(true);
            scope2.SetActive(false);
            Debug.Log("out");
        }
    }
}
