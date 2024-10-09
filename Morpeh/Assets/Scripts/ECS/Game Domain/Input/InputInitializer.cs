using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(InputInitializer))]
public sealed class InputInitializer : Initializer {
    public override void OnAwake()
    {
        var inputEntity = this.World.CreateEntity();
        inputEntity.AddComponent<InputComponent>();

        var systemGroup = this.World.CreateSystemsGroup();

        if (Application.isEditor || (!Application.isMobilePlatform && !Application.isConsolePlatform))
        {
            //Assuming we have a keaboard and a mouse
            systemGroup.AddSystem(ScriptableObject.CreateInstance<InputKeyboardDetectionSystem>());
            systemGroup.AddSystem(ScriptableObject.CreateInstance<InputMouseDirectionSystem>());
            systemGroup.AddSystem(ScriptableObject.CreateInstance<InputMouseFireSystem>());
        }
        
        //TODO: add a mobile input on screen joystick system for Movement
        //TODO: add a mobile input on screen joystick system for Aiming
        
        this.World.AddSystemsGroup(1, systemGroup);
    }

    public override void Dispose() {
    }
}