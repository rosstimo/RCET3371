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

        // Event Handlers below here ****************************************** 

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DrawPictureBox_Click(object sender, EventArgs e)
        {
            DrawLine();
        }
    }
}
