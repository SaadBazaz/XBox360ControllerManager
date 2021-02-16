using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Controls;
using ContextMenu = System.Windows.Forms.ContextMenu;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MenuItem = System.Windows.Forms.MenuItem;

using System;
using System.Windows.Forms;
using System;
using System.Runtime.InteropServices;

using WindowsInput;
using WindowsInput.Native;




namespace XBox360ControllerManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    [Windows.Foundation.Metadata.Static(typeof(Windows.Gaming.UI.GameBar), 131072, "Windows.Foundation.UniversalApiContract")]
    public partial class MainWindow
    {

        bool GameBarVisible = false;



        private const int WM_SETTEXT = 0x000c;

        private const int WM_KEYDOWN = 0x0100;

        public MainWindow()
        {
            InitializeComponent();
            InitializeXBox360Timer();
            InitializeTrayIcon();
        }

        private void InitializeTrayIcon()
        {
            ContextMenu contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(new MenuItem(Properties.Resources.OpenMenuTitle, (sender, args) => Show()));
            contextMenu.MenuItems.Add(new MenuItem(Properties.Resources.ExitMenuTitle, (sender, args) => Close()));
            contextMenu.MenuItems.Add(Properties.Resources.SeparatorString);
            contextMenu.MenuItems.Add(new MenuItem(Properties.Resources.AboutMenuTitle, (sender, args) => Process.Start(Properties.Resources.AboutURL)));

            NotifyIcon notifyIcon = new NotifyIcon
            {
                Icon = Properties.Resources.Icon,
                Visible = true,
                ContextMenu = contextMenu
            };
            notifyIcon.DoubleClick += (sender, args) => Show();
        }
        internal static class NativeMethods
        {
            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool PostMessage(IntPtr hWnd, int Msg, Keys wParam, IntPtr lParam);

            [DllImport("User32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);
        }
    private void InitializeXBox360Timer()
        {
                    //Listen to GameBar visibility status
                    Windows.Gaming.UI.GameBar.VisibilityChanged +=
                (s, e) => {
                    GameBarVisible = !GameBarVisible;
                };
            System.Diagnostics.Debug.WriteLine("This is a log");
            Timer timer = new Timer { Enabled = true };
            int guideCounter = 0;
            timer.Tick += (sender, args) =>
            {
                if (AnyGamepadHasButton(XBox360GamepadButton.Guide) && !IsVisible)
                {

                    System.Diagnostics.Debug.WriteLine(GameBarVisible);
                    System.Diagnostics.Debug.WriteLine(Windows.Gaming.UI.GameBar.IsInputRedirected);
                    System.Diagnostics.Debug.WriteLine(guideCounter);

                    guideCounter++;
                    if (guideCounter == 1)
                    {
                        var sim = new InputSimulator();
                        sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_G);
                        //if (Windows.Gaming.UI.GameBar.Visible)
                        //{
                        //    //the game bar is visible;
                        //}

                        //if (Windows.Gaming.UI.GameBar.IsInputRedirected)
                        //{
                        //    //the game bar has input;
                        //}
                    }
                    else if (guideCounter == 10)
                    {
                        Show();
                    }
                    //else if (guideCounter > 10)
                    //{
                    //    guideCounter = 0;
                    //}
                }
                else 
                //if (true)
                //{

                    if (AnyGamepadHasButton(XBox360GamepadButton.A))
                    {

                        var sim = new InputSimulator();
                        sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                        //IntPtr hWndNotepad = NativeMethods.FindWindow("Xbox Game Bar", null);

                        //IntPtr hWndEdit = NativeMethods.FindWindowEx(hWndNotepad, IntPtr.Zero, "Edit", null);

                        ////NativeMethods.SendMessage(hWndEdit, WM_SETTEXT, 0, " key");

                        //NativeMethods.PostMessage(hWndEdit, WM_KEYDOWN, Keys.Tab, IntPtr.Zero);

                        //Console.ReadKey(true);

                    }
                    else if (AnyGamepadHasButton(XBox360GamepadButton.Y))
                    {
                        var sim = new InputSimulator();
                        sim.Keyboard.KeyPress(VirtualKeyCode.SPACE);
                        //IntPtr hWndNotepad = NativeMethods.FindWindow("Xbox Game Bar", null);

                        //IntPtr hWndEdit = NativeMethods.FindWindowEx(hWndNotepad, IntPtr.Zero, "Edit", null);

                        ////NativeMethods.SendMessage(hWndEdit, WM_SETTEXT, 0, " key");

                        //NativeMethods.PostMessage(hWndEdit, WM_KEYDOWN, Keys.Tab, IntPtr.Zero);

                        //Console.ReadKey(true);

                    }
                    else if (AnyGamepadHasButton(XBox360GamepadButton.DPadRight))
                    {
                        var sim = new InputSimulator();
                        sim.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
                        //SendKeys.SendWait("{TAB}");
                    }
                    else if (AnyGamepadHasButton(XBox360GamepadButton.DPadLeft))
                    {
                        var sim = new InputSimulator();
                        sim.Keyboard.KeyPress(VirtualKeyCode.LEFT);

                    }
                    else if (AnyGamepadHasButton(XBox360GamepadButton.DPadDown))
                    {
                        var sim = new InputSimulator();
                        sim.Keyboard.KeyPress(VirtualKeyCode.DOWN);

                    }
                    else if (AnyGamepadHasButton(XBox360GamepadButton.DPadUp))
                    {
                        var sim = new InputSimulator();
                        sim.Keyboard.KeyPress(VirtualKeyCode.UP);

                    }
                    else if (AnyGamepadHasButton(XBox360GamepadButton.RightShoulder))
                    {
                        var sim = new InputSimulator();
                        sim.Keyboard.KeyPress(VirtualKeyCode.TAB);

                    }
                    else if (AnyGamepadHasButton(XBox360GamepadButton.LeftShoulder))
                    {
                        var sim = new InputSimulator();
                        sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.TAB);
                    }
                    else if (AnyGamepadHasButton(XBox360GamepadButton.Back))
                    {
                        var sim = new InputSimulator();
                        sim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.SHIFT }, VirtualKeyCode.TAB);
                    }

                    else if (AnyGamepadHasButton(XBox360GamepadButton.Start))
                    {
                        var sim = new InputSimulator();
                        sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.TAB);
                    }
                    else if (AnyGamepadHasButton(XBox360GamepadButton.B))
                    {
                        System.Diagnostics.Debug.WriteLine(Windows.Gaming.UI.GameBar.Visible);
                        System.Diagnostics.Debug.WriteLine(Windows.Gaming.UI.GameBar.IsInputRedirected);
                        var sim = new InputSimulator();
                        sim.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
                    }
                //}

                else if (AnyGamepadHasButton(XBox360GamepadButton.B) && IsVisible)
                {
                    var sim = new InputSimulator();
                    sim.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
                    Hide();
                }
                else if (AnyGamepadHasButton(XBox360GamepadButton.X) && IsVisible)
                {
                    PowerOff();
                }
                else
                {
                    guideCounter = 0;
                }
            };
        }

        private void UIElement_OnIsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ToggleUIElementMouseOver((Image)sender, (bool)e.NewValue);
        }

        private void UIElement_OnMouseLeftButtonUp_PowerOff(object sender, MouseButtonEventArgs e)
        {
            PowerOff();
        }

        private void UIElement_OnMouseLeftButtonUp_HideWindow(object sender, MouseButtonEventArgs e)
        {
            Hide();
        }

        private void MainWindow_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Hide();
            }
        }

        private static bool AnyGamepadHasButton(XBox360GamepadButton button)
        {
            return XBox360.GetGamepads().Any(gamepad => gamepad.WButtons == button);
        }

        private static void ToggleUIElementMouseOver(UIElement image, bool isMouseOver)
        {
            image.Opacity = isMouseOver ? 0.75 : 1;
        }

        private void PowerOff()
        {
            XBox360.PowerOffGamepads();
            Hide();
        }
    }
}
