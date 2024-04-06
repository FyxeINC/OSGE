# OSRL

An Open Source Roguelike that I'm developing intermittently in my spare time. Initially I'll be building the game engine, and then branching a game on top of it. Hopefully someone else can make use of this to make neat projects in the future!

## Currently Includes:

- An object heirarchy system and parent/child positioning 
- A robust UI management system that handles layering, focusing, and navigation
    - Example Widgets Include:
        - Solid Fill
        - Border
        - Panel
        - TextArea
        - Vertical Group
- A threaded Input system that supports key rebinding and multiple maps per key.
    - Additionally supports InputMappingContexts for easy enabling/disabling of inputs
- A Tag based architecture and searching systems
- A Localization system for modifying user facing text with minimal code effort
    - Soon to be CSV import / export
- A tickable interface that allows objects to react to time
- A display management system that attempts to render at a set FPS and only redraws when dirty

## Resources Used

- Additional resources used within files will be labeled with comments.
- [Console Renderer](https://github.com/NinovanderMark/ConsoleRenderer), a great library for displaying graphics in console for C#
- [ConsoleHelperLibrary](https://github.com/karenpayneoregon/console-apps/blob/master/ConsoleHelperLibrary/Classes/WindowUtility.cs), for console window utilties such as Maximizing on startup.

## Getting Started

View Examples.cs for a quick reference on how to interface with the main systems.

