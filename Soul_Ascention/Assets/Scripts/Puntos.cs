using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puntos : MonoBehaviour
{
    public TextMeshProUGUI puntos;
    public int gemas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        puntos.text = gemas.ToString();
    }

    public void sumarPuntos(){
        gemas += 50;
    }
}
