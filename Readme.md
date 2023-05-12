# Instructions

## How to play

- Clone the repository, open in Unity, press play *or*
- Download the newest binary from releases and start the binary.

## Controls
- A: Turn left
- D: Turn right
- W: Fire thruster
- Space: Fire bullet

## Game Design
- The game starts with one large asteroid.
- Shooting an asteroid will spawn three new ones in the same position of the next smaller size.
- When all of the smallest size have been destroyed, one new large asteroid is spawned.
- The bullets have a limited lifetime.
- All objects leaving the screen will be teleported to the respective opposite side of the screen.
- The ship cannot break; you have to provide thrust in the opposite direction.
- When the ship collides with an asteroid, both will be destroyed and the ship will be teleported back to the starting position.
- There is no logic for score, lives or logic for Game Over.