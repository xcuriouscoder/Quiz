using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz
{
    public partial class QuizItemForm : Form
    {
        private List<QuizItem> QuizItems { get; set; }
        public bool InResultsMode { get; private set; }
        public int CurrentQuestionNumber { get; private set; }

        public QuizItemForm()
        {
            var filePicker = new OpenFileDialog()
            {
                Filter = "Quiz Files (*.json)|*.json",
                Title = "Open Quiz file"
            };

            if (filePicker.ShowDialog() == DialogResult.OK)
            {
                var filePath = filePicker.FileName;

                var quizItems = RandomizeOrder(filePath);

                this.QuizItems = quizItems;
//                RunQuiz(quizItems);
            }

            InitializeComponent();

            this.SetFieldsForCurrentItem();
        }

        public QuizItemForm(List<QuizItem> quiz)
        {
            this.QuizItems = quiz;

            InitializeComponent();

            this.SetFieldsForCurrentItem();
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

        private void NextButton_Click(object sender, EventArgs e)
        {
            var currQ = OptionsList.SelectedItem as QuestionOption;
            if (currQ != null)
            {
                var qItem = this.QuizItems[this.CurrentQuestionNumber];
                qItem.SelectedAnswer = currQ.Option;
                qItem.WasAnsweredCorrectly = currQ.IsAnswer;
                
            }

            if(this.CurrentQuestionNumber < QuizItems.Count -1)
            {
                this.CurrentQuestionNumber++;
                this.SetFieldsForCurrentItem();

                if(this.CurrentQuestionNumber == QuizItems.Count - 1 && this.InResultsMode)
                {
                    this.NextButton.Text = "Close";
                }

            }
            else if(!this.InResultsMode)
            {
//                this.InResultsMode = true;
                this.ShowResults();
            }
            else
            {
                this.Close();
            }
        }
    }
}
