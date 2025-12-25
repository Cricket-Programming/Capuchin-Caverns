## Capuchin Caverns Codebase

- Capuchin Caverns is a is a live multiplayer VR title on the Meta Horizon Store which I built and shipped.
- This codebase contains the entire version-controlled Unity Project for Capuchin Caverns, including C# scripts, prefabs, scenes, media, dependencies, project settings. Art assets are functional and gameplay driven. The systems in this repository are all deployed to production and have stood up in live multiplayer conditions.

## Key Directories
### C# Scripts
**Path:**
Capuchin Caverns REVERTED URP/Assets/Scripts
5,000-20,000 lines of original gameplay and systems code throughout project: here are some scripts of core features. This code has been refactored, optimized and improved so many times.
Sample Scripts covering a few of the important functionality:
Skin Saving including Multiplayer Networking of these skins (reusable component for all skins in the cosmetic shop)
My game allows players to put on different skins, where skin changes sync to all other players in the room. It also uses buffering so that new players that join the existing server are “caught up” on recent changes:
https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/NetworkSkin.cs
Virtual Currency Syncing with my game’s backend.
Capuchin Caverns REVERTED URP/Assets/Scripts/CurrencyManager.cs
One of the most fun features in my game is a train, which is basically a roller coaster. Here’s the waypoint finding code.
https://github.com/Cricket-Programming/Capuchin-Caverns/blob/main/Capuchin%20Caverns%20REVERTED%20URP/Assets/Scripts/TrainController.cs
Prefabs:
