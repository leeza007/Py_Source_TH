using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
namespace Py_Server_Launcher
{
    public partial class Main : Form
    {
        public int irestart = 0;
        public int iping = 0;

        public int Authconnect = 0;
        public int Loginconnect = 0;
        public int Messconnect = 0;

        public int Game01connect = 0;
        public int Game02connect = 0;

        Thread CheakThread;

        //Create new bitmap
        Bitmap RED = new Bitmap(22, 22);
        Bitmap GREEN = new Bitmap(22, 22);

        //Grab console
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        const int WS_BORDER = 8388608;
        const int WS_DLGFRAME = 4194304;
        const int WS_CAPTION = WS_BORDER | WS_DLGFRAME;
        const int WS_SYSMENU = 524288;
        const int WS_THICKFRAME = 262144;
        const int WS_MINIMIZE = 536870912;
        const int WS_MAXIMIZEBOX = 65536;
        const int GWL_STYLE = (int)-16L;
        const int GWL_EXSTYLE = (int)-20L;
        const int WS_EX_DLGMODALFRAME = (int)0x1L;
        const int SWP_NOMOVE = 0x2;
        const int SWP_NOSIZE = 0x1;
        const int SWP_FRAMECHANGED = 0x20;
        const uint MF_BYPOSITION = 0x400;
        const uint MF_REMOVE = 0x1000;


        public Main()
        {
            InitializeComponent();          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheakThread = new Thread(CheakServer);
            CheakThread.IsBackground = true;
            CheakThread.Start();
            Graphics flagGraphics = Graphics.FromImage(RED);
            flagGraphics.FillRectangle(Brushes.Red, 0, 0, 22, 22);
            Graphics flagGraphics2 = Graphics.FromImage(GREEN);
            flagGraphics2.FillRectangle(Brushes.Green, 0, 0, 22, 22);
        }


        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RequestStopt();
            //Close them all before
            ProssKill("GameServer");
            ProssKill("Login");
            ProssKill("AuthServer");
            ProssKill("Messenger");
            CheakThread.Join();
            Close();
        }

        private void LoadApplication(Process compiler, IntPtr handle, int x, int y)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int timeout = 10 * 1000;

            compiler.Start();
            while (compiler.MainWindowHandle == IntPtr.Zero)
            {
                System.Threading.Thread.Sleep(10);
                compiler.Refresh();

                if (sw.ElapsedMilliseconds > timeout)
                {
                    sw.Stop();
                    return;
                }
            }

            SetParent(compiler.MainWindowHandle, handle);

            SetWindowPos(compiler.MainWindowHandle, 0, 0, 0, x, y, 0x0040);
        }


        private void autoRestartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (irestart == 0)
            {
                this.autoRestartToolStripMenuItem.Text = "Auto-Restart : ON";
                irestart = 1;
            }
            else
            {
                this.autoRestartToolStripMenuItem.Text = "Auto-Restart : OFF";
                irestart = 0;
            }
        }

        public void CheakServer()
        {
            while (!_shouldStop)
            {

                Loginconnect = ExeCheaker("Login");
                Authconnect = ExeCheaker("AuthServer");
                Game01connect = ExeCheaker("GameServer");
                Game02connect = ExeCheaker("Game02");
                Messconnect = ExeCheaker("Messenger");

                if (irestart == 1)
                {

                    if (Authconnect == 0)
                    {
                        //Close them all before
                        ProssKill("GameServer");
                        ProssKill("Login");
                        ProssKill("AuthServer");
                        ProssKill("Messenger");
                        //server ON
                        ProssLaunch(1);
                        ProssLaunch(2);
                        ProssLaunch(3);
                        ProssLaunch(5);
                    }
                    else
                    {
                        if (Loginconnect == 0)
                        {
                            ProssLaunch(2);//Login  
                        }
                        if (Game01connect == 0)
                        {
                            ProssLaunch(2);//game 1
                        }

                        if (Messconnect == 0)
                        {
                            ProssLaunch(5);//msg 1
                        }

                    }


                }

                Thread.Sleep(300);
            }

        }

        public int PortCheaker(string host, int port)
        {
            TcpClient tc = new TcpClient();
            try
            {

                tc.Connect(host, port);
                bool stat = tc.Connected;
                if (stat)
                    return 1;

                tc.Close();
                return 0;
            }
            catch
            {
                tc.Close();
                return 0;
            }
        }

        public int ExeCheaker(string name)
        {
            Process[] myProcesses;
            myProcesses = Process.GetProcessesByName(name);
            foreach (Process myProcess in myProcesses)
            {
                return 1;
            }
            return 0;
        }

        public void RequestStopt()
        {
            _shouldStop = true;
        }
        private volatile bool _shouldStop;
        public void ProssKill(string kill)
        {
            Process[] myProcesses;
            myProcesses = Process.GetProcessesByName(kill);
            foreach (Process myProcess in myProcesses)
            {
                myProcess.Kill();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (Authconnect == 0)
                pictureBox1.Image = RED;
            else
                pictureBox1.Image = GREEN;

            if (Loginconnect == 0)
                pictureBox2.Image = RED;
            else
                pictureBox2.Image = GREEN;

            if (Messconnect == 0)
                pictureBox5.Image = RED;
            else
                pictureBox5.Image = GREEN;

            if (Game01connect == 0)
                pictureBox3.Image = RED;
            else
                pictureBox3.Image = GREEN;

            if (Game02connect == 0)
                pictureBox4.Image = RED;
            else
                pictureBox4.Image = GREEN;
        }

        public void ProssLaunch(int Type)
        {
            Process compiler = new Process();

            try
            {
                switch (Type)
                {
                    case 1:
                        compiler.StartInfo.FileName = "AuthServer.exe";
                        compiler.StartInfo.UseShellExecute = true;
                        compiler.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                        LoadApplication(compiler, tabPage1.Handle, tabPage1.Size.Width, tabPage1.Size.Height);
                        Authconnect = 1;
                        Thread.Sleep(9000);
                        break;
                    case 2:
                        compiler.StartInfo.FileName = "Login.exe";
                        compiler.StartInfo.UseShellExecute = true;
                        compiler.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                        LoadApplication(compiler, tabPage2.Handle, tabPage1.Size.Width, tabPage1.Size.Height);
                        Loginconnect = 1;
                        break;
                    case 3:
                        compiler.StartInfo.FileName = "GameServer.exe";
                        compiler.StartInfo.UseShellExecute = true;
                        compiler.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                        LoadApplication(compiler, tabPage3.Handle, tabPage1.Size.Width, tabPage1.Size.Height);
                        Game01connect = 1;
                        break;
                    case 4:
                        {
                            compiler.StartInfo.FileName = "Game02.exe";
                            compiler.StartInfo.UseShellExecute = true;
                            compiler.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                            LoadApplication(compiler, tabPage4.Handle, tabPage1.Size.Width, tabPage1.Size.Height);
                            Game02connect = 1;
                        }
                        break;
                    case 5:
                        {
                            compiler.StartInfo.FileName = "Messenger.exe";
                            compiler.StartInfo.UseShellExecute = true;
                            compiler.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                            LoadApplication(compiler, tabPage5.Handle, tabPage1.Size.Width, tabPage1.Size.Height);
                            Messconnect = 1;
                        }
                        break;
                    default:
                        {
                            compiler.StartInfo.FileName = "Unknown.exe";
                            compiler.StartInfo.UseShellExecute = true;
                            compiler.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                            LoadApplication(compiler, tabPage4.Handle, tabPage1.Size.Width, tabPage1.Size.Height);
                        }
                        break;
                }
            }
            catch( Exception ex)
            {
                MessageBox.Show(ex.Message + ": " + compiler.StartInfo.FileName, "Pangya Server Launcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            ProssLaunch(1);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ProssKill("AuthServer");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            ProssLaunch(2);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            ProssKill("Login");
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            ProssLaunch(3);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            ProssKill("GameServer");
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            ProssLaunch(4);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            ProssKill("Game02");
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            ProssLaunch(5);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            ProssKill("Messenger");
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            //Close them all before
            ProssKill("GameServer");
            ProssKill("Login");
            ProssKill("AuthServer");
            ProssKill("Messenger");
            //server ON
            ProssLaunch(1);
            ProssLaunch(2);
            ProssLaunch(3);
            ProssLaunch(5);
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            //Server OFF
            ProssKill("GameServer");
            ProssKill("Messenger");
            ProssKill("Login");
            ProssKill("AuthServer");
        }
    }
}