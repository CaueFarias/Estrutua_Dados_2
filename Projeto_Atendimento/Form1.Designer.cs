namespace Projeto_Atendimento
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.Button btnListarSenhas;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Button btnChamar;
        private System.Windows.Forms.Button btnListarAtendimentos;
        private System.Windows.Forms.TextBox txtGuiche;
        private System.Windows.Forms.Label lblQtdeGuiches;
        private System.Windows.Forms.ListBox listBoxSenhas;
        private System.Windows.Forms.ListBox listBoxAtendimentos;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnGerar = new Button();
            btnListarSenhas = new Button();
            btnAdicionar = new Button();
            btnChamar = new Button();
            btnListarAtendimentos = new Button();
            txtGuiche = new TextBox();
            lblQtdeGuiches = new Label();
            listBoxSenhas = new ListBox();
            listBoxAtendimentos = new ListBox();
            SuspendLayout();

            // btnGerar
            btnGerar.Location = new Point(30, 20);
            btnGerar.Size = new Size(100, 30);
            btnGerar.Text = "Gerar";
            btnGerar.Click += btnGerar_Click;

            // listBoxSenhas
            listBoxSenhas.Location = new Point(30, 60);
            listBoxSenhas.Size = new Size(250, 200);

            // btnListarSenhas
            btnListarSenhas.Location = new Point(30, 270);
            btnListarSenhas.Size = new Size(250, 30);
            btnListarSenhas.Text = "Listar senhas";
            btnListarSenhas.Click += btnListarSenhas_Click;

            // lblQtdeGuiches
            lblQtdeGuiches.Location = new Point(320, 120);
            lblQtdeGuiches.Text = "0";
            lblQtdeGuiches.AutoSize = true;
            lblQtdeGuiches.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            // btnAdicionar
            btnAdicionar.Location = new Point(300, 160);
            btnAdicionar.Size = new Size(100, 30);
            btnAdicionar.Text = "Adicionar";
            btnAdicionar.Click += btnAdicionar_Click;

            // txtGuiche
            txtGuiche.Location = new Point(450, 20);
            txtGuiche.Size = new Size(50, 23);

            // btnChamar
            btnChamar.Location = new Point(520, 20);
            btnChamar.Size = new Size(80, 30);
            btnChamar.Text = "Chamar";
            btnChamar.Click += btnChamar_Click;

            // listBoxAtendimentos
            listBoxAtendimentos.Location = new Point(420, 60);
            listBoxAtendimentos.Size = new Size(250, 200);

            // btnListarAtendimentos
            btnListarAtendimentos.Location = new Point(420, 270);
            btnListarAtendimentos.Size = new Size(250, 30);
            btnListarAtendimentos.Text = "Listar Atendimentos";
            btnListarAtendimentos.Click += btnListarAtendimentos_Click;

            // Form1
            ClientSize = new Size(720, 330);
            Controls.Add(btnGerar);
            Controls.Add(btnListarSenhas);
            Controls.Add(btnAdicionar);
            Controls.Add(btnChamar);
            Controls.Add(btnListarAtendimentos);
            Controls.Add(txtGuiche);
            Controls.Add(lblQtdeGuiches);
            Controls.Add(listBoxSenhas);
            Controls.Add(listBoxAtendimentos);
            Text = "Projeto Atendimento";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
