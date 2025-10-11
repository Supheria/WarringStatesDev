using System;
using Godot;

public partial class HexagonMapContext : Node3D
{
    public override void _Input(InputEvent @event)
    {
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        GC.WaitForPendingFinalizers();

        if (@event is not InputEventKey key || key.IsPressed())
            return;
        if (key.Keycode == Key.P)
        {
            var viewPort = GetViewport();
            viewPort.DebugDraw =
                viewPort.DebugDraw == Viewport.DebugDrawEnum.Disabled
                    ? Viewport.DebugDrawEnum.Wireframe
                    : Viewport.DebugDrawEnum.Disabled;
        }
    }
}
