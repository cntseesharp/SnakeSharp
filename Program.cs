using System;
using System.Windows.Forms;

namespace snake
{
    /*
     *
     * Игровое поле - 300х300 пикселей
     * Один юнит - 15х15 пикселей
     * Количество юнитов - 20*20 = 400
     * Обьект "змея" - направляющий блок: сплошной квадрат, 
     * 
     */
    static class Program
    {
        //static SnakeObject mySnake = new SnakeObject(new Vector2(10, 10));
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Snake());
        }
    }
}
