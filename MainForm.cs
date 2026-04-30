using PgpCore;
using System.Security.Cryptography;

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

            btnSelectPublicKey.Click += btnSelectPublicKey_Click;
            btnGenerateNewKey.Click += btnGenerateRSAKey_Click;

            UpdateLayout();
        }

        private void UpdateLayout()
        {
            btnEncrypt.Visible = rbEncrypt.Checked;
            btnDecrypt.Visible = rbDecrypt.Checked;

            panelAES.Visible = false;
            panelEncryptRSA.Visible = false;

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
                    
                    btnEncrypt.Location = new Point(344, 466);
                    btnDecrypt.Location = new Point(344, 466);

                    txtPublicKeyPath.Enabled = rbUseExistingKey.Checked;
                    btnSelectPublicKey.Enabled = rbUseExistingKey.Checked;
                    
                    txtNewKeyPassword.Enabled = rbGenerateNewKey.Checked;
                    btnGenerateNewKey.Enabled = rbGenerateNewKey.Checked;
                }
            }
        }

        private void btnSelectPublicKey_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "공개키 파일 (*.asc;*.pub)|*.asc;*.pub|모든 파일 (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtPublicKeyPath.Text = openFileDialog.FileName;
            }
        }

        private async void btnGenerateRSAKey_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewKeyPassword.Text))
            {
                MessageBox.Show("키를 보호할 암호를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using FolderBrowserDialog folderDialog = new();
            folderDialog.Description = "키 쌍(공개키, 개인키)을 저장할 폴더를 선택하세요.";
            
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string pubPath = Path.Combine(folderDialog.SelectedPath, "publicKey.asc");
                    string privPath = Path.Combine(folderDialog.SelectedPath, "privateKey.asc");

                    PGP pgp = new PGP();
                    // 키 생성이 오래 걸릴 수 있으므로 비동기로 실행
                    await pgp.GenerateKeyAsync(new FileInfo(pubPath), new FileInfo(privPath), "SecureShare@localhost", txtNewKeyPassword.Text);

                    txtPublicKeyPath.Text = pubPath;
                    rbUseExistingKey.Checked = true; // 생성 완료 후 사용 모드로 자동 전환
                    
                    MessageBox.Show($"보안 키 쌍이 생성되었습니다!\n\n공개키: {pubPath}\n개인키: {privPath}\n\n개인키 파일과 암호는 복호화 시 반드시 필요하므로 안전한 곳에 보관하세요.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewKeyPassword.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"키 생성 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

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

        private async void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text) || !File.Exists(txtFilePath.Text))
            {
                MessageBox.Show("암호화할 파일을 선택해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbAES.Checked)
            {
                if (string.IsNullOrEmpty(txtAESPassword.Text))
                {
                    MessageBox.Show("암호를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                encryptFileAES(txtFilePath.Text, txtAESPassword.Text);
            }
            else if (rbRSA.Checked)
            {
                if (string.IsNullOrEmpty(txtPublicKeyPath.Text) || !File.Exists(txtPublicKeyPath.Text))
                {
                    MessageBox.Show("공개키를 선택하거나 생성해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                await encryptFileRSA(txtFilePath.Text, txtPublicKeyPath.Text);
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text) || !File.Exists(txtFilePath.Text))
            {
                MessageBox.Show("복호화할 파일을 선택해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbAES.Checked)
            {
                if (string.IsNullOrEmpty(txtAESPassword.Text))
                {
                    MessageBox.Show("암호를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                decryptFileAES(txtFilePath.Text, txtAESPassword.Text);
            }
        }

        public async Task encryptFileRSA(string inputFilePath, string publicKeyPath)
        {
            try
            {
                string outputFilePath = inputFilePath + ".pgp";

                using FileStream inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read);
                using FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
                using FileStream publicKeyStream = new FileStream(publicKeyPath, FileMode.Open, FileAccess.Read);

                // 공개키 스트림으로 EncryptionKeys 객체 생성
                EncryptionKeys encryptionKeys = new EncryptionKeys(publicKeyStream);
                
                // EncryptionKeys를 PGP 생성자에 전달
                PGP pgp = new PGP(encryptionKeys);

                // 대용량 스트림 암호화 (비동기 실행)
                await pgp.EncryptStreamAsync(inputFileStream, outputFileStream);

                MessageBox.Show("파일이 RSA로 성공적으로 암호화되었습니다: " + outputFilePath, "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA 암호화 중 오류가 발생했습니다: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

                MessageBox.Show("파일이 성공적으로 암호화되었습니다: " + outputFilePath, "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("암호화 중 오류가 발생했습니다: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void decryptFileAES(string inputFilePath, string password)
        {
            try
            {
                string outputFilePath = inputFilePath.EndsWith(".aes") ? inputFilePath[..^4] : inputFilePath + ".decrypted";

                using var fsInput = new FileStream(inputFilePath, FileMode.Open);
                byte[] salt = new byte[16];
                if (fsInput.Read(salt, 0, salt.Length) != salt.Length) throw new Exception("Salt 누락");

                byte[] iv = new byte[16];
                if (fsInput.Read(iv, 0, iv.Length) != iv.Length) throw new Exception("IV 누락");

                using var aes = Aes.Create();
                using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
                aes.KeySize = 256;
                aes.Key = pbkdf2.GetBytes(32);
                aes.IV = iv;

                using var fsOutput = new FileStream(outputFilePath, FileMode.Create);
                using var cs = new CryptoStream(fsInput, aes.CreateDecryptor(), CryptoStreamMode.Read);
                try { cs.CopyTo(fsOutput); }
                catch { throw new Exception("암호가 틀렸거나 파일이 손상되었습니다."); }

                MessageBox.Show("파일이 성공적으로 복호화되었습니다: " + outputFilePath, "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("복호화 중 오류가 발생했습니다: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
