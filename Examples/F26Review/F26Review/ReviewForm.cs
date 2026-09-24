namespace F26Review
{
    public partial class ReviewForm : Form
    {
        public ReviewForm()
        {
            InitializeComponent();
        }

        void Spiral()
        {
            Graphics g = DrawPictureBox.CreateGraphics();
            Pen thePen = new Pen(Color.RebeccaPurple);
            int xOffset = DrawPictureBox.Width / 2;
            int yOffset = DrawPictureBox.Height / 2;

            double x, y, xOld = 0 , yOld = 0;
            int radius = 1, size = 1;

            for (int i = 0; i < 720; i += 1)
            {
                x = radius * Math.Cos(i * (Math.PI/180));
                y = radius * Math.Sin(i * (Math.PI/180));
                x += xOffset;
                y += yOffset;
                g.DrawLine(thePen, (int)xOld, (int)yOld, (int)x, (int)y);
                DrawDart((int)x, (int)y, size);
                xOld = x;
                yOld = y;
                radius += 1;
                size += 1;
            }
            thePen.Dispose();
            g.Dispose();
        }

        int GetNumberFrom(int max, int min = 0)
        {   
            Random randy = new Random();
            return randy.Next(min , max + 1);
        }

        void DrawLine(int x, int y)
        {
            Graphics g = DrawPictureBox.CreateGraphics();
            Pen thePen = new Pen(Color.RebeccaPurple);
            g.DrawLine(thePen, 0, 0, x, y);
            
            thePen.Dispose();
            g.Dispose();
        }
        void DrawDart(int x, int y, int size = 30)
        {
            Graphics g = DrawPictureBox.CreateGraphics();
            Pen thePen = new Pen(Color.FromArgb(255 ,GetNumberFrom(255), GetNumberFrom(255) ,GetNumberFrom(255)),3);
            
            g.DrawEllipse(thePen, x - size/2, y - size/2, size, size);
            //g.DrawLine(thePen, x - size/4, y, x + size/4, y);
            //g.DrawLine(thePen, x, y - size/4, x, y + size/4);

            thePen.Dispose();
            g.Dispose();
        }

        // Event Handlers below here ****************************************** 

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DrawPictureBox_Click(object sender, EventArgs e)
        {
            int x = GetNumberFrom(DrawPictureBox.Width);
            int y = GetNumberFrom(DrawPictureBox.Height);
            Spiral();
            //DrawLine(x,y);
            DrawDart(x,y, GetNumberFrom(75, 25));
        }
    }
}
