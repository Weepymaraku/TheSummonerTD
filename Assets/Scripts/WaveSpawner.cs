
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
  public Transform enemyPrefab;

  public float timeBetweenWaves = 5f;
  private float countDown = 2f;
  private int waveNumber = 0;
  
  public Transform spawnPoint;

  public Text waveCountDownText;

  void Update() {
      if(countDown <= 0f) {
        //Executem la funcio spawnwave com una subrutina en comptes de ferho a cada frame com amb lupdate
         StartCoroutine(SpawnWave());
          
          countDown = timeBetweenWaves;
      }

    countDown -= Time.deltaTime;
    countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
    //modificam el text del UI
    waveCountDownText.text = Mathf.Round(countDown).ToString();
    // waveCountDownText.text = string.Format("{0:00.00}", countDown);
  }

//hem de crear la funcion amb ienumerator per cridarho com una subrutina
  IEnumerator SpawnWave() {
        Debug.Log("Wave incomming");
        waveNumber++;
        PlayerStats.Rounds++;
        for (int i = 0; i < waveNumber; i++)
        {
                
            SpawnEnemy();
            //y s'hna de acabar aixi, y fem que esperi mig segon abans de que es torni a executar
            yield return new WaitForSeconds(0.5f);
        }

        

  }

  void SpawnEnemy() {
    // Crea un nou item enemyprefab, a la posicio on tenim l'spawn i amb la mateixa encaracio
      Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
  }

}
