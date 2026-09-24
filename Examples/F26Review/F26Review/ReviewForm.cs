namespace F26Review
{
    public partial class ReviewForm : Form
    {
        public ReviewForm()
        {
            InitializeComponent();
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
            Pen thePen = new Pen(Color.FromArgb(255 ,GetNumberFrom(255), GetNumberFrom(255) ,GetNumberFrom(255)),2);
            
            g.DrawEllipse(thePen, x - size/2, y - size/2, size, size);
            g.DrawLine(thePen, x - size/4, y, x + size/4, y);
            g.DrawLine(thePen, x, y - size/4, x, y + size/4);

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
            //DrawLine(x,y);
            DrawDart(x,y, GetNumberFrom(75, 25));
        }
    }
}
