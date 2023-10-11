using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private int shootForce, upwardForce;

    [SerializeField] private float timeBetweenShots;
    [SerializeField] private int magazineSize;

    [SerializeField] private Camera fpCam;

    [SerializeField] private Barrel barrel;

    [SerializeField] private ParticleSystem impact;
    public ParticleManager particleManager;

    private void Start()
    {
        barrel = GetComponent<Barrel>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shoot();
        if (Input.GetButtonDown("Dump")) Dump();

        if (barrel.barrelStock.Count == 0) particleManager.NextBullet(new Color(0, 0, 0, 0));
        else if (barrel.barrelStock.Count == 1) particleManager.NextBullet(barrel.barrelStock[0]);
    }

    private void Shoot()
    {
        if (barrel.barrelStock.Count > 0)
        {
            Ray ray = fpCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Crée un point de référence au centre de l'écran (à ne pas confondre avec le pointeur)
            RaycastHit hit;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit)) //Je lance un Raycast avec comme point de départ ma variable Ray, et je check s'il touche quelque chose
            {
                targetPoint = hit.point; //Je récupère le point de collision de mon Raycast
                if (hit.transform.tag == "Destroyable" && hit.collider.gameObject.layer == 0 || hit.collider.gameObject.layer == 10)
                {
                    Destroy(hit.transform.gameObject); //Je détruis l'objet touché s'il remplis les conditions
                }

                //Je joue ma particule d'impacte à l'endroit du contact avec la couleur de l'élément
                particleManager.Impact(barrel.barrelStock[0], targetPoint, hit.normal);
                if (barrel.barrelStock.Count > 1) particleManager.NextBullet(barrel.barrelStock[1]);
            }
            else
                targetPoint = ray.GetPoint(75); //S'il ne touche rien, je récupère un point vide pour éviter une erreur

            barrel.RemoveStock(); //J'enlève de la liste la première couleur
        }
        else
            Debug.LogWarning("Il n'y a pas de balle dans le barillet !");
    }

    private void Dump()
    {
        barrel.barrelStock.Clear();
    }
}