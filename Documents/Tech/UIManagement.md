# UI MANAGEMENT

**UIObjects** are a subclass of **GameObject** that have additional properties:
- They have a **Rect**
- They have AnchorPoints and Offsets that function in relation to their Parent's rect
- They can be "frontmost" (see below)
- They can be "focused" (recieve Navigation input events)

There are currently 3 archetypes of **UIObjects**
- Main objects, which can be "frontmost" (see below)
- Piece objects, which can be described as "parts of a widget" (UI_Border)
- Combination objects, which instantiate their own pieces. (e.g. UI_Panel)

The **UIManager** creates a fullscreen **UIObject** called Layout which it handles internally as a unique **UIObject**.
This Layout's children are the only **UIObjects** that can be frontmost. Frontmost is a way to track which **UIObject** and its children are "in focus". 
