using System.Collections;
using UnityEngine;

public class MouvementPion : MonoBehaviour
{
    // Référence unique vers cette classe
    static MouvementPion _instance;

    // Moyen d'accès hors de la classe
    public static MouvementPion instance
    {
        get
        {
            return _instance;
        }
    }

    // Liste des cibles à visiter dans l'ordre
    [SerializeField]
    private Vector3[] _cibles;
    // Moyen d'obtenir les cibles hors de la classe
    public Vector3[] Cibles
    {
        get {return _cibles;}
    }


    // Vitesse de déplacement
    [SerializeField] private float speed = 1f;

    // Position dans la liste de la cible que l'on vise (-1 signifie qu'on ne bouge pas)
    public int _cibleActuelle = -1;

    // Sert à avoir accès à la case d'origine pour la déverouiller après utilisation
    private Vector3 _positionDepart;

    // Sert à arrêter le mouvement si on recalcule un nouveau trajet (mur ajouté pendant le déplacement)
    private bool _peutAvoirNouvelleCible = true;

    // Dit à l'objet de ne pas bouger, sert de moyen pour attendre avant un déplacement (afin d'éviter une diagonale)
    private bool resterImmobile = true;

    // Est appelée si A* ne trouve pas de chemin vers la destination (destination murée/Enmurée)
    private IEnumerator lancerTrajetVide()
    {
        // Attendre d'être arrivé sur une case
        while (!resterImmobile)
        {
            yield return null;
        }
        // Détruit le trajet actuel s'il existe et réinitialise les variables
        _cibles = new Vector3[0];
        _cibleActuelle = -1;
    }

    // Lance le trajet calculé par A*
    private IEnumerator lancerTrajet(Vector2[] destinationsSurLaGrille)
    {
        // Convertit le trajet de Vector2 (position dans la grille) en Vector3 (position utilisable)
        Vector3[] trajet = new Vector3[destinationsSurLaGrille.Length];
        for (int arret = 0; arret < destinationsSurLaGrille.Length; arret++)
        {
            trajet[arret] = new Vector3(destinationsSurLaGrille[arret].x, 0, destinationsSurLaGrille[arret].y);
        }

        // Attendre d'être arrivé sur une case
        while (!resterImmobile)
        {
            yield return null;
        }
        // Initialise les variables proprement pour le trajet
        _cibles = trajet;
        _cibleActuelle = 0;
        // Occupe la prochaine case (qu'elle ne puisse plus être murée)
        GenerateurDeGrille.instance._grille[(int) _cibles[_cibleActuelle].x, (int) _cibles[_cibleActuelle].z].Etat = CelluleDeGrille.EtatDeCellule.Occupee;
        // Donne le nouveau point de départ (pour déverouiller à l'arriver) - Redondant en théorie mais sert de sécurité
        _positionDepart = new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z));
        // Libère le mouvement, cela lance l'action
        _peutAvoirNouvelleCible = true;
        resterImmobile = false;
    }
    
    // Appelle la bonne coroutine selon le mouvement reçu
    public void nouveauTrajet(Vector2[] destinationsSurLaGrille)
    {
        // On n'initie pas d'action si l'itinéraire reçu n'a pas d'arrêts
        if (destinationsSurLaGrille.Length == 0)
        {
            _peutAvoirNouvelleCible = false;
            GestionUIJeu.instance.callRetourUtilisateur("La case visée n'est pas disponible");
                       
            StartCoroutine(lancerTrajetVide());
            return;
        }
        // Si l'itinérraire reçu a des arrêts
        _peutAvoirNouvelleCible = false;
        GestionUIJeu.instance.callRetourUtilisateur("Le pion se dirige vers la case : " + ((int) destinationsSurLaGrille[destinationsSurLaGrille.Length - 1].x, (int) destinationsSurLaGrille[destinationsSurLaGrille.Length - 1].y).ToString());
                       
        StartCoroutine(lancerTrajet(destinationsSurLaGrille));
    }



    // On définie une référence unique pour accéder aux dimensions et à l'état des cases dans d'autres scripts
    void Awake()
    {
        if (instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Si on peut bouger
        if (!resterImmobile)
        {
            // Si on est pas encore arrivé à destination
            if (Vector3.Distance(_cibles[_cibleActuelle], transform.position) > 0.02f)
            {
                Vector3 direction = new Vector3(_cibles[_cibleActuelle].x - transform.position.x, _cibles[_cibleActuelle].y - transform.position.y, _cibles[_cibleActuelle].z - transform.position.z);
                direction = direction.normalized;
                transform.position = transform.position + direction * speed * Time.deltaTime;
            }
            else
            {
                // On est arrivé : on libère la case d'où on vient
                GenerateurDeGrille.instance._grille[(int) _positionDepart.x, (int) _positionDepart.z].Etat = CelluleDeGrille.EtatDeCellule.Libre;
                // On tente de passer à la cible suivante
                if (_peutAvoirNouvelleCible)
                {
                    // On redéfinie la position de départ pour le trajet suivant
                    _positionDepart = new Vector3(_cibles[_cibleActuelle].x, 0, _cibles[_cibleActuelle].z);
                    // On augmente la cible dans la liste de 1
                    _cibleActuelle += 1;
                    // Si on a atteint la fin de la liste des cibles
                    if (_cibleActuelle == _cibles.Length)
                    {
                        resterImmobile = true;
                        _cibleActuelle = -1;
                    }
                    else
                    {
                        // On verrouille la prochaine cible
                        GenerateurDeGrille.instance._grille[(int) _cibles[_cibleActuelle].x, (int) _cibles[_cibleActuelle].z].Etat = CelluleDeGrille.EtatDeCellule.Occupee;
                    }
                }
                else
                {
                    // Si on ne peut pas attribuer de nouvelle cible alors on s'arrête
                    resterImmobile = true;
                }

            }
        }
    }
}
