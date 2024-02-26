using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleFlappy : MonoBehaviour
{

    public float vitesseX;
    public float vitesseY;
    public bool flappyBlesse;
    public bool finDePartie;

    public Sprite flappySpriteBlesse;
    public Sprite flappySpriteBlesseAile;
    public Sprite flappySpriteNormal;
    public Sprite flappySpriteNormalAile;

    public GameObject pieceOr;
    public GameObject packVie;
    public GameObject champingon;

    public AudioClip sonCol;
    public AudioClip sonOr;
    public AudioClip sonPack;
    public AudioClip sonChamp;
    public AudioClip sonFinDePartie;
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
            vitesseX = -3;
        }
        else{
            vitesseX = GetComponent<Rigidbody2D>().velocity.x;
        }
        
        if(Input.GetKeyDown("w")){
            vitesseY = 6;
            if(flappyBlesse == true){
                GetComponent<SpriteRenderer>().sprite = flappySpriteBlesseAile;
            }
            else{
                GetComponent<SpriteRenderer>().sprite = flappySpriteNormalAile;
            }
        }
        else{
            vitesseY = GetComponent<Rigidbody2D>().velocity.y;
        }
        if(Input.GetKeyUp("w")){
            vitesseY = 6;
            if(flappyBlesse == true){
                GetComponent<SpriteRenderer>().sprite = flappySpriteBlesse;
            }
            else{
                GetComponent<SpriteRenderer>().sprite = flappySpriteNormal;
            }
        }
        else{
            vitesseY = GetComponent<Rigidbody2D>().velocity.y;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);
    }
    void OnCollisionEnter2D(Collision2D flappy){
        if(flappy.gameObject.name == "Colonne_0" || flappy.gameObject.name == "Colonne_1"){
            GetComponent<SpriteRenderer>().sprite = flappySpriteBlesse;
            GetComponent<AudioSource>().PlayOneShot(sonCol);
            if(flappyBlesse == false){
                flappyBlesse = true;
            }
            else{
                flappyBlesse = true;
                finDePartie = true;
                GetComponent<Rigidbody2D>().freezeRotation = false;
                GetComponent<Collider2D>().enabled = false;
                GetComponent<AudioSource>().PlayOneShot(sonFinDePartie);
                Invoke("", 3f);
            }
        }
        else if(flappy.gameObject.name == "PieceOr_0"){
            flappy.gameObject.SetActive(false);
            Invoke("activePiece", 3.5f);
            pieceOr.transform.position = new Vector2(pieceOr.transform.position.x, Random.Range(-2f, 2f));
            GetComponent<AudioSource>().PlayOneShot(sonOr);
        }
        else if(flappy.gameObject.name == "PackVie_0"){
            flappy.gameObject.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = flappySpriteNormal;
            Invoke("activePack", 3.5f);
            packVie.transform.position = new Vector2(packVie.transform.position.x, Random.Range(-2f, 2f));
            GetComponent<AudioSource>().PlayOneShot(sonPack);
            if(flappyBlesse == true){
                flappyBlesse = false;
            }
            else{
                flappyBlesse = false;
            }
        }
        else if(flappy.gameObject.name == "Champingon_0"){
            flappy.gameObject.SetActive(false);
            transform.localScale *= 1.3f;
            Invoke("activeChampingon", 3.5f);
            champingon.transform.position = new Vector2(champingon.transform.position.x, Random.Range(-2f, 2f));
            GetComponent<AudioSource>().PlayOneShot(sonChamp);
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
    void recommencerPartie(){
        finDePartie = false;
    }
}
