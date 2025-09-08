using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace Quiz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var filePicker = new OpenFileDialog()
            {
                Filter = "Quiz Files (*.json)|*.json",
                Title = "Open Quiz file"
            };

            if (filePicker.ShowDialog() == DialogResult.OK)
            {
                var filePath = filePicker.FileName;

                var quizItems = RandomizeOrder(filePath);

                RunQuiz(quizItems);
            }
        }

        private static List<QuizItem> RandomizeOrder(string filePath)
        {
            var random = new Random();

            var contents = File.ReadAllText(filePath);
            var quizItems = JsonSerializer.Deserialize<List<QuizItem>>(contents);

            quizItems = quizItems.OrderBy(item => random.Next()).ToList();

            foreach (var qi in quizItems)
            {
                qi.Options = qi.Options.OrderBy(i => random.Next()).ToList();
            }

            return quizItems;
        }

        private void RunQuiz(List<QuizItem> quiz)
        {
            var qd = new QuizItemForm(quiz);
            qd.ShowDialog();
            qd.ShowResults();

            //foreach (var item in quiz)
            //{
            //    qd.SetFields(item);
            //    qd.ShowDialog();
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var filePicker = new OpenFileDialog()
            //{
            //    Filter = "Quiz Files (*.json)|*.json",
            //    Title = "Open Quiz file"
            //};

            //if(filePicker.ShowDialog() == DialogResult.OK)
            //{
            //    var filePath = filePicker.FileName;

            //    var contents = File.ReadAllText(filePath);
            //    var quiz = JsonSerializer.Deserialize<QuizItem[]>(contents);
            //}
        }
    }
}
