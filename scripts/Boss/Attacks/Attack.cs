using UnityEngine;

public class Attack : MonoBehaviour
{
	public AttackStage attackStage = AttackStage.Ready;

	public virtual void StartAttack() { }

	public void ResetAttack()
	{
		attackStage = AttackStage.Ready;
	}

	public virtual int WeightAttack()
	{
		return 0;
	}
}
