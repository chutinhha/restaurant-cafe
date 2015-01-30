using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Utilities
{
    /*
    /// <summary>
    /// 
    /// </summary>
    public class RawInput
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lParam"></param>
        /// <param name="fromDevices"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static IntPtr DispatchMessage(IntPtr lParam, IEnumerable<IntPtr> fromDevices, ref object data)
        {                        
            var dataSize = UIntPtr.Zero;
            if (Win32.GetRawInputData(lParam, (UIntPtr)CommandFlag.Input, IntPtr.Zero, ref dataSize, (UIntPtr)Marshal.SizeOf(typeof(Win32.RAWINPUTHEADER))) == UIntPtr.Zero)
            {
                var buff = Marshal.AllocHGlobal((int)dataSize);
                var outSize = Win32.GetRawInputData(lParam, (UIntPtr)CommandFlag.Input, buff, ref dataSize, (UIntPtr)Marshal.SizeOf(typeof(Win32.RAWINPUTHEADER)));

                Debug.Assert(outSize == dataSize);
                var input = (Win32.RAWINPUT)Marshal.PtrToStructure(buff, typeof(Win32.RAWINPUT));

                var hDevice = IntPtr.Zero;
                if (fromDevices == null)
                    hDevice = input.header.hDevice;
                else
                    hDevice = fromDevices.FirstOrDefault(h => h == input.header.hDevice);

                if (hDevice != IntPtr.Zero)
                {
                    switch ((Type)input.header.dwType)
                    {
                        case Type.HID:
                            var hidData = new HIDDATA()
                            {
                                count = input.hid.dwCount,
                                size = input.hid.dwSizeHid,
                                raw = new byte[input.hid.dwCount * input.hid.dwSizeHid]
                            };

                            Marshal.Copy(
                                new IntPtr(
                                    buff.ToInt64() +
                                    Marshal.SizeOf(typeof(Win32.RAWINPUTHEADER)) +
                                    Marshal.SizeOf(typeof(Win32.RAWHID))
                                ),
                                hidData.raw, 0, hidData.raw.Length
                            );

                            data = hidData;
                            break;
                        case Type.Keyboard:
                            data = new KEYBOARDDATA
                            {
                                makeCode = input.keyboard.MakeCode,
                                flags = (KeyboardFlag)input.keyboard.Flags,
                                vKey = input.keyboard.VKey,
                                message = (MessageContext)input.keyboard.Message,
                                extraInfo = input.keyboard.ExtraInformation
                            };
                            break;
                        case Type.Mouse:
                            var mouseData = new MOUSEDATA
                            {
                                extraInfo = input.mouse.ulExtraInformation,
                                flags = (MouseFlag)input.mouse.usFlags,
                                transitions = (MouseTransition)input.mouse.usButtonFlags,
                                motionX = input.mouse.lLastX,
                                motionY = input.mouse.lLastY
                            };

                            if (mouseData.transitions == MouseTransition.MouseWheel)
                                mouseData.wheelDelta = input.mouse.usButtonData;
                            data = mouseData;
                            break;
                    }
                }

                Marshal.FreeHGlobal(buff);
                return hDevice;
            }
            else
                throw new ApplicationException("An error occurred while receiving raw input data.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fromDevices"></param>
        /// <param name="source"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool DispatchMessage(Win32.MSG message, IEnumerable<Device> fromDevices, ref Device source, ref object data)
        {
            var hDevice = IntPtr.Zero;
            if (message.message == MessageContext.Input)
            {
                if (fromDevices != null)
                {
                    hDevice = DispatchMessage(message.lParam, fromDevices.Select(h => h.Handle), ref data);
                    if (hDevice != IntPtr.Zero)
                        source = fromDevices.First(h => h.Handle == hDevice);
                }
                else
                {
                    Func<Device> getRawInputDeviceInfo = () =>
                    {
                        var name_size = UIntPtr.Zero;
                        Win32.GetRawInputDeviceInfo(hDevice, (UIntPtr)Win32.RIDI_DEVICENAME, IntPtr.Zero, ref name_size);

                        if (name_size != UIntPtr.Zero)
                        {
                            var name = Marshal.AllocHGlobal((int)name_size);
                            Win32.GetRawInputDeviceInfo(hDevice, (UIntPtr)Win32.RIDI_DEVICENAME, name, ref name_size);
                            string rid_name = (string)Marshal.PtrToStringAnsi(name);

                            var ridSize = (UIntPtr)Marshal.SizeOf(typeof(Win32.RID_DEVICE_INFO));
                            var buff = Marshal.AllocHGlobal((int)ridSize);
                            var outSize = Win32.GetRawInputDeviceInfo(hDevice, (UIntPtr)Win32.RIDI_DEVICEINFO, buff, ref ridSize);
                            Debug.Assert(outSize == ridSize);

                            var rid = (Win32.RID_DEVICE_INFO)Marshal.PtrToStructure(buff, typeof(Win32.RID_DEVICE_INFO));

                            var device = new Device()
                            {
                                Handle = hDevice,
                                ID = rid_name,
                                Type = (Type)rid.dwType,
                                Description = GetRawInputDeviceDescription(rid_name)
                            };

                            Marshal.FreeHGlobal(name);
                            return device;
                        }

                        return null;
                    };

                    hDevice = DispatchMessage(message.lParam, null, ref data);
                    if (hDevice != IntPtr.Zero)
                        source = getRawInputDeviceInfo();
                }
            }

            return hDevice != IntPtr.Zero;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Device> EnumerateDevices()
        {
            var ridSize = (UIntPtr)Marshal.SizeOf(typeof(Win32.RAWINPUTDEVICELIST));
            var ridCnt = UIntPtr.Zero;
            var list = new List<Device>();

            if (Win32.GetRawInputDeviceList(IntPtr.Zero, ref ridCnt, ridSize) == UIntPtr.Zero)
            {
                var buff = Marshal.AllocHGlobal((int)ridCnt * (int)ridSize);
                Win32.GetRawInputDeviceList(buff, ref ridCnt, ridSize);

                for (int i = 0; i < (int)ridCnt; ++i)
                {
                    var rid = (Win32.RAWINPUTDEVICELIST)Marshal.PtrToStructure(
                        new IntPtr((buff.ToInt64() + (int)ridSize * i)),
                        typeof(Win32.RAWINPUTDEVICELIST)
                    );

                    var nameSize = UIntPtr.Zero;
                    Win32.GetRawInputDeviceInfo(rid.hDevice, (UIntPtr)Win32.RIDI_DEVICENAME, IntPtr.Zero, ref nameSize);

                    if (nameSize != UIntPtr.Zero)
                    {
                        var name = Marshal.AllocHGlobal((int)nameSize);
                        Win32.GetRawInputDeviceInfo(rid.hDevice, (UIntPtr)Win32.RIDI_DEVICENAME, name, ref nameSize);
                        string rid_id = (string)Marshal.PtrToStringAnsi(name);

                        // filter Terminal Services and Remote Desktop devices.
                        if (!rid_id.ToUpper().Contains("ROOT"))
                        {
                            yield return new Device()
                            {
                                Handle = rid.hDevice,
                                ID = rid_id,
                                Type = (Type)rid.dwType,
                                Description = GetRawInputDeviceDescription(rid_id)
                            };

                            Marshal.FreeHGlobal(name);
                        }
                    }
                }

                Marshal.FreeHGlobal(buff);
            }
            else
                throw new ApplicationException("An error occurred while retrieving a list of raw input devices");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="regs"></param>
        /// <param name="mode"></param>
        public static void Register(IntPtr hwnd, REGISTERCLASS[] regs)
        {
            Debug.Assert(hwnd != IntPtr.Zero);

            // Usage Pages and Usages:
            // http://www.usb.org/developers/devclass_docs/Hut1_12.pdf

            var rid = new Win32.RAWINPUTDEVICE[regs.Length];
            for (int i = 0; i < regs.Length; ++i)
            {
                rid[i].dwFlags = (uint)regs[i].flags;
                rid[i].usUsage = regs[i].usage;
                rid[i].usUsagePage = (ushort)regs[i].usagePage;
                rid[i].hwndTarget = hwnd;
            }

            if (Win32.RegisterRawInputDevices(rid, (UIntPtr)rid.Length, (UIntPtr)Marshal.SizeOf(typeof(Win32.RAWINPUTDEVICE))) == IntPtr.Zero)
                throw new ApplicationException("Failed to register raw input devices");
        }


        /// <summary>
        /// 
        /// </summary>
        public class Device
        {
            public static bool operator ==(Device x, Device y) { if ((object)x != null & (object)y != null) { return x.Handle == y.Handle; } else return (object)x == null && (object)y == null; }
            public static bool operator !=(Device x, Device y) { return !(x == y); }

            public override bool Equals(object obj) { return this == (Device)obj; }
            public override int GetHashCode() { return base.GetHashCode(); }

            public IntPtr Handle { get; set; }
            public Type Type { get; set; }
            public string ID { get; set; }
            public string Description { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public struct HIDDATA
        {
            public uint size;
            public uint count;
            public byte[] raw;
        }

        /// <summary>
        /// 
        /// </summary>
        public struct KEYBOARDDATA
        {
            public ushort makeCode;
            public KeyboardFlag flags;
            public ushort vKey;
            public MessageContext message;
            public uint extraInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        public struct MOUSEDATA
        {
            public MouseFlag flags;
            public MouseTransition transitions;
            public ushort wheelDelta;
            public int motionX;
            public int motionY;
            public uint extraInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        public struct REGISTERCLASS
        {
            public UsagePage usagePage;
            public ushort usage;
            public ModeFlag flags;
        }

        /// <summary>
        /// 
        /// </summary>
        public enum KeyboardFlag : ushort
        {
            KeyUp = Win32.RI_KEY_BREAK,
            KeyDown = Win32.RI_KEY_MAKE,
            KeyLeft = Win32.RI_KEY_E0,
            KeyRight = Win32.RI_KEY_E1
        }

        /// <summary>
        /// 
        /// </summary>
        public enum MouseFlag : ushort
        {
            AttributesChanged = Win32.MOUSE_ATTRIBUTES_CHANGED,
            MoveRelative = Win32.MOUSE_MOVE_RELATIVE,
            MoveAbsolute = Win32.MOUSE_MOVE_ABSOLUTE,
            VirtualDesktop = Win32.MOUSE_VIRTUAL_DESKTOP
        }

        /// <summary>
        /// 
        /// </summary>
        public enum MouseTransition : ushort
        {
            LeftButtonDown = Win32.RI_MOUSE_LEFT_BUTTON_DOWN,
            LeftButtonUp = Win32.RI_MOUSE_LEFT_BUTTON_UP,
            MiddleButtonDown = Win32.RI_MOUSE_MIDDLE_BUTTON_DOWN,
            MiddleButtonUp = Win32.RI_MOUSE_MIDDLE_BUTTON_UP,
            RightButtonDown = Win32.RI_MOUSE_RIGHT_BUTTON_DOWN,
            RightButtonUp = Win32.RI_MOUSE_RIGHT_BUTTON_UP,
            XButton1Down = Win32.RI_MOUSE_BUTTON_4_DOWN,
            XButton1Up = Win32.RI_MOUSE_BUTTON_4_UP,
            XButton2Down = Win32.RI_MOUSE_BUTTON_5_DOWN,
            XButton2Up = Win32.RI_MOUSE_BUTTON_5_UP,
            MouseWheel = Win32.RI_MOUSE_WHEEL
        }

        /// <summary>
        /// 
        /// </summary>
        public enum CommandFlag
        {
            Header = Win32.RID_HEADER,
            Input = Win32.RID_INPUT
        }

        /// <summary>
        /// 
        /// </summary>
        public enum ModeFlag : uint
        {
            Default = 0,
            ApplicationKeys = Win32.RIDEV_APPKEYS,
            CaptureMouse = Win32.RIDEV_CAPTUREMOUSE,
            DeviceNotify = Win32.RIDEV_DEVNOTIFY,
            Exclude = Win32.RIDEV_EXCLUDE,
            ExcludeInputSink = Win32.RIDEV_EXINPUTSINK,
            InputSink = Win32.RIDEV_INPUTSINK,
            NoHotKeys = Win32.RIDEV_NOHOTKEYS,
            NoLegacy = Win32.RIDEV_NOLEGACY,
            PageOnly = Win32.RIDEV_PAGEONLY,
            Remove = Win32.RIDEV_REMOVE
        };

        /// <summary>
        /// 
        /// </summary>
        public enum UsagePage : ushort
        {
            Undefined = 0x00,
            GenericDesktopControls = 0x01,
            SimulationControls = 0x02,
            VRControls = 0x03,
            SportControls = 0x04,
            GameControls = 0x05,
            GenericDeviceControls = 0x06,
            Keyboard = 0x07,
            Keypad = Keyboard,
            LEDs = 0x08,
            Button = 0x09,
            Ordinal = 0x0a,
            Telephony = 0x0b,
            Consumer = 0x0c,
            Digitizer = 0x0d,
            PIDPage = 0x0f,
            Unicode = 0x10,
            AlphanumericDisplay = 0x14,
            MedicalInstruments = 0x40,
            MonitorPage0 = 0x80,
            MonitorPage1 = 0x81,
            MonitorPage2 = 0x82,
            MonitorPage3 = 0x83,
            PowerPage0 = 0x84,
            PowerPage1 = 0x85,
            PowerPage2 = 0x86,
            PowerPage3 = 0x87,
            BarCodeScannerPages = 0x8c,
            Scale = 0x8d,
            MagneticStripeReadingDevices = 0x8e,
            ReservedPointofSalePages = 0x8f,
            CameraControlPage = 0x90,
            ArcadePage = 0x91
        }

        /// <summary>
        /// 
        /// </summary>
        public enum GenericDesktopPage : ushort
        {
            Undefined = 0x00,
            Pointer = 0x01,
            Mouse = 0x02,
            Joystick = 0x04,
            GamePad = 0x05,
            Keyboard = 0x06,
            Keypad = 0x07,
            MultiAxisController = 0x08,
            TabletPCSystemControls = 0x09,
            X = 0x30,
            Y = 0x31,
            Z = 0x32,
            Rx = 0x33,
            Ry = 0x34,
            Rz = 0x35,
            Slider = 0x36,
            Dial = 0x37,
            Wheel = 0x38,
            HatSwitch = 0x39,
            CountedBuffer = 0x3a,
            ByteCount = 0x3b,
            MotionWakeup = 0x3c,
            Start = 0x3d,
            Select = 0x3e,
            Vx = 0x40,
            Vy = 0x41,
            Vz = 0x42,
            Vbrx = 0x43,
            Vbry = 0x44,
            Vbrz = 0x45,
            Vno = 0x46,
            FeatureNotification = 0x47,
            ResolutionMultiplier = 0x48,
            SystemControl = 0x80,
            SystemPowerDown = 0x81,
            SystemSleep = 0x82,
            SystemWakeUp = 0x83,
            SystemContextMenu = 0x84,
            SystemMainMenu = 0x85,
            SystemAppMenu = 0x86,
            SystemMenuHelp = 0x87,
            SystemMenuExit = 0x88,
            SystemMenuSelect = 0x89,
            SystemMenuRight = 0x8a,
            SystemMenuLeft = 0x8b,
            SystemMenuUp = 0x8c,
            SystemMenuDown = 0x8d,
            SystemColdRestart = 0x8e,
            SystemWarmRestart = 0x8f,
            DPadUp = 0x90,
            DPadDown = 0x91,
            DPadRight = 0x92,
            DPadLeft = 0x93,
            SystemDock = 0xa0,
            SystemUndock = 0xa1,
            SystemSetup = 0xa2,
            SystemBreak = 0xa3,
            SystemDebuggerBreak = 0xa4,
            ApplicationBreak = 0xa5,
            ApplicationDebuggerBreak = 0xa6,
            SystemSpeakerMute = 0xa7,
            SystemHibernate = 0xa8,
            SystemDisplayInvert = 0xb0,
            SystemDisplayInternal = 0xb1,
            SystemDisplayExternal = 0xb2,
            SystemDisplayBoth = 0xb3,
            SystemDisplayDual = 0xb4,
            SystemDisplayToggleIntExt = 0xb5,
            SystemDisplaySwapPrimarySecondary = 0xb6,
            SystemDisplayLCDAutoscale = 0xb7
        };

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="http://www.usb.org/developers/devclass_docs/pos1_02.pdf"/>
        public enum BarCodeScannerPages : ushort
        {
            BarCodeScanner = 0x0002
        }

        /// <summary>
        /// 
        /// </summary>
        public enum Type : uint
        {
            Mouse = Win32.RIM_TYPEMOUSE,
            Keyboard = Win32.RIM_TYPEKEYBOARD,
            HID = Win32.RIM_TYPEHID
        }


        private static string GetRawInputDeviceDescription(string deviceid)
        {
            string[] id = deviceid.Substring(4).Split('#');

            // get registry entry by appropriate key
            return ((string)Registry.LocalMachine.OpenSubKey(string.Format(
                @"System\CurrentControlSet\Enum\{0}\{1}\{2}",
                id[0], id[1], id[2]), false
            ).GetValue("DeviceDesc")).Split(';')[1];
        }
    }
    */
}
