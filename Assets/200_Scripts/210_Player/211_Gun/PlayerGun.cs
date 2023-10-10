using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerGun : MonoBehaviour
{
    [SerializeField] private int shootForce, upwardForce;

    [SerializeField] private float timeBetweenShots;
    [SerializeField] private int magazineSize;

    [SerializeField] private Camera fpCam;

    [SerializeField] private Barrel barrel;

    public ParticleManager particleManager;


    public TMP_Text texteRecharge;

    private bool texteActif;





    private void Start()
    {
        barrel = GetComponent<Barrel>();
    }

    private void Update()
    {

        if (Input.GetButtonDown("Fire1")) Shoot();

        if (barrel.barrelStock.Count == 0) particleManager.NextBullet(new Color(0, 0, 0, 0));
        else if (barrel.barrelStock.Count == 1) particleManager.NextBullet(barrel.barrelStock[0]);

        {
            if (texteActif)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    texteRecharge.gameObject.SetActive(false);
                    texteActif = false;
                }
            }
        }

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
                if (hit.transform.tag == "Destroyable")
                {
                    Destroy(hit.transform.gameObject); //Je d�truis l'objet touch� s'il a le tag "Destroyable"
                }

                //Je joue ma particule d'impacte � l'endroit du contact avec la couleur de l'�l�ment
                particleManager.Impact(barrel.barrelStock[0], targetPoint, hit.normal);
                if (barrel.barrelStock.Count > 1) particleManager.NextBullet(barrel.barrelStock[1]);
            }
            else
                targetPoint = ray.GetPoint(75); //S'il ne touche rien, je r�cup�re un point vide pour �viter une erreur

            barrel.RemoveStock(); //J'enl�ve de la liste la premi�re couleur
        }
        else
            Debug.Log("Reload");

        if (barrel.barrelStock.Count == 0)
        {
            texteRecharge.gameObject.SetActive(true);
            texteActif = true;

        }

    }



}
    
