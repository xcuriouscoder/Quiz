using System;
using System.Drawing.Design;

namespace Quiz
{
    partial class QuizItemForm
    {
        private const string CorrectIdentifierText = "Correct ==> ";
        private const string IncorrectIdentifierText = "You Selected ==> ";

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void ShowResults()
        {
            this.InResultsMode = true;
            this.CurrentQuestionNumber = 0;

            SetTextBasedOnResults();
            this.SetFieldsForCurrentItem();

            this.ResultsLabel.Visible = true;
            this.ExportButton.Visible = true;

            var total = this.QuizItems.Count;
            var correct = this.QuizItems.Where(it => it.WasAnsweredCorrectly).Count();

            var pct = ((float)correct * 100) / (float)total;

            this.ResultsLabel.Text = $"Final Score: {correct} correct out of {total}.  {pct.ToString("F1")}%";
        }

        private void SetTextBasedOnResults()
        {
            foreach (var qi in this.QuizItems)
            {
                if(!qi.WasAnsweredCorrectly)
                {
                    var correctOption = qi.Options.First(op => op.IsAnswer);
                    var selectedOption = qi.Options.First(op => op.Option == qi.SelectedAnswer);
                    correctOption.Option = CorrectIdentifierText + correctOption.Option;
                    selectedOption.Option = IncorrectIdentifierText + selectedOption.Option;
                }
            }
        }

        private void SetFieldsForCurrentItem()
        {
            var item = this.QuizItems[this.CurrentQuestionNumber];

            this.QuestionTextbox.Text = item.Question;
            this.OptionsList.DataSource = item.Options;
            //            this.OptionsList.DataSource = item.Options.Select(op => op.Option).OrderBy(x => random.Next()).ToList();
            this.OptionsList.SelectedItems.Clear();

            SetProgressLabel();

            SetColorForResult(item);
        }

        private void SetProgressLabel()
        {
            this.ProgressLabel.Text = $"{this.CurrentQuestionNumber+1}/{this.QuizItems.Count}";
        }

        private void SetColorForResult(QuizItem item)
        {
            if (!string.IsNullOrEmpty(item.SelectedAnswer))
            {
                if (item.WasAnsweredCorrectly)
                {
                    this.OptionsList.BackColor = Color.LightGreen;
                }
                else
                {
                    this.OptionsList.BackColor = Color.LightPink;
                }
            }
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            OptionsList = new ListBox();
            NextButton = new Button();
            ResultsLabel = new Label();
            ProgressLabel = new Label();
            QuestionTextbox = new TextBox();
            ExportButton = new Button();
            SuspendLayout();
            // 
            // OptionsList
            // 
            OptionsList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            OptionsList.Font = new Font("Segoe UI", 11F);
            OptionsList.FormattingEnabled = true;
            OptionsList.ItemHeight = 30;
            OptionsList.Location = new Point(12, 106);
            OptionsList.Name = "OptionsList";
            OptionsList.Size = new Size(974, 304);
            OptionsList.TabIndex = 1;
            // 
            // NextButton
            // 
            NextButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            NextButton.Location = new Point(14, 435);
            NextButton.Name = "NextButton";
            NextButton.Size = new Size(112, 34);
            NextButton.TabIndex = 2;
            NextButton.Text = "Next";
            NextButton.UseVisualStyleBackColor = true;
            NextButton.Click += NextButton_Click;
            // 
            // ResultsLabel
            // 
            ResultsLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ResultsLabel.AutoSize = true;
            ResultsLabel.Location = new Point(477, 440);
            ResultsLabel.Name = "ResultsLabel";
            ResultsLabel.Size = new Size(67, 25);
            ResultsLabel.TabIndex = 3;
            ResultsLabel.Text = "Results";
            ResultsLabel.Visible = false;
            // 
            // ProgressLabel
            // 
            ProgressLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ProgressLabel.AutoSize = true;
            ProgressLabel.Location = new Point(148, 440);
            ProgressLabel.Name = "ProgressLabel";
            ProgressLabel.Size = new Size(81, 25);
            ProgressLabel.TabIndex = 4;
            ProgressLabel.Text = "Progress";
            // 
            // QuestionTextbox
            // 
            QuestionTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            QuestionTextbox.Font = new Font("Segoe UI", 12F);
            QuestionTextbox.Location = new Point(12, 12);
            QuestionTextbox.Multiline = true;
            QuestionTextbox.Name = "QuestionTextbox";
            QuestionTextbox.ReadOnly = true;
            QuestionTextbox.Size = new Size(974, 88);
            QuestionTextbox.TabIndex = 5;
            // 
            // ExportButton
            // 
            ExportButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ExportButton.Location = new Point(249, 435);
            ExportButton.Name = "ExportButton";
            ExportButton.Size = new Size(112, 34);
            ExportButton.TabIndex = 6;
            ExportButton.Text = "Export Fails";
            ExportButton.UseVisualStyleBackColor = true;
            ExportButton.Visible = false;
            ExportButton.Click += ExportButton_Click;
            // 
            // QuizItemForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(998, 481);
            Controls.Add(ExportButton);
            Controls.Add(QuestionTextbox);
            Controls.Add(ProgressLabel);
            Controls.Add(ResultsLabel);
            Controls.Add(NextButton);
            Controls.Add(OptionsList);
            Name = "QuizItemForm";
            Text = "QuizItemForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListBox OptionsList;
        private Button NextButton;
        private Label ResultsLabel;
        private Label ProgressLabel;
        private TextBox QuestionTextbox;
        private Button ExportButton;
    }
}