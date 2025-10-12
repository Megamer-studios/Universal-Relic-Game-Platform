# 🎭 Universal Relic Game Platform (URGP)

![URGP](https://github.com/Megamer-studios/Universal-Relic-Game-Platform/blob/master/Logo1.png)

**Universal Relic Game Platform (URGP)** is an original C# storytelling engine built upon **MonoGame** — a hybrid of art, code, and orchestration.  
It is a spiritual successor to the golden age of narrative systems: a **visual novel framework** with a **real-time console**, **encrypted save system**, and **scene-based sequencing**, handcrafted for precision storytelling.

---

## 🌌 Overview

URGP transforms the structure of a traditional visual novel into a flexible **cinematic engine**.  
Every dialogue, transition, and animation is orchestrated in real time, allowing developers to craft interactive narratives where every moment feels deliberate.

Built for **clarity, control, and creativity**, URGP is not just a game — it’s a *framework for stories*.

---

## 🧩 Core Features

### 🎮 Gameplay Engine
- Dialogue-driven narrative using `.dlg` scripts  
- Layered backgrounds, portraits, and sprite compositions  
- Keyboard and mouse input with auto-progress support  
- Smooth vector-based transitions for dynamic scenes  
- Background music (BGM) layering with `SoundEffectInstance`  
- Interactive “question” states for player-driven branching  

### 💾 Save & Encryption
- Full save/load system using **AES-256 encryption (PBKDF2)**  
- Persists current dialogue line, scene file, and inventory  

### 🧰 Developer Console
Toggle with **`~` (tilde key)** for direct runtime control.  

| Command | Action |
|----------|--------|
| `exit` | Quit the game |
| `reset` | Restart the current scene |
| `save` / `load` | Save or restore encrypted progress |
| `gamemode1` / `gamemode0` | Enable or disable debug mode |
| `additem#` / `delitem#` | Add or remove items (debug only) |
| `lines#` | Jump to a specific dialogue line |
| `fullscreen` | Toggle fullscreen |
| `credits` | Load developer credits scene |
| `inventory` | Show player inventory |

---

## 🧠 Class Architecture

### 🏗️ `Game1.cs`
The beating heart of URGP.  
Handles:
- Initialization, rendering, and input polling  
- Music control and sound effect playback  
- Reading and rendering dialogue text  
- Console input and command execution  
- Save/load encryption using `Aes` with PBKDF2  
- Dynamic scene rendering with `SpriteBatch`

Also features real-time debug overlay with live data on:
- Current line and dialogue file  
- Active sprites and backgrounds  
- Audio states  
- Encrypted save content preview  

---

### ⚙️ `Progress.cs`
The **storyboard director** of URGP.  
Defines what happens on each dialogue line — sprite changes, transitions, and music cues.

```csharp
if (game.Line == 3)
{
    game.bgImg = true;
    game.background = game.Content.Load<Texture2D>("bg2");
    game.Cem1 = game.Content.Load<Texture2D>("Sprite2");
    game.Portrait = game.Content.Load<Texture2D>("Portrait2");
    game.BMG = game.Content.Load<SoundEffect>("AMachine");
}
```

Each dialogue line can alter:
- Backgrounds  
- Sprites (Cem1, Cem2, Cem3)  
- Portrait images  
- Sound effects  
- Movement vectors  
- Visual transitions  

---

### ⏱️ `Sequencer.cs`
The **temporal brain** — automates scene progression based on motion and timing.

```csharp
if (Vector2.Distance(game.bottomMid, game.NewbottomMid) <= 0.1f)
{
    game.Line++;
    Progress.ProgressLines(game);
}
```

Perfect for **cinematic moments** that play out without user input.

---

### 💼 `InventoryItems.cs`
A lightweight **global inventory manager** for tracking items across sessions.

```csharp
public static void InitializeItems()
{
    Items = new List<InventoryItem>();
    AddItem("Calling Card", 0);
}
```

- Central registry for all items  
- Integrated with the save/load system  
- Accessible via the in-game console (`inventory`, `additem#`, `delitem#`)

---

### 🧾 `InventoryItem.cs`
A simple data model:
```csharp
public class InventoryItem
{
    public string name;
    public int id;
}
```

---

## 📜 Dialogue System

### File Format: `.dlg`
Each line in a `.dlg` file represents a dialogue or narration entry.

#### Example:
```
You awaken to the hum of machinery.
The room is cold and quiet.
A familiar voice whispers...
Who are you?
```

`
` is automatically converted into a new line during rendering.

Dialogue progression is handled by `Line` numbers, starting at `1`, with events in `Progress.cs` mapping directly to specific lines.

---

## 🗂️ Folder Structure

```
URGP/
│
├── Game1.cs
├── Progress.cs
├── Sequencer.cs
│
├── Inventory/
│   ├── InventoryItems.cs
│   └── InventoryItem.cs
│
├── Content/
│   ├── bg1.png, bg2.png
│   ├── Sprite1.png, Sprite2.png, Sprite5.png
│   ├── Portrait1.png, Portrait2.png, Portrait3.png
│   ├── AMachine.wav, Dead.wav, Scream.wav
│   ├── File.spritefont
│
├── Dialogues/
│   ├── Dia1.dlg
│   ├── Dev.dlg
│
└── Saves/
    ├── Save.dat
    └── BC.dat
```

---

## ⚙️ Technical Details

| Feature | Description |
|----------|--------------|
| Engine | MonoGame |
| Language | C# (.NET 9) |
| Resolution | 1024×768 (internal, letterboxed) |
| Audio | SoundEffect & SoundEffectInstance |
| Encryption | AES-256 (PBKDF2 key derivation, 100k iterations) |
| Input | Keyboard & Mouse |

---

## 🚀 Setup & Run

### Prerequisites
- .NET 9.0 SDK or later  
- MonoGame Framework  
- Visual Studio or Rider  

---

## 🧑‍💻 Credits

**Created by:** Megamer Studios  
**Framework:** MonoGame  
**License:** MIT  

> “A relic reborn in code —  
> A machine that breathes story.”

---

## 🌠 Future Outlook
URGP is designed to scale:
- Dialogue branching via tagged `.dlg` blocks  
- Choice UI and visual transitions  
- Scripting layer for dynamic variables  
- Custom save serialization

---

