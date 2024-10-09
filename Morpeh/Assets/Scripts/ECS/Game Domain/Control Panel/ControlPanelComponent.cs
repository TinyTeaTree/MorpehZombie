using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct ControlPanelComponent : IComponent
{
    public bool isAssigned;
    public UnityEngine.UI.Button add10ZombiesPerSecondButton;
    public UnityEngine.UI.Button add100ZombiesPerSecondButton;
    public UnityEngine.UI.Button toggleVisibilityButton;
}