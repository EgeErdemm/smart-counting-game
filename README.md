# 🎓 Tez Projesi: Toplama ve Çıkarma Mekanikli Eğitici Mobil Oyun

Bu tez projesi kapsamında, çocukların temel aritmetik becerilerini ve problem çözme yeteneklerini geliştirmeye yönelik bir mobil eğitim oyunu geliştirilmiştir. Oyun, süre sınırlı bir oynanış içerisinde hedef skora ulaşmayı amaçlayan bir puzzle mekaniğine sahiptir.

Oyuncu, rastgele oluşturulan sayılarla dolu bir grid üzerinde yön tuşları ya da kaydırma hareketleriyle gezinerek ilerler. Her adımda ulaşılan hücredeki sayılar, toplama veya çıkarma moduna göre oyuncunun skoruna eklenir ya da çıkarılır. Oyunun temel amacı, verilen süre içinde en uygun rotayı takip ederek hedef skora ulaşmaktır.

Bu yapı, sadece temel işlem pratiği sunmakla kalmayıp aynı zamanda stratejik planlama ve dikkat gibi bilişsel becerileri de desteklemeyi hedeflemektedir.

## 🎮 Oyun Mekanikleri

- Oyun sahası rastgele oluşturulan sayılarla dolu bir grid sistemine sahiptir.
- Oyuncu, dört yöne kaydırma (swipe) hareketiyle grid üzerinde gezinir.
- Gezinilen hücredeki sayılar, oyuncunun skoruna eklenir.
- Oyuncu, ekran üzerindeki UI aracılığıyla toplama veya çıkarma modunu seçebilir.
- Amaç, verilen adım sayısı içinde hedef skora ulaşmaktır.
- Zaman dolmadan doğru rotayı izleyerek hedef skora ulaşan oyuncu kazanır.

## 🧠 Yazılım Mimarisi

Proje, **OOP** prensiplerine ve **SOLID yazılım geliştirme ilkelerine** uygun olarak inşa edilmiştir. Kod yapısı modüler ve genişletilebilir olacak şekilde tasarlanmıştır.

### 🔧 Kullanılan SOLID Prensipleri

- **Single Responsibility**: Her sınıf sadece tek bir sorumluluğa sahiptir. (Örn: `ScoreManager`, sadece skor takibini yapar.)
- **Open/Closed**: Kod açık ama değişikliğe kapalı olacak şekilde genişletilebilir olarak yazılmıştır. (Örn: farklı zaman yöneticileri eklemek için `AbstractTimerManager` türetilebilir.)
- **Interface Segregation & Dependency Inversion**: `IGameWinCheck`, `INextLevelLoader`, `ISeaAbleArea` gibi arayüzlerle davranışlar soyutlanmıştır. `GameManager` bu soyutlamalar üzerinden çalışır.

### 🧩 Event Sistemi ve EventBus

Projede **event tabanlı mimari** kullanılmıştır. `GameManager` sınıfı, oyun olaylarını merkezi bir kanal olan `IEventBus` üzerinden dinlemektedir:

```csharp
_eventBus.Subscribe<TimeUpEvent>(OnTimeUp);

┌────────────────────────────┐
│        IEventBus           │◄────────────────────┐
└────────────────────────────┘                     │
                                                  ▼
                                subscribes/publishes events
                               (TimeUpEvent, PlayerMoveEvent, etc.)

┌────────────────────────────┐
│      IGameWinCheck         │◄────────┐
└────────────────────────────┘         │
                                       ▼
                       ┌────────────────────────────┐
                       │ ScoreBasedWinChecker       │
                       │ - levelLoader              │
                       │ - _eventBus                │
                       │ + CheckWin(...)            │
                       └────────────────────────────┘

┌────────────────────────────┐
│     INextLevelLoader       │◄────────┐
└────────────────────────────┘         │
                                       ▼
                       ┌────────────────────────────┐
                       │ DefaultLevelLoader         │
                       │ + LoadNextLevel()          │
                       │ + ReLoadLevel()            │
                       └────────────────────────────┘

┌────────────────────────────┐
│      ISeaAbleArea          │◄────────┐
└────────────────────────────┘         │
                                       ▼
                       ┌────────────────────────────┐
                       │ BlindModeSeaAbleArea       │
                       │ - tile                     │
                       │ + SeaAble(index)           │
                       └────────────────────────────┘

                 ▲
                 │ inherits
┌──────────────────────────────────────┐
│         BaseGamaManager              │
│--------------------------------------│
│ - Score: int                         │
│ - timeIsUp: bool                     │
│ - _eventBus: IEventBus               │
│ - _winCheck: IGameWinCheck           │
│ - _nextLevelLoader: INextLevelLoader │
│ - _seaAbleArea: ISeaAbleArea         │
│ + Awake(), Start()                   │
└──────────────────────────────────────┘
                 ▲
                 │ inherits
┌──────────────────────────────────────┐
│           GameManager                │
│--------------------------------------│
│ + OnEnable()                         │
│ + UpdateScore(int)                   │
│ + OnTimeUp(TimeUpEvent)              │
│ + NextLevel()                        │
│ + ReGame()                           │
└──────────────────────────────────────┘


## 🖼️ Oyun Görselleri

### 🧩 Ana Oyun Ekranı  
<img src="Screenshots/game1.png" title="Oyuncu rastgele sayılarla dolu grid üzerinde toplama/çıkarma işlemleriyle ilerliyor." width="100%" style="max-width: 500px;" />

### 💣 Bomba Modu  
<img src="Screenshots/BombMode.png" title="Bomba modunda yanlış hücreye gitmek, zaman kaybına ve oyunun kaybedilmesine yol açar." width="100%" style="max-width: 500px;" />

### 🧪 Blind Mod Özelliği  
<img src="Screenshots/BlindMode.png" title="Her adımda sadece 1’er birim sol, sağ, yukarı ve aşağı hücreler görünür." width="100%" style="max-width: 500px;" />

