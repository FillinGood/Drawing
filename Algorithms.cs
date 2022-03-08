namespace Drawing;
public class Algorithms {
    private Bitmap bmp;
    public Algorithms(Bitmap b) {
        bmp = b;
        
    }

    public void Plot(int x, int y, float c = 1) {
        bmp.SetPixel(x, y, Color.FromArgb((int)Math.Floor(255*c), 0, 0, 0));
    }
    public void NaiveLine(Point a, Point b) {
        int dx = b.X - a.X;
        int dy = b.Y - a.Y;
        for (int x = a.X; x <= b.X; x++) {
            int y = a.Y + dy * (x - a.X) / dx;
            Plot(x, y);
        }
        bmp.SetPixel(a.X, a.Y, Color.Red);
        bmp.SetPixel(b.X, b.Y, Color.Blue);
    }
    public void BresenhamLine(Point a, Point b) {
        void PlotLineLow(int x0, int y0, int x1, int y1) {
            int dx = x1 - x0;
            int dy = y1 - y0;
            int yi = 1;
            if(dy < 0) {
                yi = -1;
                dy = -dy;
            }
            int D = (2 * dy) - dx;
            int y = y0;
            for(int x = x0; x <= x1; x++) {
                Plot(x, y);
                if(D > 0) {
                    y += yi;
                    D += 2 * (dy - dx);
                } else {
                    D += 2 * dy;
                }
            }
        }
        void PlotLineHigh(int x0, int y0, int x1, int y1) {
            int dx = x1 - x0;
            int dy = y1 - y0;
            int xi = 1;
            if(dx < 0) {
                xi = -1;
                dx = -dx;
            }
            int D = (2 * dx) - dy;
            int x = x0;
            for(int y = y0; y <= y1; y++) {
                Plot(x, y);
                if(D > 0) {
                    x += xi;
                    D += 2 * (dx - dy);
                } else {
                    D += 2 * dx;
                }
            }
        }
        if(Math.Abs(b.Y - a.Y) < Math.Abs(b.X - a.X)) {
            if(a.X > b.X) {
                PlotLineLow(b.X, b.Y, a.X, a.Y);
            } else {
                PlotLineLow(a.X, a.Y, b.X, b.Y);
            }
        } else {
            if(a.Y > b.Y) {
                PlotLineHigh(b.X, b.Y, a.X, a.Y);
            } else {
                PlotLineHigh(a.X, a.Y, b.X, b.Y);
            }
        }
    }
    public void XiaolinWuLine(PointF a, PointF b) {
        static float IPart(float x) => (float)Math.Floor(x);
        static float Round(float x) => (float)Math.Round(x);
        static float FPart(float x) => x - IPart(x);
        static float RFPart(float x) => 1f - FPart(x);
        float x0 = a.X;
        float x1 = b.X;
        float y0 = a.Y;
        float y1 = b.Y;
        float gradient = 1f;
        bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
        if(steep) {
            (x0, y0) = (y0, x0);
            (x1, y1) = (y1, x1);
        }
        if(x0 > x1) {
            (x0, x1) = (x1, x0);
            (y0, y1) = (y1, y0);
        }
        float dx = x1 - x0;
        float dy = y1 - y0;
        if(dx != 0f) {
            gradient = dy / dx;
        }

        float xend = Round(x0);
        float yend = y0 + gradient * (xend - x0);
        float xgap = RFPart(x0 + 0.5f);
        float xpxl1 = xend;
        float ypxl1 = IPart(yend);
        if (steep) {
            Plot((int)ypxl1, (int)xpxl1, RFPart(yend)*xgap);
            Plot((int)ypxl1+1, (int)xpxl1, FPart(yend) * xgap);
        } else {
            Plot((int)xpxl1, (int)ypxl1, RFPart(yend) * xgap);
            Plot((int)xpxl1, (int)ypxl1+1, FPart(yend) * xgap);
        }
        float intery = yend + gradient;
        xend = Round(x1);
        yend = y1 + gradient * (xend - x1);
        xgap = FPart(x1 + 0.5f);
        float xpxl2 = xend;
        float ypxl2 = IPart(yend);
        if(steep) {
            Plot((int)ypxl2, (int)xpxl2, RFPart(yend) * xgap);
            Plot((int)ypxl2 + 1, (int)xpxl2, FPart(yend) * xgap);
        } else {
            Plot((int)xpxl2, (int)ypxl2, RFPart(yend) * xgap);
            Plot((int)xpxl2, (int)ypxl2 + 1, FPart(yend) * xgap);
        }
        if(steep) {
            for(float x = xpxl1 + 1; x <= xpxl2 - 1; x++) {
                Plot((int)IPart(intery), (int)x, RFPart(intery));
                Plot((int)IPart(intery) + 1, (int)x, FPart(intery));
                intery += gradient;
            }
        } else {
            for(float x = xpxl1 + 1; x <= xpxl2 - 1; x++) {
                Plot((int)x, (int)IPart(intery), RFPart(intery));
                Plot((int)x, (int)IPart(intery) + 1, FPart(intery));
                intery += gradient;
            }
        }
    }
}
