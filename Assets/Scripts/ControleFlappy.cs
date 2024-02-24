using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleFlappy : MonoBehaviour
{

    public float vitesseX;
    public float vitesseY;
    public Sprite flappyBlesse;
    public Sprite flappyNormal;
    public GameObject pieceOr;
    public GameObject packVie;
    public GameObject champingon;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("d")){
            vitesseX = 3;
        }
        else if(Input.GetKey("a")){
            vitesseX = -2;
        }
        else{
            vitesseX = GetComponent<Rigidbody2D>().velocity.x;
        }
        
        if(Input.GetKeyDown("w")){
            vitesseY = 5;
        }
        else{
            vitesseY = GetComponent<Rigidbody2D>().velocity.y;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);
    }
    void OnCollisionEnter2D(Collision2D flappy){
        if(flappy.gameObject.name == "Colonne_0" || flappy.gameObject.name == "Colonne_1"){
            GetComponent<SpriteRenderer>().sprite = flappyBlesse;
        }
        else if(flappy.gameObject.name == "PieceOr_0"){
            flappy.gameObject.SetActive(false);
            Invoke("activePiece", 3.5f);
            pieceOr.transform.position = new Vector2(pieceOr.transform.position.x, Random.Range(-2f, 2f));
        }
        else if(flappy.gameObject.name == "PackVie_0"){
            flappy.gameObject.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = flappyNormal;
            Invoke("activePack", 3.5f);
            packVie.transform.position = new Vector2(packVie.transform.position.x, Random.Range(-2f, 2f));
        }
        else if(flappy.gameObject.name == "Champingon_0"){
            flappy.gameObject.SetActive(false);
            transform.localScale *= 1.3f;
            Invoke("activeChampingon", 3.5f);
            champingon.transform.position = new Vector2(champingon.transform.position.x, Random.Range(-2f, 2f));
        }
    }

    void activePiece(){
        pieceOr.SetActive(true);  
    }
    void activePack(){
        packVie.SetActive(true);
    }
    void activeChampingon(){
        champingon.SetActive(true);
        transform.localScale /= 1.3f;
    }
}
