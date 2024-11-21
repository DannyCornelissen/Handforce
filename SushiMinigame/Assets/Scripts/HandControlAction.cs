//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/HandControlAction.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @HandControlAction: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @HandControlAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""HandControlAction"",
    ""maps"": [
        {
            ""name"": ""HandControl"",
            ""id"": ""4ce5c071-31fe-4b09-95c7-f1bb27463c25"",
            ""actions"": [
                {
                    ""name"": ""ControlIndex"",
                    ""type"": ""Value"",
                    ""id"": ""25ceb3c2-55aa-487c-8396-ec56bddbe43d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ControlMiddle"",
                    ""type"": ""Value"",
                    ""id"": ""4c85d92d-1388-44ef-aedd-8b1c70164269"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PalmMovement"",
                    ""type"": ""Value"",
                    ""id"": ""e38921f7-aabe-4269-8bb3-9c4c875f8165"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PalmSideMovement"",
                    ""type"": ""Value"",
                    ""id"": ""9bfed394-6b2f-4f06-af66-e7c7e2a2f15a"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""38c5816b-7441-449c-b88d-32b7109e3130"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ControlIndex"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d78204e8-ffb2-4809-b185-574a3185fabe"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ControlMiddle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0167e069-ad70-4f56-9949-9a742f7144aa"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PalmMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0953cb08-baf0-4fab-99b2-9a14c6a81333"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PalmMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""705f61e3-f017-4c29-94b4-77e8a040e008"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PalmSideMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""054a3226-e09c-44b7-bbc1-b27a66382f2e"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PalmSideMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // HandControl
        m_HandControl = asset.FindActionMap("HandControl", throwIfNotFound: true);
        m_HandControl_ControlIndex = m_HandControl.FindAction("ControlIndex", throwIfNotFound: true);
        m_HandControl_ControlMiddle = m_HandControl.FindAction("ControlMiddle", throwIfNotFound: true);
        m_HandControl_PalmMovement = m_HandControl.FindAction("PalmMovement", throwIfNotFound: true);
        m_HandControl_PalmSideMovement = m_HandControl.FindAction("PalmSideMovement", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // HandControl
    private readonly InputActionMap m_HandControl;
    private List<IHandControlActions> m_HandControlActionsCallbackInterfaces = new List<IHandControlActions>();
    private readonly InputAction m_HandControl_ControlIndex;
    private readonly InputAction m_HandControl_ControlMiddle;
    private readonly InputAction m_HandControl_PalmMovement;
    private readonly InputAction m_HandControl_PalmSideMovement;
    public struct HandControlActions
    {
        private @HandControlAction m_Wrapper;
        public HandControlActions(@HandControlAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @ControlIndex => m_Wrapper.m_HandControl_ControlIndex;
        public InputAction @ControlMiddle => m_Wrapper.m_HandControl_ControlMiddle;
        public InputAction @PalmMovement => m_Wrapper.m_HandControl_PalmMovement;
        public InputAction @PalmSideMovement => m_Wrapper.m_HandControl_PalmSideMovement;
        public InputActionMap Get() { return m_Wrapper.m_HandControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HandControlActions set) { return set.Get(); }
        public void AddCallbacks(IHandControlActions instance)
        {
            if (instance == null || m_Wrapper.m_HandControlActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_HandControlActionsCallbackInterfaces.Add(instance);
            @ControlIndex.started += instance.OnControlIndex;
            @ControlIndex.performed += instance.OnControlIndex;
            @ControlIndex.canceled += instance.OnControlIndex;
            @ControlMiddle.started += instance.OnControlMiddle;
            @ControlMiddle.performed += instance.OnControlMiddle;
            @ControlMiddle.canceled += instance.OnControlMiddle;
            @PalmMovement.started += instance.OnPalmMovement;
            @PalmMovement.performed += instance.OnPalmMovement;
            @PalmMovement.canceled += instance.OnPalmMovement;
            @PalmSideMovement.started += instance.OnPalmSideMovement;
            @PalmSideMovement.performed += instance.OnPalmSideMovement;
            @PalmSideMovement.canceled += instance.OnPalmSideMovement;
        }

        private void UnregisterCallbacks(IHandControlActions instance)
        {
            @ControlIndex.started -= instance.OnControlIndex;
            @ControlIndex.performed -= instance.OnControlIndex;
            @ControlIndex.canceled -= instance.OnControlIndex;
            @ControlMiddle.started -= instance.OnControlMiddle;
            @ControlMiddle.performed -= instance.OnControlMiddle;
            @ControlMiddle.canceled -= instance.OnControlMiddle;
            @PalmMovement.started -= instance.OnPalmMovement;
            @PalmMovement.performed -= instance.OnPalmMovement;
            @PalmMovement.canceled -= instance.OnPalmMovement;
            @PalmSideMovement.started -= instance.OnPalmSideMovement;
            @PalmSideMovement.performed -= instance.OnPalmSideMovement;
            @PalmSideMovement.canceled -= instance.OnPalmSideMovement;
        }

        public void RemoveCallbacks(IHandControlActions instance)
        {
            if (m_Wrapper.m_HandControlActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IHandControlActions instance)
        {
            foreach (var item in m_Wrapper.m_HandControlActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_HandControlActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public HandControlActions @HandControl => new HandControlActions(this);
    public interface IHandControlActions
    {
        void OnControlIndex(InputAction.CallbackContext context);
        void OnControlMiddle(InputAction.CallbackContext context);
        void OnPalmMovement(InputAction.CallbackContext context);
        void OnPalmSideMovement(InputAction.CallbackContext context);
    }
}
