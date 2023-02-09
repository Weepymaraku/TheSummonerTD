using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint misileLauncher;
    public TurretBlueprint laserBeamer;
    BuildManager buildManager;
    void Start(){
        buildManager = BuildManager.instance;
    }
   public void SelectStandadTurret(){
       Debug.Log("Torreta Noramal~~");
       buildManager.SelectTurretToBuild(standardTurret);
   }
   public void SelectMisileLauncher(){
       Debug.Log("Misile Launcher~~");
       buildManager.SelectTurretToBuild(misileLauncher);
   }
   public void SelectLaserBeamer(){
       Debug.Log("Laser Beamer~~");
       buildManager.SelectTurretToBuild(laserBeamer);
   }
   
}
