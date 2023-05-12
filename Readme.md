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

# Code Design
## Overview
- Overall, I tried to work in a Unity-like fashion: Figure out which constructs and methods are best suited for what I wanted to do, instead of reinventin the wheel.
- The project makes heavy use of buildin Unity features such as Rigidbody 2D and Polygon Collider 2D to simulate movement and collision. That cuts down the need of boilerplate code for such functions to a minimum.
- There is only one scene, that eliminates the need for dealing e.g. with manager classes that get reloaded and need constructs like Singleton, Service Locator or the like.
- Manager classes for the overall logic for bullets (aka. birds) and asteroids (aka pigs), controller classes for instance-specific logic attached to the respective prefab.
- The game objects for bullets and asteroids are instantiated from their prefabs at runtime; the game object for the ship is set up in the scene.
- There are object pools for bullets and asteroids, as they are spawned and despawned frequently. For such a simple project an object pool is not strictly necessary, but the Unity builtin [ObjectPool](https://docs.unity3d.com/ScriptReference/Pool.ObjectPool_1.html) class is very easy to work with and I consider object pools a good practice in Unity in general.
- Results of expensive calls (like `GetComponent()`) are cached.