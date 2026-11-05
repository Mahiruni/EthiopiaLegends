# Phase 2 setup and verification

1. Create a Unity 6 URP project and copy Assets scripts into it.
2. In Unity Editor run **Ethiopia Legends > Create Prototype Scene**.
3. Open Assets/Scenes/AxumPrototype.unity and assign the scene to Android Build Settings.
4. Wire the camera transform reference on PlayerMotor and add a Canvas, EventSystem, joystick, interaction, combat, and save buttons. Connect UnityEvents to the public methods.
5. Test quest progression, controls, enemy behavior, and save/load in the Editor.
6. Commit Unity-generated ProjectSettings, Packages, scenes, and meta files before running Android CI.

**Important:** This scene generator produces placeholders, not a polished or verified playable game. The uploaded simplified generator is not identical to the original Phase 2 archive. A complete Unity project and device testing remain required.
