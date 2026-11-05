# Technical architecture

Engine: Unity 6 with Universal Render Pipeline (target). Platform: Android landscape, offline-first.

Modules: PlayerMotor, VirtualJoystick, ThirdPersonCamera, CameraTouchArea, QuestTracker, WorldInteractable, PlayerInteractor, PrototypeCombat, PrototypeHUD, LocalSave, and editor-only scene builder.

Android target: scalable graphics, 30 FPS baseline, optional 60 FPS on capable devices. Neither performance target has been verified.

Next steps: complete Unity project metadata, create and serialize scenes and UI prefabs, test controls and quest flow, build APK, then run real-device QA.
