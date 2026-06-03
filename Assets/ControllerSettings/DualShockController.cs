// This code is licensed under the terms of the MIT license.
// For details, see https://gist.github.com/andanteyk/d08ab296665b3fc68df58beff3ea39cb .

using System;
using System.IO.Hashing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace OperatorOverload.Bridge.Serialization
{
    [StructLayout(LayoutKind.Explicit)]
    public readonly struct Dualshock4ControllerState : IInputStateTypeInfo
    {
        public readonly FourCC format => new FourCC('D', '4', 'V', 'S');


        [InputControl(name = "leftStick", layout = "Stick", format = "VC2B")]       // VC2B means Vector2Byte. see InputStateBlock.cs
        [InputControl(name = "leftStick/x", offset = 0, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5")]
        [InputControl(name = "leftStick/left", offset = 0, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp,clampMin=0,clampMax=0.5,invert")]
        [InputControl(name = "leftStick/right", offset = 0, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp,clampMin=0.5,clampMax=1")]
        [InputControl(name = "leftStick/y", offset = 1, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,invert")]
        [InputControl(name = "leftStick/up", offset = 1, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp,clampMin=0,clampMax=0.5,invert")]
        [InputControl(name = "leftStick/down", offset = 1, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp,clampMin=0.5,clampMax=1")]
        [FieldOffset(0)]
        public readonly byte leftStickX;

        [FieldOffset(1)]
        public readonly byte leftStickY;

        [InputControl(name = "rightStick", layout = "Stick", format = "VC2B")]
        [InputControl(name = "rightStick/x", offset = 0, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5")]
        [InputControl(name = "rightStick/left", offset = 0, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp,clampMin=0,clampMax=0.5,invert")]
        [InputControl(name = "rightStick/right", offset = 0, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp,clampMin=0.5,clampMax=1")]
        [InputControl(name = "rightStick/y", offset = 1, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,invert")]
        [InputControl(name = "rightStick/up", offset = 1, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp,clampMin=0,clampMax=0.5,invert")]
        [InputControl(name = "rightStick/down", offset = 1, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp,clampMin=0.5,clampMax=1")]
        [FieldOffset(2)]
        public readonly byte rightStickX;

        [FieldOffset(3)]
        public readonly byte rightStickY;

        [InputControl(name = "dpad", format = "BIT", layout = "Dpad", sizeInBits = 4, defaultState = 8)]
        [InputControl(name = "dpad/up", format = "BIT", layout = "DiscreteButton", parameters = "minValue=7,maxValue=1,nullValue=8,wrapAtValue=7", bit = 0, sizeInBits = 4)]
        [InputControl(name = "dpad/right", format = "BIT", layout = "DiscreteButton", parameters = "minValue=1,maxValue=3", bit = 0, sizeInBits = 4)]
        [InputControl(name = "dpad/down", format = "BIT", layout = "DiscreteButton", parameters = "minValue=3,maxValue=5", bit = 0, sizeInBits = 4)]
        [InputControl(name = "dpad/left", format = "BIT", layout = "DiscreteButton", parameters = "minValue=5,maxValue=7", bit = 0, sizeInBits = 4)]
        [InputControl(name = "buttonWest", displayName = "Square", bit = 4)]
        [InputControl(name = "buttonSouth", displayName = "Cross", bit = 5)]
        [InputControl(name = "buttonEast", displayName = "Circle", bit = 6)]
        [InputControl(name = "buttonNorth", displayName = "Triangle", bit = 7)]
        [FieldOffset(4)]
        public readonly byte buttons1;

        [InputControl(name = "leftShoulder", bit = 0)]
        [InputControl(name = "rightShoulder", bit = 1)]
        [InputControl(name = "leftTriggerButton", layout = "Button", bit = 2, synthetic = true)]
        [InputControl(name = "rightTriggerButton", layout = "Button", bit = 3, synthetic = true)]
        [InputControl(name = "select", displayName = "Share", bit = 4)]
        [InputControl(name = "start", displayName = "Options", bit = 5)]
        [InputControl(name = "leftStickPress", bit = 6)]
        [InputControl(name = "rightStickPress", bit = 7)]
        [FieldOffset(5)]
        public readonly byte buttons2;

        /// <summary>
        /// bit 2-7 is counter
        /// </summary>
        [InputControl(name = "systemButton", layout = "Button", displayName = "System", bit = 0)]
        [InputControl(name = "touchpadButton", layout = "Button", displayName = "Touchpad Press", bit = 1)]
        [FieldOffset(6)]
        public readonly byte buttons3;

        [InputControl(name = "leftTrigger", format = "BYTE")]
        [FieldOffset(7)]
        public readonly byte leftTrigger;

        [InputControl(name = "rightTrigger", format = "BYTE")]
        [FieldOffset(8)]
        public readonly byte rightTrigger;


        // ---- this is the end of reportId 0x01 
        // ---- reportId 0x11 contains below.


        [InputControl(name = "temparature", format = "BYTE", layout = "Integer")]
        [FieldOffset(11)]
        public readonly byte temparature;

        [InputControl(name = "gyro", format = "GYRO", layout = "Vector3", noisy = true, processors = "compensateDirection")]
        [InputControl(name = "gyro/x", format = "SHRT", offset = 0, bit = 0, sizeInBits = 16, parameters = "scale,scaleFactor=1310.72")]
        [InputControl(name = "gyro/y", format = "SHRT", offset = 2, bit = 0, sizeInBits = 16, parameters = "scale,scaleFactor=1310.72")]
        [InputControl(name = "gyro/z", format = "SHRT", offset = 4, bit = 0, sizeInBits = 16, parameters = "scale,scaleFactor=1310.72")]
        [FieldOffset(12)]
        public readonly short gyroX;

        [FieldOffset(14)]
        public readonly short gyroY;

        [FieldOffset(16)]
        public readonly short gyroZ;

        // maybe 1G (9.80665 m/s^2) == 16384; scaleFactor = 65536 / 16384 * 9.80665
        [InputControl(name = "accelerometer", format = "ACCL", layout = "Vector3", noisy = true, processors = "compensateDirection")]
        [InputControl(name = "accelerometer/x", format = "SHRT", offset = 0, bit = 0, sizeInBits = 16, parameters = "scale,scaleFactor=39.2266")]
        [InputControl(name = "accelerometer/y", format = "SHRT", offset = 2, bit = 0, sizeInBits = 16, parameters = "scale,scaleFactor=39.2266")]
        [InputControl(name = "accelerometer/z", format = "SHRT", offset = 4, bit = 0, sizeInBits = 16, parameters = "scale,scaleFactor=39.2266")]
        [FieldOffset(18)]
        public readonly short accelerometerX;

        [FieldOffset(20)]
        public readonly short accelerometerY;

        [FieldOffset(22)]
        public readonly short accelerometerZ;

        /// <summary>
        /// bit 0-3 is battery level; 0-11 (11 is fully charged)
        /// bit 4 is "is USB connected"
        /// bit 5 is "is mic connected"
        /// bit 6 is "is phone connected"
        /// </summary>
        [InputControl(name = "batteryLevel", layout = "Integer", format = "BIT", sizeInBits = 4)]
        [FieldOffset(29)]
        public readonly byte batteryLevel;

        [InputControl(name = "touchCount", layout = "Integer")]
        [FieldOffset(32)]
        public readonly byte touchCount;

        [InputControl(name = "packetCounter1", layout = "Integer")]
        [FieldOffset(33)]
        public readonly byte packetCounter1;

        [InputControl(name = "touch1/id", layout = "Integer", format = "BIT", offset = 0, bit = 0, sizeInBits = 7)]
        [InputControl(name = "touch1/flag", layout = "DiscreteButton", format = "BIT", offset = 0, bit = 7, sizeInBits = 1, parameters = "minValue=0,maxValue=0,nullValue=1")]
        [InputControl(name = "touch1", layout = "Vector2")]
        [InputControl(name = "touch1/x", layout = "Axis", format = "BIT", offset = 0, bit = 8, sizeInBits = 12, minValue = 0, maxValue = 1919, parameters = "scale,scaleFactor=4095")]
        [InputControl(name = "touch1/y", layout = "Axis", format = "BIT", offset = 0, bit = 20, sizeInBits = 12, minValue = 0, maxValue = 942, parameters = "scale,scaleFactor=4095")]
        [FieldOffset(34)]
        public readonly uint touch1;

        [InputControl(name = "touch2/id", layout = "Integer", format = "BIT", offset = 0, bit = 0, sizeInBits = 7)]
        [InputControl(name = "touch2/flag", layout = "DiscreteButton", format = "BIT", offset = 0, bit = 7, sizeInBits = 1, parameters = "minValue=0,maxValue=0,nullValue=1")]
        [InputControl(name = "touch2", layout = "Vector2")]
        [InputControl(name = "touch2/x", layout = "Axis", format = "BIT", offset = 0, bit = 8, sizeInBits = 12, minValue = 0, maxValue = 1919, parameters = "scale,scaleFactor=4095")]
        [InputControl(name = "touch2/y", layout = "Axis", format = "BIT", offset = 0, bit = 20, sizeInBits = 12, minValue = 0, maxValue = 942, parameters = "scale,scaleFactor=4095")]
        [FieldOffset(38)]
        public readonly uint touch2;

        [InputControl(name = "packetCounter2", layout = "Integer")]
        [FieldOffset(42)]
        public readonly byte packetCounter2;

        [InputControl(name = "touch3/id", layout = "Integer", format = "BIT", offset = 0, bit = 0, sizeInBits = 7)]
        [InputControl(name = "touch3/flag", layout = "DiscreteButton", format = "BIT", offset = 0, bit = 7, sizeInBits = 1, parameters = "minValue=0,maxValue=0,nullValue=1")]
        [InputControl(name = "touch3", layout = "Vector2")]
        [InputControl(name = "touch3/x", layout = "Axis", format = "BIT", offset = 0, bit = 8, sizeInBits = 12, minValue = 0, maxValue = 1919, parameters = "scale,scaleFactor=4095")]
        [InputControl(name = "touch3/y", layout = "Axis", format = "BIT", offset = 0, bit = 20, sizeInBits = 12, minValue = 0, maxValue = 942, parameters = "scale,scaleFactor=4095")]
        [FieldOffset(43)]
        public readonly uint touch3;

        [InputControl(name = "touch4/id", layout = "Integer", format = "BIT", offset = 0, bit = 0, sizeInBits = 7)]
        [InputControl(name = "touch4/flag", layout = "DiscreteButton", format = "BIT", offset = 0, bit = 7, sizeInBits = 1, parameters = "minValue=0,maxValue=0,nullValue=1")]
        [InputControl(name = "touch4", layout = "Vector2")]
        [InputControl(name = "touch4/x", layout = "Axis", format = "BIT", offset = 0, bit = 8, sizeInBits = 12, minValue = 0, maxValue = 1919, parameters = "scale,scaleFactor=4095")]
        [InputControl(name = "touch4/y", layout = "Axis", format = "BIT", offset = 0, bit = 20, sizeInBits = 12, minValue = 0, maxValue = 942, parameters = "scale,scaleFactor=4095")]
        [FieldOffset(47)]
        public readonly uint touch4;

        [InputControl(name = "packetCounter3", layout = "Integer")]
        [FieldOffset(51)]
        public readonly byte packetCounter3;

        [InputControl(name = "touch5/id", layout = "Integer", format = "BIT", offset = 0, bit = 0, sizeInBits = 7)]
        [InputControl(name = "touch5/flag", layout = "DiscreteButton", format = "BIT", offset = 0, bit = 7, sizeInBits = 1, parameters = "minValue=0,maxValue=0,nullValue=1")]
        [InputControl(name = "touch5", layout = "Vector2")]
        [InputControl(name = "touch5/x", layout = "Axis", format = "BIT", offset = 0, bit = 8, sizeInBits = 12, minValue = 0, maxValue = 1919, parameters = "scale,scaleFactor=4095")]
        [InputControl(name = "touch5/y", layout = "Axis", format = "BIT", offset = 0, bit = 20, sizeInBits = 12, minValue = 0, maxValue = 942, parameters = "scale,scaleFactor=4095")]
        [FieldOffset(52)]
        public readonly uint touch5;

        [InputControl(name = "touch6/id", layout = "Integer", format = "BIT", offset = 0, bit = 0, sizeInBits = 7)]
        [InputControl(name = "touch6/flag", layout = "DiscreteButton", format = "BIT", offset = 0, bit = 7, sizeInBits = 1, parameters = "minValue=0,maxValue=0,nullValue=1")]
        [InputControl(name = "touch6", layout = "Vector2")]
        [InputControl(name = "touch6/x", layout = "Axis", format = "BIT", offset = 0, bit = 8, sizeInBits = 12, minValue = 0, maxValue = 1919, parameters = "scale,scaleFactor=4095")]
        [InputControl(name = "touch6/y", layout = "Axis", format = "BIT", offset = 0, bit = 20, sizeInBits = 12, minValue = 0, maxValue = 942, parameters = "scale,scaleFactor=4095")]
        [FieldOffset(56)]
        public readonly uint touch6;

        [InputControl(name = "packetCounter4", layout = "Integer")]
        [FieldOffset(60)]
        public readonly byte packetCounter4;

        [InputControl(name = "touch7/id", layout = "Integer", format = "BIT", offset = 0, bit = 0, sizeInBits = 7)]
        [InputControl(name = "touch7/flag", layout = "DiscreteButton", format = "BIT", offset = 0, bit = 7, sizeInBits = 1, parameters = "minValue=0,maxValue=0,nullValue=1")]
        [InputControl(name = "touch7", layout = "Vector2")]
        [InputControl(name = "touch7/x", layout = "Axis", format = "BIT", offset = 0, bit = 8, sizeInBits = 12, minValue = 0, maxValue = 1919, parameters = "scale,scaleFactor=4095")]
        [InputControl(name = "touch7/y", layout = "Axis", format = "BIT", offset = 0, bit = 20, sizeInBits = 12, minValue = 0, maxValue = 942, parameters = "scale,scaleFactor=4095")]
        [FieldOffset(61)]
        public readonly uint touch7;

        [InputControl(name = "touch8/id", layout = "Integer", format = "BIT", offset = 0, bit = 0, sizeInBits = 7)]
        [InputControl(name = "touch8/flag", layout = "DiscreteButton", format = "BIT", offset = 0, bit = 7, sizeInBits = 1, parameters = "minValue=0,maxValue=0,nullValue=1")]
        [InputControl(name = "touch8", layout = "Vector2")]
        [InputControl(name = "touch8/x", layout = "Axis", format = "BIT", offset = 0, bit = 8, sizeInBits = 12, minValue = 0, maxValue = 1919, parameters = "scale,scaleFactor=4095")]
        [InputControl(name = "touch8/y", layout = "Axis", format = "BIT", offset = 0, bit = 20, sizeInBits = 12, minValue = 0, maxValue = 942, parameters = "scale,scaleFactor=4095")]
        [FieldOffset(65)]
        public readonly uint touch8;

        [FieldOffset(71)]
        public readonly uint crc32;
    }

    [InputControlLayout(stateType = typeof(Dualshock4ControllerState))]
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    public class Dualshock4ControllerDevice : Gamepad, IInputStateCallbackReceiver, IDualShockHaptics
    {
        // private static readonly ILogger<Dualshock4ControllerDevice> logger = LogManager.GetLogger<Dualshock4ControllerDevice>();


        static Dualshock4ControllerDevice()
        {
            InputSystem.RemoveLayout(nameof(DualShock4GamepadHID));
            InputSystem.RegisterLayout<Dualshock4ControllerDevice>(nameof(Dualshock4ControllerDevice),
                new InputDeviceMatcher()
                    .WithInterface("HID")
                    .WithManufacturer("Sony.+Entertainment")
                    .WithProduct("Wireless Controller"));

            InputSystem.onFindLayoutForDevice += InputSystem_onFindLayoutForDevice;
        }


        private static string InputSystem_onFindLayoutForDevice(ref InputDeviceDescription description, string matchedLayout, InputDeviceExecuteCommandDelegate executeDeviceCommand)
        {
            if (description.interfaceName == "HID" &&
                Regex.Match(description.manufacturer, "Sony.+Entertainment").Success &&
                description.product == "Wireless Controller")
            {
                var layoutName = nameof(Dualshock4ControllerDevice);
                var matcher = InputDeviceMatcher.FromDeviceDescription(description);

                InputSystem.RegisterLayoutBuilder(() =>
                {
                    var builder = new InputControlLayout.Builder()
                        .WithDisplayName("Wireless Controller");

                    return builder.Build();
                }, layoutName, matches: matcher);

                return layoutName;
            }

            return null!;
        }

        /// <summary>
        /// to call static ctor
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
        }


        public ButtonControl leftTriggerButton { get; protected set; } = default!;
        public ButtonControl rightTriggerButton { get; protected set; } = default!;
        public ButtonControl playStationButton { get; protected set; } = default!;


        /// <summary>
        /// unit: maybe deg/s; but details are unknown
        /// </summary>
        public Vector3Control gyro { get; private set; } = default!;

        /// <summary>
        /// unit: m/s^2
        /// </summary>
        public Vector3Control accelerometer { get; private set; } = default!;


        public IntegerControl batteryLevel { get; private set; } = default!;

        public ButtonControl touch1Button { get; private set; } = default!;
        public Vector2Control touch1Position { get; private set; } = default!;
        public ButtonControl touch2Button { get; private set; } = default!;
        public Vector2Control touch2Position { get; private set; } = default!;


        private float? m_LowFrequencyMotorSpeed;
        private float? m_HighFrequencyMotorSpeed;
        private Color? m_LightBarColor;
        private float? m_LightBarBlinkOnMilliseconds;
        private float? m_LightBarBlinkOffMilliseconds;



        protected override void FinishSetup()
        {
            base.FinishSetup();

            leftTriggerButton = GetChildControl<ButtonControl>("leftTriggerButton");
            rightTriggerButton = GetChildControl<ButtonControl>("rightTriggerButton");
            playStationButton = GetChildControl<ButtonControl>("systemButton");

            gyro = GetChildControl<Vector3Control>("gyro");
            accelerometer = GetChildControl<Vector3Control>("accelerometer");

            batteryLevel = GetChildControl<IntegerControl>("batteryLevel");

            touch1Button = GetChildControl<ButtonControl>("touch1/flag");
            touch1Position = GetChildControl<Vector2Control>("touch1");
            touch2Button = GetChildControl<ButtonControl>("touch2/flag");
            touch2Position = GetChildControl<Vector2Control>("touch2");
        }

        public void OnNextUpdate()
        {
        }

        unsafe bool PreProcessEvent(InputEventPtr eventPtr)
        {
            if (eventPtr.type != 1398030676)            // STAT
            {
                return eventPtr.type != 1145852993;     // DLTA
            }

            StateEvent* ptr = (StateEvent*)eventPtr.data;
            if (ptr->stateFormat == 1144280659)     // D4VS
            {
                return true;
            }

            uint stateSizeInBytes = ptr->stateSizeInBytes;
            if (ptr->stateFormat != 1212761120)      // HID
            {
                return false;
            }


            byte* state = (byte*)ptr->state;
            switch (*state)     // reportID
            {
                case 0x01:
                    // USB or bluetooth(default)
                    {
                        if (stateSizeInBytes == 64)
                        {
                            // USB
                            Buffer.MemoryCopy(state + 1, ptr->state, stateSizeInBytes, stateSizeInBytes - 1);
                            ptr->stateFormat = 1144280659;      // D4VS
                            return true;
                        }

                        if (stateSizeInBytes < sizeof(Dualshock4ControllerState) + 2)
                        {
                            return false;
                        }

                        var copy = *(Dualshock4ControllerState*)(state + 2);
                        *(Dualshock4ControllerState*)ptr->state = copy;
                        ptr->stateFormat = 1144280659;      // D4VS
                        return true;
                    }
                case 0x11:
                    // Bluetooth (after rumble/light was changed)
                    {
                        if (stateSizeInBytes < sizeof(Dualshock4ControllerState) + 3)
                        {
                            return false;
                        }

                        var copy = *(Dualshock4ControllerState*)(state + 3);
                        *(Dualshock4ControllerState*)ptr->state = copy;
                        ptr->stateFormat = 1144280659;      // D4VS
                        return true;
                    }
                default:
                    {
                        // logger.ZLogDebug($"unknown state 0x{*state:x2}, length = {stateSizeInBytes} bytes");
                        return false;
                    }
            }
        }

        public unsafe void OnStateEvent(InputEventPtr eventPtr)
        {
            if (!PreProcessEvent(eventPtr))
            {
                return;
            }

            FourCC stateFormat;
            if (eventPtr.type == 1398030676)    // STAT
            {
                stateFormat = ((StateEvent*)eventPtr.data)->stateFormat;
            }
            else if (eventPtr.type == 1145852993)    // DLTA
            {
                stateFormat = ((DeltaStateEvent*)eventPtr.data)->stateFormat;
            }
            else
            {
                throw new InvalidOperationException();
            }


            InputState.Change(this, eventPtr);
        }

        public bool GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset)
        {
            return false;
        }

        protected override void OnAdded()
        {
            base.OnAdded();

            // This is necessary if you want to obtain extended report 0x11 via Bluetooth.
            SetMotorSpeedsAndLightBarColor(0, 0, Color.blue, 0, 0);
        }


        public void SetLightBarColor(Color color)
        {
            m_LightBarColor = color;
            SetMotorSpeedsAndLightBarColor(null, null, color, null, null);
        }

        public void SetLightBarColor(Color color, float blinkOnMilliseconds, float blinkOffMilliseconds)
        {
            m_LightBarColor = color;
            m_LightBarBlinkOnMilliseconds = blinkOnMilliseconds;
            m_LightBarBlinkOffMilliseconds = blinkOffMilliseconds;
            SetMotorSpeedsAndLightBarColor(null, null, color, blinkOnMilliseconds, blinkOffMilliseconds);
        }

        public override void PauseHaptics()
        {
            var lowFrequency = m_LowFrequencyMotorSpeed;
            var highFrequency = m_HighFrequencyMotorSpeed;

            SetMotorSpeedsAndLightBarColor(0, 0, null, null, null);

            m_LowFrequencyMotorSpeed = lowFrequency;
            m_HighFrequencyMotorSpeed = highFrequency;
        }

        public override void ResumeHaptics()
        {
            SetMotorSpeedsAndLightBarColor(m_LowFrequencyMotorSpeed, m_HighFrequencyMotorSpeed, null, null, null);
        }

        public override void ResetHaptics()
        {
            SetMotorSpeedsAndLightBarColor(0, 0, null, null, null);
        }

        public override void SetMotorSpeeds(float lowFrequency, float highFrequency)
        {
            SetMotorSpeedsAndLightBarColor(lowFrequency, highFrequency, null, null, null);
        }

        public bool SetMotorSpeedsAndLightBarColor(
            float? lowFrequencyMotorSpeed, float? highFrequencyMotorSpeed,
            Color? lightBarColor)
        {
            return SetMotorSpeedsAndLightBarColor(lowFrequencyMotorSpeed, highFrequencyMotorSpeed, lightBarColor, m_LightBarBlinkOnMilliseconds, m_LightBarBlinkOffMilliseconds);
        }

        /// <summary>
        /// Set motor speeds and Light Bar's color.
        /// Pass null if you do not want to change the settings.
        /// </summary>
        /// <param name="lowFrequencyMotorSpeed">strong, low frequency motor speed [0, 255]</param>
        /// <param name="highFrequencyMotorSpeed">weak, high frequency motor speed [0, 255]</param>
        /// <param name="lightBarColor">light bar's color (rgb)</param>
        /// <param name="blinkOnMilliseconds">light bar's blink (on) milliseconds [0, 2550]</param>
        /// <param name="blinkOffMilliseconds">light bar's blink (off) milliseconds [0, 2550]</param>
        /// <returns>true if succeeded</returns>
        public bool SetMotorSpeedsAndLightBarColor(
            float? lowFrequencyMotorSpeed, float? highFrequencyMotorSpeed,
            Color? lightBarColor, float? blinkOnMilliseconds, float? blinkOffMilliseconds)
        {
            var payload = new Dualshock4ControllerOutputReportPayload
            {
                flags = 0xf7,
                lowFrequencyMotorSpeed = (byte)(lowFrequencyMotorSpeed ?? m_LowFrequencyMotorSpeed ?? 0),
                highFrequencyMotorSpeed = (byte)(highFrequencyMotorSpeed ?? m_HighFrequencyMotorSpeed ?? 0),
                redColor = (byte)((lightBarColor?.r ?? m_LightBarColor?.r ?? 0) * 255),
                greenColor = (byte)((lightBarColor?.g ?? m_LightBarColor?.g ?? 0) * 255),
                blueColor = (byte)((lightBarColor?.b ?? m_LightBarColor?.b ?? 0) * 255),
                blinkOn = (byte)((blinkOnMilliseconds ?? m_LightBarBlinkOnMilliseconds ?? 0) * 0.1f),
                blinkOff = (byte)((blinkOffMilliseconds ?? m_LightBarBlinkOffMilliseconds ?? 0) * 0.1f),
            };

            var usbReport = Dualshock4ControllerUsbOutputReport.Create(payload);
            long usbResult = ExecuteCommand(ref usbReport);

            var bluetoothReport = Dualshock4ControllerBluetoothOutputReport.Create(payload);
            long bluetoothResult = ExecuteCommand(ref bluetoothReport);


            if (lowFrequencyMotorSpeed != null)
            {
                m_LowFrequencyMotorSpeed = lowFrequencyMotorSpeed;
            }
            if (highFrequencyMotorSpeed != null)
            {
                m_HighFrequencyMotorSpeed = highFrequencyMotorSpeed;
            }
            if (lightBarColor != null)
            {
                m_LightBarColor = lightBarColor;
            }
            if (blinkOnMilliseconds != null)
            {
                m_LightBarBlinkOnMilliseconds = blinkOnMilliseconds;
            }
            if (blinkOffMilliseconds != null)
            {
                m_LightBarBlinkOffMilliseconds = blinkOffMilliseconds;
            }


            return usbResult >= 0 || bluetoothResult >= 0;
        }
    }


    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public struct Dualshock4ControllerOutputReportPayload
    {
        /// <summary>
        /// basic: 0xf0, rumble|=0x01, LED color|=0x02, LED blink|=0x04
        /// </summary>
        [FieldOffset(0)]
        public byte flags;

        [FieldOffset(3)]
        public byte highFrequencyMotorSpeed;

        [FieldOffset(4)]
        public byte lowFrequencyMotorSpeed;

        [FieldOffset(5)]
        public byte redColor;

        [FieldOffset(6)]
        public byte greenColor;

        [FieldOffset(7)]
        public byte blueColor;

        /// <summary>
        /// n x 10 ms on when blink (e.g. 50 when 1 Hz(500 ms on / 500 ms off))
        /// </summary>
        [FieldOffset(8)]
        public byte blinkOn;

        /// <summary>
        /// n x 10 ms on when blink (e.g. 50 when 1 Hz(500 ms on / 500 ms off))
        /// </summary>
        [FieldOffset(9)]
        public byte blinkOff;
    }

    [StructLayout(LayoutKind.Explicit, Size = InputDeviceCommand.BaseCommandSize + 32)]
    public struct Dualshock4ControllerUsbOutputReport : IInputDeviceCommandInfo
    {
        public readonly FourCC typeStatic => new FourCC('H', 'I', 'D', 'O');

        [FieldOffset(0)]
        public InputDeviceCommand baseCommand;

        [FieldOffset(InputDeviceCommand.BaseCommandSize + 0)]
        public byte reportId;

        [FieldOffset(InputDeviceCommand.BaseCommandSize + 1)]
        public Dualshock4ControllerOutputReportPayload payload;

        public static Dualshock4ControllerUsbOutputReport Create(Dualshock4ControllerOutputReportPayload payload)
        {
            return new Dualshock4ControllerUsbOutputReport
            {
                baseCommand = new InputDeviceCommand(new FourCC('H', 'I', 'D', 'O'), InputDeviceCommand.BaseCommandSize + 32),
                reportId = 0x05,
                payload = payload
            };
        }

    }

    [StructLayout(LayoutKind.Explicit, Size = 78)]
    public struct Dualshock4ControllerBluetoothOutputReport : IInputDeviceCommandInfo
    {
        public readonly FourCC typeStatic => new FourCC('H', 'I', 'D', 'O');

        [FieldOffset(0)]
        public InputDeviceCommand baseCommand;

        [FieldOffset(InputDeviceCommand.BaseCommandSize + 0)]
        public byte reportId;

        [FieldOffset(InputDeviceCommand.BaseCommandSize + 1)]
        public byte hardwareControl;        // polling rate?

        [FieldOffset(InputDeviceCommand.BaseCommandSize + 2)]
        public byte audioControl;

        [FieldOffset(InputDeviceCommand.BaseCommandSize + 3)]
        public Dualshock4ControllerOutputReportPayload payload;

        [FieldOffset(InputDeviceCommand.BaseCommandSize + 74)]
        public uint crc32;

        public static Dualshock4ControllerBluetoothOutputReport Create(Dualshock4ControllerOutputReportPayload payload)
        {
            var crc = new Crc32();
            crc.Append(new byte[] { 0xa2 });

            var report = new Dualshock4ControllerBluetoothOutputReport
            {
                baseCommand = new InputDeviceCommand(new FourCC('H', 'I', 'D', 'O'), InputDeviceCommand.BaseCommandSize + 547),
                reportId = 0x11,
                hardwareControl = 0xc0,
                audioControl = 0x20,
                payload = payload
            };

            var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref report, 1));
            crc.Append(bytes[InputDeviceCommand.BaseCommandSize..^4]);

            report.crc32 = crc.GetCurrentHashAsUInt32();

            return report;
        }
    }
}