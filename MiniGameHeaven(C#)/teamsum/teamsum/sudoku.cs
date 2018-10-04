using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.Devices;
namespace teamsum
{
    public partial class sudoku : Form
    {
        Form1 main = null;
        //  color for empty cells
        private Color DEFAULT_BACKCOLOR = Color.White;

        //  dimension of each cell in the grid
        const int CellWidth = 45;
        const int CellHeight = 45;

        //  offset from the top-left corner of the window
        const int xOffset = 20;
        const int yOffset = 20;

        //check if the game start
        int state = 0;

        string saveFileName = String.Empty;
        string itemSelected;

        //  used to represent the values in the grid
        private int[,] actual = new int[10, 10];

        //  color for original puzzle values
        private Color FIXED_FORECOLOR = Color.Blue;
        private Color FIXED_BACKCOLOR = Color.LightSteelBlue;

        //  color for user inserted values
        private Color USER_FORECOLOR = Color.Black;
        private Color USER_BACKCOLOR = Color.LightYellow;

        //  the number currently selected for insertion
        private int SelectedNumber;

        //  stacks to keep track of all the moves
        private Stack<string> Moves = new Stack<string>();

        string[] data = { "Easy", "Medium", "High", "file open" };  //level combox

        string file_name = null;  //for sending filename 

        public sudoku()
        {
            InitializeComponent();
        }
        public sudoku(Form1 main)//스도쿠 생성
        {
            InitializeComponent();
            this.main = main;

        }

        private void sudoku_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.Visible = true;
        }

        private void sudoku_Load(object sender, EventArgs e)
        {
            //  used to store the location of the cell
            Point location = new Point();

            for (int row = 1; row < 10; row++)
            {
                for (int col = 1; col < 10; col++)
                {
                    location.X = col * (CellWidth + 1) + xOffset;
                    location.Y = row * (CellHeight + 1) + yOffset;

                    Label lbl = new Label();

                    lbl.Name = col.ToString() + row.ToString();
                    lbl.BorderStyle = BorderStyle.None;
                    lbl.Location = location;
                    lbl.Width = CellWidth;
                    lbl.Height = CellHeight;
                    lbl.TextAlign = ContentAlignment.MiddleCenter;
                    lbl.BackColor = DEFAULT_BACKCOLOR;
                    lbl.Font = new Font(lbl.Font, lbl.Font.Style | FontStyle.Bold);
                    lbl.Tag = "1";

                    //  Add Handler
                    lbl.Click += new EventHandler(Cell_Click);

                    this.Controls.Add(lbl);
                }
            }

            comboBox1.Items.AddRange(data);
            comboBox1.SelectedIndex = 0;
        }

        private void sudoku_Paint(object sender, PaintEventArgs e)
        {
            //  draw the lines outlining the minigrids
            int x1, y1, x2, y2;

            //  draw the horizontal lines
            x1 = 1 * (CellWidth + 1) + xOffset - 1;
            x2 = 9 * (CellWidth + 1) + xOffset + CellWidth;

            for (int i = 1; i <= 10; i = i + 3)
            {
                y1 = i * (CellHeight + 1) + yOffset - 1;
                y2 = y1;

                e.Graphics.DrawLine(Pens.Black, x1, y1, x2, y2);
            }

            //  draw the vertical lines
            y1 = 1 * (CellHeight + 1) + yOffset - 1;
            y2 = 9 * (CellHeight + 1) + yOffset + CellHeight;

            for (int j = 1; j <= 10; j += 3)
            {
                x1 = j * (CellWidth + 1) + xOffset - 1;
                x2 = x1;

                e.Graphics.DrawLine(Pens.Black, x1, y1, x2, y2);
            }
        }
    

   
    //  event handler for cell click
    private void Cell_Click(object sender, EventArgs e)
    {
        //  check to see if game has even started or not

        if (state == 0)
        {
            MessageBox.Show("Click File->New to start a new game or File->Open to load an existing game");
            return;
        }

        Label cellLabel = sender as Label;

        //  if cell is not erasable then exit
        if (cellLabel.Tag.ToString() == "0")
        {
            MessageBox.Show("Selected cell is not empty");
            return;
        }

        //  determine the col and row of the selected cell
        int col = int.Parse(cellLabel.Name.Substring(0, 1));
        int row = int.Parse(cellLabel.Name.Substring(1, 1));

        //  if erasing a cell
        if (SelectedNumber == 0)
        {
            //  if cell is empty then no need to erase
            if (actual[col, row] == 0)
                return;

            //  save the value in the array
            SetCell(col, row, SelectedNumber, 1);
            MessageBox.Show("Number erased at (" + col + ", " + row + ")");
        }
        else if (cellLabel.Text == String.Empty)
        {
            //  else set a value, check if move is valid
            if (!IsMoveValid(col, row, SelectedNumber))
            {
                MessageBox.Show("Invalid move at (" + col + ", " + row + ")");
                return;
            }

            //  save the value in the array
            SetCell(col, row, SelectedNumber, 1);
            MessageBox.Show("Number " + SelectedNumber.ToString() + " placed at (" + col + ", " + row + ")");

            //  save the move into the stack
            Moves.Push(cellLabel.Name.ToString() + SelectedNumber);

            //  check if the puzzle is solved
            if (IsPuzzleSolved())
            {
                timer1.Enabled = false;
                // Console.Beep();
                MessageBox.Show("**** Puzzle is Solved ****");
            }
        }
    }

    private void ToolStripButton_Click(object sender, EventArgs e)
    {
        //  ToolStripButton selectedButton = (ToolStripButton)sender;       //  ok
        ToolStripButton selectedButton = sender as ToolStripButton;

        //  uncheck all the Button controls in the ToolStrip
        toolStripButton1.Checked = false;
        toolStripButton2.Checked = false;
        toolStripButton3.Checked = false;
        toolStripButton4.Checked = false;
        toolStripButton5.Checked = false;
        toolStripButton6.Checked = false;
        toolStripButton7.Checked = false;
        toolStripButton8.Checked = false;
        toolStripButton9.Checked = false;
        toolStripButton10.Checked = false;

        //  set the selected button to "checked"
        selectedButton.Checked = true;

        //  set the appropriate number selected
        if (selectedButton.Text == "erase")
            SelectedNumber = 0;
        else
            SelectedNumber = int.Parse(selectedButton.Text);

    }

    public Boolean IsMoveValid(int col, int row, int value)
    {
        Boolean puzzleSolved = true;

        //  scan through column
        for (int i = 1; i < 10; i++)
            if (actual[col, i] == value)      //  duplicate
                return false;

        //  scan through row
        for (int i = 1; i < 10; i++)
            if (actual[i, row] == value)      //  duplicate
                return false;

        //  scan through minigrid
        int startCol = col - ((col - 1) % 3);
        int startRow = row - ((row - 1) % 3);

        for (int rr = 0; rr < 3; rr++)
            for (int cc = 0; cc < 3; cc++)
                if (actual[startCol + cc, startRow + rr] == value)    //  duplicate
                    return false;

        return true;
    }

    //  check whether a puzzle is solved
    public Boolean IsPuzzleSolved()
    {
        String pattern;

        //  check row by row
        for (int r = 1; r < 10; r++)
        {
            pattern = "123456789";

            for (int c = 1; c < 10; c++)
                pattern = pattern.Replace(actual[c, r].ToString(), String.Empty);

            if (pattern.Length > 0)
                return false;
        }

        //  check column by column
        for (int c = 1; c < 10; c++)
        {
            pattern = "123456789";

            for (int r = 1; r < 10; r++)
                pattern = pattern.Replace(actual[c, r].ToString(), String.Empty);

            if (pattern.Length > 0)
                return false;
        }

        //  check by minigrid

        for (int c = 1; c < 10; c = c + 3)
        {
            pattern = "123456789";

            for (int r = 1; r < 10; r = r + 3)
            {
                for (int cc = 0; cc < 3; cc++)
                    for (int rr = 0; rr < 3; rr++)
                        pattern = pattern.Replace(actual[c + cc, r + rr].ToString(), String.Empty);
            }

            if (pattern.Length > 0)
                return false;
        }

        return true;
    }

    

    private void button1_Click(object sender, EventArgs e)
    {  //start button
        state = 1;

        string fileContents;
            string currentPath = Environment.CurrentDirectory;
            Encoding euckr = Encoding.GetEncoding("euc-kr");
        Computer myComputer = new Computer();

        int counter = 0;
        int value;

        //  initialize the cells in the board
        for (int row = 1; row < 10; row++)
            for (int col = 1; col < 10; col++)
                SetCell(col, row, 0, 1);

        if (itemSelected == "Easy")
        {
                saveFileName = @"./easy.txt";
        }
        else if (itemSelected == "Medium")
        {
            saveFileName = @"./medium.txt";
            }
        else if (itemSelected == "High")
        {
            saveFileName = @"./high.txt";
        }

        if (File.Exists(saveFileName))
        {
            using (StreamReader sr = new StreamReader(saveFileName, Encoding.Default))
            {
                fileContents = myComputer.FileSystem.ReadAllText(saveFileName);
                for (int row = 1; row < 10; row++)
                {
                    for (int col = 1; col < 10; col++)
                    {
                        try
                        {
                            value = int.Parse(fileContents[counter].ToString());

                            if (value != 0)
                                SetCell(col, row, value, 0);
                        }
                        catch (Exception)
                        {
                        }
                        counter += 1;
                    }
                }
            }
        }
        else
        {
            MessageBox.Show("읽을 파일이 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private void button3_Click(object sender, EventArgs e)
    {//exit 버튼
        if (state == 1)
        {//if game start
            DialogResult reponse = MessageBox.Show("Do you want to save current game?",
                                                "Save current game",
                                                MessageBoxButtons.YesNoCancel);
            if (reponse == DialogResult.Yes)
                Devsave(false);
            else if (reponse == DialogResult.Cancel)
                return;
        }
        this.Close();
        Application.Exit();
    }

    public void Devsave(Boolean saveAs)
    { //file저장

        //  if saveFileName is empty, means game has not been saved before
        if ((saveFileName == String.Empty) || saveAs)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "텍스트파일(*.txt)|*.txt|SDO files (*.sdo)|*.sdo|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = false;
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.AddExtension = true;  //확장자명 추가 여부

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //  store the filename first
                saveFileName = saveFileDialog1.FileName;
                file_name = saveFileDialog1.FileName;
            }
            else
            {
                return;
            }
        }
        //  formulate the string representing the values to store
        StringBuilder str = new StringBuilder();

        for (int row = 1; row < 10; row++)
            for (int col = 1; col < 10; col++)
                str.Append(actual[col, row].ToString());

        //  save the values to file
        Computer myComputer = new Computer();

        try
        {
            Boolean fileExists;

            fileExists = myComputer.FileSystem.FileExists(saveFileName);

            if (fileExists)
                myComputer.FileSystem.DeleteFile(saveFileName);

            myComputer.FileSystem.WriteAllText(saveFileName, str.ToString(), true);
        }
        catch (Exception)
        {
            MessageBox.Show("Error saving game. Please try again.");
            throw;
        }
    }
        
    public void SetCell(int col, int row, int value, int erasable)
    {
        //  locate particular Label control
        Control[] lbl = this.Controls.Find(col.ToString() + row.ToString(), true);
        Label cellLabel = lbl[0] as Label;

        //  save the value in the array
        actual[col, row] = value;

        //  set the appearance for the Label control
        if (value == 0)       //  erasing the cell
        {
            cellLabel.Text = String.Empty;
            cellLabel.Tag = erasable;
            cellLabel.BackColor = DEFAULT_BACKCOLOR;
        }
        else
        {
            if (erasable == 0)
            {
                //  means default puzzle values
                cellLabel.BackColor = FIXED_BACKCOLOR;
                cellLabel.ForeColor = FIXED_FORECOLOR;
            }
            else
            {
                //  means user-set value
                cellLabel.BackColor = USER_BACKCOLOR;
                cellLabel.ForeColor = USER_FORECOLOR;
            }

            cellLabel.Text = value.ToString();
            cellLabel.Tag = erasable;
        }
    }

   

    private void button4_Click(object sender, EventArgs e)
    {            //show answer

        string fileContents = null;
        string temp1, temp2;
        Computer myComputer = new Computer();

        int counter = 0;
        int value = 0;
        int pauseTime = 0;

        if (itemSelected == data[0])
        {
            MessageBox.Show("파일을 임시로 저장합니다.");
            Devsave(true);

            temp1 = @"./easyanswer.txt";

            for (int row = 1; row < 10; row++)
                for (int col = 1; col < 10; col++)
                    SetCell(col, row, 0, 1);

            if (File.Exists(temp1))
            {
                using (StreamReader sr = new StreamReader(temp1, Encoding.Default))
                {
                    fileContents = myComputer.FileSystem.ReadAllText(temp1);
                    for (int row = 1; row < 10; row++)
                    {
                        for (int col = 1; col < 10; col++)
                        {
                            try
                            {
                                value = int.Parse(fileContents[counter].ToString());
                                SetCell(col, row, value, 0);
                            }
                            catch (Exception)
                            {
                            }
                            counter += 1;
                        }
                    }
                }
            }

            pauseTime = 10;
            MessageBox.Show("확인버튼을 누르면 정답은 사라집니다.");
        }
        else if (itemSelected == data[1])
        {
            MessageBox.Show("파일을 임시로 저장합니다.");
            Devsave(true);

            temp1 = @"./mediumanswer.txt";

            for (int row = 1; row < 10; row++)
                for (int col = 1; col < 10; col++)
                    SetCell(col, row, 0, 1);

            if (File.Exists(temp1))
            {
                using (StreamReader sr = new StreamReader(temp1, Encoding.Default))
                {
                    fileContents = myComputer.FileSystem.ReadAllText(temp1);
                    for (int row = 1; row < 10; row++)
                    {
                        for (int col = 1; col < 10; col++)
                        {
                            try
                            {
                                value = int.Parse(fileContents[counter].ToString());
                                SetCell(col, row, value, 0);
                            }
                            catch (Exception)
                            {
                            }
                            counter += 1;
                        }
                    }
                }
            }

            pauseTime = 10;
            MessageBox.Show("확인버튼을 누르면 정답은 사라집니다.");
        }
        else if (itemSelected == data[2])
        {
            MessageBox.Show("파일을 임시로 저장합니다.");
            Devsave(true);

            temp1 = @"./highanswer.txt";

            for (int row = 1; row < 10; row++)
                for (int col = 1; col < 10; col++)
                    SetCell(col, row, 0, 1);

            if (File.Exists(temp1))
            {
                using (StreamReader sr = new StreamReader(temp1, Encoding.Default))
                {
                    fileContents = myComputer.FileSystem.ReadAllText(temp1);
                    for (int row = 1; row < 10; row++)
                    {
                        for (int col = 1; col < 10; col++)
                        {
                            try
                            {
                                value = int.Parse(fileContents[counter].ToString());
                                SetCell(col, row, value, 0);
                            }
                            catch (Exception)
                            {
                            }
                            counter += 1;
                        }
                    }
                }
            }

            pauseTime = 10;
            MessageBox.Show("확인버튼을 누르면 정답은 사라집니다.");
        }

        System.Threading.Thread.Sleep(pauseTime);

        temp2 = file_name;
        counter = 0;

        for (int row = 1; row < 10; row++)
            for (int col = 1; col < 10; col++)
                SetCell(col, row, 0, 1);

        if (File.Exists(temp2))
        {
            using (StreamReader sr = new StreamReader(temp2, Encoding.Default))
            {
                fileContents = myComputer.FileSystem.ReadAllText(temp2);
                for (int row = 1; row < 10; row++)
                {
                    for (int col = 1; col < 10; col++)
                    {
                        try
                        {
                            value = int.Parse(fileContents[counter].ToString());
                            SetCell(col, row, value, 0);
                        }
                        catch (Exception)
                        {
                        }
                        counter += 1;
                    }
                }
            }
        }
    }
        

        private void 열기ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
           
            if (state == 1)
            {
                var response = MessageBox.Show("Do you want to save current game?",
                                                "Save current game",
                                                MessageBoxButtons.YesNoCancel,
                                                MessageBoxIcon.Question);

                if (response == DialogResult.Yes)
                    Devsave(false);
                else if (response == DialogResult.Cancel)
                    return;

            }

            //  load the game from disk
            string fileContents;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            Computer myComputer = new Computer();

            openFileDialog1.Filter = "텍스트파일(*.txt)|*.txt|SDO files (*.sdo)|*.sdo|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = false;
            openFileDialog1.DefaultExt = "txt";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileContents = myComputer.FileSystem.ReadAllText(openFileDialog1.FileName);
                saveFileName = openFileDialog1.FileName;
            }
            else
            {
                return;
            }
            int counter = 0;
            int value;

            comboBox1.SelectedItem = data[3];

            for (int row = 1; row < 10; row++)
                for (int col = 1; col < 10; col++)
                    SetCell(col, row, 0, 1);

            for (int row = 1; row < 10; row++)
            {
                for (int col = 1; col < 10; col++)
                {
                    try
                    {
                        value = int.Parse(fileContents[counter].ToString());

                        if (value != 0)
                            SetCell(col, row, value, 0);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("File does not contain a valid Sudoku puzzle");
                    }

                    counter += 1;
                }
            }
        }

        private void 저장ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
            if (state == 0)
            {
                MessageBox.Show("Game not started yet.");
                return;
            }

            Devsave(true);
        }

        private void 다른이름으로저장ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
            if (state == 0)
            {
                MessageBox.Show("Game not started yet.");
            }

            Devsave(true);


        }

        private void 정보ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //help 
            String info = "1) 아홉 3×3 칸에 숫자가 1부터 9까지 하나씩만 들어가야 한다. \n" +
                "2) 아홉 가로줄에 숫자가 1부터 9까지 하나씩만 들어가야 한다 \n" +
                "3) 아홉 세로줄에 숫자가 1부터 9까지 하나씩만 들어가야 한다.\n"
                + "=>select number 옆에 나와있는 숫자버튼을 클릭하고 \n" + "    해당하는 숫자가 들어갈 cell을 클릭하세요.";
            MessageBox.Show(info, "How to do?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                this.itemSelected = comboBox1.SelectedItem as string;
            }
        }
    
    }

}