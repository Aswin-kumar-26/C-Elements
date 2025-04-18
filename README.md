# AR Chemistry Educational App (Unity + AR Foundation)

An **Android-based Augmented Reality** application designed to visualize chemical elements and simulate reactions using 3D models and interactive UI. Built with **Unity 6 (6000.0.37f1)**, using **AR Foundation** and **Google XR Plugin** for AR support.

---

## Project Structure

### Scenes
- `ElementViewerScene`: Displays atomic structure for elements (1–10).
- `ReactionScene`: Interactive chemical lab with test tubes and beaker simulation.

---

## Core Components & Architecture

### 1. AR Integration
- **Framework**: AR Foundation 6.0 + Google XR Plugin.
- **Device Compatibility**: Android devices supporting ARCore.
- **AR Session Origin**: Controls camera tracking and object placement in world space.
- **AR Raycasting**: Not used — all objects are placed at fixed positions relative to the camera (for consistency and speed).

---

### 2. Prefab System

| Prefab           | Purpose                            | Children / Notes                                      |
|------------------|------------------------------------|--------------------------------------------------------|
| `Beaker`         | Reaction container                 | `LiquidHolder/Cylinder` (used for showing final mix)  |
| `TestTube`       | Chemical holder                    | `TubeLiquid` (shows individual chemical materials)     |
| `ElementModel`   | Atomic structure models (GLB/FBX)  | Includes rotation animation and electron particles     |
| `NotificationPanel` | UI for showing reaction info    | Uses **TextMeshProUGUI**                              |

---

## Key Scripts

### `ModelPlace.cs`
- Places prefabs at a fixed distance in front of the AR camera.
- Handles spawning beakers, test tubes, and atomic models.
- Includes reset and destroy functionality.

### `ChemicalSpawner.cs`
- Spawns two test tubes with selected chemicals via dropdown/button.
- Assigns material to each based on selected chemical index.
- Maps materials from an array and resets the scene for re-selection.

### `ReactionManager.cs`
- Waits until both chemicals are selected.
- Computes a reaction index and validates combination.
- Updates beaker material (`Cylinder`) with reaction material.
- Displays reaction name via TextMeshPro.
- Triggers test tube pouring animations if needed.

---

## Materials & Chemistry Data

- **Material Index Mapping**:
  - `0–2`: Chemical A (e.g., H₂, O₂, Na)
  - `3–5`: Chemical B (e.g., Cl₂, H₂O, CO₂)
  - `6–8`: Reaction results (e.g., HCl, NaCl, NaOH)

- Code Example:
```csharp
Renderer rend = liquidObject.GetComponent<Renderer>();
rend.material = new Material(chemicals[selectedIndex]);
```

- Reaction logic only triggers for predefined valid pairs.

---

## UI Components

- **Canvas** in World Space or Overlay mode.
- UI Buttons:
  - Spawn Beaker
  - Spawn Chemical A (Tube 1)
  - Spawn Chemical B (Tube 2)
  - React Button
- **Dropdowns / Buttons**: Select chemicals.
- **Notification Panel**: Shows the result of valid reactions.

---

## Animation & Visual Feedback

- Electrons rotate using Animator.
- Test tubes can pour using either:
  - Animator Clip
  - Coroutine-based rotation
- Reaction feedback:
  - Changes beaker’s `Cylinder` material.
  - Activates hidden `LiquidHolder` if needed.

---

## Fixed Placement System

All objects (beaker, test tubes, atomic models) are placed at fixed offsets from the `ARCamera`.

```csharp
Vector3 spawnPosition = arCamera.transform.position + arCamera.transform.forward * 0.5f;
```

- **Advantages**:
  - Faster interaction.
  - No surface detection required.
  - Ideal for rapid prototyping and demo environments.

---

## Known Limitations

- Limited to predefined 1-to-1 chemical combinations.
- Dragging / plane interaction not implemented.
- Reaction effects are simple material swaps.
- Only 10 elements and 3 reactions are currently implemented.

---

## To-Do / Future Enhancements

- Show actual pouring animation between test tube and beaker.
- Add more elements (20+) and reactions (20+ combinations).
- Enable AR plane detection and tap-to-place logic.
- Add sound feedback and reaction visual effects.
- Implement reset AR session support.

---

## Output Screenshots

| Action                          | Screenshot                                                 |
|----------------------------------|-------------------------------------------------------------|
| Placing Beaker                  | ![Beaker](https://your-image-link-1.png)                    |
| Spawning Test Tube 1            | ![Test Tube 1](https://your-image-link-2.png)               |
| Spawning Test Tube 2            | ![Test Tube 2](https://your-image-link-3.png)               |
| Reaction Triggered              | ![Reaction](https://your-image-link-4.png)                  |
| Element Structure (e.g. Carbon) | ![Element](https://your-image-link-5.png)                   |

> **Replace the links** above with actual screenshot URLs from your repo or GitHub uploads.

---

## Build Instructions

1. Open project in **Unity 6.0.0f1** or later.
2. Install `AR Foundation` and `Google XR Plugin` via Package Manager.
3. Switch build target to Android.
4. Enable `ARCore` support and set required permissions.
5. Connect your Android device via USB.
6. Click **Build & Run**.

---

## Contributors

<a href="https://github.com/Aswin-kumar-26"><img src="https://github.com/Aswin-kumar-26.png" width="80"/></a>
<a href="https://github.com/hari-prasanth-20"><img src="https://github.com/hari-prasanth-20.png" width="80"/></a>

| Name             | GitHub Profile                                        |
|------------------|--------------------------------------------------------|
| Aswin Kumar      | [@Aswin-kumar-26](https://github.com/Aswin-kumar-26)  |
| Hari Prasanth    | [@hari-prasanth-20](https://github.com/hari-prasanth-20) |

---
