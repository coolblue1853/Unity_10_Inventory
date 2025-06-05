using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UIStatus : UIBase
{
    Character character;

    private TextMeshProUGUI _attackTxt;
    private TextMeshProUGUI _defenceTxt;
    private TextMeshProUGUI _healthTxt;
    private TextMeshProUGUI _criticalTxt;
    enum Txts
    {
        AttackTxt,
        DefenceTxt,
        HealthTxt,
        CriticalTxt,
    }


    protected override void Start()
    {
        base.Start();
        character = GameManager.Instance.Character;
        Bind<TextMeshProUGUI>(typeof(Txts));

        _attackTxt = Get<TextMeshProUGUI>((int)Txts.AttackTxt);
        _defenceTxt = Get<TextMeshProUGUI>((int)Txts.DefenceTxt);
        _healthTxt = Get<TextMeshProUGUI>((int)Txts.HealthTxt);
        _criticalTxt = Get<TextMeshProUGUI>((int)Txts.CriticalTxt);
        SetStatus();
    }


    void SetStatus()
    {
        _attackTxt.text = character.Stats.Attack.ToString();
        _defenceTxt.text = character.Stats.Defence.ToString();
        _healthTxt.text = character.Stats.Health.ToString();
        _criticalTxt.text = character.Stats.Critical.ToString();
    }

}
