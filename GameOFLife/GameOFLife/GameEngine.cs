using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace GameOFLife
{
    public class GameEngine  
    {
        public uint CurrettGeneration { get; private set; }
        private bool[,] field;
        private readonly int rows;
        private readonly int cols;

        public GameEngine(int rows, int cols, int density)
        {
            this.rows = rows;
            this.cols = cols;
            field = new bool[cols, rows];
            Random rnd = new Random();

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    field[x, y] = rnd.Next(density) == 0;
                }
            }
        }
        private int CountNeighbours(int x, int y)
        {
            int count = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var col = (x + i + cols) % cols;
                    var row = (y + j + rows) % rows;

                    var isSelfChecking = col == x && row == y;
                    var hasLife = field[col, row];

                    if (hasLife && !isSelfChecking)
                        count++;
                }
            }
            return count;
        }

        public void NextGeneratoin()
        {

            var newField = new bool[cols, rows];

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var neighboursCount = CountNeighbours(x, y);
                    var hasLife = field[x, y];

                    if (!hasLife && neighboursCount == 3)
                        newField[x, y] = true;

                    else if (hasLife && (neighboursCount < 2 || neighboursCount > 3))
                        newField[x, y] = false;

                    else
                        newField[x, y] = field[x, y];

                }
            }
            field = newField;
            CurrettGeneration++;
        }

        public bool[,] GetCurrentGeneration()
        {
            var result = new bool[cols, rows];
            for (int x = 0;x < cols; x++)
            {
                for (int y = 0;y < rows; y++)
                {
                    result[x, y] = field[x, y];
                }
            }
            return result;
        }
        private bool ValidateCellPosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < cols && y < rows;
        }

        private void UPdateCell(int x, int y, bool state)
        {
            if (ValidateCellPosition(x, y))
                field[x, y] = state;
        }
        public void AddCell(int x, int y)
        {
            UPdateCell(x, y, state: true);
        }

        public void RemoveCell(int x, int y)
        {
            UPdateCell(x, y, state: false); 
        }
    }
}
