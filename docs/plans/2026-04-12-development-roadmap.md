# TurnipmanRPG — Development Roadmap

## Story Arc

Tutorial → mini-dungeon (gate key) → first village (objective revealed: "find the three keys") → Area 1 center hub (locked final door visible) → explore Areas 2, 3, 4 → collect 3 keys from 3 dungeons → return to Area 1 → unlock final door → defeat final boss → utopia built.

## World Layout

```
[Tutorial]---[Mini-dungeon]---[Village]---[Area 1 / CENTER]
                                               |
                                        +------+------+
                                        |      |      |
                                     [Area 2] [Area 3] [Area 4]
```

Area 1 is the center hub. The locked final boss door is visible immediately. Areas 2-4 branch outward, each containing a dungeon with a boss guarding a key. Areas are more open than the tutorial — the player is free to explore in any direction.

## Development Philosophy

**Vertical slices, not horizontal layers.** Each phase produces a complete, demoable unit with real art, sound, and UI — not just code. Art, music, and sound ship with the features they serve, not in separate phases at the end.

**Demo milestones:**
| Timeframe | Phase | Demo |
|-----------|-------|------|
| Days | 1-2 | Real character fighting real enemies with sound in test arena |
| ~1 week | 3 | Full tutorial with art, music, mini-boss, village |
| ~2 weeks | 4-5 | Tutorial + weapons/spells + Area 1 hub with locked door |
| Ongoing | 6-8 | Each new area adds a complete playable chunk |

---

## Phase 1: Core Prototype
**Playable:** A colored square that moves, attacks, and dashes in a blank test arena with walls. Visual cues (flash/particle for attacks, trail for dash) communicate actions even with placeholder art.

**Question:** *Does the core movement and combat feel good?*

**Work:**
- Player movement (8-directional, Rigidbody2D)
- Melee attack with commitment
- Dash with cooldown
- Visual cues for primary attack, secondary attack, and dash (particles/flash/trail)
- Gamepad + keyboard input
- All placeholder art (colored squares)

**Done when:** You can move around, attack, and dash — and it feels snappy and fun with just shapes. Visual cues clearly communicate what action is happening.

**Status:** COMPLETE (movement, attack, dash implemented)

---

## Phase 2: Combat Test
**Playable:** The test arena has real pixel art characters. Enemies move, attack, and can be killed. The player can take damage and die. Combat has sound effects.

**Question:** *Is fighting enemies fun? Is the difficulty balance right?*

**Work:**
- Player character sprite + animations (idle, walk, attack, dash, death)
- 2-3 enemy sprites + animations (e.g., a slow melee rabbit, a fast caterpillar, a ranged bird)
- Basic attack effects (slash visual, hit flash)
- Combat sound effects (attack, hit, dash, enemy death, health pickup)
- Basic enemy AI (move toward player, attack)
- Health system (player + enemies) with real health bar UI
- Health drops from dead enemies
- Damage dealing and hit detection
- Death and respawn (restart arena for now)

**Done when:** Fighting feels like a real game loop — engaging, not frustrating, not trivial. Looks and sounds like a real game, not a prototype.

---

## Phase 3: Tutorial Area
**Playable:** The full tutorial zone with proper tiles, real enemies, a mini-dungeon with a mini-boss, music, and a path to the first village where you learn the main objective. Save system works.

**Question:** *Does the game teach itself? Does the mini-dungeon/boss loop feel right at a small scale? Could you demo this to someone and they'd say "this is a real game"?*

**Work:**
- Generate the tutorial tileset in Piskel
- Set up LDtk → Unity pipeline
- Design and build the tutorial area in LDtk (outdoor, with natural enemy placement)
- Mini-dungeon cave near the end of the tutorial
- Mini-boss (tougher variant of a normal enemy, visually distinct — bigger, different color/markings)
- Mini-boss drops gate key to reach the village
- First village: save point, basic NPC sprites, objective reveal ("find the three keys")
- Save/load system (save at village, death respawns at last save)
- Tutorial area music (chiptune)
- Village music

**Done when:** A new player learns movement, combat, and dash through play. Finds a secondary weapon in a chest (placeholder until Phase 4 builds the system). Beats the mini-boss, reaches the village, understands the goal. The whole experience looks, sounds, and feels polished.

**Note:** The tutorial chest for the secondary weapon is placed in Phase 3 but the weapon system isn't built until Phase 4. In Phase 3, it can be a placeholder pickup or the chest can be empty/blocked. Phase 4 fills it in.

---

## Phase 4: Combat Systems
**Playable:** Full combat toolkit online — secondary weapons, spells, magic meter, pause inventory. The tutorial now has the secondary weapon chest working. First village has a weapon shop.

**Question:** *Do secondary weapons and spells feel distinct and useful? Does the loadout system add meaningful choice? Is the magic meter economy balanced?*

**Work:**
- Secondary weapon system (one slot, pause inventory swap)
- Design and implement 2-3 secondary weapons (one for the tutorial chest, one for the village shop)
- Spell system (2-3 slots, magic meter, melee recharge)
- Design and implement 2-3 spells for testing (place 1 hidden in the tutorial area)
- Magic meter UI
- Money system (junk drops from enemies, selling at village, buying weapons)
- Weapon damage upgrade (village gets one)
- Pause inventory screen (weapon swap + spell assignment)
- Secondary weapon and spell visual effects and sound effects
- Place first secondary weapon in tutorial outdoor chest
- Village shop now sells a different secondary weapon

**Done when:** You can play the full tutorial with melee + secondary + spells + dash. The pause inventory works. The village shop works. Different weapon/spell combos feel meaningfully different. The combat loop feels complete.

---

## Phase 5: World Layout + Area 1
**Playable:** The tutorial flows into Area 1 — the center of the open world. The locked final boss door is visible. 1-2 towns with shops. The world layout skeleton exists in LDtk. Fog-of-war map works.

**Question:** *Does the open world feel real? Does seeing the locked door create motivation? Does the hub area feel like a crossroads?*

**Work:**
- **World layout sketch in LDtk:** Place placeholder rooms for the entire world — all 4 areas, all connections, all dungeon entrances, all town locations. Empty rooms, correctly positioned. This is the blueprint.
- Generate Area 1 tileset in Piskel (visually distinct from tutorial)
- Design and build Area 1 rooms in LDtk (the center hub)
- Locked final boss door (visible, clearly important, can't open yet)
- 1-2 towns in Area 1: save points, unique secondary weapons, weapon upgrade
- Transition zone between village/tutorial and Area 1
- Path entrances to Areas 2, 3, 4 (visible but areas not yet built — could be blocked or lead to "coming soon")
- 1-2 hidden spells in Area 1
- Enemy placement in Area 1
- Area 1 music (chiptune)
- Fog-of-war map system
- NPC sprites for Area 1 towns

**Done when:** You can play from tutorial through the village into Area 1. You see the locked door, explore the hub, find towns, buy weapons, discover spells. The world skeleton exists in LDtk even though Areas 2-4 are empty. You feel motivated to go find those keys.

---

## Phase 6: Area 2 + Dungeon 1
**Playable:** Area 2 is fully explorable with its own visual identity. Contains a dungeon with a boss guarding the first key.

**Question:** *Does a full area + dungeon feel complete? Is the boss satisfying with the full combat toolkit? Does this area feel connected to Area 1?*

**Work:**
- Generate Area 2 tileset in Piskel
- Design and build Area 2 rooms in LDtk
- Transition zone between Area 1 and Area 2
- 1-2 towns: save points, unique secondary weapons, possible weapon upgrade
- Design dungeon layout in LDtk
- Boss enemy (unique AI, big health bar, designed around full combat toolkit)
- Boss sprite + animations
- Key reward from boss
- Key collection tracking system
- 1-2 hidden spells in Area 2
- Enemy placement (existing types + possibly a new type)
- Area 2 music, dungeon music, boss music
- NPC sprites for Area 2 towns

**Done when:** You can play from tutorial through Area 1 into Area 2, explore, find towns, beat the dungeon boss, collect the first key. The area has its own identity but feels connected to the rest of the world.

---

## Phase 7: Area 3 + Dungeon 2
**Playable:** Area 3 adds another full explorable region with its own dungeon and second key.

**Question:** *Does the second area feel distinct from Area 2? Is the world starting to feel large and varied?*

**Work:**
- Generate Area 3 tileset in Piskel
- Design and build Area 3 rooms in LDtk
- Transition zone between Area 1 and Area 3
- 1-2 towns: save points, unique secondary weapons, possible weapon upgrade
- Design dungeon layout in LDtk
- Boss enemy (unique AI, different from Area 2 boss)
- Boss sprite + animations
- Key reward from boss
- 1-2 hidden spells in Area 3
- Enemy placement (existing types + possibly a new type)
- Area 3 music
- NPC sprites for Area 3 towns

**Done when:** Two of three keys collected. The world feels substantial. Each area has distinct personality.

---

## Phase 8: Area 4 + Dungeon 3
**Playable:** Area 4 is fully explorable with its own dungeon and the third key. All three keys are now obtainable.

**Question:** *Does the third area feel distinct? Does having all three keys create excitement to return to Area 1?*

**Work:**
- Generate Area 4 tileset in Piskel
- Design and build Area 4 rooms in LDtk
- Transition zone between Area 1 and Area 4
- 1-2 towns: save points, unique secondary weapons, possible weapon upgrade
- Design dungeon layout in LDtk
- Boss enemy (unique AI, third key)
- Boss sprite + animations
- Key reward from boss
- 1-2 hidden spells in Area 4
- Enemy placement (existing types + possibly a new type)
- Area 4 music, dungeon music, boss music
- NPC sprites for Area 4 towns

**Done when:** All three keys are collectible. The full open world is explorable. The player can gather all keys and return to the locked door in Area 1. Each area has distinct personality.

---

## Phase 9: Final Boss + Ending
**Playable:** The complete game — start to finish. The player returns to Area 1 with all three keys, unlocks the final door, fights the final boss, and wins.

**Question:** *Is the final boss a satisfying conclusion? Does the ending feel earned? Is the complete game fun from start to finish?*

**Work:**
- Final door unlock mechanic in Area 1 (all 3 keys)
- Final area behind the door
- Final boss fight (the hardest challenge in the game, designed around full combat toolkit)
- Final boss sprite + animations
- Final boss music
- Victory sequence / utopia ending
- Victory music

**Done when:** The game is completable from start to finish. Tutorial → explore → 3 keys → return to Area 1 → final boss → utopia. Someone can play the whole thing and have a complete experience.

---

## Phase 10: Polish + Balancing
**Playable:** The finished game.

**Question:** *Is it fun from start to finish? Is it too hard? Too easy? Are there soft-locks or dead ends?*

**Work:**
- Full playthrough testing (multiple runs, different routes through the world)
- Difficulty tuning per area (enemies should get harder as you go further out)
- Enemy health/damage balancing
- Spell and weapon balancing (are any useless? any overpowered?)
- Boss difficulty curve
- Save point placement review (are there enough? too many?)
- Economy balancing (do players have enough money? too much?)
- Map/fog-of-war polish
- Bug fixes
- Edge case cleanup

**Done when:** Someone who's never seen the game can play it start to finish and have fun.

---

## Visual Overview

```
PHASE 1:  Core Prototype            → "Does movement/combat feel good?"
  ↓                                    [squares in test arena, visual cues]
PHASE 2:  Combat Test               → "Is fighting enemies fun?"
  ↓  ← PLAYER + ENEMY ART + SFX        [real sprites, sound, health bar]
PHASE 3:  Tutorial Area             → "Does the game teach itself?"
  ↓  ← TILESET + MUSIC + SAVE          [full tutorial, mini-boss, village]
PHASE 4:  Combat Systems            → "Do weapons/spells add choice?"
  ↓  ← FULL COMBAT TOOLKIT + UI        [weapons, spells, inventory, magic meter]
PHASE 5:  World Layout + Area 1     → "Does the hub feel right?"
  ↓  ← WORLD SKELETON + MAP            [center hub, locked door, towns]
PHASE 6:  Area 2 + Dungeon 1       → "Does area + dungeon feel complete?"
  ↓  ← FIRST KEY                       [full vertical slice, first boss]
PHASE 7:  Area 3 + Dungeon 2       → "Does the world feel large?"
  ↓  ← SECOND KEY                      [another full vertical slice]
PHASE 8:  Area 4 + Dungeon 3       → "Does the third area feel distinct?"
  ↓  ← THIRD KEY                       [all keys collectible, full world]
PHASE 9:  Final Boss + Ending       → "Is the ending satisfying?"
  ↓  ← RETURN TO AREA 1                [game completable start to finish]
PHASE 10: Polish + Balancing        → "Is it fun start to finish?"
                                       [THE FINISHED GAME]
```

## Tools & Pipeline

- **Level design:** LDtk → LDtkToUnity importer → Unity Tilemap
- **Tileset art:** Piskel (one tileset per area, generated + hand-tweaked)
- **Character art:** Piskel (player + enemies in Phase 2, bosses with each dungeon)
- **Music:** Chiptune (each area gets its theme when built)
- **Sound effects:** Ship with the phase that needs them
- **Input:** Unity Input System (gamepad + keyboard equally supported)
- **Editor integration:** Coplay MCP for direct Unity interaction
