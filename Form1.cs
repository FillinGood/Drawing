using System.Diagnostics;

namespace Drawing;

public partial class Form1 : Form {
    const int SCALE = 5;
    PictureBox pictureBox1;
    Algorithms algo;
    List<PointF> points = new List<PointF>();
    public Form1() {
        InitializeComponent();
        pictureBox1 = new MyPictureBox() { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom };
        Controls.Add(pictureBox1);
        pictureBox1.MouseClick += PictureBox1_MouseClick;
    }

    private void PictureBox1_MouseClick(object? sender, MouseEventArgs e) {
        PointF p = new PointF(e.X/SCALE, e.Y/SCALE);
        points.Add(p);
        if(points.Count < 2) {
            return;
        }
        using Graphics g = Graphics.FromImage(pictureBox1.Image);
        g.Clear(Color.White);
        for(int i = 1; i < points.Count; i++) {
            /*algo.NaiveLine(points[i - 1].ToInt(), points[i].ToInt());*/
            /*algo.BresenhamLine(points[i - 1].ToInt(), points[i].ToInt());*/
            algo.XiaolinWuLine(points[i - 1], points[i]);
        }
        pictureBox1.Refresh();
    }

    protected override void OnShown(EventArgs e) {
        base.OnShown(e);
        Bitmap bmp = new Bitmap(pictureBox1.Width / SCALE, pictureBox1.Height / SCALE);
        pictureBox1.Image = bmp;
        algo = new Algorithms(bmp);
        pictureBox1.Refresh();
    }
}
