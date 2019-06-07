using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Console_PE_Parser
{
    public partial class Entropy : Form
    {
        List<KeyValuePair<byte, int>> ent { get; set; }

        public Entropy(List<KeyValuePair<byte, int>> _ent)
        {
            ent = _ent;
            InitializeComponent();
            Draw();
        }

        private void Draw()
        {
            GraphPane pane = zedGraph.GraphPane;
            pane.Title.Text = "Гистограмма распределения байтов";
            pane.CurveList.Clear();

            BarItem bar = pane.AddBar("Байт", null, ent.Select(s => (double)s.Value).ToArray(), Color.AliceBlue);

            pane.XAxis.Type = AxisType.Text;
            pane.XAxis.Scale.TextLabels = ent.Select(s => s.Key.ToString("X")).ToArray();
            pane.XAxis.Title.IsVisible = false;
            pane.YAxis.Title.IsVisible = false;

            pane.BarSettings.MinClusterGap = 0.0f;
            zedGraph.AxisChange();
            zedGraph.Invalidate();
        }
    }
}
