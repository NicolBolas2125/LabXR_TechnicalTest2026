using UnityEngine;

public class GenerateurDeGrille : MonoBehaviour
{
    // Référence unique vers cette classe
    static GenerateurDeGrille _instance;

    // Moyen d'accès hors de la classe
    public static GenerateurDeGrille instance
    {
        get
        {
            return _instance;
        }
    }
  
    // Référence vers l'instanciation d'une case
    [SerializeField]
    private CelluleDeGrille _celluleGrillePrefab;
    
    // Taile de la grille que l'on veut instancier
    [SerializeField]
    public int _tailleGrille;

    // La grille instanciée sous forme de matrice
    public CelluleDeGrille[,] _grille;



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
        _tailleGrille = TransfertInformation.instance.Taille;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Icic elle sert de fonction d'initialisation
    void Start()
    {
        // On génére une nouvelle matrice
        _grille = new CelluleDeGrille[_tailleGrille, _tailleGrille];

        // On parcourt toutes les cases de la grille
        for (int x = 0; x < _tailleGrille; x = x + 1)
        {
            for (int z = 0; z < _tailleGrille; z = z + 1)
            {
                // On y instaancie notre objet et on dit que la case est libre
                _grille[x, z] = Instantiate(_celluleGrillePrefab, new Vector3(x , 0, z), Quaternion.identity);
                _grille[x, z].Etat = CelluleDeGrille.EtatDeCellule.Libre;
            }
        }
        // On dit que le joueur apparaît en (0,0), la case est marquée comme occupée
        _grille[0, 0].Etat = CelluleDeGrille.EtatDeCellule.Occupee;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
