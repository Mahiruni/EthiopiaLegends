# Android development build gates

1. Unity 6 Editor must match ProjectVersion.txt (or update it using the actual installed editor).
2. Package Manager must resolve the manifest; versions are proposed, not tested.
3. The generated scene must be committed as Assets/Scenes/AxumPrototype.unity with its .meta.
4. Complete the Canvas/EventSystem and input wiring, plus proper gameplay state persistence.
5. Unity license must be configured as GitHub secrets. Do not store credentials in Git.
6. Trigger GitHub Actions manually. Only a successful build with a downloadable APK artifact counts as an APK.
7. Install on Android, verify mission, touch input, pause/resume, and saves.

The prototype is not yet gameplay-complete and has no verified Android artifact.
