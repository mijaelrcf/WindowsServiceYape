using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Timers;

namespace WindowsServiceYape
{
    partial class Service1
    {
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

        private Timer timer;

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            string sourceDir = @"C:\temp";
            string targetDir = @"C:\temp\cypher";

            foreach (var filePath in Directory.GetFiles(sourceDir))
            {
                string fileName = Path.GetFileName(filePath);
                string targetPath = Path.Combine(targetDir, fileName);

                // read file
                byte[] fileContent = File.ReadAllBytes(filePath);

                // encrypt file
                byte[] encryptedContent = EncryptFile(fileContent);

                // Guardar en la ruta destino
                File.WriteAllBytes(targetPath, encryptedContent);
            }
        }

        private byte[] EncryptFile(byte[] fileContent)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes("secret_key_123456*");
                aes.IV = Encoding.UTF8.GetBytes("tu_vector_ini_1234");

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(fileContent, 0, fileContent.Length);
                            cs.Close();
                        }
                        return ms.ToArray();
                    }
                }
            }
        }


        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "Service1";
        }

        #endregion
    }
}
