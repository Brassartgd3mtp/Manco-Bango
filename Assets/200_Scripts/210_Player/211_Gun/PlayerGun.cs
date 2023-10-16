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
            Ray ray = fpCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Cr�e un point de r�f�rence au centre de l'�cran (� ne pas confondre avec le pointeur)
            RaycastHit hit;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit)) //Je lance un Raycast avec comme point de d�part ma variable Ray, et je check s'il touche quelque chose
            {
                targetPoint = hit.point; //Je r�cup�re le point de collision de mon Raycast
                if (hit.transform.tag == "Destroyable" && hit.collider.gameObject.layer == 0 || hit.collider.gameObject.layer == 10)
                {
                    Destroy(hit.transform.gameObject); //Je d�truis l'objet touch� s'il remplis les conditions
                }
                //Je joue ma particule d'impacte � l'endroit du contact avec la couleur de l'�l�ment
                particleManager.Impact(barrel.barrelStock[0], targetPoint, hit.normal);
            }
            else
                targetPoint = ray.GetPoint(75); //S'il ne touche rien, je r�cup�re un point vide pour �viter une erreur

            barrel.RemoveStock(); //J'enl�ve de la liste la premi�re couleur
        }
        else if (!Cursor.visible)
            Debug.LogWarning("Il n'y a pas de balle dans le barillet !");
    }

    private void Dump()
    {
        barrel.barrelStock.Clear();
    }
}