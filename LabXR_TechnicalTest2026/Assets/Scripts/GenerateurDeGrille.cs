using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField]
    private CelluleDeGrille _celluleGrillePrefab;
    
    [SerializeField]
    private int _tailleGrille;


    private CelluleDeGrille[,] _grille;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _grille = new CelluleDeGrille[_tailleGrille, _tailleGrille];

        for (int x = 0; x < _tailleGrille; x = x + 1)
        {
            for (int z = 0; z < _tailleGrille; z = z + 1)
            {
                _grille[x, z] = Instantiate(_celluleGrillePrefab, new Vector3(x,0,z), Quaternion.identity);
                _grille[x, z].State = CelluleDeGrille.EtatDeCellule.Libre;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
