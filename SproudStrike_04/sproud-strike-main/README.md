# UWP 2D Shooter Game Project

## Project Description

This project is a simple 2D shooter game created in Unity as part of the **UWP (eXtended Reality, Games, Immersive Systems)** course. The game follows a **top-down view** format and involves a player, enemy spawners, enemies, and a HUD system. The main objective of the game is for the player to survive and eliminate enemies while tracking the player's health and the number of enemies killed.

## Features

### **Player**

- Moves in four directions: left, right, up, and down.
- Shoots a projectile in the last moved direction.

### **Enemy Spawner**

- Spawns enemies within predefined borders.
- Spawns a set number of enemies per a given time interval.

### **Enemy**

- Moves in a direction set by the **EnemySpawner**.
- Gets destroyed when it moves out of camera view.
- If it detects the player, it stops and shoots a projectile towards the player a set number of times before continuing its path.

### **HUD**

- Displays **Player’s health**.
- Tracks **number of enemies killed**.
