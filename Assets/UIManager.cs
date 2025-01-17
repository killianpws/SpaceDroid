using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text restartText;
    [SerializeField] private GameObject mapLimit;
    [SerializeField] private Text hpText;
    private bool isGameOver = false;
    void Start()
    {
        // Désactive le panneau et les textes si actives
        gameOverPanel.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
    }
    void Update()
    {
        if(gameObject.GetComponent<Droid>()){
            hpText.text = "HP: " + gameObject.GetComponent<Droid>().hp.ToString() + " | Fusée(s) : " + gameObject.GetComponent<Droid>().fuseeCount.ToString();
        }
        //ai la touche G est pressée, lance la méthode StartCoroutine qui affiche le panneau avec le message Game Over et
        // attends 5 séconds puis affiche un deuxième message invitant le jouer à presser R pour relancer le jeu
        if ((gameObject.GetComponent<Rigidbody>().position.y < mapLimit.transform.position.y || gameObject.GetComponent<Droid>().hp < 1) && !isGameOver)
        {
            isGameOver = true;
            StartCoroutine(GameOverSequence());
        }
        //si le jeu est terminé et on saisie la touche R, le jeu est relancé
        if (isGameOver)
        {
            //If R is hit, restart the current scene
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            //la touche Q nous permet de sortir du jeu à tout moment
            if (Input.GetKeyDown(KeyCode.Q))
            {
                print("Application Quit");
                Application.Quit();
            }
        }
    }
    //la méthode GameOverSequence affiche le message GameOver puis attends 3 séconds et affiche le message "Press R to restart"
    private IEnumerator GameOverSequence()
    {
        gameOverPanel.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        restartText.text = "Press R to restart";
        restartText.gameObject.SetActive(true);
    }
}