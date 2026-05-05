using System.Windows.Forms;

namespace CSCustomTabControlDemo
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        protected override void OnDpiChanged(DpiChangedEventArgs e)
        {
            base.OnDpiChanged(e);
            tabControlExtra1.OnDpiChanged(e);
            tabControlExtra2.OnDpiChanged(e);
            tabControlExtra3.OnDpiChanged(e);
            tabControlExtra4.OnDpiChanged(e);
            tabControlExtra5.OnDpiChanged(e);
            tabControlExtra6.OnDpiChanged(e);

        }
    }
}
