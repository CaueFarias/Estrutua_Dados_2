using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        const int FILEIRAS = 15;
        const int POLTRONAS = 40;

        Button[,] lugares = new Button[FILEIRAS, POLTRONAS];
        int ocupado = 0;
        float faturamento = 0.0f;

        public Form1()
        {
            InitializeComponent();  // <-- precisa existir no Form1.Designer.cs
            CriarMapa();
        }

        private float GetSeatPrice(int fileira)
        {
            if (fileira >= 1 && fileira <= 5) return 50.0f;
            if (fileira >= 6 && fileira <= 10) return 30.0f;
            return 15.0f;
        }

        private void CriarMapa()
        {
            int startX = 20, startY = 20, size = 20, margin = 2;

            for (int i = 0; i < FILEIRAS; i++)
            {
                for (int j = 0; j < POLTRONAS; j++)
                {
                    Button poltrona = new Button();
                    poltrona.Size = new Size(size, size);
                    poltrona.Location = new Point(startX + j * (size + margin), startY + i * (size + margin));
                    poltrona.BackColor = Color.LightGreen;
                    poltrona.Tag = new Tuple<int, int>(i, j);
                    poltrona.Click += ReservarPoltrona; // correto
                    this.Controls.Add(poltrona);
                    lugares[i, j] = poltrona;
                }
            }

            Button btnFaturamento = new Button();
            btnFaturamento.Text = "Faturamento";
            btnFaturamento.Location = new Point(20, startY + FILEIRAS * (size + margin) + 20);
            btnFaturamento.Click += MostrarFaturamento; // correto
            this.Controls.Add(btnFaturamento);
        }

        private void ReservarPoltrona(object sender, EventArgs e)
        {
            Button poltrona = sender as Button;
            var pos = (Tuple<int, int>)poltrona.Tag;
            int fileira = pos.Item1;
            int lugar = pos.Item2;

            if (poltrona.BackColor == Color.Red)
            {
                MessageBox.Show("Poltrona já ocupada!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                poltrona.BackColor = Color.Red;
                ocupado++;
                faturamento += GetSeatPrice(fileira + 1);
                MessageBox.Show($"Reserva efetuada! Preço: R$ {GetSeatPrice(fileira + 1):0.00}", "Sucesso");
            }
        }

        private void MostrarFaturamento(object sender, EventArgs e)
        {
            MessageBox.Show(
                $"Lugares ocupados: {ocupado}\nFaturamento: R$ {faturamento:0.00}",
                "Faturamento",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
