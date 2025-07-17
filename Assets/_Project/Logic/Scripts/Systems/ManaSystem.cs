using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ManaSystem : Singleton<ManaSystem>
{
    private const int MAX_MANA = 3;

    [SerializeField] private ManaUI _manaUI;

    private int _currentMana = MAX_MANA;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<SpendManaGA>(SpendManaPerformer);
        ActionSystem.AttachPerformer<RefillManaGA>(RefillManaPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<SpendManaGA>();
        ActionSystem.DetachPerformer<RefillManaGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    private void Start()
    {
        _manaUI.UpdateManaText(MAX_MANA);
    }

    public bool HasEnoughMana(int mana)
    {
        return _currentMana >= mana;
    }

    //Performers

    private IEnumerator SpendManaPerformer(SpendManaGA spendManaGA)
    {
        _currentMana -= spendManaGA.Amount;
        _manaUI.UpdateManaText(_currentMana);
        yield return null;
    }

    private IEnumerator RefillManaPerformer(RefillManaGA refillManaGA)
    {
        _currentMana = MAX_MANA;
        _manaUI.UpdateManaText(_currentMana);
        yield return null;
    }

    //Reactions

    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        RefillManaGA refillManaGA = new();
        ActionSystem.Instance.AddReaction(refillManaGA);
    }
}
