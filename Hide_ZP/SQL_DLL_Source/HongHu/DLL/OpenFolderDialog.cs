namespace HongHu.DLL
{
    using System;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    public class OpenFolderDialog : FolderNameEditor, IDisposable
    {
        private FolderNameEditor.FolderBrowser fDialog = new FolderNameEditor.FolderBrowser();

        public void Dispose()
        {
            this.fDialog.Dispose();
        }

        public DialogResult ShowDialog()
        {
            return this.ShowDialog("Select a folder:");
        }

        public DialogResult ShowDialog(string description)
        {
            this.fDialog.Description = description;
            return this.fDialog.ShowDialog();
        }

        public string Path
        {
            get
            {
                return this.fDialog.DirectoryPath;
            }
        }
    }
}

