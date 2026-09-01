# Unity Project Context

Last inspected: 2026-09-01 (initial repository baseline)

## Project summary

- **Confirmed:** Unity project root is `E:/tutorial`.
- **Confirmed:** The project is a new 2D learning project. It currently contains one sample scene and no first-party C# gameplay code.
- **Intended direction:** Build a small Metroidvania incrementally while learning Unity fundamentals.

## Confirmed environment

- Unity: 2022.3.62f3c1, revision `1623fc0bbb97`
- Render pipeline: Universal Render Pipeline 14.0.12 with the 2D Renderer
- 2D feature set: 2.0.1, including Tilemap, SpriteShape, 2D Animation and Pixel Perfect
- Input: legacy Input Manager (`activeInputHandler: 0`); the Input System package is not installed
- Product settings: `DefaultCompany / tutorial`

## Important packages and tooling

- Unity Test Framework 1.1.33 is installed; no project tests exist yet.
- TextMeshPro, Timeline, uGUI and Visual Scripting are installed.
- Unity MCP (`com.ivanmurzak.unity.mcp` 0.90.0) is installed and local Codex tooling is present. Connection availability must be verified per session.
- Rider and Visual Studio editor integrations are installed.

## Project structure and architecture

- `Assets/Scenes/SampleScene.unity` is the only enabled build scene and current startup scene.
- `Assets/Settings/` contains the URP 2D renderer and pipeline assets.
- `Assets/Plugins/NuGet/` contains Unity MCP runtime dependencies; treat it as tooling/vendor code.
- No assembly definitions, prefabs, input action assets, tests or gameplay scripts currently exist.
- Architecture and coding conventions are therefore not established yet.

## Working conventions

- Make one focused Git commit after each completed, verifiable learning step.
- Explain code-only changes, then continue without requiring manual Unity interaction.
- Stop and give exact instructions when a step genuinely requires Editor interaction or visual judgment.
- Keep generated Unity directories and local MCP/Codex configuration out of Git.
- Preserve Unity `.meta` files together with their corresponding assets.

## Important unknowns

- Target platform, art resolution and pixels-per-unit are not decided.
- Player controller requirements and input approach are not decided.
- The sample scene has not yet been used as a gameplay scene.

## Evidence inspected

- `ProjectSettings/ProjectVersion.txt`
- `ProjectSettings/ProjectSettings.asset`
- `ProjectSettings/GraphicsSettings.asset`
- `ProjectSettings/EditorBuildSettings.asset`
- `Packages/manifest.json`
- `Packages/packages-lock.json`
- First-party paths under `Assets/`
