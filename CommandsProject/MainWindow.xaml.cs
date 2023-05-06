using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CommandsProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void SetF1CommandBinding()
        {
            CommandBinding helpBinding = new CommandBinding(ApplicationCommands.Help);
            helpBinding.CanExecute += CanHelpExecute;
            helpBinding.Executed += HelpExecute;
            CommandBindings.Add(helpBinding);
        }
        private void CanHelpExecute(object sender,CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void HelpExecute(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Помогите!!! Не понимаю.");
        }
        private void OpenCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void SaveCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void OpenCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog { Filter="Text Files|*.txt"};
            if(openDlg.ShowDialog()==true)
            {
                string data=File.ReadAllText(openDlg.FileName);
                txtData.Text = data;
            }
        }
        private void SaveCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog { Filter = "Text Files|*.txt" };
            if (saveDlg.ShowDialog() == true)
            {
                File.WriteAllText(saveDlg.FileName,txtData.Text);
            }
        }
        private void MouseEnterExitArea(object sender,MouseEventArgs e)
        {
            statBarText.Text = "Закрыть приложение";
        }
        private void FileExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void MouseEnterToolsHintsArea(object sender, MouseEventArgs e)
        {
            statBarText.Text = "Показать";
        }
        private void MouseLeaveArea(object sender, MouseEventArgs e)
        {
            statBarText.Text = "Готов";
        }
        
        private void ToolsSpellingHints_Click(object sender, RoutedEventArgs e)
        {
            string spellingHints = string.Empty;
            SpellingError error = txtData.GetSpellingError(txtData.CaretIndex);
            if(error!=null)
            {
                foreach (string s in error.Suggestions)
                {
                    spellingHints += $"{s}\n";
                }
                lblSpellingHints.Content = spellingHints;
                expanderSpelling.IsExpanded = true;
            }
        }
    }
}
