
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Security.Permissions;
using System.Windows.Forms;

namespace CSCustomTabControlDemo
{
	public partial class MainForm : System.Windows.Forms.Form
	{
		public MainForm() {
			InitializeComponent();
		}
        protected override void OnDpiChanged(DpiChangedEventArgs e)
        {
            base.OnDpiChanged(e);

            //tabControlExtra1.ApplyDpi(e.DeviceDpiNew);

        }
    }
}
