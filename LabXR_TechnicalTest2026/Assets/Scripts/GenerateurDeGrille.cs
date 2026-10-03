using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    // Référence vers l'instanciation d'une case
    [SerializeField]
    private CelluleDeGrille _celluleGrillePrefab;
    
    // Taile de la grille que l'on veut instancier
    [SerializeField]
    private int _tailleGrille;

    // La grille instanciée sous forme de matrice
    private CelluleDeGrille[,] _grille;

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
                _grille[x, z] = Instantiate(_celluleGrillePrefab, new Vector3(x,0,z), Quaternion.identity);
                _grille[x, z].State = CelluleDeGrille.EtatDeCellule.Libre;
            }
        }
        // On dit que le joueur apparaît en (0,0), la case est juste coloriée en rouge pour l'instant
        _grille[0, 0].GetComponent<MeshRenderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
