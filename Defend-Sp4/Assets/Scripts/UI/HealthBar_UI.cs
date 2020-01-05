using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    public GameObject healthSliderPrefab;
    public Transform target;

    private Transform ui;
    private Image healthImage;

    public Image HealthImage
    {
        get { return healthImage; }
    }
    public Transform UI
    {
        get { return ui; }
    }

    private void Awake()
    {
        Canvas[] allCav = FindObjectsOfType<Canvas>();
        foreach(Canvas c in allCav )
        {
            if(c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(healthSliderPrefab, c.transform).transform;
                healthImage = ui.GetChild(0).GetComponent<Image>();
                break;
            }
        }
    }

    private void LateUpdate()
    {
        if (ui == null) return;
        ui.transform.position = target.transform.position;
        ui.forward = -Camera.main.transform.forward;
    }


} //Class
