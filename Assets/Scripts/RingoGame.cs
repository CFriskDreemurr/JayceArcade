using UnityEngine;

public class RingoGame : MonoBehaviour
{
    public static RingoGame Instance { get; private set; }

    private int _points;

    private void Awake()
    {
        if (Instance == null)Instance = this;
        else Destroy(gameObject);
    }
    public void GameStart()
    {
        _points = 0;
    }

    public void GetPoints(int points)
    {
        _points += points;
    }
}
