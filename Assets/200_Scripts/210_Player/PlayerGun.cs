using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject bullet;

    [SerializeField] private int shootForce, upwardForce;

    [SerializeField] private float timeBetweenShots;
    [SerializeField] private int magazineSize;

    [SerializeField] private Camera fpCam;
    [SerializeField] private Transform attackPoint; //Je récupère un point dans ma scène qui correspond au bout de mon canon

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shoot();
    }

    private void Shoot()
    {
        Ray ray = fpCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Crée un point de référence au centre de l'écran (à ne pas confondre avec le pointeur)
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit)) //Je lance un Raycast avec comme point de départ ma variable Ray, et je check s'il touche quelque chose
        {
            targetPoint = hit.point; //Je récupère le point de collision de mon Raycast
            if (hit.transform.tag == "Destroyable")
            {
                Destroy(hit.transform.gameObject); //Je détruis l'objet touché s'il a le tag "Destroyable"
            }
        }
        else
            targetPoint = ray.GetPoint(75); //S'il ne touche rien, je récupère un point vide à une distance assez loin

        Vector3 direction = targetPoint - attackPoint.position; //Je déduis la position de ma target à celle de mon canon (pour que la balle parte en direction du centre de la caméra)

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //Je crée une instance de la balle instanciée)

        currentBullet.transform.forward = direction.normalized; //Je définit dans quelle direction elle doit aller

        currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse); //Je lui applique une vélocité en direction de ma cible
        currentBullet.GetComponent<Rigidbody>().AddForce(fpCam.transform.up * upwardForce, ForceMode.Impulse);
    }
}
