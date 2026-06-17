using PgpCore;
using System.Security.Cryptography;
using System.Diagnostics;

namespace SecureShare
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(460, 600);
            rbEncrypt.Checked = true;
            rbAES.Checked = true;
            rbUseExistingKey.Checked = true;

            rbEncrypt.CheckedChanged += (s, ev) => UpdateLayout();
            rbDecrypt.CheckedChanged += (s, ev) => UpdateLayout();
            rbAES.CheckedChanged += (s, ev) => UpdateLayout();
            rbRSA.CheckedChanged += (s, ev) => UpdateLayout();
            rbUseExistingKey.CheckedChanged += (s, ev) => UpdateLayout();
            rbGenerateNewKey.CheckedChanged += (s, ev) => UpdateLayout();

            UpdateLayout();
        }

        // Manage panels and buttons
        private void UpdateLayout()
        {
            btnEncrypt.Visible = rbEncrypt.Checked;
            btnDecrypt.Visible = rbDecrypt.Checked;

            panelAES.Visible = false;
            panelEncryptRSA.Visible = false;
            panelDecryptRSA.Visible = false;

            if (rbAES.Checked)
            {
                panelAES.Location = new Point(12, 354);
                panelAES.Visible = true;
                btnEncrypt.Location = new Point(344, 400);
                btnDecrypt.Location = new Point(344, 400);
            }
            else if (rbRSA.Checked)
            {
                if (rbEncrypt.Checked)
                {
                    panelEncryptRSA.Location = new Point(12, 354);
                    panelEncryptRSA.Visible = true;
                    btnEncrypt.Location = new Point(344, 480);

                    txtPublicKeyPath.Enabled = rbUseExistingKey.Checked;
                    btnSelectPublicKey.Enabled = rbUseExistingKey.Checked;
                    txtNewKeyPassword.Enabled = rbGenerateNewKey.Checked;
                    btnGenerateNewKey.Enabled = rbGenerateNewKey.Checked;
                }
                else
                {
                    panelDecryptRSA.Location = new Point(12, 354);
                    panelDecryptRSA.Visible = true;
                    btnDecrypt.Location = new Point(344, 480);
                }
            }
        }

        // Encrypt button click event
        private async void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text) || !File.Exists(txtFilePath.Text))
            {
                MessageBox.Show("암호화할 파일을 먼저 선택해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbAES.Checked)
            {
                if (string.IsNullOrEmpty(txtAESPassword.Text))
                {
                    MessageBox.Show("AES 암호를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                encryptFileAES(txtFilePath.Text, txtAESPassword.Text);
            }
            else if (rbRSA.Checked)
            {
                if (string.IsNullOrEmpty(txtPublicKeyPath.Text))
                {
                    MessageBox.Show("공개키 파일을 선택해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                await encryptFileRSA(txtFilePath.Text, txtPublicKeyPath.Text);
            }
        }

        // Decrypt button click event
        private async void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text) || !File.Exists(txtFilePath.Text))
            {
                MessageBox.Show("복호화할 파일을 먼저 선택해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbAES.Checked)
            {
                if (string.IsNullOrEmpty(txtAESPassword.Text))
                {
                    MessageBox.Show("AES 암호를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                decryptFileAES(txtFilePath.Text, txtAESPassword.Text);
            }
            else if (rbRSA.Checked)
            {
                if (string.IsNullOrEmpty(txtPrivateKeyPath.Text))
                {
                    MessageBox.Show("개인키 파일을 선택해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                await decryptFileRSA(txtFilePath.Text, txtPrivateKeyPath.Text, txtPrivateKeyPassword.Text);
            }
        }

        // Select file or drag and drop
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "모든 파일 (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
                btnSelectFile.Text = Path.GetFileName(openFileDialog.FileName);
            }
        }

        private void btnSelectFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void btnSelectFile_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data?.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
            {
                txtFilePath.Text = files[0];
                btnSelectFile.Text = Path.GetFileName(files[0]);
            }
        }

        // AES Encryption
        public void encryptFileAES(string inputFilePath, string password)
        {
            try
            {
                string outputFilePath = inputFilePath + ".aes";
                byte[] salt = new byte[16];
                using (var rng = RandomNumberGenerator.Create()) { rng.GetBytes(salt); }

                using var aes = Aes.Create();
                using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
                aes.KeySize = 256;
                aes.Key = pbkdf2.GetBytes(32);
                aes.GenerateIV();

                using var fsOutput = new FileStream(outputFilePath, FileMode.Create);
                fsOutput.Write(salt, 0, salt.Length);
                fsOutput.Write(aes.IV, 0, aes.IV.Length);

                using var cs = new CryptoStream(fsOutput, aes.CreateEncryptor(), CryptoStreamMode.Write);
                using var fsInput = new FileStream(inputFilePath, FileMode.Open);
                fsInput.CopyTo(cs);

                MessageBox.Show("AES 암호화 성공!\n결과 파일: " + outputFilePath, "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OpenFolderAndSelectFile(outputFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("AES 암호화 중 오류가 발생했습니다:\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // AES Decryption
        public void decryptFileAES(string inputFilePath, string password)
        {
            try
            {
                string outputFilePath = inputFilePath.EndsWith(".aes") ? inputFilePath[..^4] : inputFilePath + ".decrypted";
                using var fsInput = new FileStream(inputFilePath, FileMode.Open);
                byte[] salt = new byte[16];
                if (fsInput.Read(salt, 0, salt.Length) != salt.Length) throw new Exception("파일 형식이 올바르지 않습니다 (Salt 누락).");
                byte[] iv = new byte[16];
                if (fsInput.Read(iv, 0, iv.Length) != iv.Length) throw new Exception("파일 형식이 올바르지 않습니다 (IV 누락).");

                using var aes = Aes.Create();
                using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
                aes.KeySize = 256;
                aes.Key = pbkdf2.GetBytes(32);
                aes.IV = iv;

                using var fsOutput = new FileStream(outputFilePath, FileMode.Create);
                using var cs = new CryptoStream(fsInput, aes.CreateDecryptor(), CryptoStreamMode.Read);
                cs.CopyTo(fsOutput);

                MessageBox.Show("AES 복호화 성공!\n결과 파일: " + outputFilePath, "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OpenFolderAndSelectFile(outputFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("AES 복호화 중 오류가 발생했습니다:\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Key Generation
        private void btnSelectPublicKey_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "공개키 파일 (*.asc;*.pub)|*.asc;*.pub|모든 파일 (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                txtPublicKeyPath.Text = openFileDialog.FileName;
        }

        private async void btnGenerateRSAKey_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewKeyPassword.Text))
            {
                MessageBox.Show("키 암호를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using FolderBrowserDialog folderDialog = new();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string pubPath = Path.Combine(folderDialog.SelectedPath, "publicKey.asc");
                    string privPath = Path.Combine(folderDialog.SelectedPath, "privateKey.asc");

                    PGP pgp = new PGP();
                    await pgp.GenerateKeyAsync(new FileInfo(pubPath), new FileInfo(privPath), "SecureShare@localhost", txtNewKeyPassword.Text);

                    txtPublicKeyPath.Text = pubPath;
                    rbUseExistingKey.Checked = true;
                    MessageBox.Show("RSA 키 쌍이 생성되었습니다!\n\n저장 폴더: " + folderDialog.SelectedPath, "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OpenFolderAndSelectFile(pubPath);
                    txtNewKeyPassword.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("키 생성 중 오류가 발생했습니다:\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSelectPrivateKey_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "개인키 파일 (*.asc;*.key;*.priv)|*.asc;*.key;*.priv|모든 파일 (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                txtPrivateKeyPath.Text = openFileDialog.FileName;
        }

        // Key verification
        private void btnVerifyKeyPassword_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrivateKeyPath.Text) || !File.Exists(txtPrivateKeyPath.Text))
            {
                MessageBox.Show("개인키 파일을 먼저 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using FileStream privFS = new FileStream(txtPrivateKeyPath.Text, FileMode.Open, FileAccess.Read);
                EncryptionKeys keys = new EncryptionKeys(privFS, txtPrivateKeyPassword.Text);

                if (keys.SecretKeys == null || keys.SecretKeys.Count == 0)
                {
                    throw new Exception("유효한 개인키를 찾을 수 없거나 암호가 일치하지 않습니다.");
                }

                MessageBox.Show("키 암호가 성공적으로 인증되었습니다!", "인증 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("인증 실패: 암호가 틀렸거나 잘못된 키 파일입니다.\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // RSA Encryption
        public async Task encryptFileRSA(string inputFilePath, string publicKeyPath)
        {
            try
            {
                string outputFilePath = inputFilePath + ".pgp";
                using FileStream inFS = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read);
                using FileStream outFS = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
                using FileStream pubFS = new FileStream(publicKeyPath, FileMode.Open, FileAccess.Read);

                EncryptionKeys keys = new EncryptionKeys(pubFS);
                PGP pgp = new PGP(keys);
                await pgp.EncryptStreamAsync(inFS, outFS);

                MessageBox.Show("RSA 암호화 성공!\n결과 파일: " + outputFilePath, "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OpenFolderAndSelectFile(outputFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA 암호화 중 오류가 발생했습니다:\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // RSA Decryption
        public async Task decryptFileRSA(string inputFilePath, string privateKeyPath, string password)
        {
            try
            {
                string outputFilePath = inputFilePath.EndsWith(".pgp") ? inputFilePath[..^4] : inputFilePath + ".decrypted";
                using FileStream inFS = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read);
                using FileStream outFS = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
                using FileStream privFS = new FileStream(privateKeyPath, FileMode.Open, FileAccess.Read);

                EncryptionKeys keys = new EncryptionKeys(privFS, password);
                PGP pgp = new PGP(keys);
                await pgp.DecryptStreamAsync(inFS, outFS);

                MessageBox.Show("RSA 복호화 성공!\n결과 파일: " + outputFilePath, "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OpenFolderAndSelectFile(outputFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA 복호화 중 오류가 발생했습니다:\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenFolderAndSelectFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    Process.Start("explorer.exe", $"/select,\"{filePath}\"");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("폴더 열기 오류: " + ex.Message);
            }
        }

        private void txtPublicKeyPath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txtPublicKeyPath_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data?.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
            {
                txtPublicKeyPath.Text = files[0];
            }
        }

        private void txtPrivateKeyPath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txtPrivateKeyPath_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data?.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
            {
                txtPrivateKeyPath.Text = files[0];
            }
        }
    }
}
