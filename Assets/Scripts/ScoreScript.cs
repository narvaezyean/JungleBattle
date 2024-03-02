using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class ScoreScript : MonoBehaviour
{
    private float points;

    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textMesh.text = points.ToString("0");
    }

    public void ScorePoints(float entryPoints)
    {
        points += entryPoints;
    }
}

