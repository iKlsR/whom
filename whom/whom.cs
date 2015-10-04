using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace whom
{
    public partial class whom : Form
    {
        public whom()
        {
            InitializeComponent();
            this.MaximizeBox = this.MinimizeBox = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            var screen = Screen.FromPoint(this.Location);
            this.Location = new Point(screen.WorkingArea.Right - this.Width, screen.WorkingArea.Bottom - this.Height);
            base.OnLoad(e);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        void ChildFormClosed(object sender, FormClosedEventArgs e)
        {
            whom_Load(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form AddNew = new AddNew();

            AddNew.FormClosed += new FormClosedEventHandler(ChildFormClosed);

            AddNew.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            File.Copy(@"%systemroot%\System32\drivers\etc\hosts", Directory.GetCurrentDirectory().ToString() + "\\.helper\\hosts");
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void whom_Load(object sender, EventArgs e)
        {
            if (!(new FileInfo(Directory.GetCurrentDirectory() + "\\.helper\\custom.txt").Length == 0))
            {
                var lines = File.ReadLines(Directory.GetCurrentDirectory() + "\\.helper\\custom.txt");

                foreach (var line in lines)
                {
                    var match = Regex.Match(line, @"\b(?<ipaddr>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\b\s+(?<sitename>\w+.?\w+)");
                    treeView1.Nodes[0].Nodes.Add(match.Groups["ipaddr"].ToString() + new String(' ', 10) + match.Groups["sitename"].ToString());
                    treeView1.ExpandAll();
                }
            }
        }
    }
}
