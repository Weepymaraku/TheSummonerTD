
using UnityEngine;

public class Waypoints : MonoBehaviour
{
   public static Transform[] points;

      /// Awake is called when the script instance is being loaded.
   /// </summary>
   void Awake()
   {
      //Fem que al començar el joc posi tots els waypoints en una array per no tinguer que estarho calculant tot el rato
        points = new Transform[transform.childCount];
        for( int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);   
        }     
   }
}
