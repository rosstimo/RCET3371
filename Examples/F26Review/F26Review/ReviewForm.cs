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
            MessageBox.Show("ima draw line now!");
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
