using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disapear : MonoBehaviour
{
    [SerializeField]
    float disapearTime;

    private void OnEnable()
    {
        StartCoroutine(DisapearCortine());
    }


    IEnumerator DisapearCortine()
    {
        yield return new WaitForSeconds(disapearTime);
        this.gameObject.SetActive(false);
    }

}
