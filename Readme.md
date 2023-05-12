# Instructions

## Gameplay Video
Here you can find a [Gameplay Video](https://github.com/TheBlackOne/Asteroinks/releases/latest/download/Asteroinks.Gameplay.mp4)

## How to play

- Clone the repository, open in Unity, press play *or*
- Download the [Windows 64 build](https://github.com/TheBlackOne/Asteroinks/releases/latest/download/Win64.zip) from releases, open the ZIP archive and start the binary inside.

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
- There is no logic for score, lives or logic for Game Over, I focused on the code for the core game loop.

# Code Design
## Overview
- Overall, I tried to work in a Unity-like fashion: Figure out which constructs and methods are best suited for what I wanted to do, instead of reinventin the wheel.
- The project makes heavy use of builtin Unity features such as Rigidbody 2D and Polygon Collider 2D to simulate movement and collision. That cuts down the need of boilerplate code for such functions to a minimum.
- There is only one scene, that eliminates the need for dealing e.g. with manager classes that get reloaded and need constructs like Singleton, Service Locator or the like.
- Manager classes for the overall logic for bullets (aka. birds) and asteroids (aka pigs), controller classes for instance-specific logic attached to the respective prefab.
- The game objects for bullets and asteroids are instantiated from their prefabs at runtime; the game object for the ship is set up in the scene.
- There are object pools for bullets and asteroids, as they are spawned and despawned frequently. For such a simple project an object pool is not strictly necessary, but the Unity builtin [ObjectPool](https://docs.unity3d.com/ScriptReference/Pool.ObjectPool_1.html) class is very easy to work with and I consider object pools a good practice in Unity in general.
- Results of expensive calls (like `GetComponent()`) are cached.
- All values that are relevant for game design / balancing are exposed as properties of components on scene objects or prefabs, there are no hard coded / magic numbers.

# Challenges
- This is the first ever project I put together in Unity.
- Some concepts took a moment to wrap my head around, like local vs. world coordinates, units vs. pixels, and quaternions.
- Navigating the references one can find about Unity problems needs to be done carefully. Some things work a bit different in 2D space, some solutions are outdated or - well ugly :-)
- There was one occasion where game input would stop to work completely, restarting the Unity Editor fixed it.

# Things *not* done/implemented
> if this were a more complex project, these would be good to do
- A more proper way to handle dependencies instead of `GetComponent()` or `GameObject.Find()`.
- Sprite Sheets / Sprite Atlases
- Sprites in size of Power of Two
- Collision matrizes
- Pre-heating the object pools
- Missing pieces to make it a complete game, like:
	- Game Over logic
	- Score
	- Lives
	- UI / HUD
	- Sound
	- Animations & effects