using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public int score;
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
}
