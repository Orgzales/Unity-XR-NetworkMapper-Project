using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDiagnostics : MonoBehaviour
{
    public bool DiagnosticsEnabled = false; //windows testing

    void Start()
    {
        if (!DiagnosticsEnabled)
        {
            foreach (Transform child in transform)
            {
                if (child.name.Contains("Diagnostics"))
                {
                    child.gameObject.SetActive(DiagnosticsEnabled);
                }
            }
        }

    }

}
