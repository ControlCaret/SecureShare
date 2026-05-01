namespace SecureShare
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnEncrypt = new Button();
            txtFilePath = new TextBox();
            btnSelectFile = new Button();
            btnDecrypt = new Button();
            groupBoxEncrypt = new GroupBox();
            rbRSA = new RadioButton();
            rbAES = new RadioButton();
            rbDecrypt = new RadioButton();
            rbEncrypt = new RadioButton();
            panelAES = new Panel();
            txtAESPassword = new TextBox();
            labelAESPassword = new Label();
            panelEncryptRSA = new Panel();
            btnGenerateNewKey = new Button();
            rbGenerateNewKey = new RadioButton();
            rbUseExistingKey = new RadioButton();
            txtNewKeyPassword = new TextBox();
            labelGenerateNewKey = new Label();
            labelEncryptRSAPublicKey = new Label();
            txtPublicKeyPath = new TextBox();
            btnSelectPublicKey = new Button();
            commentPanelAES = new Label();
            commentPanelEncryptRSA = new Label();
            progressBarMain = new ProgressBar();
            commentPanelDecryptRSA = new Label();
            panelDecryptRSA = new Panel();
            btnVerifyKeyPassword = new Button();
            txtPrivateKeyPassword = new TextBox();
            labelEnterPrivateKeyPassword = new Label();
            labelDecryptRSAPrivateKey = new Label();
            txtPrivateKeyPath = new TextBox();
            btnSelectPrivateKey = new Button();
            groupBoxEncrypt.SuspendLayout();
            panelAES.SuspendLayout();
            panelEncryptRSA.SuspendLayout();
            panelDecryptRSA.SuspendLayout();
            SuspendLayout();
            // 
            // btnEncrypt
            // 
            btnEncrypt.Location = new Point(344, 497);
            btnEncrypt.Name = "btnEncrypt";
            btnEncrypt.Size = new Size(88, 23);
            btnEncrypt.TabIndex = 100;
            btnEncrypt.Text = "암호화";
            btnEncrypt.UseVisualStyleBackColor = true;
            btnEncrypt.Click += btnEncrypt_Click;
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(12, 325);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.ReadOnly = true;
            txtFilePath.Size = new Size(420, 23);
            txtFilePath.TabIndex = 9;
            // 
            // btnSelectFile
            // 
            btnSelectFile.AllowDrop = true;
            btnSelectFile.Font = new Font("맑은 고딕", 16F);
            btnSelectFile.Location = new Point(12, 139);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(420, 180);
            btnSelectFile.TabIndex = 10;
            btnSelectFile.Text = "파일을 선택하거나\r\n드래그 앤 드롭하세요\r\n";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click;
            btnSelectFile.DragDrop += btnSelectFile_DragDrop;
            btnSelectFile.DragEnter += btnSelectFile_DragEnter;
            // 
            // btnDecrypt
            // 
            btnDecrypt.Location = new Point(344, 526);
            btnDecrypt.Name = "btnDecrypt";
            btnDecrypt.Size = new Size(88, 23);
            btnDecrypt.TabIndex = 101;
            btnDecrypt.Text = "복호화";
            btnDecrypt.UseVisualStyleBackColor = true;
            btnDecrypt.Click += btnDecrypt_Click;
            // 
            // groupBoxEncrypt
            // 
            groupBoxEncrypt.Controls.Add(rbRSA);
            groupBoxEncrypt.Controls.Add(rbAES);
            groupBoxEncrypt.Location = new Point(12, 83);
            groupBoxEncrypt.Name = "groupBoxEncrypt";
            groupBoxEncrypt.Size = new Size(420, 50);
            groupBoxEncrypt.TabIndex = 19;
            groupBoxEncrypt.TabStop = false;
            groupBoxEncrypt.Text = "암호화 방식";
            // 
            // rbRSA
            // 
            rbRSA.AutoSize = true;
            rbRSA.Location = new Point(242, 22);
            rbRSA.Name = "rbRSA";
            rbRSA.Size = new Size(47, 19);
            rbRSA.TabIndex = 4;
            rbRSA.Text = "RSA";
            rbRSA.UseVisualStyleBackColor = true;
            // 
            // rbAES
            // 
            rbAES.AutoSize = true;
            rbAES.Location = new Point(127, 22);
            rbAES.Name = "rbAES";
            rbAES.Size = new Size(46, 19);
            rbAES.TabIndex = 3;
            rbAES.Text = "AES";
            rbAES.UseVisualStyleBackColor = true;
            // 
            // rbDecrypt
            // 
            rbDecrypt.Appearance = Appearance.Button;
            rbDecrypt.Location = new Point(332, 12);
            rbDecrypt.Name = "rbDecrypt";
            rbDecrypt.Size = new Size(100, 50);
            rbDecrypt.TabIndex = 2;
            rbDecrypt.Text = "복호화";
            rbDecrypt.TextAlign = ContentAlignment.MiddleCenter;
            rbDecrypt.UseVisualStyleBackColor = true;
            // 
            // rbEncrypt
            // 
            rbEncrypt.Appearance = Appearance.Button;
            rbEncrypt.Location = new Point(12, 12);
            rbEncrypt.Name = "rbEncrypt";
            rbEncrypt.Size = new Size(100, 50);
            rbEncrypt.TabIndex = 1;
            rbEncrypt.Text = "암호화";
            rbEncrypt.TextAlign = ContentAlignment.MiddleCenter;
            rbEncrypt.UseVisualStyleBackColor = true;
            // 
            // panelAES
            // 
            panelAES.Controls.Add(txtAESPassword);
            panelAES.Controls.Add(labelAESPassword);
            panelAES.Location = new Point(503, 27);
            panelAES.Name = "panelAES";
            panelAES.Size = new Size(420, 40);
            panelAES.TabIndex = 25;
            // 
            // txtAESPassword
            // 
            txtAESPassword.Location = new Point(40, 8);
            txtAESPassword.Name = "txtAESPassword";
            txtAESPassword.Size = new Size(380, 23);
            txtAESPassword.TabIndex = 20;
            // 
            // labelAESPassword
            // 
            labelAESPassword.AutoSize = true;
            labelAESPassword.Location = new Point(3, 12);
            labelAESPassword.Name = "labelAESPassword";
            labelAESPassword.Size = new Size(31, 15);
            labelAESPassword.TabIndex = 12;
            labelAESPassword.Text = "암호";
            // 
            // panelEncryptRSA
            // 
            panelEncryptRSA.Controls.Add(btnGenerateNewKey);
            panelEncryptRSA.Controls.Add(rbGenerateNewKey);
            panelEncryptRSA.Controls.Add(rbUseExistingKey);
            panelEncryptRSA.Controls.Add(txtNewKeyPassword);
            panelEncryptRSA.Controls.Add(labelGenerateNewKey);
            panelEncryptRSA.Controls.Add(labelEncryptRSAPublicKey);
            panelEncryptRSA.Controls.Add(txtPublicKeyPath);
            panelEncryptRSA.Controls.Add(btnSelectPublicKey);
            panelEncryptRSA.Location = new Point(503, 88);
            panelEncryptRSA.Name = "panelEncryptRSA";
            panelEncryptRSA.Size = new Size(420, 106);
            panelEncryptRSA.TabIndex = 26;
            // 
            // btnGenerateNewKey
            // 
            btnGenerateNewKey.Location = new Point(358, 74);
            btnGenerateNewKey.Name = "btnGenerateNewKey";
            btnGenerateNewKey.Size = new Size(63, 23);
            btnGenerateNewKey.TabIndex = 105;
            btnGenerateNewKey.Text = "키 생성";
            btnGenerateNewKey.UseVisualStyleBackColor = true;
            btnGenerateNewKey.Click += btnGenerateRSAKey_Click;
            // 
            // rbGenerateNewKey
            // 
            rbGenerateNewKey.AutoSize = true;
            rbGenerateNewKey.Location = new Point(221, 13);
            rbGenerateNewKey.Name = "rbGenerateNewKey";
            rbGenerateNewKey.Size = new Size(93, 19);
            rbGenerateNewKey.TabIndex = 104;
            rbGenerateNewKey.TabStop = true;
            rbGenerateNewKey.Text = "신규 키 생성";
            rbGenerateNewKey.UseVisualStyleBackColor = true;
            // 
            // rbUseExistingKey
            // 
            rbUseExistingKey.AutoSize = true;
            rbUseExistingKey.Location = new Point(122, 13);
            rbUseExistingKey.Name = "rbUseExistingKey";
            rbUseExistingKey.Size = new Size(93, 19);
            rbUseExistingKey.TabIndex = 103;
            rbUseExistingKey.TabStop = true;
            rbUseExistingKey.Text = "기존 키 사용";
            rbUseExistingKey.UseVisualStyleBackColor = true;
            // 
            // txtNewKeyPassword
            // 
            txtNewKeyPassword.Location = new Point(53, 74);
            txtNewKeyPassword.Name = "txtNewKeyPassword";
            txtNewKeyPassword.Size = new Size(299, 23);
            txtNewKeyPassword.TabIndex = 30;
            // 
            // labelGenerateNewKey
            // 
            labelGenerateNewKey.AutoSize = true;
            labelGenerateNewKey.Location = new Point(2, 78);
            labelGenerateNewKey.Name = "labelGenerateNewKey";
            labelGenerateNewKey.Size = new Size(47, 15);
            labelGenerateNewKey.TabIndex = 29;
            labelGenerateNewKey.Text = "키 암호";
            // 
            // labelEncryptRSAPublicKey
            // 
            labelEncryptRSAPublicKey.AutoSize = true;
            labelEncryptRSAPublicKey.Location = new Point(4, 47);
            labelEncryptRSAPublicKey.Name = "labelEncryptRSAPublicKey";
            labelEncryptRSAPublicKey.Size = new Size(43, 15);
            labelEncryptRSAPublicKey.TabIndex = 14;
            labelEncryptRSAPublicKey.Text = "공개키";
            // 
            // txtPublicKeyPath
            // 
            txtPublicKeyPath.Location = new Point(53, 44);
            txtPublicKeyPath.Name = "txtPublicKeyPath";
            txtPublicKeyPath.ReadOnly = true;
            txtPublicKeyPath.Size = new Size(299, 23);
            txtPublicKeyPath.TabIndex = 28;
            // 
            // btnSelectPublicKey
            // 
            btnSelectPublicKey.Location = new Point(358, 43);
            btnSelectPublicKey.Name = "btnSelectPublicKey";
            btnSelectPublicKey.Size = new Size(63, 23);
            btnSelectPublicKey.TabIndex = 27;
            btnSelectPublicKey.Text = "키 찾기";
            btnSelectPublicKey.UseVisualStyleBackColor = true;
            btnSelectPublicKey.Click += btnSelectPublicKey_Click;
            // 
            // commentPanelAES
            // 
            commentPanelAES.AutoSize = true;
            commentPanelAES.Location = new Point(503, 9);
            commentPanelAES.Name = "commentPanelAES";
            commentPanelAES.Size = new Size(57, 15);
            commentPanelAES.TabIndex = 14;
            commentPanelAES.Text = "panelAES";
            // 
            // commentPanelEncryptRSA
            // 
            commentPanelEncryptRSA.AutoSize = true;
            commentPanelEncryptRSA.Location = new Point(503, 70);
            commentPanelEncryptRSA.Name = "commentPanelEncryptRSA";
            commentPanelEncryptRSA.Size = new Size(98, 15);
            commentPanelEncryptRSA.TabIndex = 27;
            commentPanelEncryptRSA.Text = "panelEncryptRSA";
            // 
            // progressBarMain
            // 
            progressBarMain.Location = new Point(12, 526);
            progressBarMain.Name = "progressBarMain";
            progressBarMain.Size = new Size(326, 23);
            progressBarMain.TabIndex = 102;
            // 
            // commentPanelDecryptRSA
            // 
            commentPanelDecryptRSA.AutoSize = true;
            commentPanelDecryptRSA.Location = new Point(503, 197);
            commentPanelDecryptRSA.Name = "commentPanelDecryptRSA";
            commentPanelDecryptRSA.Size = new Size(100, 15);
            commentPanelDecryptRSA.TabIndex = 104;
            commentPanelDecryptRSA.Text = "panelDecryptRSA";
            // 
            // panelDecryptRSA
            // 
            panelDecryptRSA.Controls.Add(btnVerifyKeyPassword);
            panelDecryptRSA.Controls.Add(txtPrivateKeyPassword);
            panelDecryptRSA.Controls.Add(labelEnterPrivateKeyPassword);
            panelDecryptRSA.Controls.Add(labelDecryptRSAPrivateKey);
            panelDecryptRSA.Controls.Add(txtPrivateKeyPath);
            panelDecryptRSA.Controls.Add(btnSelectPrivateKey);
            panelDecryptRSA.Location = new Point(503, 215);
            panelDecryptRSA.Name = "panelDecryptRSA";
            panelDecryptRSA.Size = new Size(420, 73);
            panelDecryptRSA.TabIndex = 103;
            // 
            // btnVerifyKeyPassword
            // 
            btnVerifyKeyPassword.Location = new Point(358, 38);
            btnVerifyKeyPassword.Name = "btnVerifyKeyPassword";
            btnVerifyKeyPassword.Size = new Size(63, 23);
            btnVerifyKeyPassword.TabIndex = 31;
            btnVerifyKeyPassword.Text = "키 검증";
            btnVerifyKeyPassword.UseVisualStyleBackColor = true;
            btnVerifyKeyPassword.Click += btnVerifyKeyPassword_Click;
            // 
            // txtPrivateKeyPassword
            // 
            txtPrivateKeyPassword.Location = new Point(53, 38);
            txtPrivateKeyPassword.Name = "txtPrivateKeyPassword";
            txtPrivateKeyPassword.Size = new Size(299, 23);
            txtPrivateKeyPassword.TabIndex = 30;
            // 
            // labelEnterPrivateKeyPassword
            // 
            labelEnterPrivateKeyPassword.AutoSize = true;
            labelEnterPrivateKeyPassword.Location = new Point(2, 42);
            labelEnterPrivateKeyPassword.Name = "labelEnterPrivateKeyPassword";
            labelEnterPrivateKeyPassword.Size = new Size(47, 15);
            labelEnterPrivateKeyPassword.TabIndex = 29;
            labelEnterPrivateKeyPassword.Text = "키 암호";
            // 
            // labelDecryptRSAPrivateKey
            // 
            labelDecryptRSAPrivateKey.AutoSize = true;
            labelDecryptRSAPrivateKey.Location = new Point(4, 12);
            labelDecryptRSAPrivateKey.Name = "labelDecryptRSAPrivateKey";
            labelDecryptRSAPrivateKey.Size = new Size(43, 15);
            labelDecryptRSAPrivateKey.TabIndex = 14;
            labelDecryptRSAPrivateKey.Text = "개인키";
            // 
            // txtPrivateKeyPath
            // 
            txtPrivateKeyPath.Location = new Point(53, 9);
            txtPrivateKeyPath.Name = "txtPrivateKeyPath";
            txtPrivateKeyPath.ReadOnly = true;
            txtPrivateKeyPath.Size = new Size(299, 23);
            txtPrivateKeyPath.TabIndex = 28;
            // 
            // btnSelectPrivateKey
            // 
            btnSelectPrivateKey.Location = new Point(358, 8);
            btnSelectPrivateKey.Name = "btnSelectPrivateKey";
            btnSelectPrivateKey.Size = new Size(63, 23);
            btnSelectPrivateKey.TabIndex = 27;
            btnSelectPrivateKey.Text = "키 찾기";
            btnSelectPrivateKey.UseVisualStyleBackColor = true;
            btnSelectPrivateKey.Click += btnSelectPrivateKey_Click;
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(984, 561);
            Controls.Add(commentPanelDecryptRSA);
            Controls.Add(panelDecryptRSA);
            Controls.Add(progressBarMain);
            Controls.Add(commentPanelEncryptRSA);
            Controls.Add(commentPanelAES);
            Controls.Add(panelEncryptRSA);
            Controls.Add(panelAES);
            Controls.Add(btnEncrypt);
            Controls.Add(rbDecrypt);
            Controls.Add(rbEncrypt);
            Controls.Add(groupBoxEncrypt);
            Controls.Add(btnDecrypt);
            Controls.Add(btnSelectFile);
            Controls.Add(txtFilePath);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Secure Share";
            Load += MainForm_Load;
            groupBoxEncrypt.ResumeLayout(false);
            groupBoxEncrypt.PerformLayout();
            panelAES.ResumeLayout(false);
            panelAES.PerformLayout();
            panelEncryptRSA.ResumeLayout(false);
            panelEncryptRSA.PerformLayout();
            panelDecryptRSA.ResumeLayout(false);
            panelDecryptRSA.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnEncrypt;
        private TextBox txtFilePath;
        private Button btnSelectFile;
        private Button btnDecrypt;
        private GroupBox groupBoxEncrypt;
        private RadioButton rbRSA;
        private RadioButton rbAES;
        private RadioButton rbDecrypt;
        private RadioButton rbEncrypt;
        private Panel panelAES;
        private TextBox txtAESPassword;
        private Label labelAESPassword;
        private Panel panelEncryptRSA;
        private TextBox txtPublicKeyPath;
        private Button btnSelectPublicKey;
        private Label commentPanelAES;
        private Label commentPanelEncryptRSA;
        private Label labelEncryptRSAPublicKey;
        private TextBox txtNewKeyPassword;
        private Label labelGenerateNewKey;
        private ProgressBar progressBarMain;
        private RadioButton rbGenerateNewKey;
        private RadioButton rbUseExistingKey;
        private Button btnGenerateNewKey;
        private Label commentPanelDecryptRSA;
        private Panel panelDecryptRSA;
        private TextBox txtPrivateKeyPassword;
        private Label labelEnterPrivateKeyPassword;
        private Label labelDecryptRSAPrivateKey;
        private TextBox txtPrivateKeyPath;
        private Button btnSelectPrivateKey;
        private Button btnVerifyKeyPassword;
    }
}
