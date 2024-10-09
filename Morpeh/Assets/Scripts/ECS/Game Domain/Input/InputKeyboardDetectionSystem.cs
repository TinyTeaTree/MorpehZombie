using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InputKeyboardDetectionSystem))]
public sealed class InputKeyboardDetectionSystem : UpdateSystem
{
    private Filter inputFilter;
    
    public override void OnAwake()
    {
        this.inputFilter = this.World.Filter.With<InputComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        var inputEntity = inputFilter.FirstOrDefault();
        if (inputEntity == null)
        {
            Debug.LogWarning($"{nameof(InputKeyboardDetectionSystem)} has no entity to work with, consider deactivating the system when no Input is needed or provided");
            return;
        }

        ref var component = ref inputEntity.GetComponent<InputComponent>();

        component.isPressingUp = UnityEngine.Input.GetKey(KeyCode.W);
        component.isPressingDown = UnityEngine.Input.GetKey(KeyCode.S);
        component.isPressingLeft = UnityEngine.Input.GetKey(KeyCode.A);
        component.isPressingRight = UnityEngine.Input.GetKey(KeyCode.D);
    }
}