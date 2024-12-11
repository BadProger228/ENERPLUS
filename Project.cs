using System;
using System.Windows.Forms;  
namespace ENERPLUS
{
    internal class Project
    {
        public string Dir { get; private set; } = null;
        public string NameProject { get; private set; } = null;

        public void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "IDF Files|*.idf"; 

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    IDF idf = new IDF();
                    idf.SetFile(openFileDialog.FileName);

                    Dir = openFileDialog.FileName;
                    NameProject = openFileDialog.SafeFileName;

                    MessageBox.Show($"Project '{NameProject}' was opened");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error to open the file {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("File didn't choose");
            }
        }
    }
}
