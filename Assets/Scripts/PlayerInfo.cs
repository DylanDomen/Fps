using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;

    #region Sigleton
    private static PlayerInfo instance;
    public static PlayerInfo Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerInfo>();
            return instance;
        }
    }
    #endregion

    public TMP_Text textEndGame;
    public int life = 3;
    public int maxLife = 5;
    public bool canBeDamaged = true;
    public GameObject LifeBar;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(life == 0)
        {
            loose();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "monster")
        {
            // force appliqué sur le joueur
            float force = 10;
            // angle entre joueur et monstre
            Vector3 dir = collision.contacts[0].point - transform.position;
            //vecteur opposé
            dir = -dir.normalized;
            //pousser le joueur
            GetComponent<CharacterController>().Move(dir * force);
        }
    }

    public void heal(int gainLife)
    {
        life += gainLife;
        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }

    public void takeDamage()
    {
        life -= 1;
        canBeDamaged = false;
        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
        StartCoroutine(cooldown());
    }

    public void loose()
    {
        Time.timeScale = 0;
        textEndGame.text = "Vous avez perdu";
    }
    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canBeDamaged = true;
    }
}
