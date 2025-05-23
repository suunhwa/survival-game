# Survival Game
### 소개
자연을 배경으로 자원을 채집하고, 생존하는 것을 목표로 하는 3D 서바이벌 게임입니다. 플레이어는 숲 속을 탐험하면서 나무, 돌 등을 채집해 아이템을 수집하고, 이를 인벤토리에 저장하거나 활용할 수 있습니다. 간단한 전투 시스템과 몬스터도 구현되어 있으며, 기능을 확장 중입니다.

### 기술 스택
- 엔진: Unity 6 (6000.0.40f1)
- 언어: C#
- 버전관리: Git + GitHub
- 데이터 관리: CSV 기반 아이템 데이터

### 주요 구현 기능
#### 🔍 플레이어 시스템
- 이동: `PlayerMovement.cs` – WASD 이동 및 점프 기능
- 카메라: `FollowCamera.cs` – 플레이어 뒤를 따라다니는 3인칭 카메라 구현
- 상호작용: `InteractionDetector.cs` – 플레이어 주변 상호작용 가능한 오브젝트를 감지하고 F 키 입력으로 상호작용 실행
- 체력/공격:
  - `PlayerHealth.cs` – 체력 관리
  - `PlayerAttack.cs`, `WeaponColliderHandler.cs` – 무기 공격 및 충돌 판정

#### 🪓 채집 & 아이템 시스템
- 나무 및 돌 상호작용:
  - `TreeInteractable.cs`, `RockInteractable.cs` – 채집 시 아이템 드랍
- 아이템 드랍: `DroppedItem.cs`, `ItemPoolManager.cs`, `ObjectPool.cs` – 오브젝트 풀링을 활용한 드랍 최적화
- 아이템 데이터 관리: `ItemData.cs`, `Item.cs`, `ObjectData.cs`, `RewardData.cs` – CSV 기반 데이터 연동
- 인벤토리 UI: `InventorySlot.cs`, `InventoryUI.cs` – 아이템 저장 및 UI 표시

#### 🐻 몬스터 시스템
- `Monster.cs` – 체력/공격력/죽음 로직 포함, 사망 시 아이템 드랍

 #### ⚠️ 경고 시스템 (옵저버 패턴)
- `WarningSound.cs` – 체력이 30 이하로 떨어졌을 때 경고 사운드 출력
- 옵저버 패턴 인터페이스: `IObserver<T>`, `GameDataManager.cs`에서 Notify 호출

###  개발 중이거나 예정된 기능
- 몬스터 AI 추적 및 공격
- 허기/갈증 등 생존 요소 확장
- 저장 및 로드 기능
- 캠프파이어와 회복 아이템
- 날씨와 시간 흐름 시스템
