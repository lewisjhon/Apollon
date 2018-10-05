using System;
using System.Collections.Generic;
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
using MahApps.Metro.Controls;
using System.ComponentModel;

using ProjectApollon.Controls;
using ProjectApollon.Models;
using System.IO;
using MahApps.Metro.Controls.Dialogs;



namespace ProjectApollon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();

            this.btnFind.Click += BtnFind_Click;
            this.btnConvert.Click += BtnConvert_Click;
            this.DataContextChanged += MainWindow_DataContextChanged;
            this.btnSetting.Click += BtnSetting_Click;
            this.btnFindSave.Click += BtnFindSave_Click;
        }

        private delegate void UpdateProgressBarDelegate(System.Windows.DependencyProperty dp, Object value);

        private void BtnFindSave_Click(object sender, RoutedEventArgs e)
        {
            txtSavePath.Text = BrowFolder();
        }

        private void BtnSetting_Click(object sender, RoutedEventArgs e)
        {
            this.ToggleFlyout();
        }

        private void BtnConvert_Click(object sender, RoutedEventArgs e)
        {
            if (FileList.FileLists != null)
            {
                FileList.SavePath = txtSavePath.Text;

                for (int i = 0; i < FileList.FileLists.Count; i++)
                {
                    FileList.ConvertFileNameAt(i);
                    UpdateProgressBarDelegate updatePbDelegate = new UpdateProgressBarDelegate(prgConvert.SetValue);
                    Dispatcher.Invoke(updatePbDelegate, System.Windows.Threading.DispatcherPriority.Background,
                            new object[] { ProgressBar.ValueProperty, (double)(((i + 1) * 100) / FileList.FileLists.Count) });
                }

                this.ShowMessageAsync("Complete", "변환이 완료되었습니다.", MessageDialogStyle.Affirmative, new MetroDialogSettings()
                {
                    ColorScheme = MetroDialogColorScheme.Inverted
                });
            }
        }


        private void MainWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.DataContext = null;
        }

        private void BtnFind_Click(object sender, RoutedEventArgs e)
        {
            string tmp = Environment.GetFolderPath(Environment.SpecialFolder.Desktop, Environment.SpecialFolderOption.None);

            txtPath.Text = BrowFolder();
            FileList.CutParenthesisArtist = (bool)chkCutArtist.IsChecked;
            FileList.CutParenthesisTitle = (bool)chkCutTitle.IsChecked;
            FileList.CreateListByExtenstion(txtPath.Text);
            this.DataContext = FileList.FileLists;
        }


        private string BrowFolder()
        {

            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            dialog.ShowNewFolderButton = true;
            
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return dialog.SelectedPath;
            else
                return string.Empty;

            //Gat.Controls.OpenDialogView openDialog = new Gat.Controls.OpenDialogView();
            //Gat.Controls.OpenDialogViewModel vm = (Gat.Controls.OpenDialogViewModel)openDialog.DataContext;

            //vm.IsDirectoryChooser = true;
            //vm.Caption = caption;
            //vm.CancelText = cancelText;
            //vm.OpenText = openText;            

            //bool? result = vm.Show();

            //if (result == true)
            //    return vm.SelectedFilePath;
            //else
            //    return "";
        }
    
        private void ToggleFlyout()
        {
            var flyout = this.flyoutSetting as Flyout;
            if (flyout == null)
            {
                return;
            }
            flyout.IsOpen = !flyout.IsOpen;
        }

    }
}
