using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.Android.Gradle;

public class ControleFlappy : MonoBehaviour
{

    public float vitesseX;
    public float vitesseY;
    public bool flappyBlesse;
    public bool finDePartie;
    public int pointage = 0;

    public TextMeshProUGUI txtPointage;
    public TextMeshProUGUI txtFinPartie;

    public Color controleAlpha;

    public Sprite flappySpriteBlesse;
    public Sprite flappySpriteBlesseAile;
    public Sprite flappySpriteNormal;
    public Sprite flappySpriteNormalAile;

    public GameObject pieceOr;
    public GameObject packVie;
    public GameObject champingon;
    public GameObject grille;

    public AudioClip sonCol;
    public AudioClip sonOr;
    public AudioClip sonPack;
    public AudioClip sonChamp;
    public AudioClip sonFinDePartie;
    // Start is called before the first frame update
    void Start()
    {
        txtFinPartie.GetComponent<TextMeshProUGUI>().fontSize = 0;
        
        controleAlpha = txtFinPartie.GetComponent<TextMeshProUGUI>().color;
        controleAlpha.a = -0f;
        txtFinPartie.GetComponent<TextMeshProUGUI>().color = controleAlpha;

        grille = GameObject.Find("grille");
    }

    // Update is called once per frame
    void Update()
    {
        if(finDePartie == false){
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
        else{
            Invoke("recommencerPartie", 3f);
            flappyBlesse = false;
        }
        if(finDePartie == true){
            if(txtFinPartie.GetComponent<TextMeshProUGUI>().fontSize < 100){
                    txtFinPartie.GetComponent<TextMeshProUGUI>().fontSize += 0.2f;
            }
            controleAlpha.a += 0.002f;
            txtFinPartie.GetComponent<TextMeshProUGUI>().color = controleAlpha;
        }

    }
        
    void OnCollisionEnter2D(Collision2D flappy){
        if(flappy.gameObject.name == "Colonne_0" || flappy.gameObject.name == "Colonne_1" || flappy.gameObject.name == "Decor"){
            GetComponent<SpriteRenderer>().sprite = flappySpriteBlesse;
            GetComponent<AudioSource>().PlayOneShot(sonCol);
            pointage -= 5;
            txtPointage.text = "Pointage : " + pointage;
            if(flappyBlesse == false){
                flappyBlesse = true;
            }
            else{
                flappyBlesse = true;
                finDePartie = true;
                GetComponent<Rigidbody2D>().freezeRotation = false;
                GetComponent<Collider2D>().enabled = false;
                GetComponent<AudioSource>().PlayOneShot(sonFinDePartie);
                txtFinPartie.gameObject.SetActive(true);
            }
        }
        else if(flappy.gameObject.name == "PieceOr_0"){
            flappy.gameObject.SetActive(false);
            Invoke("activePiece", 3.5f);
            pieceOr.transform.position = new Vector2(pieceOr.transform.position.x, Random.Range(-2f, 2f));
            GetComponent<AudioSource>().PlayOneShot(sonOr);
            pointage += 5;
            txtPointage.text = "Pointage : " + pointage;
            grille.GetComponent<Animator>().enabled = true;
            Invoke("fermerGrille", 4f);
            
        }
        else if(flappy.gameObject.name == "PackVie_0"){
            flappy.gameObject.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = flappySpriteNormal;
            Invoke("activePack", 3.5f);
            packVie.transform.position = new Vector2(packVie.transform.position.x, Random.Range(-2f, 2f));
            GetComponent<AudioSource>().PlayOneShot(sonPack);
            pointage += 5;
            txtPointage.text = "Pointage : " + pointage;
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
            pointage += 10;
            txtPointage.text = "Pointage : " + pointage;
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
        flappyBlesse = false;
        SceneManager.LoadScene("SampleScene");
    }
    void fermerGrille(){
        grille.GetComponent<Animator>().enabled = false;
    }
}
