using UnityEngine;
using System.Collections.Generic;

public class MoleGame : MonoBehaviour
{
    [SerializeField] private List<MoleBehaviour> moles = new List<MoleBehaviour>();

    private void Start()
    {
        GameStart();
    }

    private void GameStart()
    {
        DrawMole();
    }

    private void DrawMole()
    {
        int index;
        index = Random.Range(0, moles.Count);
        MoleBehaviour chosenMole = moles[index];

        chosenMole.RiseUp();
    }
}
