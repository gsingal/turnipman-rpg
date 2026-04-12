# TurnipmanRPG — Development Roadmap

## Story Arc

Tutorial → first village (objective revealed: "find the three keys") → open world exploration across 4 areas → collect 3 keys from 3 dungeons → unlock final building → defeat final boss → utopia built.

The tutorial mini-dungeon has a mini-boss that drops a gate key to reach the first village. This key is NOT one of the three progression keys — it just unlocks passage to the village.

---

## Phase 1: Core Prototype
**Playable:** A colored square that moves, attacks, and dashes in a blank test arena with walls.

**Question:** *Does the core movement and combat feel good?*

**Work:**
- Player movement (8-directional, Rigidbody2D)
- Melee attack with commitment
- Dash with cooldown
- Gamepad + keyboard input
- All placeholder art (colored squares)

**Done when:** You can move around, attack, and dash — and it feels snappy and fun with just shapes.

---

## Phase 2: Combat Test
**Playable:** The test arena has enemies that move, attack, and can be killed. The player can take damage and die.

**Question:** *Is fighting enemies fun? Is the difficulty balance right?*

**Work:**
- Basic enemy AI (move toward player, attack)
- Health system (player + enemies)
- Health drops from dead enemies
- Damage dealing and hit detection
- Death and respawn (restart arena for now)
- 2-3 enemy types (e.g., a slow melee rabbit, a fast caterpillar, a ranged bird)

**Done when:** Fighting feels like a real game loop — engaging, not frustrating, not trivial.

---

## Phase 3: Tutorial Area
**Playable:** The tutorial zone with proper tiles, enemies, a mini-dungeon with a mini-boss, and a path to the first village where you learn the main objective.

**Question:** *Does the game teach itself? Does the mini-dungeon/boss loop feel right at a small scale?*

**Work:**
- Generate the base tileset in Piskel
- Set up LDtk → Unity pipeline
- Design and build the tutorial area in LDtk
- Place enemies that introduce combat naturally
- Mini-dungeon cave near the end of the tutorial
- Mini-boss fight (drops a gate key to reach the village — not a progression key)
- First village with a save point
- Objective reveal: "find the three keys"

**Done when:** A new player learns controls through play, beats the mini-boss, reaches the village, and understands the goal.

---

## Phase 4: First Open World Area + First Dungeon
**Playable:** The tutorial flows into a real explorable area with a town and a full dungeon with a boss guarding the first key.

**Question:** *Does the open world exploration loop work? Does a full-sized dungeon feel different from the tutorial mini-dungeon?*

**Work:**
- Design Area 1 in LDtk
- First town: save point
- First full dungeon with boss guarding first key

**Done when:** You can play from tutorial through Area 1, beat the boss, and get the first key.

---

## Phase 5: Full World Design
**Playable:** All 4 areas connected and walkable. Towns placed with save points. Dungeon entrances exist but are empty. The full world map is explorable end-to-end.

**Question:** *Does the world feel right? Is the layout fun to navigate? Are areas distinct? Is the pacing of discovery satisfying?*

**Work:**
- Generate remaining tilesets in Piskel (one per area for visual variety)
- Design all 4 areas in LDtk with connections between them
- Place all towns (save points)
- Place dungeon entrances (locked / blocked for now)
- Place spell hiding spots (empty for now, but locations are designed)
- Fog-of-war map system
- Enemy placement across all areas (using enemy types from Phase 2)

**Done when:** You can walk from the tutorial through all 4 areas, visit every town, see every dungeon entrance, and the world feels like a cohesive place worth exploring.

---

## Phase 6: Dungeons + Bosses
**Playable:** All 3 key dungeons are playable — linear interior areas with enemies, a boss at the end, and a key as the reward. Final area with last boss is playable.

**Question:** *Are the dungeons challenging and fun? Do bosses feel like real gatekeepers? Is the final boss a satisfying conclusion?*

**Work:**
- Design 3 dungeon layouts in LDtk
- Create 3-4 boss enemies (unique AI, big health bars)
- Key collection system (track which keys the player has)
- Final area: unlocks when all 3 keys collected
- Final boss fight
- Victory screen / utopia ending

**Done when:** The game is completable from start to finish — tutorial → explore → 3 keys → final boss → utopia.

---

## Phase 7: Secondary Weapons + Spells
**Playable:** Full combat system online — each town sells a unique secondary weapon, spells are hidden throughout the world, pause inventory lets you manage your loadout.

**Question:** *Do the secondary weapons and spells feel distinct and useful? Does the loadout system add meaningful player choice? Is the magic meter economy balanced?*

**Work:**
- Secondary weapon system (one slot, pause inventory swap)
- Design and implement unique secondary weapons for each town
- Spell system (2-3 slots, magic meter, melee recharge)
- Place spells in their hiding spots across the world
- Weapon damage upgrades in designated towns
- Money system (junk drops, selling at towns, buying weapons)
- Pause inventory screen (weapon swap + spell assignment)

**Done when:** The full combat toolkit is available. Different players might tackle the same dungeon with different loadouts.

---

## Phase 8: Character Art + Animation
**Playable:** The game looks real. Placeholder shapes replaced with pixel art characters, enemies, and effects.

**Question:** *Does the game have visual identity? Do animations communicate attacks, damage, and movement clearly?*

**Work:**
- Player character sprite + animations (idle, walk, attack, dash, death)
- Enemy sprites + animations for each type
- Boss sprites + animations
- NPC sprites for towns
- Attack effects (slash, spell impacts)
- Health/magic drops visual
- UI art (health bar, magic meter, boss bar, inventory screen)

**Done when:** The game looks like a finished pixel art game, not a prototype.

---

## Phase 9: Music + Sound
**Playable:** The game sounds complete — each area has music, combat has impact, UI has feedback.

**Question:** *Does the audio reinforce the tone? Do areas feel distinct? Does combat feel impactful?*

**Work:**
- Chiptune tracks per area (4 area themes + dungeon theme + boss theme + town theme)
- Sound effects (attack, dash, hit, enemy death, pickup, menu)
- Ambient sounds if needed
- Victory/ending music

**Done when:** You can play with sound on and it feels like a complete experience.

---

## Phase 10: Polish + Balancing
**Playable:** The finished game.

**Question:** *Is it fun from start to finish? Is it too hard? Too easy? Are there soft-locks or dead ends?*

**Work:**
- Full playthrough testing and difficulty tuning
- Enemy health/damage balancing across all areas
- Spell and weapon balancing
- Save point placement review
- Bug fixes
- Edge case cleanup

**Done when:** Someone who's never seen the game can play it start to finish and have fun.

---

## Visual Overview

```
PHASE 1:  Core Prototype            → "Does movement/combat feel good?"
  ↓                                    [square in test arena]
PHASE 2:  Combat Test               → "Is fighting enemies fun?"
  ↓                                    [enemies + health + death]
PHASE 3:  Tutorial Area             → "Does the game teach itself?"
  ↓  ← TILESET ART STARTS             [tutorial → mini-dungeon → village]
PHASE 4:  First Area + Dungeon      → "Does the full loop work?"
  ↓                                    [tutorial → area 1 → boss → key]
PHASE 5:  Full World Design         → "Does the world feel right?"
  ↓                                    [all 4 areas walkable]
PHASE 6:  Dungeons + Bosses         → "Are dungeons/bosses satisfying?"
  ↓                                    [game completable start to finish]
PHASE 7:  Secondary Weapons+Spells  → "Does loadout choice matter?"
  ↓                                    [full combat system]
PHASE 8:  Character Art+Animation   → "Does it have visual identity?"
  ↓  ← CHARACTER ART                  [looks like a real game]
PHASE 9:  Music + Sound             → "Does audio reinforce the tone?"
  ↓  ← MUSIC/SFX                      [sounds complete]
PHASE 10: Polish + Balancing        → "Is it fun start to finish?"
                                       [THE FINISHED GAME]
```

## Tools & Pipeline

- **Level design:** LDtk → LDtkToUnity importer → Unity Tilemap
- **Tileset art:** Piskel (programmatically generated, hand-tweaked)
- **Character art:** Piskel (Phase 8)
- **Music:** Chiptune (+ non-retro instruments if needed)
- **Input:** Unity Input System (gamepad + keyboard equally supported)
- **Editor integration:** Coplay MCP for direct Unity interaction
