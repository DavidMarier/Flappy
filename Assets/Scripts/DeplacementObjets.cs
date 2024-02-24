using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementObjets : MonoBehaviour
{

    public float vitesse;
    public float positionDebut;
    public float positionFin;
    public float deplacementAleatoire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-vitesse, 0f, 0f);

        float valeurAleatoireY = Random.Range(-deplacementAleatoire, deplacementAleatoire);
        
        if(transform.position.x <= positionDebut){
            transform.position = new Vector2(positionFin, valeurAleatoireY);
        }
 
    }
}
