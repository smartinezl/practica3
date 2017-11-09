using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Signature
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string imatge;

        static string signatura;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLoadImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Selecciona una imatge";
            op.Filter = "Tots els arxius d'imatge|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "PNG (*.png)|*.png|" +
                        "Tots els arxius|*.*";
            if (op.ShowDialog() == true)
            {
                imgFoto.Source = new BitmapImage(new Uri(op.FileName));
                imatge = op.FileName;
            }
        }

        private void BtnLoadSignature_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Selecciona la signatura";
            op.Filter = "RSA|*.rsa|" +
                        "Tots els arxius|*.*";
            if (op.ShowDialog() == true)
            {
                txtSignature.Text = File.ReadAllText(op.FileName);
                signatura = op.FileName;
            }
        }

        private void BtnSign_Click(object sender, RoutedEventArgs e)
        {
            if (!Signa.KeysArePresent())
            {
                MessageBoxKeyNotFound();
            }
            else
            {
                try
                {
                    FileStream fsImatge = new FileStream(imatge, FileMode.Open, FileAccess.Read);
                    var dialog = new System.Windows.Forms.FolderBrowserDialog();
                    dialog.Description = "Selecciona la carpeta on es desarà la signatura.";
                    System.Windows.Forms.DialogResult res = dialog.ShowDialog();
                    if (res == System.Windows.Forms.DialogResult.OK)
                    {
                        string path = dialog.SelectedPath;
                        Signa.Sign(fsImatge, path);
                        string messageBoxText = "Imatge signada correctament.";
                        string caption = "Signatura completada";
                        MessageBoxButton button = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.None;
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                    }
                    fsImatge.Close();
                }
                catch (FileNotFoundException)
                {
                    MessageBoxImageNotFound();
                }
                catch (ArgumentNullException)
                {
                    MessageBoxImageNotFound();
                }
            }
        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (Signa.KeysArePresent())
            {
                try
                {
                    FileStream fsImatge = new FileStream(imatge, FileMode.Open, FileAccess.Read);
                    FileStream fsSignatura = new FileStream(signatura, FileMode.Open, FileAccess.Read);
                    string messageBoxText, caption;
                    if (Signa.ValidateSignature(fsImatge, fsSignatura))
                    {
                        messageBoxText = "La signatura és correcta.";
                        caption = "Signatura Correcta";
                    }
                    else
                    {
                        messageBoxText = "La signatura no és correcta.";
                        caption = "Signatura Incorrecta";
                    }
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.None;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                }
                catch (FileNotFoundException)
                {
                    if (!File.Exists(imatge))
                    {
                        MessageBoxImageNotFound();
                    }
                    if (!File.Exists(signatura))
                    {
                        MessageBoxSignatureNotFound();
                    }
                }
                catch (ArgumentNullException)
                {
                    if (!File.Exists(imatge))
                    {
                        MessageBoxImageNotFound();
                    }
                    if (!File.Exists(signatura))
                    {
                        MessageBoxSignatureNotFound();
                    }
                }
            }
            else
            {
                MessageBoxKeyNotFound();
            }
        }

        private void MessageBoxImageNotFound()
        {
            string messageBoxText = "No s'ha entrat cap imatge. Vols triar-ne una?";
            string caption = "Imatge no trobada";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    BtnLoadImg_Click(new object(), new RoutedEventArgs());
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void MessageBoxSignatureNotFound()
        {
            string messageBoxText = "No s'ha entrat cap signatura. Vols triar-ne una?";
            string caption = "Signatura no trobada";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    BtnLoadSignature_Click(new object(), new RoutedEventArgs());
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void MessageBoxKeyNotFound()
        {
            string messageBoxText = "No s'han trobat les claus. Clica 'Sí' si les vols generar o 'No' si vols especificar la seva ubicació";
            string caption = "Clau no trobada";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult res;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    dialog.Description = "Selecciona la carpeta on es desaran les claus.";
                    res = dialog.ShowDialog();
                    if (res == System.Windows.Forms.DialogResult.OK)
                    {
                        Signa.keysPath = dialog.SelectedPath;
                        Signa.GenerateKeys();
                        MessageBoxKeys("generades");
                    }
                    break;
                case MessageBoxResult.No:
                    dialog.Description = "Selecciona la carpeta on es troben les claus.";
                    res = dialog.ShowDialog();
                    if (res == System.Windows.Forms.DialogResult.OK)
                    {
                        Signa.keysPath = dialog.SelectedPath;
                        Signa.GenerateKeys();
                        MessageBoxKeys("trobades");
                    }
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        private void MessageBoxKeys(string msg)
        {
            string messageBoxText = "Claus " + msg + " correctament.";
            string caption = "Claus " + msg;
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.None;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
        }
    }
}
