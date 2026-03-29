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

**Combat feel:** Attacks come out quickly but have commitment — once you start an attack, you can't cancel it and must wait for it to finish before acting again. The player is not overpowered. Speed comes from smart use of all your tools (melee, secondary, spells, dash) together, not from rapid-fire combos.

**Secondary weapon:** One slot. Interchangeable. Found as loot drops in the world. Swapped via the pause inventory screen — no instant switching mid-combat, making your choice a deliberate commitment.

**Spells:** 2-3 equippable slots. Offensive damage tools on a magic meter. Found as exploration rewards hidden in the world. Swapped via pause inventory with a per-slot selection screen. No duplicate spells across slots.

**Magic meter:** Recharges by landing melee attacks. Rewards aggressive play.

**Health:** Hit points that increase with progression. Healed only by random drops from killed enemies. No healing spells or items.

**Save points / rest:** Manual save at specific locations (town rest points). Saving fully restores health and magic. Death rolls back to your last save.

## 3. World Structure

**Overworld:** Open world with 4-6 distinct areas. Fog-of-war map that reveals as you explore. No fast travel — you walk everywhere, with dash for faster movement. The world is dangerous for vegetables, with safe pockets of civilization.

**Tutorial area:** A structured intro zone that teaches core mechanics (movement, combat, dash). At the end, the player is told what they need to accomplish to beat the game, and the world opens up.

**Towns:** Safe vegetable settlements scattered across the overworld. Each town has:
- A rest/save point (fully restores health and magic)
- A unique secondary weapon for sale
- NPCs but minimal dialogue — no story-heavy text
- Some towns have a one-time weapon damage upgrade (not all towns, and only one per town)

**Interior areas:** Self-contained linear zones accessed through specific entryways in the overworld. These are where key progression items are found. Each ends with a boss fight guarding the item. These are the main progression gates.

**Bosses:** Rare and meaningful. No special intros — just a big health bar. Placed directly before critical progression items. When a player sees a boss, they know it matters.

**Enemies:** Cute vegetable eaters — rabbits, deer, caterpillars, birds, etc. Adorable from a human perspective, terrifying from the vegetables' point of view.

**End goal:** Collect the tools and resources from interior areas to build a safe vegetable utopia. No central villain — the world itself is the challenge.

## 4. Progression & Economy

**Progression is exploration-gated, not grind-based.** Staying in one area and farming enemies gives you nothing meaningful. Progress comes from pushing into new territory.

**Progression items:** Found at the end of interior areas (behind bosses). These are the key items needed to beat the game.

**Weapon upgrades:** Limited number, found in specific towns only. One per town max. Makes finding a town with an upgrade feel rewarding.

**Secondary weapons:** One available for purchase in each town. The player builds their collection over time but can only equip one at a time.

**Spells:** Hidden in the world as exploration rewards. Not in towns, not from enemies — you find them by exploring off the beaten path.

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

## 6. Build Order

**Phase 1 — Core Movement & Combat Prototype**
- Top-down player movement
- Primary melee attack (with commitment — no canceling)
- Dash (near-instant startup, cooldown, works for dodging and traversal)

**Phase 2 — Tutorial Area & Enemies**
- Design and build the tutorial zone
- Create first enemy types (cute vegetable eaters)
- Teach the player movement, combat, and dash through gameplay
- End of tutorial reveals the main objective

**Phase 3 — Open World & Interior Areas**
- Design the 4-6 overworld areas and their connections
- Build towns with save points, secondary weapon shops, weapon upgrades
- Build interior areas (linear, boss at the end, progression item reward)
- Fog-of-war map system

**Phase 4 — Secondary Weapons & Spells**
- Secondary weapon system (inventory swap, loot drops, town shops)
- Spell system (2-3 slots, magic meter, melee recharge)
- Pause inventory screen for loadout management

**Phase 5 — Polish & Completion**
- Health/magic balancing
- Enemy variety and tuning
- Music and sound
- Final progression item and endgame (building the utopia)
