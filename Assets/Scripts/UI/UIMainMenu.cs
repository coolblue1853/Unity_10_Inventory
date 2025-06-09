using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIBase
{
    private AudioManager _audioManager;

    private GameObject _statusObj;
    private GameObject _InventoryObj;
    private Button _statusBtn;
    private Button _InventoryBtn;
    private Button _mainBtn;
    private TextMeshProUGUI _jobTxt;
    private TextMeshProUGUI _nameTxt;
    private TextMeshProUGUI _levelTxt;
    private TextMeshProUGUI _expTxt;
    private TextMeshProUGUI _descriptionTxt;
    private TextMeshProUGUI _coinTxt;
    private Image _expImg;

    bool isInit = false;

    enum Btn
    {
        StatusBtn,
        InventoryBtn,
        MainBtn,
    }
    enum Txts
    {
        JobTxt,
        NameTxt,
        LevelTxt,
        ExpTxt,
        DescriptionTxt,
        CoinTxt,
    }
    enum Images
    {
        ExpBar,
    }
    protected override void Start()
    {
        base.Start();
        _audioManager = GameManager.Instance.AudioManager;

        // 자동화된 UI 연결
        Bind<Button>(typeof(Btn));
        Bind<TextMeshProUGUI>(typeof(Txts));
        Bind<Image>(typeof(Images));

        _statusBtn = Get<Button>((int)Btn.StatusBtn);
        _InventoryBtn = Get<Button>((int)Btn.InventoryBtn);
        _mainBtn = Get<Button>((int)Btn.MainBtn);
        _jobTxt = Get<TextMeshProUGUI>((int)Txts.JobTxt);
        _nameTxt = Get<TextMeshProUGUI>((int)Txts.NameTxt);
        _levelTxt = Get<TextMeshProUGUI>((int)Txts.LevelTxt);
        _expTxt = Get<TextMeshProUGUI>((int)Txts.ExpTxt);
        _descriptionTxt = Get<TextMeshProUGUI>((int)Txts.DescriptionTxt);
        _coinTxt = Get<TextMeshProUGUI>((int)Txts.CoinTxt);
        _expImg = Get<Image>((int)Images.ExpBar);

        _statusObj = _uIManager.UIStatus.gameObject;
        _InventoryObj = _uIManager.UIInventory.gameObject;

        // 버튼 함수 지정
        _mainBtn.onClick.AddListener(OpenMainMenu);
        _statusBtn.onClick.AddListener(OpenStatus);
        _InventoryBtn.onClick.AddListener(OpenInventory);

        OpenMainMenu();
        isInit = true;
        // 최초 데이터 갱신 요청
        GameManager.Instance.SetData();
    }
    void OpenMainMenu()
    {
        if (isInit)
        {
            _audioManager.PlaySFX("CancelBtn");
        }

        _uIManager.ResetUI();
        ResetBtn(Define.Button.Main);
    }
    void OpenStatus()
    {
        _audioManager.PlaySFX("SelectBtn");
        _uIManager.ResetUI();
        ResetBtn(Define.Button.Status);
        _statusObj.SetActive(true);
    }
    void OpenInventory()
    {
        _audioManager.PlaySFX("SelectBtn");
        _uIManager.ResetUI();
        ResetBtn(Define.Button.Inventory);
        _InventoryObj.SetActive(true);
    }
    public void SetDetail(Character character)
    {
        _jobTxt.text = character.Stats.Job.Name;
        _nameTxt.text = character.Name;
        _levelTxt.text = $"LV {character.Stats.Level}";
        _expTxt.text = $"{character.Stats.NowExp} / {character.Stats.MaxExp}";
        _expImg.fillAmount = (float)character.Stats.NowExp / character.Stats.MaxExp;
        _coinTxt.text = Utils.FormatBigInteger(character.Stats.Coin);
        _descriptionTxt.text = character.Stats.Job.Description;
    }

    private void ResetBtn(Define.Button type)
    {

        _mainBtn.gameObject.SetActive(false);
        _statusBtn.gameObject.SetActive(false);
        _InventoryBtn.gameObject.SetActive(false);

        switch (type)
        {
            case Define.Button.Main:
                _statusBtn.gameObject.SetActive(true);
                _InventoryBtn.gameObject.SetActive(true);
                break;
            default:
                _mainBtn.gameObject.SetActive(true);
                break;
        }
    }
}
