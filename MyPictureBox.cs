using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing;
public class MyPictureBox : PictureBox {
    protected override void OnPaint(PaintEventArgs e) {
        e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        base.OnPaint(e);
    }
}
