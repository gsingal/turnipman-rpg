# TurnipmanRPG — Game Design Document

## 1. Game Overview

**TurnipmanRPG** is a top-down 2D action RPG with pixel art graphics. You play as a turnip in a world of sentient vegetables, navigating a dangerous open world filled with cute-but-deadly vegetable eaters. The goal: gather the tools and resources needed to build a safe vegetable utopia.

- **Genre:** Top-down 2D action RPG
- **Art style:** Pixel art
- **Music:** Chiptune (with flexibility to add non-retro instruments)
- **Platform:** Multi-platform, single player
- **Input:** Gamepad and keyboard equally supported
- **Audience:** Kids + parents (ages 8-14), accessible with depth
- **Tone:** Lighthearted, charming, low-stakes feel but real tension in combat. Original — not derivative of existing games

## 2. Core Combat

**Primary weapon:** A fixed melee weapon. Always available, core of the combat loop.

**Dash:** Quick burst of speed with near-instant startup. Has a cooldown — no spamming. Used for dodging attacks in combat and faster traversal in the overworld. Cannot dash through obstacles or enemies.

**Combat feel:** Attacks come out quickly but have commitment — once you start an attack, you can't cancel it and must wait for it to finish before acting again. The player is not overpowered. Speed comes from smart use of all your tools (melee, secondary, spells, dash) together, not from rapid-fire combos. Visual cues for all actions — flash/particle for attacks, trail for dash.

**Secondary weapon:** One slot. Interchangeable. The first is found in a chest early in the tutorial. Additional secondary weapons are available for purchase in towns (each town sells a unique one). Swapped via the pause inventory screen — no instant switching mid-combat, making your choice a deliberate commitment. ~6-9 secondary weapons total across the game.

**Spells:** 2-3 equippable slots. Offensive damage tools on a magic meter. 6-8 spells total, found as exploration rewards hidden throughout the world. Swapped via pause inventory with a per-slot selection screen. No duplicate spells across slots. Added incrementally — not all spells need to exist at once.

**Magic meter:** Recharges by landing melee attacks. Rewards aggressive play.

**Health:** Hit points that increase with progression. Healed only by random drops from killed enemies. No healing spells or items.

**Save points / rest:** Manual save at specific locations (town rest points). Saving fully restores health and magic. Death rolls back to your last save.

## 3. World Structure

**World layout:**

```
[Tutorial]---[Mini-dungeon]---[Village]---[Area 1 / CENTER]
                                               |
                                        +------+------+
                                        |      |      |
                                     [Area 2] [Area 3] [Area 4]
```

**Area 1 (Center)** is the hub of the open world. The player arrives from the village and immediately sees the locked final boss door. To unlock it, they must venture out to Areas 2, 3, and 4 to find the three keys. Every time they pass through Area 1, the locked door reminds them of their progress.

**Tutorial area:** A structured outdoor intro zone that teaches core mechanics through play:
1. Open area — movement + dash
2. First enemies — melee combat
3. Chest — first secondary weapon, practice on more enemies
4. Mini-dungeon — tougher enemies requiring full toolkit
5. Mini-boss fight (tougher variant of a normal enemy, visually distinct) — proves mastery of melee + secondary + dash
6. Gate key → first village → main objective revealed ("find the three keys")

The player's first secondary weapon is found early in the tutorial (outdoor chest), not in a town. This teaches the weapon-swap system before the world opens up.

**Overworld:** Open world with 4 distinct areas. Fog-of-war map that reveals as you explore. No fast travel — you walk everywhere, with dash for faster movement. The world is dangerous for vegetables, with safe pockets of civilization. Areas are separated by natural geography (rivers, cliffs, dense forest) with transition zones that blend adjacent tilesets. Areas 2-4 are more open than the tutorial — the player is free to explore.

**Towns:** 1-2 safe vegetable settlements per area, scattered across the overworld. Each town has:
- A rest/save point (fully restores health and magic)
- A unique secondary weapon for sale
- NPCs but minimal dialogue — no story-heavy text
- Some towns have a one-time weapon damage upgrade (not all towns, and only one per town)

**Interior areas (dungeons):** Self-contained linear zones accessed through specific entryways in the overworld. One per outer area (3 total). Each ends with a boss fight guarding a key. These are the main progression gates.

**Bosses:** Rare and meaningful. No special intros — just a big health bar. Designed around the full combat toolkit (melee + secondary + spells + dash). Placed directly before keys. When a player sees a boss, they know it matters.

**Mini-boss:** A tougher variant of a normal enemy, visually distinct enough to be memorable but not a completely new design. Guards the tutorial gate key.

**Enemies:** Cute vegetable eaters — rabbits, deer, caterpillars, birds, etc. Adorable from a human perspective, terrifying from the vegetables' point of view.

**End goal:** Collect the three keys from the dungeons in Areas 2-4, return to Area 1, unlock the final door, defeat the final boss, and build the vegetable utopia.

## 4. Progression & Economy

**Progression is exploration-gated, not grind-based.** Staying in one area and farming enemies gives you nothing meaningful. Progress comes from pushing into new territory.

**Three keys:** Found at the end of dungeons in Areas 2, 3, and 4 (behind bosses). All three are needed to unlock the final door in Area 1.

**Weapon upgrades:** Limited number, found in specific towns only. One per town max. Makes finding a town with an upgrade feel rewarding.

**Secondary weapons:** The first is found in a chest during the tutorial. Additional secondary weapons are available for purchase in each town (~5-8 towns total). The player builds their collection over time but can only equip one at a time.

**Spells:** 6-8 total, hidden in the world as exploration rewards. Not in towns, not from enemies — you find them by exploring off the beaten path. Added incrementally across areas.

**HP increases:** Found through progression (details TBD — could be in interior areas, hidden in the world, or tied to milestones).

**Money:** Earned by selling designated junk items dropped by enemies. Spent on secondary weapons and weapon upgrades in towns.

**There is no XP or leveling system.** You get stronger through items, not numbers going up.

## 5. UI & Controls

**Input:** Gamepad and keyboard/mouse equally supported from the start.

**Pause inventory screen:** Accessed mid-gameplay. Used to:
- Swap your secondary weapon (select one from your collection)
- Assign spells to your 2-3 slots (per-slot selection, no duplicates)
- Pauses the game — gives the player a breather to make strategic decisions

**Map:** Fog-of-war style. Opens as a screen/overlay. Shows explored areas, unexplored areas hidden.

**HUD (minimal):**
- Health bar
- Magic meter
- Boss health bar (appears only during boss fights)

**Text/dialogue:** Kept to a minimum. Only used for tutorials and explaining the main objective. No heavy dialogue or story text.

## 6. Build Order (Vertical Slices)

Each phase produces a demoable, polished unit — art, sound, and UI ship with the features they serve.

**Phase 1 — Core Prototype** (placeholder squares, visual cues for actions)
**Phase 2 — Combat Test** (real sprites, enemies, health, SFX)
**Phase 3 — Tutorial** (tileset, mini-boss, village, music, save system)
**Phase 4 — Combat Systems** (weapons, spells, inventory, UI)
**Phase 5 — World Layout + Area 1** (sketch full world in LDtk, build Area 1 center hub with locked door)
**Phase 6 — Area 2 + Dungeon 1** (full vertical slice — tileset, towns, dungeon, boss, first key, music)
**Phase 7 — Area 3 + Dungeon 2** (full vertical slice, second key)
**Phase 8 — Area 4 + Dungeon 3** (full vertical slice, third key, all keys collectible)
**Phase 9 — Final Boss + Ending** (return to Area 1, unlock door, final boss, victory)
**Phase 10 — Polish + Balancing** (playthrough testing, difficulty tuning, bug fixes)

See `docs/plans/2026-04-12-development-roadmap.md` for the detailed roadmap.
