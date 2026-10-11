# GulpThemUp

**GulpThemUp** is a neon-styled arcade action game built with Unity. The player steers a character and aims a cannon through arenas filled with numbered targets. Use positive numbers to increase your own score, and avoid negative numbers, which reduce it. If your score drops below the game's threshold, the run ends. Positive numbers are protected by covers that must be broken through before the numbers can be used.

The game was previously listed on Google Play, where it received **more than 150 downloads**.

## Screenshots

### Google Play listing

![GulpThemUp Google Play listing](./ReadMeAssets/Playstore%20listing.png)

### Gameplay

| Arena combat | Targeting and enemies |
|---|---|
| ![Gameplay screenshot: arena combat](./ReadMeAssets/Screenshot%202026-10-12%20003810.png) | ![Gameplay screenshot: targeting and enemies](./ReadMeAssets/Screenshot%202026-10-12%20003909.png) |

| Level hazards | Neon battle arena |
|---|---|
| ![Gameplay screenshot: level hazards](./ReadMeAssets/Screenshot%202026-10-12%20004024.png) | ![Gameplay screenshot: neon battle arena](./ReadMeAssets/Screenshot%202026-10-12%20004050.png) |

| Arena challenge |
|---|
| ![Gameplay screenshot: arena challenge](./ReadMeAssets/Screenshot%202026-10-12%20004143.png) |

## How the game works

Each run is configured for the chosen level, then moves into the gameplay scene. The player controls movement and a crosshair, while the cannon fires the currently selected ammunition toward the aim point. Positive numbers are resources: break their protective covers, then use the exposed numbers to increase the player's score. Negative numbers are threats that decrease the score on contact; falling below the score threshold ends the run. Their values, health, and visual effects change as they take damage, making target selection and ammunition management central to survival.

Level setup scales the amount of arena content and adjusts spawn pacing and combat stats as levels advance. During a run, pickups provide coins or ammunition, and collected items can be triggered from the in-game item slots. A run can be paused, resumed, retried, or ended from the gameplay UI.

## Gameplay systems

### Weapons and ammunition

The cannon’s ammunition is selected in-game through the bullet-change UI. The selection updates the active bullet type, firing rate, and displayed ammunition count. `PlayerAttack` checks that ammunition is available, enforces the fire interval, and asks `BulletInstantiater` to create the selected projectile. Each projectile script handles its movement and interactions with targets.

The four ammunition types are:

- **Normal** — standard shots for damaging targets and breaking positive-number covers.
- **Piercing** — passes through targets rather than ending on the first hit.
- **Reflective** — bounces from arena walls and applies direct damage.
- **Missile** — slower-firing, high-damage ammunition with a blast effect.

Some cannons unlock access to additional ammunition types. Ammo is tracked per type, shown in the HUD, and replenished through pickups and the Ammo Pack item. The 2× and 3× multipliers temporarily fan normal or piercing fire into multiple projectiles, consuming the corresponding number of rounds.

### Character and cannon selection

The customization menus manage player-character and cannon selections independently. Items can be gated by progression, purchased with coins, and selected after purchase. A selected character supplies its configured defensive stats (such as shield duration and cooldown); the selected cannon supplies its appearance and weapon access. `GameManager` applies the selected sprites and materials when preparing a level.

### In-game items and power-ups

Before starting a level, players can spend coins to prepare a limited selection of items. Chosen items are passed to gameplay and displayed in item slots with their quantity and cooldown state. Activating one consumes a charge; related items can share cooldown/lockout behavior.

Items implemented in the game include:

- **2× / 3× Multiplier** — temporarily fires multiple normal or piercing bullets per shot.
- **Guns of Retribution** — deploys a supporting turret.
- **Rain of Life** — triggers the configured rain support effect.
- **Nectar of the Gods** — provides a powerful recovery/clear effect against the current number field and grants player invincibility.
- **Ammo Pack** — creates an ammunition pickup.

### Tutorials

The guided tutorial is a sequence of messages, pointers, and staged gameplay demonstrations. `TutorialManager` controls tutorial steps, displays the message UI, and enables or disables scene objects for each part. It temporarily gates player input, spawns example targets and items, and records tutorial completion so it is not presented as a first-time experience again.

### Coins, items, and progression

Coins collected during gameplay update the HUD and the persistent balance. They are spent on character and cannon unlocks and on items prepared for a level. A rewarded advertisement in the main-menu flow can grant **10 coins**.

Experience points are awarded during play and contribute to rank progression. Level progress, stars, and unlock state feed into the level selector and determine which content becomes available.

### Unity Ads

The project integrates Unity Ads for both interstitial and rewarded placements. Interstitial ads are checked against a level-completion counter. Rewarded ads are used for optional coin rewards in the menu and a one-time in-game recovery opportunity; the gameplay reward is only granted when the ad finishes successfully.

### Persistent save data

`GameManager` owns the cross-scene game state and survives scene changes. Its serializable `GameData` model is written under Unity’s `Application.persistentDataPath` to `alphabuild_final.dat`. The save includes the player’s coin and experience balances, rank and level progress, stars, tutorial completion, settings, character/cannon ownership and selection, and other configured progression values. The manager loads this data when initializing and saves it when the application is paused or closed.

## Code structure

The Unity project is organized under `Assets/`. The primary runtime code is in `Assets/Scripts/`:

| Directory | Responsibility |
|---|---|
| `ManagerScripts/` | Cross-scene game state, scene/menu flow, gameplay HUD and stats, tutorial orchestration, ads, and in-game effects. |
| `Player/`, `Cannon/`, `CrossHair/`, `JoyStickScripts/` | Player movement, aiming, firing, cannon behavior, and input controls. |
| `BulletScripts/`, `TurretScripts/`, `MineScripts/`, `LaserScripts/` | Projectile types and other combat actors. |
| `PlayerCharacterCannonSelectionScripts/` | Character and cannon unlock, purchase, and selection behavior. |
| `ItemScript/`, `Coin&powerUp/` | Item preparation and activation, item slots, currency/ammo pickups, and pickup spawning. |
| `SpawnerScripts/`, `NumberScripts/`, `RainOfNumbers/`, `LevelScripts/` | Number spawning and behavior, arena progression, level setup, and loading tips. |
| `SaveLoad/`, `XpRankScripts/` | Serializable save model and experience/rank progression. |
| `ColorUnification/`, `ParticleSystem/`, `CameraScripts/`, `TeleportScripts/` | Visual themes, combat effects, camera behavior, and teleport effects. |
| `Ads/` | Timed rewarded-ad UI support. |

The principal scenes are `Assets/Scenes/MainMenu.unity` and `Assets/Scenes/GamePlay.unity`. Reusable gameplay and menu objects are composed from prefabs in `Assets/Prefabs/`; art, audio, fonts, animation, and plugin assets are held in their corresponding `Assets/` directories.

### Main gameplay flow

1. **Main menu** — choose a level, open settings, browse character/cannon customization, and prepare purchasable items.
2. **Level initialization** — `GameManager` calculates the level’s spawn and combat parameters and applies the selected character and cannon.
3. **Gameplay** — movement and aiming feed into `PlayerAttack`; the selected ammunition is instantiated, target interactions update the arena, and pickups/items update combat resources.
4. **Progress and save** — coins, experience, level results, unlocks, and settings update the central game state and are persisted by the save system.

## Project contents

- `Assets/` — Unity scenes, scripts, prefabs, and the game’s art, audio, and supporting resources.
- `Packages/` — Unity package manifest and package configuration.
- `ProjectSettings/` — Unity project settings.
- `ReadMeAssets/` — screenshots used in this document.
