using System;
using System.Windows.Forms;  
namespace ENERPLUS
{
    internal class Project
    {
        public string Dir { get; private set; } = null;
        public string NameProject { get; private set; } = null;
        public IDF file = new();

        public void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "IDF Files|*.idf"; 

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    file.SetFile(openFileDialog.FileName);

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

        public void SaveFile()
        {
            file.SaveFile(Dir);
        }

        public void SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "IDF Files (*.idf)|*.idf", 
                Title = "Save Project As",
                FileName = $"{NameProject}"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    file.SaveFile(saveFileDialog.FileName);
                    MessageBox.Show($"Project '{NameProject}' was saved as '{saveFileDialog.FileName}' successfully.",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving the file: {ex.Message}",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("File was not saved.",
                                "Cancelled",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

    }
}
