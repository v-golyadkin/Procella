using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ManaSystem : Singleton<ManaSystem>
{
    private const int MAX_MANA = 3;

    [SerializeField] private ManaUI manaUI;

    private int _currentMana = MAX_MANA;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<SpendManaGA>(SpendManaPerformer);
        ActionSystem.AttachPerformer<RefillManaGA>(RefillManaPerformer);
        //ActionSystem.AttachPerformer<StartBattleGA>(StartBattlePerformer);
        ActionSystem.SubscribeReaction<StartBattleGA>(OnStartBattle, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<SpendManaGA>();
        ActionSystem.DetachPerformer<RefillManaGA>();
        //ActionSystem.DetachPerformer<StartBattleGA>();
        ActionSystem.UnsubscribeReaction<StartBattleGA>(OnStartBattle, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    private void Start()
    {
        manaUI.UpdateManaText(MAX_MANA);
    }

    public bool HasEnoughMana(int mana)
    {
        return _currentMana >= mana;
    }

    //Performers

    private IEnumerator SpendManaPerformer(SpendManaGA spendManaGA)
    {
        _currentMana -= spendManaGA.Amount;
        manaUI.UpdateManaText(_currentMana);
        yield return null;
    }

    private IEnumerator RefillManaPerformer(RefillManaGA refillManaGA)
    {
        _currentMana += refillManaGA.Amount;

        if(_currentMana > MAX_MANA)
        {
            _currentMana = MAX_MANA;
        }

        yield return new WaitForSeconds(0.25f); // delay before text update
        manaUI.UpdateManaText(_currentMana);
        yield return null;
    }

    private IEnumerator StartBattlePerformer(StartBattleGA startBattleGA)
    {
        _currentMana = MAX_MANA;
        manaUI.UpdateManaText(_currentMana);

        Debug.Log("Mana System: Start Battle Performer");

        yield return null;
    }

    //Reactions

    private void OnStartBattle(StartBattleGA startBattleGA)
    {
        _currentMana = MAX_MANA;
        manaUI.UpdateManaText(_currentMana);

        Debug.Log("Mana System: OnStartBattle");
    }

    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        RefillManaGA refillManaGA = new(MAX_MANA);
        ActionSystem.Instance.AddReaction(refillManaGA);
    }
}
