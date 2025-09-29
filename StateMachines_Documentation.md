# Máquinas de Estado en Unity Aqueron Prelude

Este repositorio contiene varias implementaciones de máquinas de estado desarrolladas en C# para Unity. A continuación se documentan todas las máquinas de estado encontradas:

## 1. Sistema de Diálogos (DialogueBox State Machine)

**Ubicación:** `Assets/Scenes/TicTacToe/Scripts/UI/DialogueBox/`

**Tipo:** Patrón State (State Pattern)

### Componentes principales:

#### IDialogueBoxState (Interfaz)
```csharp
public interface IDialogueBoxState
{
    void EnterState(DialogueBoxContext context);
    void WaitDialogue(DialogueBoxContext context);
    void ShowDialogue(DialogueBoxContext context);
}
```

#### Estados implementados:

1. **WaitDialogueState** - Estado de espera para nuevos diálogos
2. **ShowDialogueState** - Estado activo mostrando diálogos

#### DialogueBoxContext (Contexto)
- Mantiene el estado actual y permite transiciones
- Contiene la cola de diálogos y referencias a UI
- Método `TransitionToState()` para cambiar entre estados

#### DialogueBoxController (Controlador)
- Inicializa el sistema en `WaitDialogueState`
- Maneja eventos de diálogos entrantes
- Controla las transiciones de estado

### Funcionamiento:
- **WaitDialogueState**: Limpia la cola y espera nuevos diálogos
- **ShowDialogueState**: Procesa y muestra diálogos con diferentes prioridades (QOS)
- **Transiciones automáticas**: De Show a Wait cuando se completan todos los diálogos

---

## 2. Gestor de Juego (GameManager State Machine)

**Ubicación:** `Assets/Scenes/TicTacToe/Scripts/GameManager.cs`

**Tipo:** Máquina de Estados Finita con enum

### Estados definidos:
```csharp
public enum GameState
{
    Menu,
    GameplayLoading,
    GameplayLoaded,
    GameplayCrossTurn,
    GameplayCircleTurn,
    GameEndTie,
    GameEndVictory,
    GameEndDefeat
}
```

### Transiciones principales:
- **Loading → Loaded**: Cuando el tablero se inicializa
- **Loaded → CrossTurn**: Inicio del juego
- **CrossTurn ↔ CircleTurn**: Alternancia de turnos
- **AnyTurn → GameEnd**: Cuando hay victoria, derrota o empate

### Métodos de control:
- `PassTurn()`: Alterna entre turnos de cruz y círculo
- `ResetGame()`: Vuelve al estado Loading
- `IsPlayerTurn()`: Determina si es el turno del jugador

---

## 3. Director Cinemático (KinematicDirector State Machine)

**Ubicación:** `Assets/Scenes/TicTacToe/Scripts/Opening/MisionScene/KinematicDirector.cs`

**Tipo:** Máquina de Estados Secuencial para narrativa

### Estados de la secuencia narrativa:
```csharp
enum State
{
    None,
    News,
    NewsDialogue1,
    NewsDialogue2,
    Daily,
    DailyDialogue,
    Secret,
    SecretDialogue1,
    SecretDialogue2,
    SecretDialogue3,
    SecretDialogue4,
    End
}
```

### Características:
- **Secuencia lineal**: Cada estado avanza al siguiente automáticamente
- **Multiidioma**: Soporte para español e inglés
- **Eventos coordinados**: Usa eventos de actores para sincronizar transiciones
- **Diálogos temporizados**: Algunos estados tienen tiempo extra de lectura

### Flujo narrativo:
1. **News**: Muestra noticias sobre las criaturas
2. **Daily**: Información diaria sobre la selección
3. **Secret**: Mensaje secreto y misión del jugador
4. **End**: Finaliza la secuencia con fade out

---

## 4. Controlador de Celda (CellController States)

**Ubicación:** `Assets/Scenes/TicTacToe/Scripts/Board/CellController.cs`

**Tipo:** Enum de estados para celdas del tablero

### Estados de celda:
```csharp
public enum CellStates
{
    None,           // Celda vacía
    HoverVisible,   // Hover visible del jugador
    HoverInvisible, // Hover oculto
    Cross,          // Marcada con X
    Circle          // Marcada con O
}
```

### Transiciones:
- **None → Hover**: Cuando el mouse entra en la celda
- **Hover → None**: Cuando el mouse sale de la celda
- **Any → Cross/Circle**: Cuando se marca la celda
- **Cross/Circle → None**: Durante reset del juego

---

## Resumen de Patrones Utilizados

1. **State Pattern**: Sistema de diálogos con interfaces y clases estado
2. **Finite State Machine**: GameManager con enum y switch statements
3. **Sequential State Machine**: KinematicDirector para secuencias narrativas
4. **Simple State Enum**: CellController para estados de celda

Cada implementación está adaptada a su propósito específico, desde sistemas complejos de UI hasta control básico de estados de componentes.