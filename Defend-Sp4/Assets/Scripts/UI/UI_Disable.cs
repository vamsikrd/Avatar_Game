using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Disable : MonoBehaviour
{
    //called by the animation event on the wave image
    public void WaveButtonDisable()
    {
        this.gameObject.SetActive(false);
    }
}
