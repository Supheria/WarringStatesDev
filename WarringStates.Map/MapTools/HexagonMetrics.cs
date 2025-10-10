using Godot;

namespace WarringStates.Map;

public static class HexagonMetrics
{
    public const float OuterRadius = 10f;
    public const float InnerRadius = OuterRadius * 0.866025404f; // √3 / 2

    public static readonly Vector3[] Corners =
    [
        new(InnerRadius, 0f, 0.5f * OuterRadius),
        new(0f, 0f, OuterRadius),
        new(-InnerRadius, 0f, 0.5f * OuterRadius),
        new(-InnerRadius, 0f, -0.5f * OuterRadius),
        new(0f, 0f, -OuterRadius),
        new(InnerRadius, 0f, -0.5f * OuterRadius),
        new(InnerRadius, 0f, 0.5f * OuterRadius),
    ];
}
