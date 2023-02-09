using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;
    [Header("General")]
    public float range = 15f;
    [Header("Use Bullets (Default)")]
    public float fireRate = 1f;
    private float fireCountDown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public int damageOverTime = 40;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public float slowPct = .5f;
      
    [Header("Unity Setup Fields")]  
    public string enemyTag = "Enemy";
    public float turnSpeed = 10f;

    public Transform partToRotate;
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public Transform firePoint;




    void Start()
    {
        //Executa la funcio updatetarget a partir del segon 0 y cada 0.5 segons
        InvokeRepeating ("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget() {
        //Busca a la escena GameObjects amb el tag "Enemy" i els posa en una array de gameobjects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        //Settejem la variable com a infinit
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        
        foreach (GameObject enemy in enemies) {
            //BUsca la distancia entre aquest objecte y l'enemic que estem recorrent al bucle
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance) {
                //si la distancia al enemic enemic actual es mes petita que l'anterior pasa a ser la shortestDIstance 
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range) {
            //si l'enemic mes aprop esta a rango, target = a la posicio del enemic
            target = nearestEnemy.transform;
            targetEnemy = target.GetComponent<Enemy>();
        }else{
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) {
            //si no hi ha target no fem res
            if(useLaser){
                if(lineRenderer.enabled){
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

    if(useLaser) {
        Laser();
    }else{
        //Use Bullets
        if(fireCountDown <= 0f) {
            Shoot();
            fireCountDown = 1f/fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }

    
    }

    void Shoot() {
        //Debug.Log("Shoot");
        GameObject BulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = BulletGO.GetComponent<Bullet>();

        if(bullet != null) {
            bullet.Seek(target);
        }
    }

    //Cuan es selecciona el Objecte aquest fora del jos, nomes en modo edit
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);    
    }

    void LockOnTarget() {
        //Target Lock ON
        //La direccio = posicio del objectiu - posicio de aquest objecte
        Vector3 dir = target.position - transform.position;
        //per passar de la variable posicio a angles s'ha de posar el seguent
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Lerp serveix per suavitsar el moviment
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        // Ajustem la rotacio de la part que es mou amb angles euler al objecte partToRotate
        partToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);
    }

    void Laser() {

        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPct);

        if(!lineRenderer.enabled) {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0,firePoint.position);
        lineRenderer.SetPosition(1,target.position);

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

        impactEffect.transform.position = target.position + dir.normalized * 0.5f;
    }
}
