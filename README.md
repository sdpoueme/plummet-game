# Unity 2D Game Project with AI Navigation and Multiplayer Support

This Unity project is a 2D game that incorporates AI navigation, multiplayer functionality, and various 2D animation and sprite manipulation tools. It provides a foundation for creating interactive 2D games with advanced features.

The project utilizes Unity's 2D animation system, pixel-perfect rendering, and sprite shape tools to create visually appealing and smooth 2D graphics. It also includes AI navigation capabilities and multiplayer center integration, allowing for the development of games with intelligent NPCs and multiplayer experiences.

Key features of this project include:
- 2D animation and sprite manipulation
- Pixel-perfect rendering for crisp visuals
- AI navigation for intelligent game entities
- Multiplayer support through Unity's Multiplayer Center
- Comprehensive 2D physics and tilemap systems
- Integration with popular IDEs (Rider and Visual Studio)
- Built-in testing framework for quality assurance

## Repository Structure

```
.
├── Assets
│   └── Scripts
│       ├── CameraPosition.cs
│       ├── Djikstra.cs
│       ├── Door.cs
│       ├── DoorManager.cs
│       ├── Player.cs
│       └── PlayerData.cs
└── Packages
    ├── manifest.json
    └── packages-lock.json
```

### Key Files:
- `Assets/Scripts/`: Contains the main game logic scripts
  - `CameraPosition.cs`: Manages camera positioning
  - `Djikstra.cs`: Implements Dijkstra's algorithm, likely for pathfinding
  - `Door.cs` and `DoorManager.cs`: Handle door mechanics in the game
  - `Player.cs` and `PlayerData.cs`: Manage player-related functionality and data
- `Packages/manifest.json`: Defines the project's package dependencies
- `Packages/packages-lock.json`: Locks the versions of installed packages

## Usage Instructions

### Installation

1. Ensure you have Unity 2022.3 or later installed.
2. Clone this repository to your local machine.
3. Open the project in Unity Hub.

### Getting Started

1. Open the project in Unity.
2. Explore the `Assets/Scripts` folder to understand the game logic.
3. Use the Unity Editor to set up your scenes and game objects.

### Configuration Options

- Adjust 2D rendering settings in the Project Settings under the 2D section.
- Configure AI Navigation settings in the Navigation window (Window > AI > Navigation).

### Common Use Cases

1. Creating a new level:
   - Use the Tilemap system to design your 2D level.
   - Add colliders and configure the navigation mesh for AI.

2. Implementing player movement:
   ```csharp
   // In Player.cs
   void Update()
   {
       float moveHorizontal = Input.GetAxis("Horizontal");
       float moveVertical = Input.GetAxis("Vertical");
       Vector2 movement = new Vector2(moveHorizontal, moveVertical);
       GetComponent<Rigidbody2D>().velocity = movement * speed;
   }
   ```

3. Setting up a door:
   ```csharp
   // In Door.cs
   public void Open()
   {
       // Animation or state change logic
       isOpen = true;
   }

   public void Close()
   {
       // Animation or state change logic
       isOpen = false;
   }
   ```

### Testing & Quality

- Use the built-in Test Framework to create and run unit tests for your game logic.
- Access the Test Runner via Window > General > Test Runner in Unity.

### Troubleshooting

1. Issue: AI agents not moving
   - Check if the NavMesh has been baked correctly.
   - Ensure the AI agent has a NavMeshAgent component attached.
   - Verify that the destination is set and is on the NavMesh.

2. Issue: 2D sprites appear blurry
   - Enable Pixel Perfect Camera in your main camera.
   - Check sprite import settings and ensure "Filter Mode" is set to "Point" for pixel art.

3. Issue: Multiplayer not working
   - Verify that all necessary multiplayer packages are installed.
   - Check network settings and ensure ports are open if testing across different networks.

### Debugging

To enable debug mode:
1. Go to Edit > Project Settings > Player.
2. In the Configuration section, add "ENABLE_LOGS" to the Scripting Define Symbols field.
3. Use `Debug.Log()` statements in your code, which will now appear in the Console window.

Performance profiling:
1. Use Unity Profiler (Window > Analysis > Profiler) to monitor performance.
2. Pay attention to the CPU Usage and GPU Usage panels for potential bottlenecks.

## Data Flow

The game's data flow primarily revolves around player input, game state management, and AI decision-making. Here's a high-level overview:

1. Player Input → Player.cs → Game State
2. Game State → DoorManager.cs → Door.cs
3. AI Input → Djikstra.cs → AI Movement
4. Game State → CameraPosition.cs → Camera Movement

```
[Player Input] → [Player.cs] → [Game State] → [DoorManager.cs] → [Door.cs]
                                     ↓
                             [CameraPosition.cs]
                                     ↓
                             [Camera Movement]
                                     
[AI Input] → [Djikstra.cs] → [AI Movement]
```

Notes:
- Ensure proper synchronization between player actions and game state updates.
- AI pathfinding using Dijkstra's algorithm may require optimization for larger levels.
- Camera positioning should smoothly follow player movement while considering level boundaries.