using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Controls;
using ContextMenu = System.Windows.Forms.ContextMenu;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MenuItem = System.Windows.Forms.MenuItem;


using WindowsInput;
using WindowsInput.Native;


namespace XBox360ControllerManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
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

        private void InitializeXBox360Timer()
        {
            Timer timer = new Timer { Enabled = true };
            int guideCounter = 0;
            timer.Tick += (sender, args) =>
            {
                if (AnyGamepadHasButton(XBox360GamepadButton.Guide) && !IsVisible)
                {
                    guideCounter++;
                    if (guideCounter == 1)
                    {
                        var sim = new InputSimulator();
                        sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_G);
                    }   
                    else if (guideCounter == 10)
                    {
                        Show();
                    }
                }
                else if (AnyGamepadHasButton(XBox360GamepadButton.B) && IsVisible)
                {
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
