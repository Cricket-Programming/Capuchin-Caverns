## Capuchin Caverns Codebase

- Capuchin Caverns is a live multiplayer VR title on the Meta Horizon Store (15,000+ downloads, 2000+ Monthly Active Users at peak) which I built and shipped.
- This codebase contains the entire version-controlled Unity Project for Capuchin Caverns, including C# scripts, prefabs, scenes, media, art assets, dependencies, project settings.
- The systems in this repository are all deployed to production and have proven their worth in live multiplayer conditions.

## Overview
5,000-10,000 lines of original gameplay + systems code throughout project.
This code has been refactored, optimized and improved so many times.

## Key Directories
- [C# Scripts](https://github.com/Cricket-Programming/Capuchin-Caverns/tree/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts) (most are MonoBehaviors or MonoBehaviorPunCallbacks, some are interfaces)
- [Prefabs](https://github.com/Cricket-Programming/Capuchin-Caverns/tree/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Prefabs)

## Systems Underlying Key Features
⚪ **Feature: Physics-based locomotion (Derived from an open-source gorilla-style locomotion project; reworked to fit my XR architecture)**

[Movement Script](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/NewGorillaLocomotionScripts/Player.cs)

This movement script is what I attach to an XR Origin component, which has the player's camera and hands; I've applied this base VR movement system in surprising ways by varying constraints - creating new movement systems like slippery walls, flying, and zero gravity. All the while, I've made sure to reduce motion sickness by making momentum transfers feel self-controlled, mitigating a common problem many other VR apps face.

⚪ **Feature: Real-time multiplayer**

[My Networking Controller Script (this talks directly to Photon PUN 2 API)](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Resources/PhotonVR/Scripts/PhotonVRManager.cs)

Multiplayer system is built on Photon PUN 2 and refactored into a centralized networking layer.
Each client owns a networking controller responsible for player instantiation, state synchronization, and RPC dispatch via the Photon Network.
For instance, let's say one player wanted to change his color. Since this new color needs to be broadcasted to all other players, the networking controller script is a low overhead way to get to this outcome. 

Here's the color-changing script, one of many scripts in the codebase that directly work with my networking controller script. [Color Changing Script](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/Computer%20Scripts/Colorer.cs)

⚪ **Feature: Cosmetic Economy with real money payments system + virtual currency saved to game backend + shop with 63 distinct add-on items to purchase.**

-  Each player gets a set amount of in-game currency each day they login (helps with retention).
[Daily Marbles Reward Script](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/DailyMarblesReward.cs)

- This currency syncs to Azure PlayFab (my game's backend).
[Currency Manager Script](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/CurrencyManager.cs)

- Cosmetic Shop has 63 Distinct Cosmetic SKUs that can be bought with in-game currency. About half of them are available year round and the other half is seasonal. You can find the year round ones as prefabs in this folder: [Cosmetic Prefabs](https://github.com/Cricket-Programming/Capuchin-Caverns/tree/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Prefabs/Cosmetics)

- Real Money Payment Systems (processed thousands of dollars). I implemented a VR storefront flow that launches the Oculus checkout for a specific SKU, consumes the purchase to prevent double-spends, then credits the player’s PlayFab currency balance. Purchases are initiated from in-world interactions (hand collider trigger) so the shop feels native to VR. Side Note: Meta's compliance process was a pain! 
[In App Purchasing Script](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/PlayFabShopManager.cs)


The cosmetic shop has skins available for purchase: skin equiping syncs to all other players in the room. Network buffers allow new players who join late to an existing server to be “caught up” on game states such as who is "it" in the tag mode. [NetworkSkin.cs]() is a script that is a reusable component doing the networking heavy lifting for all skins in the cosmetic shop.
⚪ **Feature: Explore 3 levels of terrifying horror**

The names of the horror monsters are Fluffy, Gruffy, and Scruffy. They roam different parts of the game and are able to dynamically pathfind their way around obstacles to the nearest player to eat them alive. I used Unity's Navigation Mesh AI Agent because it's system can traverse environments aren't predictable or stable. In order for every player to see the monster in the exact same place, these monsters had to be networked too. See this script for the implementation: [Networked Enemy Follow Script](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/NetworkedEnemyFollow.cs)

When the monster eats the players, they player get teleported back to the spawn.
[Teleport and jumpscare script](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/TeleportAndJumpscare.cs)

⚪ **Feature: Jump on parkour obstacle courses and super bouncy trampolines**

"Take a simple idea and take it seriously." I've scattered trampolines across Capuchin Caverns - players shoot into the sky and come back down at 9.8 m/s^2. To achieve this, force is applied upward on the player's Rigid Body for a time - in other words, an impulse. I later learned the actual physics (impulse-momentum theorem (F*t=delta p) in my AP Physics 1 class and it's helped me build bigger and better trampolines.

[Trampoline Impulse Application Script:](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/ApplyForce.cs)

⚪ **Feature: Play tag inside an ever-changing forest and village**

This script represents the pinnacle of my work with event-driven RPCS. It required me to understand in depth how to search through the multiplayer client list and manipulating data streams. It now powers the "tag mode" in my game. It makes me proud that I followed through on it, considering it's difficulty all the way to production.
[Tag Mode Script](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/TagScript6.cs)


⚪ **Feature: Do an explosive, roller coaster train ride**
The "train" is basically a collection of objects that move along a path of waypoints. The waypoints are embedded into tracks, which are prefabs so that I could easily add custom routes for the train. I also had to implement networking logic for the train so that it appears in the same place for every single player. The MasterClient controls the train's movement. The train has a PhotonView component, which makes every other player see the train as the MasterClient sees it, thereby creating "syncing."

[Waypoint Train Script](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/TrainController.cs)
⚪ **Feature: A trick glass bridge with glass stepping stones**
Players must jump across glass bridge suspended high into the air like Squid Game. Some stepping stones are fake; some are real. If they step on the wrong stone, they die and teleport back to the start. Initially, I approached this as a simple teleport. I soon found out that objects blocking the path would interfere with the player moving back, which forced me to disable and reenable the map each time they fell. 
[Teleport Player If Fall Script](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/TeleportPlayer.cs)

⚪ **Feature: In Game "Computer" allowing players to join/create private rooms, change name, change color, etc**
I soon realized that the onl
See this directory, which contains all the relevant scripts: [Computer Scripts](https://github.com/Cricket-Programming/Capuchin-Caverns/tree/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/Computer%20Scripts)


https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/NetworkSkin.cs
Virtual Currency Syncing with my game’s backend.
Capuchin Caverns REVERTED URP/Assets/Scripts/CurrencyManager.cs
One of the most fun features in my game is a train, which is basically a roller coaster. Here’s the waypoint finding code.
https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/TrainController.cs
Prefabs:
