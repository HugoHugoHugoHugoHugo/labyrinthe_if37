using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace OperatorOverload.Bridge.Serialization
{
    [StructLayout(LayoutKind.Explicit, Size = InputDeviceCommand.BaseCommandSize + 32)]
    public struct DualShock4UsbOutputCommand : IInputDeviceCommandInfo
    {
        public static FourCC Type => new FourCC('H', 'I', 'D', 'O');
        public FourCC typeStatic => Type;

        [FieldOffset(0)]
        public InputDeviceCommand baseCommand;

        [FieldOffset(InputDeviceCommand.BaseCommandSize + 0)]
        public byte reportId; // Doit être 0x05 pour le mode filaire USB

        [FieldOffset(InputDeviceCommand.BaseCommandSize + 1)]
        public byte flags;    // 0x01 = Activer la vibration, 0x02 = Activer la LED, 0x03 = Activer les deux

        [FieldOffset(InputDeviceCommand.BaseCommandSize + 2)]
        public byte reserved1;

        [FieldOffset(InputDeviceCommand.BaseCommandSize + 3)]
        public byte reserved2;

        [FieldOffset(InputDeviceCommand.BaseCommandSize + 4)]
        public byte rumbleRight; // Moteur léger (Haute fréquence) -> 0 à 255

        [FieldOffset(InputDeviceCommand.BaseCommandSize + 5)]
        public byte rumbleLeft;  // Moteur lourd (Basse fréquence) -> 0 à 255

        [FieldOffset(InputDeviceCommand.BaseCommandSize + 6)]
        public byte ledR;

        [FieldOffset(InputDeviceCommand.BaseCommandSize + 7)]
        public byte ledG;

        [FieldOffset(InputDeviceCommand.BaseCommandSize + 8)]
        public byte ledB;

        public static DualShock4UsbOutputCommand Create(float lowFreq, float highFreq, Color ledColor)
        {
            return new DualShock4UsbOutputCommand
            {
                // La taille totale envoyée au driver est le header (8 octets) + 32 octets de données HID
                baseCommand = new InputDeviceCommand(Type, InputDeviceCommand.BaseCommandSize + 32),
                reportId = 0x05,
                flags = 0x03, // On demande à modifier la vibration ET la couleur de la LED simultanément
                rumbleLeft = (byte)Mathf.Clamp(lowFreq * 255, 0, 255),
                rumbleRight = (byte)Mathf.Clamp(highFreq * 255, 0, 255),
                ledR = (byte)Mathf.Clamp(ledColor.r * 255, 0, 255),
                ledG = (byte)Mathf.Clamp(ledColor.g * 255, 0, 255),
                ledB = (byte)Mathf.Clamp(ledColor.b * 255, 0, 255)
            };
        }
    }
}