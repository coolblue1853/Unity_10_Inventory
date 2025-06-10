# 스파르타코딩클럽 심화 10기_3조 설민우 인벤토리 프로젝트 입니다

# 인벤토리

스파르타 코딩클럽 10기, 유니티 심화 개인 프로젝트 인벤토리 과제작업물입니다.
기존 Step에 부가적인 기능들을 몇가지 추가하고
별도로 절차적 맵 생성과 FSM 으로 3D 방치형 게임 링크도 첨부했습니다.

## 📷 스크린샷


## 빌드 파일 주소

## 시연 영상 주소

## 3D 방치형 (절차적 맵 생성 & FSM) 레퍼지토리 링크


## 🕹️ 기능
<details>
<summary><input type="checkbox" checked disabled> (필수)Step 1. UI 구성하기 </summary>

![Step1](https://github.com/user-attachments/assets/3a4e2ce2-a1a9-4f4f-b3c8-225ccfbdab84)

- 메인, 능력치, 인벤토리 UI를 구성 하였습니다.

</details>
<details>
<summary><input type="checkbox" checked disabled> (필수)Step 2. 스크립트 만들기 </summary>

![image](https://github.com/user-attachments/assets/239269d1-8417-4a60-a519-b6ef2ede6117)

- 기본이 되는 스크립트들 생성.
- SerializedField 를 이용해서 각 UI 연결 

</details>
<details>
<summary><input type="checkbox" checked disabled> (필수)Step 3. UI간 전환 만들기 </summary>

![Step1](https://github.com/user-attachments/assets/3a4e2ce2-a1a9-4f4f-b3c8-225ccfbdab84)

```
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
    // 메인 UI 
    void OpenMainMenu()
    {
        if (isInit)
        {
            _audioManager.PlaySFX("CancelBtn");
        }

        _uIManager.ResetUI();
        ResetBtn(Define.Button.Main);
    }
    // 능력치 UI
    void OpenStatus()
    {
        _audioManager.PlaySFX("SelectBtn");
        _uIManager.ResetUI();
        ResetBtn(Define.Button.Status);
        _statusObj.SetActive(true);
    }
    // 인벤토리 UI
    void OpenInventory()
    {
        _audioManager.PlaySFX("SelectBtn");
        _uIManager.ResetUI();
        ResetBtn(Define.Button.Inventory);
        _InventoryObj.SetActive(true);
    }
    // 메인 화면의 능력치 표기
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
    // 버튼 리셋
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

```
- UIMainMenu 에서 장면 전환에 관련된 변수, 버튼들을 관리
- UI 자동 Bind, Get 을 이용하여 버튼에 함수 할당(스크립트)

</details>
<details>
<summary><input type="checkbox" checked disabled> (필수)Step 4. 캐릭터 정보 세팅하기 </summary>

![Step4](https://github.com/user-attachments/assets/77b2fd61-bdd2-42d3-9d44-7964880c5d93)

```
using System;
using UnityEngine;
using System.Numerics;

// 스탯 종류 구조체
[System.Serializable]
public struct CharacterStats
{
    public JobInfo Job;
    public int Level;
    public int MaxExp;
    public int NowExp;
    public int Attack;
    public int AttackSpeed;
    public int Defence;
    public int Health;
    public int Critical;
    public BigInteger Coin;


    public CharacterStats(JobInfo job,int level, int maxExp, int nowExp, int attack, int attackSpeed, int defence, int health, int critical, BigInteger coin)
    {
        Job = job;
        Level = level;
        MaxExp = maxExp;
        NowExp = nowExp;
        Attack = attack;
        AttackSpeed = attackSpeed;
        Defence = defence;
        Health = health;
        Critical = critical;
        Coin = coin;
    }
}
public class Character : MonoBehaviour
{
    private PlayerController _playerController;
    [SerializeField] private string _name;
    // 옵저버 패턴
    public event Action<CharacterStats> OnStatsChanged;
    public event Action<BigInteger> OnCoinChanged;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    [SerializeField]
    private CharacterStats _stats;
    public CharacterStats Stats
    {
        get => _stats;
        set
        {
            CharacterStats oldStats = _stats;
            _stats = value;

            if (oldStats.Coin != _stats.Coin)
            {
                OnCoinChanged?.Invoke(_stats.Coin);
            }
            OnStatsChanged?.Invoke(_stats);
        }
    }
    public Inventory Inventory { get; private set; }

    public void Init(string name, CharacterStats stats, Inventory inventory)
    {
        _name = name;
        _stats = stats;
        Inventory = inventory;
        Inventory.Character = this;
        _playerController.InitPlayer(this);
    }
}

```
- 추후 서술할 CharacterStatData 스크립터블 오브젝트를 이용하여 정보 반영
- 정보 갱신은 옵저버 패턴 이용

</details>
<details>
<summary><input type="checkbox" checked disabled> (필수)Step 5. UISlot 동적 생성하기 </summary>

![Step5](https://github.com/user-attachments/assets/49016472-7ae6-4b01-a2b2-ed27d53e3547)

```
    private void Init()
    {
        for (int i = 0; i < Constant.InventoryCount; i++)
        {
            var go = Instantiate(_slot);
            go.transform.SetParent(_slotGroup.transform);
            go.transform.localScale = Vector3.one;
            var slot = go.GetComponent<UISlot>();
            slot.ResetSlot(i);
            _slots.Add(slot);
        }
        SetInvenCount();
        _inventory.AddObserver(this);
    }
    // 인벤토리 아이템수 / 슬롯수 갱신
    private void SetInvenCount()
    {
        int itemCount = 0;
        for (int i = 0; i < Constant.InventoryCount; i++)
        {
            if (_inventory.Items[i] != null)
                itemCount++;
        }
        _slotTxt.text = $"Inventory {itemCount} / {Constant.InventoryCount}";
    }
```
```
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    public int Idx = -1;
    private Inventory _inventory;
    private Character _character;
    public ItemData Item = null;
    private bool _isEquiped = false;

    [SerializeField] private Image _icon;
    [SerializeField] private Image _base;
    [SerializeField] private GameObject _equiped;


    private void Start()
    {
        _inventory = GameManager.Instance.Character.Inventory;
        _character = GameManager.Instance.Character;
    }
    // 아이콘 업데이트
    public void UpdateIcon(ItemData item)
    {
        _icon.sprite = item.Icon;
        _icon.color = Constant.Alpha255;
        switch (item.Rarity)
        {
            case ItemRarity.Common:
                _base.color = Constant.White;
                break;
            case ItemRarity.Uncommon:
                _base.color = Constant.Green;
                break;
            case ItemRarity.Rare:
                _base.color = Constant.Blue;
                break;
            case ItemRarity.Unique:
                _base.color = Constant.Pink;
                break;
            case ItemRarity.Legendary:
                _base.color = Constant.Orange;
                break;

        }
    }

    public void SetItem(ItemData item)
    {
        Item = item;
    }
    // 장착여부 업데이트

    // UI 초기화 함수
    public void ResetSlot(int idx = -1)
    {
        if(idx != -1)
            Idx = idx;

        Item = null;
        _icon.sprite = null;
        _icon.color = Constant.Alpha0;
    }

    public void OnClickItemSlot()
    {
        if (_inventory.IsSellMode)
            SellItem();
        else
            ToggleEquip();
    }
    // 아이템 판매
    public void SellItem()
    {
        if (Item == null)
            return;

        //코인 추가
        var stats = _character.Stats;
        stats.Coin += Constant.sellCost / 2;
        _character.Stats = stats;

        // 인벤토리 갱신
        _base.color = Constant.White;
        if (_isEquiped)
        {
            _isEquiped = false;
            _equiped.SetActive(false);
            _inventory.RemoveItemStat(Item);
        }
        _inventory.DeleteItem(Idx);


        ResetSlot();
    }

    // UI 등장 합수
    public void ToggleEquip()
    {
        if (Item == null)
            return;

        if (!_isEquiped)
        {
            _isEquiped = true;
            _equiped.SetActive(true);
            _inventory.ApplyItemStat(Item);
        }
        else
        {
            _isEquiped = false;
            _equiped.SetActive(false);
            _inventory.RemoveItemStat(Item);
        }
    }
}

```

- 미리 준비된 Slot 프리팹을 지정한 만큼 Instantiate
- Inventory의 Item 배열 또한 같은 크기만큼 초기화

</details>
<details>
<summary><input type="checkbox" checked disabled> (필수)Step 6. Item 데이터 준비하기 </summary>


```

```
- 

</details>
<details>
<summary><input type="checkbox" checked disabled> (필수)Step 7. 아이템 장착 </summary>


```

```
- 

</details>
<details>
<summary><input type="checkbox" checked disabled> (필수)Step 8. Status에 아이템 정보 반영 </summary>


```

```
- 

</details>
<details>
<summary><input type="checkbox" checked disabled> (추가) 1. ScriptableObject 데이터 관리 </summary>


```

```
- 

</details>
<details>
<summary><input type="checkbox" checked disabled> (추가) 2. 아이템 뽑기 </summary>


```

```
- 

</details>
<details>
<summary><input type="checkbox" checked disabled> (추가) 3. 아이템 등급 분리 </summary>


```

```
- 

</details>
<details>
<summary><input type="checkbox" checked disabled> (추가) 4. 자동 전투와 연출 </summary>


```

```
- 

</details>
<details>
<summary><input type="checkbox" checked disabled> (추가) 5. 사운드 시스템 </summary>


```

```
- 

</details>
<details>
<summary><input type="checkbox" checked disabled> (추가) 6. 통화 시스템과 BigInteger </summary>


```

```
- 

</details>
<details>
<summary><input type="checkbox" checked disabled> (추가) 7. 아이템 판매 </summary>


```

```
- 

</details>

## 🛠️ 기술 스택

- C#
- .NET Core 3.1
- Unity 22.3.17f1

## 🧙 사용법

1. 이 저장소를 클론하거나 PC 빌드를 다운받습니다,
2. 조작은 마우스로만 진행합니다.
3. 인벤토리에서 랜덤 아이템을 뽑아 장착합니다.
4. Coin을 얻습니다.
   
## 🗂️ 프로젝트 구조
<details>
<summary><input type="checkbox" checked disabled> 펼쳐보기 </summary>

```
├── Camera
│ ├── CameraAspectFixer.cs
│ └── CameraController.cs
│
├── Enemy
│ └── TestEnemy.cs
│
├── Item
│ ├── ItemData.cs
│ ├── ItemObject.cs
│ └── ItemSlot.cs
│
├── Manager
│ └── UIManager.cs
│
├── Map
│ ├── JumpPlatform.cs
│ ├── LaunchPlatform.cs
│ ├── MovePlatform.cs
│ ├── Obstruction.cs
│ └── PlayerChecker.cs
│
├── Player
│ ├── BuffController.cs
│ ├── GroundChecker.cs
│ ├── PlayerController.cs
│ ├── PlayerInteractController.cs
│ ├── PlayerManager.cs
│ ├── PlayerStatHandler.cs
│ └── ResourcesController.cs
│
├── UI
│ ├── Popup
│ │ ├── UI_Inventory.cs
│ │ └── UI_Popup.cs
│ │
│ ├── Scene
│ │ ├── UI_Hover.cs
│ │ ├── UI_HpBar.cs
│ │ ├── UI_Interaction.cs
│ │ ├── UI_Scene.cs
│ │ └── UI_Stamina.cs
│ │
│ └── UI_Base.cs
│
├── Utils
│ ├── Define.cs
│ └── Utils.cs
```
</details>


## 🙋 개발자 정보

- 이름: SulMinWoo
- 연락처 : sataka1853@naver.com
