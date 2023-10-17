using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
public class PlayerGun : MonoBehaviour
{
    [SerializeField] private int shootForce, upwardForce;

    [SerializeField] private float timeBetweenShots;
    [SerializeField] private int magazineSize;

    [SerializeField] private Camera fpCam;

    [SerializeField] private Renderer nextBulletColor;

    private Barrel barrel;

    public ParticleManager particleManager;
    [SerializeField] private TextMeshProUGUI textMesh;
    private void Start()
    {
        barrel = GetComponent<Barrel>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shoot();
       

        if (Input.GetButtonDown("Dump")) Dump();

     
        if (barrel.barrelStock.Count > 0) textMesh.enabled = false;
        if (barrel.barrelStock.Count == 0) textMesh.enabled = true;
		if (barrel.barrelStock.Count == 0) nextBulletColor.material.color = Color.black;
        else if (barrel.barrelStock.Count >= 1) nextBulletColor.material.color = barrel.barrelStock[0];
    }

    private void Shoot()
    {
        if (barrel.barrelStock.Count > 0)
        {
            Ray ray = fpCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    // Obtenez la référence à l'ennemi s'il est touché par le raycast
                    EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();

                    if (enemyHealth != null)
                    {
                        // Appel de la fonction TakeDamage pour réduire les points de vie de l'ennemi
                        enemyHealth.TakeDamage(10);
                        Debug.Log("-10PV"); 
                    }
                }

                if (hit.transform.CompareTag("Destroyable") && (hit.collider.gameObject.layer == 0 || hit.collider.gameObject.layer == 10))
                {
                    Destroy(hit.transform.gameObject);
                }

                //Je joue ma particule d'impacte à l'endroit du contact avec la couleur de l'élément
                particleManager.Impact(barrel.barrelStock[0], hit.point, hit.normal);
            }
            barrel.RemoveStock();
        }
        else if (!Cursor.visible)
            Debug.LogWarning("Il n'y a pas de balle dans le barillet !");
    }
    private void Dump()
    {
        barrel.barrelStock.Clear();
    }
}