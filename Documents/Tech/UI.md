# UI 

**UIObjects** are a subclass of **GameObject** that have additional properties:
- They have a **Rect**
- They have AnchorPoints and Offsets that function in relation to their Parent's rect
- They can be "frontmost" (see below)
- They can be "focused" (recieve Navigation input events)

## The UI system 

### The Basics
- UI Objects draw their children first, then themselves
- UI Objects draw children starting from GetChildrenCollectionCount()-1 to 0
    - Such that if you add 2 children, the last added will be drawn first

### Whats going on
- The UI Manager creates a UILayout
- The UILayout creates "empty" children with the CreateLayer(Tag) Function
- The UIManager can add and remove UIObjects from layers with UIManager.instance.Add/RemoveUIObject(UIObject, LayerTag)
    - UIObjects added this way may recieve Back events if IsBackHandler is true
- One UI Layer is "Frontmost"
    - The "highest" TagLayer object that has children
    - e.g. If UILayout has 3 Tag layers added in the order A,B,C, and then has a child added to A and B, the B layer would be frontmost.


