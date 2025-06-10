# 스파르타코딩클럽 심화 10기_3조 설민우 인벤토리 프로젝트 입니다

# 인벤토리

스파르타 코딩클럽 10기, 유니티 심화 개인 프로젝트 인벤토리 과제작업물입니다.
기존 Step에 부가적인 기능들을 몇가지 추가하고
별도로 절차적 맵 생성과 FSM 으로 3D 방치형 게임 링크도 첨부했습니다.

## 📷 스크린샷

![add5](https://github.com/user-attachments/assets/323626a0-eab4-4601-a5af-e84b696e7b77)

## 빌드 파일 주소

## 시연 영상 주소

## (추가) 3D 방치형 (절차적 맵 생성 & FSM) 레퍼지토리 링크

https://github.com/coolblue1853/Unity_Master_Solo

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

![image](https://github.com/user-attachments/assets/c9a2b0ab-5d5e-43f1-96a2-4a3908f1135c)


```
using UnityEngine;

public enum ItemType
{
    Resource,
    Equipable,
    Consumable
}
public enum ConsumableType
{
    Health,
}
public enum BuffType
{
    Attack,
    AttackSpeed,
    Defence,
    Health,
    Critical,
}
public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Unique,
    Legendary
}
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType Type;
    public float Value;
}
[System.Serializable]
public class ItemDataBuff
{
    public BuffType Type;
    public float Time;
    public float Value;
}
[System.Serializable]
public class ItemDataEquip
{
    public BuffType Type;
    public float Value;
    public void Apply(ref CharacterStats stats)
    {
        switch (Type)
        {
            case BuffType.Attack:
                stats.Attack += (int)Value;
                break;
            case BuffType.AttackSpeed:
                stats.AttackSpeed += (int)Value;
                break;
            case BuffType.Defence:
                stats.Defence += (int)Value;
                break;
            case BuffType.Health:
                stats.Health += (int)Value;
                break;
            case BuffType.Critical:
                stats.Critical += (int)Value;
                break;
        }
    }

    public void Remove(ref CharacterStats stats)
    {
        switch (Type)
        {
            case BuffType.Attack:
                stats.Attack -= (int)Value;
                break;
            case BuffType.AttackSpeed:
                stats.AttackSpeed -= (int)Value;
                break;
            case BuffType.Defence:
                stats.Defence -= (int)Value;
                break;
            case BuffType.Health:
                stats.Health -= (int)Value;
                break;
            case BuffType.Critical:
                stats.Critical -= (int)Value;
                break;
        }
    }
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string DisplayName;
    public string Descrition;
    public ItemType Type;
    public ItemRarity Rarity;   
    public Sprite Icon;
    public GameObject DropPrefab;

    [Header("Stacking")]
    public int MaxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Buff")]
    public ItemDataBuff[] buffs;

    [Header("Equip")]
    public ItemDataEquip[] equips;
}


```
- 스크립터블 오브젝트를 통해서 아이템 데이터 저장
- UI에 장착, 장착해제함수 추가

</details>
<details>
<summary><input type="checkbox" checked disabled> (필수)Step 7. 아이템 장착 </summary>
    
![Step7](https://github.com/user-attachments/assets/34976f7c-e187-4fee-9407-736031bcae91)

```
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
```
```
    // 스탯치 반영
    public void ApplyItemStat(ItemData item)
    {
        if (item == null || item.equips == null) return;

        var stats = Character.Stats;

        foreach (var equip in item.equips)
            equip.Apply(ref stats);

        Character.Stats = stats; // 옵저버 트리거
    }
    public void RemoveItemStat(ItemData item)
    {
        if (item == null || item.equips == null) return;

        var stats = Character.Stats;

        foreach (var equip in item.equips)
            equip.Remove(ref stats);

        Character.Stats = stats; // 옵저버 트리거
    }
```
- 

</details>
<details>
<summary><input type="checkbox" checked disabled> (필수)Step 8. Status에 아이템 정보 반영 </summary>
    
![Step8](https://github.com/user-attachments/assets/835da24f-55ef-44ce-b2b9-27f6e8d34ce2)

```
    public void SetData()
    {
        CharacterStats stats = _defaultStatData.ToCharacterStats();

        Inventory inventory = Instantiate(_inventoryPrefab).GetComponent<Inventory>();
        inventory.UiInventory = _uiManager.UIInventory;
        inventory.Init();

        _character = _characterPrefab.GetComponent<Character>();
        _character.Init("Chad", stats, inventory);

        // 옵저버 구독
        _character.OnStatsChanged += (newStats) =>
        {
            _uiManager.UIStatus.SetStatus(_character);
        };
        _character.OnCoinChanged += (coin) =>
        {
            _uiManager.UIMainMenu.SetDetail(_character);
        };

        _uiManager.UIMainMenu.SetDetail(_character);
    }
```
```
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
```
- 옵저버 패턴을 이용해서 Stat 변경시 Invoke로 갱신

</details>
<details>
<summary><input type="checkbox" checked disabled> (추가) 1. ScriptableObject 데이터 관리 </summary>

![image](https://github.com/user-attachments/assets/31abad24-2e23-4c21-ad15-b2673a1a96f3)


```
using UnityEngine;
using System.Numerics;

[CreateAssetMenu(fileName = "CharacterStatData", menuName = "Game Data/CharacterStatData")]
public class CharacterStatData : ScriptableObject
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

    [SerializeField]
    private string _coinString = "0";

    public BigInteger Coin
    {
        get => BigInteger.TryParse(_coinString, out var result) ? result : BigInteger.Zero;
        set => _coinString = value.ToString();
    }
    // 스크립터블 오브젝트에서 CharacterStats로 추출
    public CharacterStats ToCharacterStats()
    {
        return new CharacterStats(Job, Level, MaxExp, NowExp, Attack, AttackSpeed, Defence, Health, Critical, Coin);
    }
}

```
- 아이템, 플레이어 데이터를 스크립터블 오브젝트로 관리

</details>
<details>
<summary><input type="checkbox" checked disabled> (추가) 2. 아이템 뽑기 </summary>

![add1](https://github.com/user-attachments/assets/1d29dae9-b339-46a3-827f-b1b70d434971)

```
    //  아이템 뽑기 함수
    public void SpawnRandItem()
    {
        if (_character == null)
            _character = GameManager.Instance.Character;
        if (_inventory == null)
            _inventory = _character.Inventory;

        if (_character.Stats.Coin < Constant.ItemCost)
            return;

        // 코인 차감
        var stats = _character.Stats;
        stats.Coin -= Constant.ItemCost;
        _character.Stats = stats;

        // 확률에 따라 등급 선택
        ItemRarity selectedRarity = RollRarity();

        // 해당 등급의 아이템 필터링
        List<ItemData> candidates = _dataList.FindAll(item => item.Rarity == selectedRarity);

        if (candidates.Count > 0)
        {
            int rand = Random.Range(0, candidates.Count);
            _inventory.AddItem(candidates[rand]);
        }

    }
```
- 랜덤 함수를 이용하여 아이템을 인벤토리에 추가
- 인벤토리 갱신 또한 옵저버 패턴 이용

</details>
<details>
<summary><input type="checkbox" checked disabled> (추가) 3. 아이템 등급 분리 </summary>

![add1](https://github.com/user-attachments/assets/1d29dae9-b339-46a3-827f-b1b70d434971)


```
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
```
```
    private readonly Dictionary<ItemRarity, float> rarityChances = new()
    {
        { ItemRarity.Common, 0.5f },
        { ItemRarity.Uncommon, 0.25f },
        { ItemRarity.Rare, 0.15f },
        { ItemRarity.Unique, 0.07f },
        { ItemRarity.Legendary, 0.03f }
    };

    // 등급 지정
    private ItemRarity RollRarity()
    {
        float roll = Random.value; // 0.0 ~ 1.0
        float cumulative = 0f;

        foreach (var pair in rarityChances)
        {
            cumulative += pair.Value;
            if (roll <= cumulative)
                return pair.Key;
        }

        return ItemRarity.Common; 
    }
```
- 등급에 따라서 Slot 색 변경
- 등급에 따라서 추첨 확률 변경

</details>
<details>
<summary><input type="checkbox" checked disabled> (추가) 4. 자동 전투와 연출 </summary>

![add5](https://github.com/user-attachments/assets/79c109a5-bf14-4e17-a070-c103f74e14d5)

```
using UnityEngine;


public class Weapon : MonoBehaviour
{
    private AudioManager _audioManager;
    private Character _character;
    [SerializeField] private Enemy _enemy; // 원래라면은 충돌이나 감지를 시켜줘야함
    public void Init(Character character)
    {
        _character = character;
        _audioManager = GameManager.Instance.AudioManager;

    }
    public void Attack()
    {
        int critNum =  Random.Range(1, 101);

        // 크리티컬 성공
        if(_character.Stats.Critical >= critNum)
        {
            var stats = _character.Stats;
            var attackPower = (int)(stats.Attack * Constant.CritCoinRate);
            stats.Coin += attackPower;
            _character.Stats = stats;
            _enemy.Hit(attackPower,true);
        }
        else // 크리티컬 실패
        {
            var stats = _character.Stats;
            stats.Coin += stats.Attack;
            _character.Stats = stats;
            _enemy.Hit(stats.Attack);
        }
        _audioManager.PlaySFX("Attack");
  
    }
}

```
- 생성된 능력치를 가지고 전투 기능 추가
- 공격 데미지 만큼 Coin 획득
- 공격 애니메이션, 피격 효과, 파티클효과, 플로팅 데미지 기능 추가.

</details>
<details>
<summary><input type="checkbox" checked disabled> (추가) 5. 사운드 시스템 </summary>


```
using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

    [Header("Mixer Settings")]
    public AudioMixer AudioMixer; // 오디오 믹서 (BGM, SFX 볼륨 제어)

    [Header("Background Music")]
    public AudioSource BgmSource; // BGM 재생용 AudioSource
    public AudioClip[] BgmClips;  // 재생 가능한 BGM 클립 배열

    [Header("Sound Effects (Auto Register)")]
    public AudioSource SfxSource; // SFX 재생용 AudioSource
    private Dictionary<string, AudioClip> _sfxDict = new Dictionary<string, AudioClip>(); // SFX 이름-클립 매핑 딕셔너리

    private AudioSource _loopSource; // 루프용 AudioSource

    private void Awake()
    {
        LoadAllSFX();
    }

    // Resources/Audio/SFX 폴더 내 모든 오디오 클립을 자동 등록
    void LoadAllSFX()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio/SFX");
        foreach (AudioClip clip in clips)
        {
            if (!_sfxDict.ContainsKey(clip.name))
            {
                _sfxDict.Add(clip.name, clip);
                Debug.Log($"SFX 로드 완료: {clip.name}");
            }
        }
    }

    // 인덱스로 BGM 재생
    public void PlayBGM(int index)
    {
        if (index >= 0 && index < BgmClips.Length)
        {
            BgmSource.clip = BgmClips[index];
            BgmSource.loop = true;
            BgmSource.Play();
        }
    }

    // 이름으로 SFX 재생
    public void PlaySFX(string name)
    {
        if (_sfxDict.TryGetValue(name, out AudioClip clip))
        {
            SfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX '{name}' 을(를) 찾을 수 없습니다.");
        }
    }

    // 피치와 볼륨 설정하여 SFX 재생
    public void PlaySFX(string name, float volume, float pitch)
    {
        if (_sfxDict.TryGetValue(name, out AudioClip clip))
        {
            SfxSource.pitch = pitch;
            SfxSource.PlayOneShot(clip, volume);
            SfxSource.pitch = 1f; // 재생 후 피치 초기화
        }
    }

    // 위치 기반으로 SFX 재생 (3D 공간)
    public void PlaySFXAtPosition(string name, Vector3 position)
    {
        if (_sfxDict.TryGetValue(name, out AudioClip clip))
        {
            AudioSource.PlayClipAtPoint(clip, position);
        }
    }

    // 루프 사운드 시작 (지속 재생 효과음)
    public void PlaySFXLoop(string name)
    {
        if (_loopSource == null)
        {
            _loopSource = gameObject.AddComponent<AudioSource>();
            _loopSource.loop = true;
            _loopSource.playOnAwake = false;
        }

        if (_sfxDict.TryGetValue(name, out AudioClip clip))
        {
            _loopSource.clip = clip;
            _loopSource.Play();
        }
    }

    // 루프 사운드 정지
    public void StopSFXLoop()
    {
        if (_loopSource != null && _loopSource.isPlaying)
        {
            _loopSource.Stop();
        }
    }

    // BGM 볼륨 설정 (0.0 ~ 1.0 범위, dB 변환)
    public void SetBGMVolume(float value)
    {
        AudioMixer.SetFloat("BGM", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }

    // SFX 볼륨 설정 (0.0 ~ 1.0 범위, dB 변환)
    public void SetSFXVolume(float value)
    {
        AudioMixer.SetFloat("SFX", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
    }
}

```
- Bgm, 공격, 피격, 버튼 사운드 효과 추가

</details>
<details>
<summary><input type="checkbox" checked disabled> (추가) 6. 통화 시스템과 BigInteger </summary>

![image](https://github.com/user-attachments/assets/cc3f3656-0a72-45a4-9705-0b00baf3b28c)


```
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Utils 
{
    // Big 인티저를 이용해서 가져오는 포맷값
    public static string FormatBigInteger(BigInteger number)
    {
        if (number < 1000) return number.ToString();

        string[] suffixes = { "", "K", "M", "B", "T", "Q" };
        int i = 0;

        while (number >= 1000 && i < suffixes.Length - 1)
        {
            number /= 1000;
            i++;
        }

        return number.ToString() + suffixes[i];
    }

}

```
- BigInteger와 이를 포맷팅 하는 기능 추가.
- 아이템 뽑기 혹은 판매시 통화 시스템에 반영

</details>
<details>
<summary><input type="checkbox" checked disabled> (추가) 7. 아이템 판매 </summary>

![add6](https://github.com/user-attachments/assets/4019bfd6-cbce-4657-9e7a-dc8c74098db3)

```
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
```
- 아이템 판매 기능 추가
- 판매는 장착과 판매를 토글하는 방식으로 생성

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
├── Audio
│   └── AudioManager.cs
│
├── Enemy
│   └── Enemy.cs
│
├── Inventory
│   └── Inventory.cs
│
├── Item
│   ├── ItemData.cs
│   └── ItemSpawner.cs
│
├── Manager
│   └── GameManager.cs
│
├── Player
│   ├── Character.cs
│   ├── CharacterStatData.cs
│   ├── PlayerController.cs
│   └── Weapon.cs
│
├── UI
│   ├── UIBase.cs
│   ├── UIInventory.cs
│   ├── UIMainMenu.cs
│   ├── UIManager.cs
│   ├── UISlot.cs
│   └── UIStatus.cs
│
├── Utils
│   ├── Constant.cs
│   ├── DamageText.cs
│   ├── Define.cs
│   └── Utils.cs

```
</details>


## 🙋 개발자 정보

- 이름: SulMinWoo
- 연락처 : sataka1853@naver.com
