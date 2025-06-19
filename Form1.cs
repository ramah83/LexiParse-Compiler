using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ScannerParserProject
{
    public partial class Form1 : Form
    {
        private Scanner scanner;
        private Parser parser;

        private List<string> examples;
        private int currentExampleIndex = 0;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            scanner = new Scanner();
            parser = new Parser();

            examples = new List<string>
{
    "int main() { int x; }",

    "int main() { int x;\nreal y; }",

    "int main() { return 0; }",

    "int main() { int arr[5]; }",

    "int sum(int a, int b) { return a + b; }",

    "void printArray(int arr[]) { }",

    "int main() { int x; x = 5; }",

    "int main() { if (x > 0) x = x - 1; }",

    "int main() { if (x > 0) x = x - 1; else x = 0; }",

    "int main() { while (x < 5) x = x + 1; }",

    "int main() { return; }",

    "int main() { return 1; }",

    "int main() { x = 5; }",

    "int main() { a + b; }",

    "int main() { arr[3] = 10; }",

    "int main() { a <= b; }",

    "int main() { a + b - c; }",

    "int main() { x * y / z; }",

    "int main() { sum(1, 2); }",

    "void main() { int x = 5; }"
};
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            string input = txtInput.Text;
            try
            {
                List<Token> tokens = scanner.Scan(input);
                DisplayTokens(tokens);
            }
            catch (Exception ex)
            {
                txtOutput.Text = "Scanning Error: " + ex.Message;
            }
        }
        private void btnParse_Click(object sender, EventArgs e)
        {
            string input = txtInput.Text;

            try
            {
                List<Token> tokens = scanner.Scan(input);

                txtOutput.Clear();
                txtOutput.AppendText($"[OK] Found {tokens.Count} tokens\r\n\r\n");

                bool success = parser.Parse(tokens);

                txtOutput.AppendText("[INFO] PARSE TREE:\r\n");
                txtOutput.AppendText("--------------------------------------------------\r\n");
                txtOutput.AppendText(parser.GetParseTree());

                txtOutput.AppendText("\r\n--------------------------------------------------\r\n");
                txtOutput.AppendText(success
                    ? "[SUCCESS] Parsing completed successfully."
                    : "[ERROR] Parsing failed. See details above.");
            }
            catch (Exception ex)
            {
                txtOutput.Text = "[ERROR] Parsing Error: " + ex.Message + "\r\n\r\n";
                txtOutput.AppendText("[DEBUG] Stack Trace:\r\n" + ex.StackTrace);
            }
        }


        private void DisplayTokens(List<Token> tokens)
        {
            txtOutput.Clear();
            txtOutput.AppendText("TOKENS:\r\n");
            txtOutput.AppendText("==================================================\r\n");
            txtOutput.AppendText(String.Format("{0,-15} {1,-15} {2}\r\n", "TOKEN TYPE", "LEXEME", "POSITION"));
            txtOutput.AppendText("--------------------------------------------------\r\n");

            foreach (Token token in tokens)
            {
                txtOutput.AppendText(String.Format("{0,-15} {1,-15} {2}\r\n",
                    token.Type, token.Lexeme, token.Position));
            }
        }

        private void btnLoadExample_Click(object sender, EventArgs e)
        {
            txtInput.Text = examples[currentExampleIndex];
            currentExampleIndex = (currentExampleIndex + 1) % examples.Count;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInput.Clear();
            txtOutput.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }
}
