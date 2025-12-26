## Capuchin Caverns Codebase

- Capuchin Caverns is a is a live multiplayer VR title on the Meta Horizon Store which I built and shipped.
- This codebase contains the entire version-controlled Unity Project for Capuchin Caverns, including C# scripts, prefabs, scenes, media, art assets, dependencies, project settings.
- The systems in this repository are all deployed to production and have proven their worth in live multiplayer conditions.

## Overview
5,000-20,000 lines of original gameplay/systems code throughout project.
This code has been refactored, optimized and improved so many times.

## Key Directories
- [C# Scripts](https://github.com/Cricket-Programming/Capuchin-Caverns/tree/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts) (most are MonoBehaviors or MonoBehaviorPunCallbacks, some are interfaces)
- [Prefabs](https://github.com/Cricket-Programming/Capuchin-Caverns/tree/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Prefabs)

## Systems Underlying Key Features
⚪ Feature: Physics-based locomotion (roots from open-source project and modified)

[Movement Script](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/NewGorillaLocomotionScripts/Player.cs)

This movement script is what I attach to an XR Origin component, which has the player's camera too; then constraints get adjusted to reduce motion sickness.

⚪ Feature: Real-time multiplayer

[My networking Controller Script (this talks directly to Photon PUN 2 API)](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Resources/PhotonVR/Scripts/PhotonVRManager.cs)

Production multiplayer system built on Photon PUN 2, refactored into a centralized networking layer.
Each client owns a networking controller responsible for player instantiation, state synchronization, and RPC dispatch via the Photon Network.
Examples would be let's say one player wanted to change color, and that new color needed to be broadcasted to all other players: this happens through the networking controller script)

⚪ Feature: Cosmetic Economy with real money payments system + virtual currency saved to game backend + shop with 40 distinct add-on items to purchase.

Each player gets a set amount of in game currency each day they login (I do this because it boosts retention).
[Daily Marbles Reward Script](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/DailyMarblesReward.cs)

This currency syncs to PlayFab (game backend).
[Currency Manager Script](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/CurrencyManager.cs)



[40 Distinct Cosmetic SKUs are saved as prefabs are in this folder](https://github.com/Cricket-Programming/Capuchin-Caverns/tree/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Prefabs/Cosmetics)
Real Money Payment Systems (processed thousands of dollars). It's a lot harder than it looks!
[In App Purchasing Script](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/PlayFabShopManager.cs)


⚪ Feature: Explore 3 levels of terrifying horror

The names of the horror monsters are Fluffy, Gruffy, and Scruffy. They roam different parts of the game and are able to dynamically pathfind their way around obstacles to the nearest player to eat them alive. In order for every player to see the monster in the same place, these monsters had to be networked too. See this script for the implementation: [Networked Enemy Follow Script](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/NetworkedEnemyFollow.cs)

⚪ Feature: Jump on parkour obstacle courses and super bouncy trampolines

"Take a simple idea and take it seriously." I've scattered trampolines across Capuchin Caverns - players can go up into the air. To achieve this, apply a force upward on the player's Rigid Body for a time - in other words an impulse. I later learned the actual physics (momentum) in my AP Physics 1 class and it's helped me build bigger and better trampolines.

[Trampoline Impulse Application Script:](https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/ApplyForce.cs)

⚪ Feature: Play tag inside an ever-changing forest and village

This script represents the pinnacle of my work with event-driven RPCS. It required me to in depth understand searching through the multiplayer client list and manipulating things like data streams. It now powers the "tag mode" in my game.



⚪ Feature: Do an explosive, roller coaster train ride

⚪ Feature: A trick glass bridge with glass stepping stones

⚪ Feature: In Game "Computer" allowing players to join/create private rooms, change name, change color, etc



Sample Scripts (to see code quality)
Skin Saving including Multiplayer Networking of these skins (reusable component for all skins in the cosmetic shop)
My game allows players to put on different skins, where skin changes sync to all other players in the room. It also uses buffering so that new players that join the existing server are “caught up” on recent changes:
https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/NetworkSkin.cs
Virtual Currency Syncing with my game’s backend.
Capuchin Caverns REVERTED URP/Assets/Scripts/CurrencyManager.cs
One of the most fun features in my game is a train, which is basically a roller coaster. Here’s the waypoint finding code.
https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/TrainController.cs
Prefabs:
