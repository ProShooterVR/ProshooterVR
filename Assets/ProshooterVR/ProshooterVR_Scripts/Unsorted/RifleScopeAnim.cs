using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleScopeAnim : MonoBehaviour
{

    public GameObject scope1, scope2;
    public GameObject sight1, sight2;
    

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

        yield return new WaitForSeconds(0.5f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (string.Compare(other.gameObject.name, "Head") == 0)
        {
            Debug.Log("In");
            // StartCoroutine(playAnim());
            scope1.SetActive(false);
            scope2.SetActive(true);
            sight1.SetActive(false);
            sight2.SetActive(true);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (string.Compare(other.gameObject.name, "Head") == 0)
        {
            //StartCoroutine(playAnim());
            scope1.SetActive(true);
            scope2.SetActive(false);

            sight1.SetActive(true);
            sight2.SetActive(false);
            Debug.Log("out");
        }
    }
}
