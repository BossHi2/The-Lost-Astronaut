================================================================
  TINY QUESTERS  —  Bat  (FREE)
  A free top-down pixel-art monster from
  "Tiny Questers — Complete Pixel RPG Asset Pack" by Bobddadoo
  (https://bobddadoo.itch.io)
================================================================

Thanks for downloading! This is the FREE Bat — a fully-animated
top-down pixel monster you can drop straight into your dungeons,
caves and night-time overworld. Use it in your personal and
commercial games, no strings attached.

Like the Bat? The full MONSTER STARTER PACK adds the Goblin,
Skeleton and Slime (4 monsters total, with attack & death sets)
on the same itch.io page.


----------------------------------------------------------------
  WHAT'S INSIDE  (16 clips, 120 frames)
----------------------------------------------------------------

  BAT  (64 x 64 — attack clips are larger, see note)
    idle    — down 6 / up 6 (back view)
    walk    — down 6 / up 6 / left 6 / right 6
    attack  — down 12 / up 12 / left 12 / right 12  (full 4-direction)
    effect  — down 6 / up 6 / left 5 / right 5  (attack projectile FX)
    hit     — down 6
    die     — down 8

  The bat is an airborne enemy with a continuous wing-flap, so
  its down / up walk uses the same flap loop as its idle on
  purpose. walk_left is the side flap; walk_right is walk_left
  mirrored. Up (idle_up / walk_up) is the back view.

  ATTACK NOTE: the attack is a lunge whose reach extends well
  past the body, so those clips use a larger canvas than 64 x 64
  (down 64 x 137, up 64 x 145, left & right 128 x 128). They keep
  the same feet-center pivot, and attack_right is attack_left
  mirrored. Slice each attack atlas at its own cell size.

  EFFECT: effect_<dir> is the bat's attack projectile (a spit /
  sonic bolt) on a 64 x 64 canvas — play it alongside the matching
  attack_<dir> to land a ranged hit.

  Each clip is provided as: individual PNG frames (single/),
  a horizontal sprite sheet (atlas/), POT-padded copies of both
  (single_pot/, atlas_pot/), and preview GIFs at 1x and 5x.


----------------------------------------------------------------
  FOLDER STRUCTURE
----------------------------------------------------------------

tiny-questers-bat/
  README.txt              ... this file
  LICENSE.txt             ... license terms (please read)

  png/
    single/bat/           ... individual PNG frames per clip
    atlas/                ... horizontal sprite sheets, 64 px/frame
                              one PNG per clip (bat_<clip>.png)
    single_pot/bat/       ... power-of-two padded versions of single/
    atlas_pot/            ... power-of-two padded versions of atlas/

  gif/
    1x/                   ... preview GIFs, original size
    5x/                   ... preview GIFs, 5x upscaled (nearest)


----------------------------------------------------------------
  HOW TO USE
----------------------------------------------------------------

  Unity:
    1. Drop png/single/ or png/atlas/ into Assets/.
    2. Set Texture Type = Sprite (2D and UI).
    3. Filter Mode = Point (no filter), Compression = None
       to keep the pixel-art crisp.
    4a. Single frames: select all PNGs of one clip and drag into
        the scene to auto-create an Animation clip.
    4b. Atlas: Sprite Mode = Multiple, Sprite Editor,
        Slice -> Grid By Cell Size, 64 x 64.

  Godot:
    1. Import png/single/ (or atlas/) into your project.
    2. In the Import dock set Filter = Off and Mipmaps = Off.
    3. Use AnimatedSprite2D / AnimationPlayer.

  General:
    - Loop idle_* and walk_*; play attack_* / hit_* / die once.
    - The bat hovers, so idle and walk are the same flap loop —
      a floating enemy can simply play it continuously.
    - Timing: ~6.67 fps matches the preview GIFs.


----------------------------------------------------------------
  LICENSE — SHORT VERSION  (full text in LICENSE.txt)
----------------------------------------------------------------

  [OK] Use in unlimited personal and commercial projects
  [OK] Credit appreciated but not required
  [OK] Modify and edit freely to fit your game
  [NO] Do not resell or redistribute the assets on their own,
       or as part of another asset pack
  [NO] Do not use these assets to train AI / machine-learning
       models

  In short: use it in as many of your games as you like — just
  don't repackage and sell the art itself.


----------------------------------------------------------------
  FEEDBACK
----------------------------------------------------------------

  Which monster should be added next? Spotted a misaligned frame?
  Leave a comment on the itch.io page — feedback directly shapes
  "Tiny Questers — Complete Pixel RPG Asset Pack".

  Thanks for your support — happy dev!  :)


================================================================
  (c) Bobddadoo — https://bobddadoo.itch.io
================================================================
