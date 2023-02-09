using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
  public Text roundsText;
  

   void OnEnable() {
    roundsText.text = PlayerStats.Rounds.ToString();
  }

  public void Retry() {
    //Carrega la escena, pillant l'index de la escena actual, per resetejarla
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //SceneManagement
  } 

  public void Menu() {
    Debug.Log("Ves al Menu");
  } 
}
