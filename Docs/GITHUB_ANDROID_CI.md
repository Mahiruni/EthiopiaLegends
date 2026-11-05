# Android CI

The workflow is manually triggered and intentionally checks for Unity project metadata. It cannot succeed until the full Unity project is committed.

Configure Unity licensing as GitHub Actions secrets (UNITY_LICENSE, UNITY_EMAIL, UNITY_PASSWORD as applicable). Never commit these values. Review GameCI licensing and build requirements before enabling.

Build output is uploaded only after a successful Unity Android build. Check the run logs and downloaded artifact before claiming an APK exists. Android SDK/NDK and Unity Android Build Support must be available through the selected GameCI image.
