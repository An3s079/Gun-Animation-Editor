using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Enable2Handed : MonoBehaviour
{
    public GameObject Hand2;
    public GameObject ToggleHand;

    public void OnToggled2Handed()
    {
        if(Hand2.activeSelf == true)
            Hand2.SetActive(false);
        else
            Hand2.SetActive(true);
        Hand2.transform.position = OnImportImagesPressed.readPoint;

        // if(ToggleHand.activeSelf == true)
        //     ToggleHand.SetActive(false);
        // else
        //     ToggleHand.SetActive(true);
    }
}
