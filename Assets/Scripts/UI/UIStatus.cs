using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UIStatus : UIBase
{

    private TextMeshProUGUI _attackTxt;
    private TextMeshProUGUI _attackSpeedTxt;
    private TextMeshProUGUI _defenceTxt;
    private TextMeshProUGUI _healthTxt;
    private TextMeshProUGUI _criticalTxt;
    enum Txts
    {
        AttackTxt,
        DefenceTxt,
        HealthTxt,
        CriticalTxt,
        AttackSpeedTxt,
    }
    protected override void Start()
    {
        base.Start();
        InitUI();
        // 최초 갱신
        if (_attackTxt.text == "")
            SetStatus(GameManager.Instance.Character);
    }
    public void InitUI()
    {
        Bind<TextMeshProUGUI>(typeof(Txts));

        _attackTxt = Get<TextMeshProUGUI>((int)Txts.AttackTxt);
        _attackSpeedTxt = Get<TextMeshProUGUI>((int)Txts.AttackSpeedTxt);
        _defenceTxt = Get<TextMeshProUGUI>((int)Txts.DefenceTxt);
        _healthTxt = Get<TextMeshProUGUI>((int)Txts.HealthTxt);
        _criticalTxt = Get<TextMeshProUGUI>((int)Txts.CriticalTxt);
    }
    public void SetStatus(Character character)
    {
        if (_attackTxt == null)
            return;

        _attackTxt.text = character.Stats.Attack.ToString();
        _attackSpeedTxt.text = character.Stats.AttackSpeed.ToString();
        _defenceTxt.text = character.Stats.Defence.ToString();
        _healthTxt.text = character.Stats.Health.ToString();
        _criticalTxt.text = character.Stats.Critical.ToString();
    }

}
