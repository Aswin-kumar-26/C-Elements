# AR Chemistry Educational App (Unity + AR Foundation)

An **Android-based Augmented Reality** application designed to visualize chemical elements and simulate reactions using 3D models and interactive UI. Built with **Unity 6 (6000.0.37f1)**, using **AR Foundation** and **Google XR Plugin** for AR support.

---

## Project Structure

### Scenes
- `ElementViewerScene`: Displays atomic structure for elements (1-10).
- `ReactionScene`: Interactive chemical lab with test tubes and beaker simulation.

---

## Core Components & Architecture

### 1. AR Integration
- **Framework**: AR Foundation 6.0 + Google XR Plugin.
- **Device Compatibility**: Android devices supporting ARCore.
- **AR Session Origin**: Controls camera tracking and object placement in world space.
- **AR Raycasting**: Not used for surface detection — all objects are placed at fixed positions relative to the camera (for speed & consistency).

---

### 2. Prefab System

| Prefab        | Purpose                            | Children / Notes                                      |
|---------------|------------------------------------|--------------------------------------------------------|
| `Beaker`      | Reaction container                 | `LiquidHolder/Cylinder` (used for showing final mix)  |
| `TestTube`    | Chemical holder                    | `TubeLiquid` (shows individual chemical materials)     |
| `ElementModel`| Atomic structure models (GLB/FBX)  | Includes rotation animation and electron particles     |
| `NotificationPanel` | UI for showing reaction info | Uses **TextMeshProUGUI**                              |

---

## Key Scripts

### `ModelPlace.cs`
- Places prefabs at a fixed distance in front of the camera.
- Handles spawning beakers, test tubes, and atomic models.
- Supports reset and destroy functionality.

### `ChemicalSpawner.cs`
- Spawns two test tubes with selected chemicals via dropdown or button.
- Assigns material based on chemical index.
- Tracks which chemicals were selected (index 0-5).
- Maps materials from a centralized array.
- Handles clearing and respawning test tubes and beaker.

### `ReactionManager.cs`
- Waits for both chemicals to be selected.
- Computes a combined reaction index (e.g. chemical1 + chemical2).
- If valid, updates the beaker's `Cylinder` material.
- Displays the corresponding reaction name on the UI.
- Supports animations for test tubes pouring if enabled.

---

## Materials & Chemistry Data

- **Material Array Indexing**
  - Index 0-2: Chemical A (e.g., H2, O2, Na)
  - Index 3-5: Chemical B (e.g., Cl2, H2O, CO2)
  - Index 6-8: Resultant Reactions (e.g., HCl, NaCl, NaOH)

- Materials are applied using:
```csharp
Renderer rend = liquidObject.GetComponent<Renderer>();
rend.material = new Material(chemicals[selectedIndex]);
```

- Reaction is only triggered if a valid pair (1-to-1) is selected.

---

## UI Components

- **Canvas** in World Space or Screen Space Overlay.
- UI Buttons:
  - Spawn Beaker
  - Spawn Test Tube 1 (Chemical A)
  - Spawn Test Tube 2 (Chemical B)
  - React (triggers material mix and reaction display)
- **Dropdowns / Buttons**: For chemical selection.
- **Notification Panel**: Displays reaction result name.

---

## Animation & Visual Feedback

- Electrons in atomic models spin using Animator component.
- Test tubes can animate rotation when pouring using either:
  - Animator clip
  - Coroutine-based rotation

- Reaction mixing is shown by:
  - Updating the **Cylinder** material inside the beaker.
  - Activating the `LiquidHolder` GameObject if it was disabled.

---

## Fixed Placement System

- All objects (beaker, tubes, models) are placed using fixed offsets from `ARCamera`.
- Ensures:
  - Predictable user experience.
  - No dependency on surface detection.

Example:
```csharp
Vector3 spawnPosition = arCamera.transform.position + arCamera.transform.forward * 0.5f;
```

---

## Known Limitations

- Reactions only occur for predefined chemical combinations.
- Materials must be properly assigned in the Unity inspector.
- Interaction is simplified for demo/testing purposes (no real-time physics or particle effects).
- Does not support object dragging via AR plane detection yet.

---

## To-Do / Future Improvements

- Add animation to show actual pouring effect.
- Expand database to include 20+ elements and 20+ reactions.
- Integrate AR plane detection and tap-to-place system.
- Add AR session reset / relocalization handling.
- Include audio cues for reactions.

---

## Build Instructions

1. Open project in **Unity 6.0.0f1** or later.
2. Install AR Foundation & Google XR packages via Package Manager.
3. Set build target to Android.
4. Enable `ARCore` and set appropriate permissions.
5. Connect Android device via USB.
6. Build & Run.
