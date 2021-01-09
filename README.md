# XBox360ControllerManager
Manage XBox360 controller to power off after game play


### Aims with forked project
- Launch the Xbox Game Bar by pressing the GUIDE button
- Control the Xbox Game Bar using an Xbox 360 Controller
- Custom Widget for Xbox Game Bar (https://docs.microsoft.com/en-us/gaming/game-bar/)
- Power off single controller by holding the GUIDE button, or by launching the Xbox Game Bar and selecting the Custom Widget

### Workflow
#### Phase 1
- Create UWP project specially for GameBar purposes
- Add ability to detect if GameBar is open, in UWP Project
- Add interprocess communication between UWP and WPF Projects
- Add Keysender which can send Raw Input to Xbox Game Bar, in WPF Project OR UWP Project

#### Phase 2
- Add System Tray view
- Create Xbox Game Bar Widget (Might require a license, so research into sideloading apps)

### Some more useful references
- [TurnOffXboxController](https://github.com/JulyIghor/TurnOffXboxController)
- [Xoff](https://github.com/manvir-singh/xOff)
- [Xbox360ControllerOff](https://github.com/rlabrecque/Xbox360ControllerOff)
