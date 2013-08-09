using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieUtilsLauncher
{
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            var toolPath = ConfigurationManager.AppSettings["SubsDownloaderPath"];
            this.RunTool(toolPath, e);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void panel2_DragDrop(object sender, DragEventArgs e)
        {
            var toolPath = ConfigurationManager.AppSettings["MovieInfoPath"];
            this.RunTool(toolPath, e);
        }

        private void RunTool(string toolPath, DragEventArgs e)
        {
            Array a = (Array)e.Data.GetData(DataFormats.FileDrop);

            if (a != null)
            {
                string s = a.GetValue(0).ToString();
                Debug.WriteLine(s);

                var p = new Process();
                p.StartInfo.Arguments = "\"" + s + "\"";
                
                if (!Path.IsPathRooted(toolPath))
                {
                    var currPath = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", string.Empty);
                    toolPath = Path.Combine(Path.GetDirectoryName(currPath), toolPath);
                }

                p.StartInfo.FileName = toolPath;
                p.Start();
            }
        }

        private void panel3_DragDrop(object sender, DragEventArgs e)
        {
            var toolPath = ConfigurationManager.AppSettings["SubsDownloaderPath"];
            this.RunTool(toolPath, e);

            toolPath = ConfigurationManager.AppSettings["MovieInfoPath"];
            this.RunTool(toolPath, e);
        }
    }
}
