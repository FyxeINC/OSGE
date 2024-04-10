# OSGE

An Open Source Game Engine that I'm developing intermittently in my spare time. Hopefully someone else can make use of this to make neat projects in the future! I plan on building a roguelike after the main engine tasks are complete.

## Currently Includes:

- An object heirarchy system and parent/child positioning 
- A robust UI management system that handles layering, focusing, events, and navigation
    - Example Widgets Include:
        - Solid Fill
        - Border
        - Bars
        - TextArea
        - Vertical Group
    - Navigation between widgets is automatically set based on their position
- A threaded Input system that supports key rebinding and multiple maps per key.
    - Additionally supports InputMappingContexts for easy enabling/disabling of inputs
- A Tag structure for conveying metadata quickly and cleanly
- A Localization system for modifying user facing text with minimal code effort
    - Soon to be CSV import / export
- A tickable interface that allows objects to react to time
- A threaded display management system that attempts to render at a set FPS and only redraws when dirty
- A player profile system that lets you add, remove, and swap the current profile
- A save system to serialize data cleanly
    - Can save data unique to Player Profiles

## Resources Used

- Additional resources used within files will be labeled with comments.
- [Console Renderer](https://github.com/NinovanderMark/ConsoleRenderer), a great library for displaying graphics in console for C#
- [ConsoleHelperLibrary](https://github.com/karenpayneoregon/console-apps/blob/master/ConsoleHelperLibrary/Classes/WindowUtility.cs), for console window utilties such as Maximizing on startup.

## Getting Started

This section will be heavily expaned upon when the project is in a more whole state. For now, you can view Examples.cs for a quick reference on how to interface with the main systems.

## Additional thoughts

The engine is heavily inspired by my numerous years of experience working with Unity and Unreal on a daily basis. The goal is to create something lightweight to use, but also rebust enough that it can handle some complex situations.

