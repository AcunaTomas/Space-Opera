using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected GameObject player;
    protected GameObject enemy;
    protected Animator animator;
    protected EnemyBehaviour2 enemyStats;

    public virtual void Enter() {}
    public virtual void Do() {}
    public virtual void FixedDo() {}
    public virtual void Exit() {}

    public void Setup(GameObject playerRef, GameObject enemyRef, Animator animatorRef)
    {
        player = playerRef;
        enemy = enemyRef;
        animator = animatorRef;
    }
}
