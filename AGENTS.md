# Repository Guidelines

## Project Structure & Module Organization

This is a Unity 2022.3.62f3c1 2D URP project. Keep game content under `Assets/` and always commit each asset together with its `.meta` file.

- `Assets/Scenes/`: playable scenes; `SampleScene.unity` is currently the startup scene.
- `Assets/Settings/`: URP 2D renderer and pipeline assets.
- `Assets/Plugins/NuGet/`: vendor dependencies used by Unity MCP; do not edit these DLLs manually.
- `Packages/`: Unity package declarations and lock data.
- `ProjectSettings/`: shared editor and project configuration.
- `Docs/AI/`: persistent technical context for contributors and agents.

Place future runtime code in `Assets/Scripts/<Feature>/` and tests in `Assets/Tests/EditMode/` or `Assets/Tests/PlayMode/`. Avoid committing generated folders such as `Library/`, `Temp/`, `Logs/`, and `UserSettings/`.

## Build, Test, and Development Commands

Open the repository root through Unity Hub with Unity `2022.3.62f3c1`. No custom build script exists yet; create builds through **File > Build Settings** and record platform-specific setup when automation is added.

Headless test example on Windows:

```powershell
& '<Unity.exe>' -batchmode -projectPath . -runTests -testPlatform EditMode -testResults TestResults.xml -quit
```

Use `git status --short` before and after Unity sessions to catch unintended serialized changes.

## Coding Style & Naming Conventions

Use four-space indentation and one C# type per file. Name types and public members with `PascalCase`, local variables and parameters with `camelCase`, and private serialized fields as `_camelCase`, for example `[SerializeField] private float _moveSpeed;`. Prefer namespaces organized by feature. Keep `MonoBehaviour` classes focused on Unity lifecycle and presentation; move reusable rules into plain C# classes. Do not hand-edit generated solution or project files.

## Testing Guidelines

Unity Test Framework 1.1.33 is installed, but the project has no tests or coverage threshold yet. Name test files `<Subject>Tests.cs` and test methods `Method_Condition_ExpectedResult`. Use EditMode tests for pure logic and PlayMode tests for scene, physics, or lifecycle behavior. New reusable gameplay systems should include focused tests where practical.

## Commit & Pull Request Guidelines

Follow the existing Conventional Commit style, such as `chore: initialize Unity project repository`. Keep each commit to one completed, verifiable learning step. Use `feat:`, `fix:`, `test:`, `docs:`, or `chore:` as appropriate.

Pull requests should explain the behavior change, list validation performed, and identify affected scenes or settings. Link related issues and include screenshots or short recordings for visible Unity changes. Never include local MCP endpoints, credentials, generated caches, or unrelated scene serialization.
