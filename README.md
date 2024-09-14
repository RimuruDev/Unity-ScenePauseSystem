# Unity Scene Pause System

**Автор:** RimuruDev  
**Лицензия:** MIT

## Описание

**Unity Scene Pause System** предоставляет простой и эффективный способ управлять паузой и возобновлением игрового процесса в сценах Unity. Этот пакет позволяет разработчикам легко приостанавливать и возобновлять игру, управлять объектами, реагирующими на паузу, и обрабатывать различные сценарии паузы.

## Ремарка
Если вы используете `Time.timeScale = 0` для реализации паузы в игре, и считаете что это вам подходит лучше всего.  Тогда можете скипать это решение. Но если костыля в виде `Time.timeScale = 0` теперь не хватает, тогда добро пожаловать.

## Возможности

- **Легкая интеграция** в существующие проекты Unity.
- **Гибкое управление паузой** с помощью интерфейсов и абстрактных классов.
- **Поддержка пользовательских запросов паузы** через класс `PauseRequest`.
- **Примеры использования** для быстрого старта.

## Установка

### Через Unity Package Manager (рекомендуется)

1. **Добавьте ссылку на Git репозиторий** в Unity:

    - Откройте ваш проект Unity.
    - Перейдите в **Window > Package Manager**.
    - Нажмите на кнопку **+** в левом верхнем углу и выберите **Add package from git URL...**.
    - Введите URL репозитория:

      ```
      https://github.com/RimuruDev/Unity-ScenePauseSystem.git
      ```

    - Нажмите **Add**, и пакет будет установлен в ваш проект.

### Установка из локальной папки

1. **Скачайте или склонируйте** репозиторий к себе на компьютер.
2. В Unity перейдите в **Window > Package Manager**.
3. Нажмите на кнопку **+** и выберите **Add package from disk...**.
4. Укажите путь к файлу `package.json` в корне скачанного репозитория.
5. Пакет будет добавлен в ваш проект.

## Использование

### Шаг 1: Добавление PauseSystem на сцену

1. **Создайте пустой GameObject** в вашей сцене.
2. **Переименуйте** его в `PauseSystem` (по желанию для удобства).
3. **Добавьте компонент** `PauseSystem` из пространства имен `AbyssMoth.ScenePauseSystem`.

### Шаг 2: Создание паузируемых компонентов

#### Вариант 1: Наследование от `PausableBehaviour`

Используйте этот вариант, если ваш скрипт может наследоваться от `PausableBehaviour`.

```csharp
using UnityEngine;
using AbyssMoth.ScenePauseSystem;

public class MyPausableComponent : PausableBehaviour
{
    private protected override void OnUpdate()
    {
        // Ваш код, который выполняется, когда игра не на паузе
    }

    private protected override void OnFixedUpdate()
    {
        // Ваш код для FixedUpdate, если необходимо
    }

    private protected override void OnLateUpdate()
    {
        // Ваш код для LateUpdate, если необходимо
    }
}
```

#### Вариант 2: Реализация интерфейса `IPausable`

Если вы не можете наследоваться от `PausableBehaviour`, реализуйте интерфейс `IPausable`.

```csharp
using UnityEngine;
using AbyssMoth.ScenePauseSystem;

public class MyPausableComponent : MonoBehaviour, IPausable
{
    public bool IsPaused { get; private set; }

    private void Update()
    {
        if (IsPaused)
            return;

        // Ваш код
    }

    public void Pause(in PauseRequest request, Object sender = null)
    {
        IsPaused = true;
        // Дополнительная логика при паузе
    }

    public void Resume(in PauseRequest request, Object sender = null)
    {
        IsPaused = false;
        // Дополнительная логика при возобновлении
    }
}
```

### Шаг 3: Управление паузой

Для управления паузой используйте методы `PauseGame` и `ResumeGame` компонента `PauseSystem`.

```csharp
using UnityEngine;
using AbyssMoth.ScenePauseSystem;

public class PauseController : MonoBehaviour
{
    [SerializeField] private PauseSystem pauseSystem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pauseSystem.IsPaused)
            {
                // Возобновляем игру
                pauseSystem.ResumeGame(new PauseRequest { IsPerformSettings = true }, this);
            }
            else
            {
                // Ставим игру на паузу
                pauseSystem.PauseGame(new PauseRequest { IsPerformSettings = true }, this);
            }
        }
    }
}
```

### Использование `PauseRequest`

Класс `PauseRequest` позволяет передавать дополнительную информацию о причине паузы или возобновления.

Пример постановки игры на паузу при победе:

```csharp
public void OnPlayerVictory()
{
    var request = new PauseRequest { IsVictory = true };
    pauseSystem.PauseGame(request, this);
}
```

В вашем паузируемом компоненте вы можете обработать этот запрос:

```csharp
public class VictoryHandler : PausableBehaviour
{
    public override void Pause(in PauseRequest request, Object sender = null)
    {
        base.Pause(request, sender);

        if (request.IsVictory)
        {
            // Логика при победе (например, показать экран победы)
        }
    }
}
```

## Примеры

### InfinityDebugLogWriter

Пример компонента, который пишет сообщения в консоль, когда игра не на паузе.

```csharp
using UnityEngine;
using AbyssMoth.ScenePauseSystem;

public class InfinityDebugLogWriter : PausableBehaviour
{
    private protected override void OnUpdate()
    {
        Debug.Log($"Сообщение от {nameof(InfinityDebugLogWriter)}");
    }
}
```

### InfinityColorDebugLogWriter

Пример реализации `IPausable` без наследования от `PausableBehaviour`.

```csharp
using UnityEngine;
using AbyssMoth.ScenePauseSystem;

public class InfinityColorDebugLogWriter : MonoBehaviour, IPausable
{
    public bool IsPaused { get; private set; }

    private void Update()
    {
        if (IsPaused)
            return;

        Debug.Log("<color=yellow>Цветное сообщение!</color>");
    }

    public void Pause(in PauseRequest request, Object sender = null)
    {
        IsPaused = true;
        // Дополнительная логика при паузе
    }

    public void Resume(in PauseRequest request, Object sender = null)
    {
        IsPaused = false;
        // Дополнительная логика при возобновлении
    }
}
```

## Дополнительная информация

### Поля `PauseRequest`

`PauseRequest` содержит следующие поля, которые вы можете использовать для передачи дополнительной информации:

- **IsPerformSettings**: пауза вызвана открытием настроек.
- **IsVictory**: пауза вызвана победой игрока.
- **IsDefeat**: пауза вызвана поражением игрока.
- **IsShowAd**: пауза для показа рекламы.
- **IsEndSceneLifeCycle**: пауза из-за окончания жизненного цикла сцены.
- **IsChangedApplicationFocus**: изменение фокуса приложения.
- **IsChangedOnApplicationPause**: приложение поставлено на паузу (например, свернуто).

### События паузы и возобновления

Вы можете переопределить методы `Pause` и `Resume` в ваших компонентах для выполнения дополнительной логики при паузе или возобновлении.

```csharp
public class CustomPausableComponent : PausableBehaviour
{
    public override void Pause(in PauseRequest request, Object sender = null)
    {
        base.Pause(request, sender);
        // Ваша дополнительная логика при паузе
    }

    public override void Resume(in PauseRequest request, Object sender = null)
    {
        base.Resume(request, sender);
        // Ваша дополнительная логика при возобновлении
    }
}
```

## Содействие

Мы приветствуем вклад сообщества! Если вы хотите помочь развитию проекта:

- **Сообщайте об ошибках**: создавайте [Issues](https://github.com/RimuruDev/Unity-ScenePauseSystem/issues) на GitHub.
- **Вносите изменения**: делайте форки и отправляйте пул-реквесты.
- **Предлагайте улучшения**: делитесь идеями и предложениями.

## Лицензия

Этот проект лицензирован под лицензией MIT. Подробности смотрите в файле [LICENSE](LICENSE).

## Контакты

- **Автор:** RimuruDev
- **Email:** rimuru.dev@gmail.com
- **GitHub:** [github.com/RimuruDev](https://github.com/RimuruDev)

## Благодарности

Спасибо за использование **Unity Scene Pause System**! Мы надеемся, что этот пакет облегчит разработку вашего проекта. Если у вас есть вопросы или нужна помощь, не стесняйтесь обращаться.

---

---

---

### TODO:
- Добавить нормальную XML документацию к коду.
- Добавить примеры для инъекций PauseSystem к IPauseSystem в локальном контексте сцены.
- Добавить расширение редактора для аркестрации PauseSystem
- Добавить удобное расширения для быстрого получения доступа к IPauseSystem для случая когда на проекте не используется DI, но при этом без DontDestroyOnLoad и проблем синглтонов. Реализовать через смесь MonoSingleton + паттерн NullObject.
- Добавить запекание (кэширование) для PauseSystem.
