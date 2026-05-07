using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [Header("HP")]
    public int playerHP = 100;
    public int enemyHP = 100;

    [Header("Damage")]
    public int playerDamage = 20;
    public int enemyDamage = 15;

    public bool IsGameOver()
    {
        return playerHP <= 0 || enemyHP <= 0;
    }

    public bool PlayerWon()
    {
        return enemyHP <= 0;
    }

    public void PlayerAttack()
    {
        enemyHP -= playerDamage;
        enemyHP = Mathf.Clamp(enemyHP, 0, 100);

        Debug.Log("Player attacks! Enemy HP: " + enemyHP);
    }

    public void EnemyAttack()
    {
        playerHP -= enemyDamage;
        playerHP = Mathf.Clamp(playerHP, 0, 100);

        Debug.Log("Enemy attacks! Player HP: " + playerHP);
    }
}