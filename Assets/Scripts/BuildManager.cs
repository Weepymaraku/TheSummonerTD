using UnityEngine;

public class BuildManager : MonoBehaviour
{
   public static BuildManager instance; 
   private TurretBlueprint turretToBuild;

   public GameObject buildEffect;
   

   private void Awake() {
       if(instance != null) {
        Debug.Log("Mes de un BuildManager a la escena");
       }
       instance = this;
   }
   
   public void TestBuilder() {
       Debug.Log("Test Builder");
   }
   /*
   void Start() {
       turretToBuild = standardTurretPrefab;
   }
   */

   public bool CanBuild { get {return turretToBuild != null; } } 

   public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost;}}

   public void SelectTurretToBuild(TurretBlueprint turret){
        turretToBuild = turret;
   }

   public void BuildTurretOn(Node node) {
            //SI no hi ha suficient parne return
            if(PlayerStats.Money < turretToBuild.cost ) {
                Debug.Log("No moneys");
                return;
            }
            //Hi ha suficient parne
            //Restem el cost de la torreta del parne del usuari
            PlayerStats.Money -= turretToBuild.cost;




            GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
            node.turret = turret;
            Debug.Log("Torreta comprada parne restant ::" + PlayerStats.Money);

            //LLencem l'efecte de construir torreta
            GameObject effect = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
            //Despres de 5 Segons destruim el objecte del efecte
            Destroy(effect,5f);


   }
}
