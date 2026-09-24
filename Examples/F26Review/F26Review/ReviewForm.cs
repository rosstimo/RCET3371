namespace F26Review
{
    public partial class ReviewForm : Form
    {
        public ReviewForm()
        {
            InitializeComponent();
        }

        void DrawLine()
        {
            Graphics g = DrawPictureBox.CreateGraphics();
            Pen thePen = new Pen(Color.Black);
            g.DrawLine(thePen, 0, 0, 100, 100);
            
            thePen.Dispose();
            g.Dispose();
        }
        void DrawDart(int x, int y)
        {
            Graphics g = DrawPictureBox.CreateGraphics();
            Pen thePen = new Pen(Color.Black);
            int size = 50;
            
            g.DrawEllipse(thePen, x, y, size, size);
            g.DrawLine(thePen, x - 10, y, x + 10, y);
            g.DrawLine(thePen, x, y - 10, x, y + 10);

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
            DrawLine();
            DrawDart(100, 100);
        }
    }
}
