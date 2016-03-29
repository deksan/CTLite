using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

//using CefSharp.WinForms.Internals;

namespace CTLite {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }


        private ChromiumWebBrowser webBrowser;
        private void MainForm_Load(object sender, EventArgs e) {
            Program.OnMouseMove += delegate(int x, int y) {
                var wasVisible = menuStrip.Visible;
                menuStrip.Visible = Bounds.Contains(Cursor.Position);
                if (!wasVisible && menuStrip.Visible) {
                    Height = Width + menuStrip.Height+extraHeight;
                    webBrowser.ExecuteScriptAsync(JsCode.jsCompactShowControls());
                }
                    
                else if (wasVisible && !menuStrip.Visible) {
                    Height = Width+10;
                    webBrowser.ExecuteScriptAsync(JsCode.jsCompactHideControls());
                }



            };
            menuStrip.Visible = (Program.HookId == IntPtr.Zero); // In case no global hook keep it always up
            webBrowser = new ChromiumWebBrowser("http://www.chesstempo.com/chess-tactics.html");
            webBrowser.Dock = DockStyle.Fill;
            webBrowserPanel.Controls.Add(webBrowser);
            // Ensure we have jquery
            webBrowser.IsBrowserInitializedChanged += (o, args) => {

            };
            webBrowser.LoadingStateChanged += (o, args) => {
                if (args.IsLoading)
                    return;
                Full();
            };
            // webBrowser.Navigate();
        }



        protected override void WndProc(ref Message m) {
            const int wmNcHitTest = 0x84;
            const int htBottomLeft = 16;
            const int htBottomRight = 17;

           
            if (m.Msg == wmNcHitTest) {
                int x = (int)(m.LParam.ToInt64() & 0xFFFF);
                int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                Point pt = PointToClient(new Point(x, y));
                Size clientSize = ClientSize;
                if (pt.X >= clientSize.Width - 16 && pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16) {
                    m.Result = (IntPtr)(IsMirrored ? htBottomLeft : htBottomRight);
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private const int extraHeight = 130;
        private void Full() {
            this.UIThread(delegate
            {
                webBrowser.ExecuteScriptAsync(JsCode.jsFull);
                Width = 1024;
                Height = 768;
            });
            
        }

        private void Compact() {
            this.UIThread(delegate {
                Width = 320;
                Height = Width + extraHeight;
                webBrowser.ExecuteScriptAsync(JsCode.jsCompact(Width-25));
                
            });
        }

       


        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e) {
            Full();
        }

        private void compactToolStripMenuItem_Click(object sender, EventArgs e) {
            Compact();

        }



        //--- toolbar drag
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Toolbar_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }



        private void MainForm_MouseLeave(object sender, EventArgs e) {
            //menuStrip.Visible = false;
        }


    }
}
