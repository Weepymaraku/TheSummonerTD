using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour
{

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;
    
    private Color startColor;

    private Renderer rend;
    [Header("Optional")]
    public GameObject turret;

    BuildManager buildManager;
    void Start() {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition() {
        return transform.position + positionOffset;
    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {   
        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }

        
        if(!buildManager.CanBuild){
            Debug.Log("CAN BUILD");
            Debug.Log(buildManager.CanBuild);
            return;

        }

        if(turret != null) {
            Debug.Log("No es pot contruir aqui");
            return;
        }

        //Build a turret
        buildManager.BuildTurretOn(this);
    }

    void OnMouseEnter() {

        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        //Debug.Log("HOVER");
        if(!buildManager.CanBuild)
            return;

        if(buildManager.HasMoney) {
            rend.material.color = hoverColor;    
        } else {
            rend.material.color = notEnoughMoneyColor;
        }

         
    }

    void OnMouseExit() {
            rend.material.color = startColor;
    }
}
